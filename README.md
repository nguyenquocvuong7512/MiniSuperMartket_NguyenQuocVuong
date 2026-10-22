🛒 MiniSupermarket - Buổi 5: ADO.NET Nâng Cao & Giao Dịch POS

Dự án thực hành Buổi 5 - Lập trình ứng dụng .NET: Xây dựng hệ sinh thái bán lẻ MiniSupermarket tập trung vào tối ưu hóa truy xuất dữ liệu hiệu năng cao với ADO.NET thuần, báo cáo doanh thu ca với Stored Procedure, và bảo toàn giao dịch thanh toán đa bảng theo chuẩn ACID bằng SqlTransaction.

📑 Mục Lục

1. Giới thiệu tổng quan

2. Cấu trúc Solution & Công nghệ sử dụng

3. Các tính năng & Trọng tâm kỹ thuật Buổi 5

3.1. 5 Thành phần cốt lõi ADO.NET

3.2. API Tra cứu mã vạch siêu tốc (SqlDataReader)

3.3. Mô hình ngắt kết nối với DataTable

3.4. Báo cáo doanh thu ca bằng Stored Procedure

3.5. Quy trình POS Checkout bọc kín trong SqlTransaction (ACID)

4. Hướng dẫn cài đặt & Triển khai

5. Tài liệu API & Kịch bản kiểm thử (Test Cases)

6. Bảng kiểm tra nghiệm thu (Checklist)

📌 1. Giới thiệu tổng quan

Trong các hệ thống bán lẻ (POS) tại siêu thị, việc thao tác qua các ORM nặng (như Entity Framework) đôi khi gây ra độ trễ cao và khó kiểm soát giao dịch ở mức vi mô. Buổi 5 giải quyết các bài toán:

Tốc độ: Quét mã vạch (Barcode Scanner) và đọc dữ liệu cực nhanh qua luồng một chiều SqlDataReader (<100ms).

Tối ưu máy chủ: Chuyển các phép gom nhóm, tính tổng báo cáo phức tạp về Stored Procedure trên SQL Server.

Toàn vẹn giao dịch: Áp dụng SqlTransaction bọc liên hoàn 4 bảng dữ liệu: Tạo đơn (Orders), lưu dòng đơn (OrderDetails), trừ tồn kho (Products), tích điểm khách hàng (Customers). Nếu có bất kỳ mặt hàng nào thiếu kho, hệ thống lập tức hoàn tác (Rollback) 100%.

📂 2. Cấu trúc Solution & Công nghệ sử dụng

Solution gồm 2 project chính:

MiniSuperMartket.sln
│
├── 🌐 MiniSupermarket.API/                   # .NET Web API phục vụ Client & Trạm POS
│   ├── Controllers/
│   │   ├── AdoProductController.cs          # [Mới] Tra cứu Barcode & Cache DataTable
│   │   ├── ReportsController.cs             # [Mới] Báo cáo doanh thu ca bằng Stored Procedure
│   │   ├── CheckoutController.cs            # [Mới] Giao dịch thanh toán POS (ACID Transaction)
│   │   ├── AuthController.cs                # Xác thực & Phân quyền JWT
│   │   ├── ProductsController.cs            # CRUD sản phẩm (ORM / EF Core)
│   │   └── ...
│   ├── Data/
│   │   └── SupermarketDbContext.cs          # Entity Framework DbContext
│   ├── Dto/
│   │   ├── CashierRevenueDto.cs             # DTO nhận kết quả từ sp_GetRevenueByCashier
│   │   ├── CheckoutDtos.cs                  # CheckoutRequestDto & CartItemCheckoutDto
│   │   └── ProductLookupDto.cs              # DTO tra cứu nhanh Barcode
│   └── appsettings.json                     # Chuỗi kết nối SQL Server
│
└── 🖥️ MiniSupermarket.WinForm/               # Ứng dụng Desktop cho thu ngân & quản lý
    ├── FormLogin.cs                         # Đăng nhập hệ thống
    ├── FormMainShell.cs                     # Điều hướng giao diện chính
    ├── FormPOS.cs                           # Giao diện bán lẻ, tích hợp quét Barcode & Checkout
    ├── FormQuickReport.cs                   # Màn hình xem nhanh báo cáo doanh thu ca
    └── SessionManager.cs                    # Quản lý phiên làm việc thu ngân hiện tại


⚡ 3. Các tính năng & Trọng tâm kỹ thuật Buổi 5

3.1. 5 Thành phần cốt lõi ADO.NET

Thành phần

Vai trò thực tế

Lưu ý kỹ thuật

SqlConnection

Quản lý phiên kết nối vật lý với SQL Server

Luôn đặt trong khối using (...) để tự động đóng kết nối (tránh cạn kiệt Connection Pool).

SqlCommand

Chứa câu lệnh T-SQL hoặc tên Stored Procedure

Chỉ định đúng thuộc tính CommandType.

SqlParameter

Tham số hóa dữ liệu đầu vào

Chống triệt để SQL Injection (không cộng chuỗi SQL trực tiếp).

SqlDataReader

Đọc dữ liệu nhanh, tiến một chiều (Forward-Only)

Tiết kiệm RAM tối đa vì không nạp toàn bộ bảng lên bộ nhớ.

DataTable / SqlDataAdapter

Mô hình ngắt kết nối (Disconnected Mode)

Tự động mở kết nối, Fill() dữ liệu vào RAM rồi đóng kết nối ngay lập tức.

3.2. API Tra cứu mã vạch siêu tốc (SqlDataReader)

Endpoint: GET /api/ado-products/barcode/{barcode}

Cơ chế: Sử dụng SqlDataReader đọc tuần tự một bản ghi duy nhất khớp với Barcode. Trả về mã HTTP 200 OK hoặc 404 Not Found nếu không tìm thấy.

using (SqlConnection connection = new SqlConnection(connectionString))
{
    string query = @"SELECT ProductId, Barcode, ProductName, Price, StockQuantity 
                     FROM Products WHERE Barcode = @Barcode";

    using (SqlCommand command = new SqlCommand(query, connection))
    {
        command.Parameters.AddWithValue("@Barcode", barcode.Trim());
        connection.Open();
        using (SqlDataReader reader = command.ExecuteReader())
        {
            if (reader.Read())
            {
                return Ok(new ProductLookupDto {
                    ProductId = Convert.ToInt32(reader["ProductId"]),
                    Barcode = reader["Barcode"].ToString(),
                    ProductName = reader["ProductName"].ToString(),
                    Price = Convert.ToDecimal(reader["Price"]),
                    StockQuantity = Convert.ToInt32(reader["StockQuantity"])
                });
            }
        }
    }
}
return NotFound(new { message = $"Không tìm thấy sản phẩm có mã: {barcode}" });


3.3. Mô hình ngắt kết nối với DataTable

Endpoint: GET /api/ado-products/disconnected-cache

Đổ toàn bộ danh mục sản phẩm vào bộ đệm RAM qua SqlDataAdapter.Fill(dataTable) để các máy POS có thể sử dụng dữ liệu ngoại tuyến mà không cần duy trì kết nối mạng liên tục.

3.4. Báo cáo doanh thu ca bằng Stored Procedure

T-SQL Script trên SQL Server (sp_GetRevenueByCashier):

CREATE PROCEDURE sp_GetRevenueByCashier
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        CashierUsername,
        COUNT(OrderId) AS TotalOrders,
        SUM(FinalAmount) AS TotalRevenue,
        SUM(RewardPoints) AS TotalPointsAwarded
    FROM Orders
    GROUP BY CashierUsername
    ORDER BY TotalRevenue DESC;
END
GO


Thực thi trong C#:

using (SqlCommand command = new SqlCommand("sp_GetRevenueByCashier", connection))
{
    command.CommandType = CommandType.StoredProcedure; // Bắt buộc
    connection.Open();
    using (SqlDataReader reader = command.ExecuteReader())
    {
        while (reader.Read())
        {
            reportList.Add(new CashierRevenueDto {
                CashierUsername = reader["CashierUsername"].ToString()!,
                TotalOrders = Convert.ToInt32(reader["TotalOrders"]),
                TotalRevenue = Convert.ToDecimal(reader["TotalRevenue"]),
                TotalPointsAwarded = reader["TotalPointsAwarded"] != DBNull.Value 
                    ? Convert.ToInt32(reader["TotalPointsAwarded"]) : 0
            });
        }
    }
}


3.5. Quy trình POS Checkout bọc kín trong SqlTransaction (ACID)

[ Client gửi CheckoutRequestDto ]
               │
               ▼
   [ connection.BeginTransaction() ]
               │
   ┌───────────┴─────────────────────────────────────────────┐
   │ BƯỚC 1: INSERT INTO Orders (Lấy OUTPUT INSERTED.OrderId)│
   │                                                         │
   │ BƯỚC 2: Duyệt từng sản phẩm trong giỏ:                  │
   │         ├── SELECT StockQuantity (Kiểm tra tồn kho)     │
   │         │   └── Tồn kho < Mua? ──► THROW EXCEPTION ──┐  │
   │         ├── INSERT INTO OrderDetails                 │  │
   │         └── UPDATE Products (Trừ tồn kho)            │  │
   │                                                      │  │
   │ BƯỚC 3: UPDATE Customers (Cộng điểm tích lũy)        │  │
   └───────────────────────────┬─────────────────────────────┘  │
                               │ Tất cả thành công              │ Lỗi
                               ▼                                ▼
                     [ transaction.Commit() ]          [ transaction.Rollback() ]
                               │                                │
                     HTTP 200 OK (Thành công)          HTTP 400 (Dữ liệu vẹn nguyên)


🚀 4. Hướng dẫn cài đặt & Triển khai

Bước 1: Chuẩn bị cơ sở dữ liệu

Mở SQL Server Management Studio (SSMS).

Đảm bảo database MiniSupermarketDb đã tồn tại dữ liệu mẫu.

Mở tab truy vấn mới, dán đoạn script tạo procedure sp_GetRevenueByCashier và nhấn Execute (F5).

Bước 2: Cấu hình Connection String

Mở file MiniSupermarket.API/appsettings.json:

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=MiniSupermarketDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}


(Nếu dùng SQL Server Express cục bộ, thay đổi Server=localhost thành Server=.\\SQLEXPRESS).

Bước 3: Chạy ứng dụng

Chạy Web API: Trong Visual Studio, chọn MiniSupermarket.API làm Startup Project và bấm F5. Truy cập Swagger tại https://localhost:7xxx/swagger/index.html.

Chạy WinForms POS: Đặt Startup Project là MiniSupermarket.WinForm và khởi chạy để kiểm tra giao diện bán hàng máy trạm.

🧪 5. Tài liệu API & Kịch bản kiểm thử (Test Cases)

Test Case 1: Tra cứu Barcode hợp lệ

Method: GET

URL: /api/ado-products/barcode/893456789001

Response 200 OK:

{
  "productId": 1,
  "barcode": "893456789001",
  "productName": "Snack Khoai Tây O'Star Gói 60g",
  "price": 12500,
  "stockQuantity": 150
}


Test Case 2: Báo cáo doanh thu ca (Stored Procedure)

Method: GET

URL: /api/reports/revenue-by-cashier

Response 200 OK:

{
  "success": true,
  "totalCashiers": 5,
  "grandTotalRevenue": 4822000.0,
  "data": [
    {
      "cashierUsername": "cashier03",
      "totalOrders": 2,
      "totalRevenue": 1435000.0,
      "totalPointsAwarded": 143
    }
  ]
}


Test Case 3: Thanh toán giỏ hàng hợp lệ (Commit)

Method: POST

URL: /api/checkout

Request Body:

{
  "cashierUsername": "cashier01",
  "customerId": 1,
  "items": [
    {
      "productId": 1,
      "quantity": 2,
      "unitPrice": 12500
    },
    {
      "productId": 5,
      "quantity": 3,
      "unitPrice": 10000
    }
  ]
}


Response 200 OK:

{
  "success": true,
  "message": "Thanh toán hóa đơn POS thành công!",
  "orderId": 16,
  "orderCode": "HD-20261022-A1B2",
  "totalItems": 5,
  "finalAmount": 55000.0,
  "rewardPointsAdded": 5
}


Test Case 4: Mua hàng vượt tồn kho (Kiểm chứng Rollback ACID)

Method: POST

URL: /api/checkout

Request Body (Đặt 999 đơn vị trong khi kho chỉ còn 40):

{
  "cashierUsername": "cashier01",
  "customerId": 1,
  "items": [
    {
      "productId": 1,
      "quantity": 1,
      "unitPrice": 12500
    },
    {
      "productId": 2,
      "quantity": 999,
      "unitPrice": 135000
    }
  ]
}


Response 400 Bad Request:

{
  "success": false,
  "message": "Giao dịch thanh toán bị hủy và đã được Rollback an toàn!",
  "errorDetail": "Sản phẩm 'Bánh Quy Bơ Danisa Hộp Thiếc 454g' (ID: 2) không đủ tồn kho! (Tồn kho thực tế: 40, Số lượng yêu cầu mua: 999)"
}


Kiểm tra đối soát SSMS:

SELECT TOP 1 * FROM Orders ORDER BY OrderId DESC; $\rightarrow$ Không có đơn rác nào được sinh ra.

Tồn kho của sản phẩm ID 1 không hề bị trừ đi 1 đơn vị. Toàn bộ phiên quay về nguyên trạng ban đầu.

🎯 6. Bảng kiểm tra nghiệm thu (Checklist)

[x] Sử dụng using bọc kín SqlConnection, phòng ngừa rò rỉ Connection Pool.

[x] Sử dụng SqlParameter cho toàn bộ tham số, chống lỗ hổng SQL Injection.

[x] Endpoint Tra cứu mã vạch sử dụng SqlDataReader phản hồi nhanh dưới 100ms.

[x] Tạo và ánh xạ thành công kết quả từ Stored Procedure sp_GetRevenueByCashier.

[x] API Checkout sử dụng SqlTransaction điều phối đủ 4 bước (Order, OrderDetails, Stock, Points).

[x] Kịch bản Rollback hoạt động hoàn hảo khi phát sinh ngoại lệ thiếu hàng trong kho.

[x] (Nâng cao) Bổ sung API Hủy đơn hàng và hoàn trả tồn kho tự động trong 1 Transaction.
