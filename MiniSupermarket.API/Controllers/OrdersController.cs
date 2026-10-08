using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;
using System.Security.Claims;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Cashier")]
    public class OrdersController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public OrdersController(SupermarketDbContext context)
        {
            _context = context;
        }

        // =========================================================
        // GET: /api/orders
        // Xem danh sách đơn hàng
        // Admin và Cashier đều được xem
        // =========================================================
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .OrderByDescending(o => o.OrderDate)
                .Select(o => new
                {
                    orderId = o.OrderId,
                    orderDate = o.OrderDate,
                    totalAmount = o.TotalAmount,
                    status = o.Status,
                    paymentMethod = o.PaymentMethod,
                    amountReceived = o.AmountReceived,
                    changeAmount = o.ChangeAmount,

                    employee = o.User != null
                        ? o.User.FullName
                        : "Không xác định",

                    items = o.OrderDetails.Select(od => new
                    {
                        productId = od.ProductId,
                        productName = od.Product != null
                            ? od.Product.ProductName
                            : "Không xác định",

                        quantity = od.Quantity,
                        unitPrice = od.UnitPrice,
                        subTotal = od.SubTotal
                    }).ToList()
                })
                .ToListAsync();

            return Ok(new
            {
                success = true,
                data = orders
            });
        }


        // =========================================================
        // GET: /api/orders/5
        // Xem chi tiết một đơn hàng
        // =========================================================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Không tìm thấy đơn hàng!"
                });
            }

            return Ok(new
            {
                success = true,

                data = new
                {
                    orderId = order.OrderId,
                    orderDate = order.OrderDate,
                    totalAmount = order.TotalAmount,
                    status = order.Status,
                    paymentMethod = order.PaymentMethod,
                    amountReceived = order.AmountReceived,
                    changeAmount = order.ChangeAmount,

                    employee = order.User?.FullName,

                    items = order.OrderDetails.Select(od => new
                    {
                        productId = od.ProductId,
                        productName = od.Product?.ProductName,
                        barcode = od.Product?.Barcode,

                        quantity = od.Quantity,
                        unitPrice = od.UnitPrice,
                        subTotal = od.SubTotal
                    })
                }
            });
        }


        // =========================================================
        // POST: /api/orders
        // Tạo đơn hàng / Thanh toán POS
        // =========================================================
        [HttpPost]
        public async Task<IActionResult> CreateOrder(
            [FromBody] CreateOrderRequest request)
        {
            // -----------------------------------------------------
            // 1. Kiểm tra dữ liệu đầu vào
            // -----------------------------------------------------
            if (request.Items == null || request.Items.Count == 0)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Giỏ hàng đang trống!"
                });
            }

            if (request.AmountReceived < 0)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Số tiền khách đưa không hợp lệ!"
                });
            }

            if (string.IsNullOrWhiteSpace(request.PaymentMethod))
            {
                request.PaymentMethod = "Cash";
            }


            // -----------------------------------------------------
            // 2. Lấy username từ JWT
            // -----------------------------------------------------
            var username = User.FindFirstValue(ClaimTypes.Name);

            if (string.IsNullOrWhiteSpace(username))
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "Không xác định được nhân viên đang đăng nhập!"
                });
            }


            // -----------------------------------------------------
            // 3. Tìm User hiện tại
            // -----------------------------------------------------
            var currentUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            if (currentUser == null)
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "Tài khoản đăng nhập không tồn tại!"
                });
            }


            // -----------------------------------------------------
            // 4. Bắt đầu Transaction
            // -----------------------------------------------------
            await using var transaction =
                await _context.Database.BeginTransactionAsync();

            try
            {
                decimal totalAmount = 0;

                var orderDetails = new List<OrderDetail>();


                // -------------------------------------------------
                // 5. Kiểm tra từng sản phẩm
                // -------------------------------------------------
                foreach (var item in request.Items)
                {
                    if (item.Quantity <= 0)
                    {
                        return BadRequest(new
                        {
                            success = false,
                            message = $"Số lượng sản phẩm ID {item.ProductId} không hợp lệ!"
                        });
                    }


                    // Lấy sản phẩm từ Database
                    var product = await _context.Products
                        .FirstOrDefaultAsync(
                            p => p.ProductId == item.ProductId);


                    if (product == null)
                    {
                        return BadRequest(new
                        {
                            success = false,
                            message =
                                $"Không tìm thấy sản phẩm ID {item.ProductId}!"
                        });
                    }


                    // ---------------------------------------------
                    // Kiểm tra tồn kho
                    // ---------------------------------------------
                    if (product.StockQuantity < item.Quantity)
                    {
                        return BadRequest(new
                        {
                            success = false,
                            message =
                                $"Sản phẩm '{product.ProductName}' chỉ còn " +
                                $"{product.StockQuantity} {product.Unit}!"
                        });
                    }


                    // ---------------------------------------------
                    // QUAN TRỌNG:
                    // Không lấy giá từ Client.
                    // Lấy giá trực tiếp từ Database.
                    // ---------------------------------------------
                    decimal unitPrice = product.Price;

                    decimal subTotal =
                        unitPrice * item.Quantity;


                    // ---------------------------------------------
                    // Tạo OrderDetail
                    // ---------------------------------------------
                    var detail = new OrderDetail
                    {
                        ProductId = product.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = unitPrice,
                        SubTotal = subTotal
                    };

                    orderDetails.Add(detail);

                    totalAmount += subTotal;


                    // ---------------------------------------------
                    // Trừ tồn kho
                    // ---------------------------------------------
                    product.StockQuantity -= item.Quantity;
                }


                // -------------------------------------------------
                // 6. Kiểm tra tiền khách đưa
                // -------------------------------------------------
                if (request.PaymentMethod == "Cash" &&
                    request.AmountReceived < totalAmount)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message =
                            $"Tiền khách đưa không đủ! " +
                            $"Cần thanh toán {totalAmount:N0} VNĐ."
                    });
                }


                // -------------------------------------------------
                // 7. Tính tiền thừa
                // -------------------------------------------------
                decimal changeAmount = 0;

                if (request.PaymentMethod == "Cash")
                {
                    changeAmount =
                        request.AmountReceived - totalAmount;
                }


                // -------------------------------------------------
                // 8. Tạo Order
                // -------------------------------------------------
                var order = new Order
                {
                    UserId = currentUser.Id,

                    OrderDate = DateTime.Now,

                    TotalAmount = totalAmount,

                    Status = "Completed",

                    PaymentMethod = request.PaymentMethod,

                    AmountReceived =
                        request.PaymentMethod == "Cash"
                            ? request.AmountReceived
                            : totalAmount,

                    ChangeAmount = changeAmount
                };


                // -------------------------------------------------
                // 9. Gắn OrderDetail vào Order
                // -------------------------------------------------
                foreach (var detail in orderDetails)
                {
                    order.OrderDetails.Add(detail);
                }


                // -------------------------------------------------
                // 10. Lưu Database
                // -------------------------------------------------
                _context.Orders.Add(order);

                await _context.SaveChangesAsync();


                // -------------------------------------------------
                // 11. Commit Transaction
                // -------------------------------------------------
                await transaction.CommitAsync();


                // -------------------------------------------------
                // 12. Trả kết quả
                // -------------------------------------------------
                return Ok(new
                {
                    success = true,

                    message = "Thanh toán thành công!",

                    data = new
                    {
                        orderId = order.OrderId,

                        orderDate = order.OrderDate,

                        employee = currentUser.FullName,

                        totalAmount = order.TotalAmount,

                        paymentMethod =
                            order.PaymentMethod,

                        amountReceived =
                            order.AmountReceived,

                        changeAmount =
                            order.ChangeAmount,

                        items = order.OrderDetails.Select(d => new
                        {
                            productId = d.ProductId,
                            quantity = d.Quantity,
                            unitPrice = d.UnitPrice,
                            subTotal = d.SubTotal
                        })
                    }
                });
            }
            catch (Exception ex)
            {
                // ---------------------------------------------
                // Nếu có lỗi → Rollback
                // ---------------------------------------------
                await transaction.RollbackAsync();

                return StatusCode(500, new
                {
                    success = false,
                    message = "Có lỗi xảy ra khi thanh toán!",
                    error = ex.Message
                });
            }
        }


        // =========================================================
        // PUT: /api/orders/5/cancel
        // Hủy đơn hàng
        // Chỉ Admin được hủy
        // =========================================================
        [HttpPut("{id}/cancel")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            await using var transaction =
                await _context.Database.BeginTransactionAsync();

            try
            {
                var order = await _context.Orders
                    .Include(o => o.OrderDetails)
                    .FirstOrDefaultAsync(o => o.OrderId == id);

                if (order == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Không tìm thấy đơn hàng!"
                    });
                }

                if (order.Status == "Cancelled")
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Đơn hàng này đã được hủy!"
                    });
                }


                // ---------------------------------------------
                // Hoàn lại tồn kho
                // ---------------------------------------------
                foreach (var detail in order.OrderDetails)
                {
                    var product = await _context.Products
                        .FirstOrDefaultAsync(
                            p => p.ProductId == detail.ProductId);

                    if (product != null)
                    {
                        product.StockQuantity += detail.Quantity;
                    }
                }


                // ---------------------------------------------
                // Đổi trạng thái
                // ---------------------------------------------
                order.Status = "Cancelled";

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();


                return Ok(new
                {
                    success = true,
                    message = "Đã hủy đơn hàng và hoàn lại tồn kho!"
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                return StatusCode(500, new
                {
                    success = false,
                    message = "Có lỗi xảy ra khi hủy đơn hàng!",
                    error = ex.Message
                });
            }
        }
    }


    // =========================================================
    // DTO tạo đơn hàng
    // =========================================================
    public class CreateOrderRequest
    {
        public string PaymentMethod { get; set; } = "Cash";

        public decimal AmountReceived { get; set; }

        public List<CreateOrderItemRequest> Items { get; set; }
            = new List<CreateOrderItemRequest>();
    }


    // =========================================================
    // DTO sản phẩm trong đơn hàng
    // =========================================================
    public class CreateOrderItemRequest
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}