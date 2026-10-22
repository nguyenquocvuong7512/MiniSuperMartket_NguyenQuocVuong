/*
 * Họ và tên sinh viên: Nguyễn Quốc Vương
 * Bài tập: Buổi 5 - Tiết 5: Hiện thực hóa API thanh toán POS bọc trong SqlTransaction & Kịch bản kiểm thử
 * Mô tả: API Thanh toán POS đa bảng và Hủy đơn hàng được bảo vệ trọn vẹn bởi SqlTransaction (ACID),
 *       sử dụng cấu trúc dữ liệu chuẩn của dự án trong /Data (SupermarketDbContext).
 */
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MiniSupermarket.API.Dto;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CheckoutController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // =========================================================================
        // POST: api/checkout
        // Chức năng: Thanh toán hóa đơn, trừ tồn kho và tích điểm trong 1 Transaction
        // =========================================================================
        [HttpPost]
        public IActionResult ProcessCheckout([FromBody] CheckoutRequestDto request)
        {
            // 1. Kiểm tra tính hợp lệ của giỏ hàng
            if (request.Items == null || request.Items.Count == 0)
            {
                return BadRequest(new { message = "Giỏ hàng rỗng, không thể tiến hành thanh toán!" });
            }

            if (string.IsNullOrWhiteSpace(request.CashierUsername))
            {
                return BadRequest(new { message = "Thiếu thông tin tài khoản thu ngân lập hóa đơn!" });
            }

            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // 2. KHỞI TẠO TRANSACTION (Bắt đầu phiên giao dịch khép kín bảo vệ ACID)
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // 2.1. Tra cứu UserId tương ứng với CashierUsername từ bảng Users trong /Data
                    string findUserSql = "SELECT TOP 1 Id FROM Users WHERE Username = @Username";
                    int userId = 3; // Mặc định là cashier01 (Id = 3) nếu không tìm thấy

                    using (SqlCommand cmdUser = new SqlCommand(findUserSql, connection, transaction))
                    {
                        cmdUser.Parameters.AddWithValue("@Username", request.CashierUsername.Trim());
                        object? objUserId = cmdUser.ExecuteScalar();
                        if (objUserId != null && objUserId != DBNull.Value)
                        {
                            userId = Convert.ToInt32(objUserId);
                        }
                    }

                    // 2.2. Tính toán tổng tiền, tổng số lượng và điểm thưởng tích lũy
                    decimal finalAmount = request.Items.Sum(i => i.Quantity * i.UnitPrice);
                    int totalItems = request.Items.Sum(i => i.Quantity);
                    int rewardPoints = (int)(finalAmount / 10000); // 10.000đ = 1 điểm
                    string orderCode = $"HD-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 4).ToUpper()}";

                    // =====================================================================
                    // BƯỚC 1: TẠO HÓA ĐƠN TỔNG THỂ TRONG BẢNG Orders
                    // =====================================================================
                    string insertOrderSql = @"
                        INSERT INTO Orders (UserId, OrderDate, TotalAmount, Status, PaymentMethod, AmountReceived, ChangeAmount)
                        OUTPUT INSERTED.OrderId
                        VALUES (@UserId, GETDATE(), @TotalAmount, 'Completed', @PaymentMethod, @AmountReceived, 0)";

                    int orderId = 0;
                    using (SqlCommand cmdOrder = new SqlCommand(insertOrderSql, connection, transaction))
                    {
                        cmdOrder.Parameters.AddWithValue("@UserId", userId);
                        cmdOrder.Parameters.AddWithValue("@TotalAmount", finalAmount);
                        cmdOrder.Parameters.AddWithValue("@PaymentMethod", string.IsNullOrWhiteSpace(request.PaymentMethod) ? "Cash" : request.PaymentMethod);
                        cmdOrder.Parameters.AddWithValue("@AmountReceived", finalAmount);

                        orderId = (int)cmdOrder.ExecuteScalar();
                    }

                    // =====================================================================
                    // BƯỚC 2 & 3: XỬ LÝ TỪNG MÓN HÀNG (KIỂM TRA TỒN KHO, LƯU CHI TIẾT VÀ TRỪ KHO)
                    // =====================================================================
                    foreach (var item in request.Items)
                    {
                        // 3.1. Kiểm tra số lượng tồn kho thực tế trước khi trừ
                        string checkStockSql = "SELECT StockQuantity, ProductName FROM Products WHERE ProductId = @ProductId";
                        int currentStock = 0;
                        string productName = string.Empty;

                        using (SqlCommand cmdCheck = new SqlCommand(checkStockSql, connection, transaction))
                        {
                            cmdCheck.Parameters.AddWithValue("@ProductId", item.ProductId);

                            using (SqlDataReader reader = cmdCheck.ExecuteReader())
                            {
                                if (!reader.Read())
                                {
                                    throw new InvalidOperationException($"Không tìm thấy sản phẩm có ID: {item.ProductId}");
                                }
                                currentStock = Convert.ToInt32(reader["StockQuantity"]);
                                productName = reader["ProductName"].ToString() ?? string.Empty;
                            }
                        }

                        // 3.2. ĐIỂM KIỂM SOÁT LOGIC: Nếu số lượng mua vượt quá tồn kho -> Kích hoạt Exception để Rollback
                        if (currentStock < item.Quantity)
                        {
                            throw new InvalidOperationException(
                                $"Sản phẩm '{productName}' (ID: {item.ProductId}) không đủ tồn kho! " +
                                $"(Tồn kho thực tế: {currentStock}, Số lượng yêu cầu mua: {item.Quantity})");
                        }

                        // 3.3. Thêm dòng chi tiết vào bảng OrderDetails (cột SubTotal theo /Data)
                        string insertDetailSql = @"
                            INSERT INTO OrderDetails (OrderId, ProductId, Quantity, UnitPrice, SubTotal)
                            VALUES (@OrderId, @ProductId, @Quantity, @UnitPrice, @SubTotal)";

                        using (SqlCommand cmdDetail = new SqlCommand(insertDetailSql, connection, transaction))
                        {
                            cmdDetail.Parameters.AddWithValue("@OrderId", orderId);
                            cmdDetail.Parameters.AddWithValue("@ProductId", item.ProductId);
                            cmdDetail.Parameters.AddWithValue("@Quantity", item.Quantity);
                            cmdDetail.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                            cmdDetail.Parameters.AddWithValue("@SubTotal", item.Quantity * item.UnitPrice);
                            cmdDetail.ExecuteNonQuery();
                        }

                        // 3.4. Khấu trừ số lượng tồn kho trong bảng Products
                        string updateStockSql = @"
                            UPDATE Products 
                            SET StockQuantity = StockQuantity - @Qty 
                            WHERE ProductId = @ProductId";

                        using (SqlCommand cmdStock = new SqlCommand(updateStockSql, connection, transaction))
                        {
                            cmdStock.Parameters.AddWithValue("@Qty", item.Quantity);
                            cmdStock.Parameters.AddWithValue("@ProductId", item.ProductId);
                            cmdStock.ExecuteNonQuery();
                        }
                    }

                    // =====================================================================
                    // BƯỚC 4: CỘNG ĐIỂM TÍCH LŨY CHO KHÁCH HÀNG TRONG BẢNG Customers
                    // =====================================================================
                    if (rewardPoints > 0 && request.CustomerId > 0)
                    {
                        string updatePointsSql = @"
                            UPDATE Customers 
                            SET RewardPoints = RewardPoints + @Points 
                            WHERE CustomerId = @CustomerId";

                        using (SqlCommand cmdCustomer = new SqlCommand(updatePointsSql, connection, transaction))
                        {
                            cmdCustomer.Parameters.AddWithValue("@Points", rewardPoints);
                            cmdCustomer.Parameters.AddWithValue("@CustomerId", request.CustomerId);
                            cmdCustomer.ExecuteNonQuery();
                        }
                    }

                    // =====================================================================
                    // BƯỚC 5: XÁC NHẬN LƯU VĨNH VIỄN TOÀN BỘ THAO TÁC (COMMIT)
                    // =====================================================================
                    transaction.Commit();

                    return Ok(new
                    {
                        success = true,
                        message = "Thanh toán hóa đơn POS thành công!",
                        orderId = orderId,
                        orderCode = orderCode,
                        totalItems = totalItems,
                        finalAmount = finalAmount,
                        rewardPointsAdded = rewardPoints
                    });
                }
                catch (Exception ex)
                {
                    // =====================================================================
                    // BƯỚC 6: HOÀN TÁC TOÀN BỘ DỮ LIỆU SẠCH SẼ KHI CÓ BẤT KỲ LỖI NÀO (ROLLBACK)
                    // =====================================================================
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception rollbackEx)
                    {
                        return StatusCode(500, new 
                        { 
                            success = false,
                            message = "Lỗi nghiêm trọng khi thực thi Rollback: " + rollbackEx.Message 
                        });
                    }

                    return BadRequest(new
                    {
                        success = false,
                        message = "Giao dịch thanh toán bị hủy và đã được Rollback an toàn!",
                        errorDetail = ex.Message
                    });
                }
            }
        }

        // =========================================================================
        // BÀI TẬP MỞ RỘNG (TIẾT 5): HỦY ĐƠN HÀNG VÀ HOÀN TRẢ TỒN KHO & ĐIỂM THƯỞNG
        // DELETE: api/checkout/cancel/{orderId}
        // =========================================================================
        [HttpDelete("cancel/{orderId}")]
        public IActionResult CancelOrder(int orderId)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // 1. Kiểm tra hóa đơn
                    string getOrderSql = "SELECT TotalAmount FROM Orders WHERE OrderId = @OrderId";
                    decimal totalAmount = 0;

                    using (SqlCommand cmdGet = new SqlCommand(getOrderSql, connection, transaction))
                    {
                        cmdGet.Parameters.AddWithValue("@OrderId", orderId);
                        object? objAmount = cmdGet.ExecuteScalar();
                        if (objAmount == null || objAmount == DBNull.Value)
                        {
                            throw new InvalidOperationException($"Không tìm thấy hóa đơn ID = {orderId} để hủy!");
                        }
                        totalAmount = Convert.ToDecimal(objAmount);
                    }

                    // 2. Lấy danh sách sản phẩm đã mua để hoàn trả kho
                    var itemsToRestore = new List<(int ProductId, int Quantity)>();
                    string getDetailsSql = "SELECT ProductId, Quantity FROM OrderDetails WHERE OrderId = @OrderId";

                    using (SqlCommand cmdDetails = new SqlCommand(getDetailsSql, connection, transaction))
                    {
                        cmdDetails.Parameters.AddWithValue("@OrderId", orderId);
                        using (SqlDataReader reader = cmdDetails.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                itemsToRestore.Add((
                                    Convert.ToInt32(reader["ProductId"]), 
                                    Convert.ToInt32(reader["Quantity"])
                                ));
                            }
                        }
                    }

                    // 3. Hoàn trả tồn kho trong bảng Products
                    foreach (var item in itemsToRestore)
                    {
                        string restoreStockSql = @"
                            UPDATE Products 
                            SET StockQuantity = StockQuantity + @Qty 
                            WHERE ProductId = @ProductId";

                        using (SqlCommand cmdRestore = new SqlCommand(restoreStockSql, connection, transaction))
                        {
                            cmdRestore.Parameters.AddWithValue("@Qty", item.Quantity);
                            cmdRestore.Parameters.AddWithValue("@ProductId", item.ProductId);
                            cmdRestore.ExecuteNonQuery();
                        }
                    }

                    // 4. Khấu trừ điểm thưởng tương ứng
                    int pointsToDeduct = (int)(totalAmount / 10000);
                    if (pointsToDeduct > 0)
                    {
                        string deductPointsSql = @"
                            UPDATE Customers 
                            SET RewardPoints = CASE 
                                WHEN RewardPoints >= @Pts THEN RewardPoints - @Pts 
                                ELSE 0 
                            END 
                            WHERE CustomerId = 1"; // Hoặc CustomerId tương ứng nếu lưu quan hệ

                        using (SqlCommand cmdDeduct = new SqlCommand(deductPointsSql, connection, transaction))
                        {
                            cmdDeduct.Parameters.AddWithValue("@Pts", pointsToDeduct);
                            cmdDeduct.ExecuteNonQuery();
                        }
                    }

                    // 5. Xóa chi tiết hóa đơn trước (tránh vi phạm ràng buộc khóa ngoại)
                    string deleteDetailsSql = "DELETE FROM OrderDetails WHERE OrderId = @OrderId";
                    using (SqlCommand cmdDelDetails = new SqlCommand(deleteDetailsSql, connection, transaction))
                    {
                        cmdDelDetails.Parameters.AddWithValue("@OrderId", orderId);
                        cmdDelDetails.ExecuteNonQuery();
                    }

                    // 6. Xóa hóa đơn trong bảng Orders
                    string deleteOrderSql = "DELETE FROM Orders WHERE OrderId = @OrderId";
                    using (SqlCommand cmdDelOrder = new SqlCommand(deleteOrderSql, connection, transaction))
                    {
                        cmdDelOrder.Parameters.AddWithValue("@OrderId", orderId);
                        cmdDelOrder.ExecuteNonQuery();
                    }

                    // 7. Xác nhận lưu vĩnh viễn (Commit)
                    transaction.Commit();

                    return Ok(new
                    {
                        success = true,
                        message = $"Đã hủy thành công hóa đơn #{orderId} và hoàn trả toàn bộ tồn kho vào CSDL!"
                    });
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch { }

                    return BadRequest(new
                    {
                        success = false,
                        message = "Hủy hóa đơn thất bại, giao dịch đã được Rollback!",
                        errorDetail = ex.Message
                    });
                }
            }
        }
    }
}
