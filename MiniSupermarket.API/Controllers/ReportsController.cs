/*
 * Họ và tên sinh viên: Nguyễn Quốc Vương
 * Bài tập: Buổi 5 - Tiết 3: Tối ưu truy vấn với Stored Procedure & Báo cáo doanh thu ca bán lẻ
 * Mô tả: Controller ADO.NET thực thi Stored Procedure xuất báo cáo ca bán dựa trên dữ liệu bảng Orders và Users
 */
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MiniSupermarket.API.Dto;
using System.Data;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ReportsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // =========================================================================
        // GET: api/reports/revenue-by-cashier
        // Chức năng: Thống kê doanh thu theo từng thu ngân
        // =========================================================================
        [HttpGet("revenue-by-cashier")]
        public IActionResult GetRevenueByCashier()
        {
            var reportList = new List<CashierRevenueDto>();
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Thử gọi Stored Procedure trước; nếu chưa tạo thủ tục trên SQL Server thì chạy truy vấn SQL ADO.NET trực tiếp
                bool useProcedure = true;
                using (SqlCommand command = new SqlCommand("sp_GetRevenueByCashier", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            ReadCashierRevenueData(reader, reportList);
                        }
                    }
                    catch (SqlException ex) when (ex.Number == 2812) // 2812: Không tìm thấy Stored Procedure
                    {
                        useProcedure = false;
                    }
                    catch (SqlException ex)
                    {
                        return StatusCode(500, new 
                        { 
                            success = false,
                            message = "Lỗi thực thi Stored Procedure trên SQL Server: " + ex.Message 
                        });
                    }
                }

                // Dự phòng trực tiếp nếu Stored Procedure chưa được tạo trên SQL Server
                if (!useProcedure)
                {
                    string fallbackSql = @"
                        SELECT 
                            u.Username AS CashierUsername,
                            COUNT(o.OrderId) AS TotalOrders,
                            SUM(o.TotalAmount) AS TotalRevenue,
                            CAST(SUM(o.TotalAmount) / 10000 AS INT) AS TotalPointsAwarded
                        FROM Orders o
                        INNER JOIN Users u ON o.UserId = u.Id
                        GROUP BY u.Username
                        ORDER BY TotalRevenue DESC";

                    using (SqlCommand fallbackCmd = new SqlCommand(fallbackSql, connection))
                    {
                        using (SqlDataReader reader = fallbackCmd.ExecuteReader())
                        {
                            ReadCashierRevenueData(reader, reportList);
                        }
                    }
                }
            }

            return Ok(new
            {
                success = true,
                generatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                totalCashiers = reportList.Count,
                grandTotalRevenue = reportList.Sum(x => x.TotalRevenue),
                data = reportList
            });
        }

        // =========================================================================
        // BÀI TẬP 3.1: BÁO CÁO DOANH THU THEO KHOẢNG NGÀY (CÓ THAM SỐ)
        // GET: api/reports/revenue-by-date-range?fromDate=2026-10-01&toDate=2026-10-31
        // =========================================================================
        [HttpGet("revenue-by-date-range")]
        public IActionResult GetRevenueByDateRange([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            var reportList = new List<CashierRevenueDto>();
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                bool useProcedure = true;

                using (SqlCommand command = new SqlCommand("sp_GetRevenueByDateRange", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FromDate", fromDate);
                    command.Parameters.AddWithValue("@ToDate", toDate);

                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            ReadCashierRevenueData(reader, reportList);
                        }
                    }
                    catch (SqlException ex) when (ex.Number == 2812)
                    {
                        useProcedure = false;
                    }
                    catch (SqlException ex)
                    {
                        return StatusCode(500, new 
                        { 
                            success = false,
                            message = "Lỗi thực thi Stored Procedure theo khoảng ngày: " + ex.Message 
                        });
                    }
                }

                if (!useProcedure)
                {
                    string fallbackSql = @"
                        SELECT 
                            u.Username AS CashierUsername,
                            COUNT(o.OrderId) AS TotalOrders,
                            SUM(o.TotalAmount) AS TotalRevenue,
                            CAST(SUM(o.TotalAmount) / 10000 AS INT) AS TotalPointsAwarded
                        FROM Orders o
                        INNER JOIN Users u ON o.UserId = u.Id
                        WHERE o.OrderDate >= @FromDate AND o.OrderDate <= @ToDate
                        GROUP BY u.Username
                        ORDER BY TotalRevenue DESC";

                    using (SqlCommand fallbackCmd = new SqlCommand(fallbackSql, connection))
                    {
                        fallbackCmd.Parameters.AddWithValue("@FromDate", fromDate);
                        fallbackCmd.Parameters.AddWithValue("@ToDate", toDate);
                        using (SqlDataReader reader = fallbackCmd.ExecuteReader())
                        {
                            ReadCashierRevenueData(reader, reportList);
                        }
                    }
                }
            }

            return Ok(new
            {
                success = true,
                fromDate = fromDate.ToString("yyyy-MM-dd HH:mm:ss"),
                toDate = toDate.ToString("yyyy-MM-dd HH:mm:ss"),
                totalCashiers = reportList.Count,
                grandTotalRevenue = reportList.Sum(x => x.TotalRevenue),
                data = reportList
            });
        }

        // =========================================================================
        // BÀI TẬP 3.2: TOP 5 SẢN PHẨM BÁN CHẠY NHẤT
        // GET: api/reports/top-selling
        // =========================================================================
        [HttpGet("top-selling")]
        public IActionResult GetTop5SellingProducts()
        {
            var topList = new List<TopSellingProductDto>();
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                bool useProcedure = true;

                using (SqlCommand command = new SqlCommand("sp_GetTop5SellingProducts", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            ReadTopSellingData(reader, topList);
                        }
                    }
                    catch (SqlException ex) when (ex.Number == 2812)
                    {
                        useProcedure = false;
                    }
                    catch (SqlException ex)
                    {
                        return StatusCode(500, new 
                        { 
                            success = false,
                            message = "Lỗi thực thi Stored Procedure top sản phẩm: " + ex.Message 
                        });
                    }
                }

                if (!useProcedure)
                {
                    string fallbackSql = @"
                        SELECT TOP 5
                            p.ProductId,
                            p.Barcode,
                            p.ProductName,
                            SUM(od.Quantity) AS TotalQuantitySold,
                            SUM(od.SubTotal) AS TotalSalesAmount
                        FROM OrderDetails od
                        INNER JOIN Products p ON od.ProductId = p.ProductId
                        GROUP BY p.ProductId, p.Barcode, p.ProductName
                        ORDER BY TotalQuantitySold DESC";

                    using (SqlCommand fallbackCmd = new SqlCommand(fallbackSql, connection))
                    {
                        using (SqlDataReader reader = fallbackCmd.ExecuteReader())
                        {
                            ReadTopSellingData(reader, topList);
                        }
                    }
                }
            }

            return Ok(new
            {
                success = true,
                totalProducts = topList.Count,
                data = topList
            });
        }

        private static void ReadCashierRevenueData(SqlDataReader reader, List<CashierRevenueDto> list)
        {
            while (reader.Read())
            {
                list.Add(new CashierRevenueDto
                {
                    CashierUsername = reader["CashierUsername"].ToString() ?? string.Empty,
                    TotalOrders = Convert.ToInt32(reader["TotalOrders"]),
                    TotalRevenue = Convert.ToDecimal(reader["TotalRevenue"]),
                    TotalPointsAwarded = reader["TotalPointsAwarded"] != DBNull.Value 
                        ? Convert.ToInt32(reader["TotalPointsAwarded"]) 
                        : 0
                });
            }
        }

        private static void ReadTopSellingData(SqlDataReader reader, List<TopSellingProductDto> list)
        {
            while (reader.Read())
            {
                list.Add(new TopSellingProductDto
                {
                    ProductId = Convert.ToInt32(reader["ProductId"]),
                    Barcode = reader["Barcode"].ToString() ?? string.Empty,
                    ProductName = reader["ProductName"].ToString() ?? string.Empty,
                    TotalQuantitySold = Convert.ToInt32(reader["TotalQuantitySold"]),
                    TotalSalesAmount = Convert.ToDecimal(reader["TotalSalesAmount"])
                });
            }
        }
    }
}
