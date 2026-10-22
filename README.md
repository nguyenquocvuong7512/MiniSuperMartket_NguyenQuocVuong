BUỔI 5: ADO.NET NÂNG CAO, STORED PROCEDURE & GIAO DỊCH POS CHECKOUT

Tài liệu hướng dẫn thực hành và tóm tắt kiến trúc cho hệ thống MiniSupermarket (bao gồm MiniSupermarket.API và MiniSupermarket.WinForm), tập trung vào kỹ thuật truy xuất dữ liệu hiệu năng cao với ADO.NET thuần, tối ưu báo cáo bằng Stored Procedure và bảo toàn dữ liệu bằng SqlTransaction (chuẩn ACID).

📋 MỤC LỤC

Mục tiêu buổi học

Cấu trúc Solution & Tệp tin phát triển

Nội dung trọng tâm 5 tiết học

Tiết 1: 5 Thành phần cốt lõi ADO.NET

Tiết 2: API Tra cứu Barcode & Mô hình DataTable Cache

Tiết 3: Stored Procedure & Báo cáo doanh thu ca

Tiết 4: Chuẩn ACID & Kỹ thuật SqlTransaction

Tiết 5: Khép kín luồng POS Checkout & Kịch bản Rollback

Hướng dẫn cài đặt & Chạy kiểm thử

Kịch bản kiểm thử API (Swagger UI / Postman)

Checklist nghiệm thu thực hành

1. MỤC TIÊU BUỔI HỌC

Nắm vững kiến trúc ADO.NET thuần: Phân biệt và làm chủ 5 thành phần: SqlConnection, SqlCommand, SqlParameter, SqlDataReader, DataTable / SqlDataAdapter.

An toàn bảo mật: Triệt tiêu hoàn toàn lỗ hổng tấn công SQL Injection bằng kỹ thuật tham số hóa SqlParameter.

Hiệu năng & Tối ưu hóa:

Ứng dụng luồng đọc siêu tốc một chiều (SqlDataReader, Forward-Only) cho bài toán quét mã vạch (Barcode) dưới 100ms.

Sử dụng Stored Procedure (sp_GetRevenueByCashier) giảm tải băng thông và tối ưu kế hoạch thực thi trên SQL Server.

Bảo toàn dữ liệu theo chuẩn ACID: Điều khiển giao dịch đa bảng (Orders, OrderDetails, Products, Customers) bằng SqlTransaction, đảm bảo tự động Rollback khi gặp lỗi vượt tồn kho hoặc sự cố hệ thống.

2. CẤU TRÚC SOLUTION & TỆP TIN PHÁT TRIỂN

MiniSuperMartket/
│
├── MiniSupermarket.API/                   # Dự án Web API (.NET Core)
│   ├── Controllers/
│   │   ├── AdoProductController.cs        # [MỚI - Tiết 2] Tra cứu Barcode & DataTable cache
│   │   ├── ReportsController.cs           # [MỚI - Tiết 3] Báo cáo doanh thu qua Stored Procedure
│   │   ├── CheckoutController.cs          # [MỚI - Tiết 5] Xử lý thanh toán POS với SqlTransaction
│   │   └── ... (Các controller khác)
│   ├── Dto/ (hoặc Models/)
│   │   ├── ProductLookupDto.cs            # DTO trả về thông tin tra cứu mã vạch
│   │   ├── CashierRevenueDto.cs           # DTO nhận kết quả từ sp_GetRevenueByCashier
│   │   └── CheckoutDtos.cs                # CheckoutRequestDto & CartItemCheckoutDto
│   ├── Data/
│   │   └── SupermarketDbContext.cs
│   └── appsettings.json                   # Chuỗi kết nối "DefaultConnection"
│
└── MiniSupermarket.WinForm/               # Dự án giao diện máy trạm quầy thu ngân
    ├── FormPOS.cs                         # Giao diện bán hàng POS tích hợp
    ├── FormQuickReport.cs                 # Giao diện xem nhanh báo cáo doanh thu ca
    └── ...


3. NỘI DUNG TRỌNG TÂM 5 TIẾT HỌC

Tiết 1: 5 Thành phần cốt lõi ADO.NET

Mô hình đời thường:

SqlConnection: Đường dây điện thoại đến siêu thị (bắt buộc Open() và bọc trong using).

SqlCommand: Phiếu đặt hàng chứa lệnh thực thi (Text hoặc Stored Procedure).

SqlParameter: Tham số an toàn, chống nối chuỗi SQL Injection (@Barcode, @Stock).

SqlDataReader: Băng chuyền quét mã siêu tốc (Connected, Read-Only, Forward-Only).

DataTable / SqlDataAdapter: Thùng hàng đóng gói mang về nhà (Disconnected Mode).

Phân biệt 3 phương thức thực thi của SqlCommand:

ExecuteScalar(): Trả về 1 ô duy nhất (Dòng 1, Cột 1) $\rightarrow$ COUNT(*), SUM(), SCOPE_IDENTITY().

ExecuteNonQuery(): Trả về số dòng bị ảnh hưởng $\rightarrow$ INSERT, UPDATE, DELETE.

ExecuteReader(): Trả về đối tượng SqlDataReader $\rightarrow$ Đọc duyệt danh sách bản ghi.

Tiết 2: API Tra cứu Barcode & Mô hình DataTable Cache

Vấn đề quầy POS: Thao tác quét mã vạch cần tốc độ phản hồi tức thì (<100ms), không thể chịu overhead từ ORM.

Giải pháp: Xây dựng AdoProductController:

GET /api/ado-products/barcode/{barcode}: Mở SqlConnection, dùng SqlDataReader đọc đúng 1 dòng và đóng ngay.

GET /api/ado-products/disconnected-cache: Dùng SqlDataAdapter.Fill(dataTable) để lấy dữ liệu về RAM, tự động giải phóng kết nối vật lý ngay khi nạp xong.

Tiết 3: Stored Procedure & Báo cáo doanh thu ca

Lợi ích Stored Procedure: Biên dịch sẵn kế hoạch thực thi (Execution Plan), giảm băng thông đường truyền, tách biệt tầng logic dữ liệu và tăng cường bảo mật phân quyền.

Script T-SQL tạo thủ tục:

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


Khai báo trong C#:

using (SqlCommand cmd = new SqlCommand("sp_GetRevenueByCashier", conn))
{
    cmd.CommandType = CommandType.StoredProcedure; // BẮT BUỘC
    // Thực thi qua ExecuteReader...
}


Tiết 4: Chuẩn ACID & Kỹ thuật SqlTransaction

4 Nguyên tắc ACID:

A (Atomicity): Tính nguyên tử - Tất cả thành công hoặc không có gì thay đổi.

C (Consistency): Tính nhất quán - Ràng buộc và logic dữ liệu luôn toàn vẹn.

I (Isolation): Tính cô lập - Các giao dịch chạy song song không thấy dữ liệu rác của nhau.

D (Durability): Tính bền vững - Đã Commit là ghi đĩa vĩnh viễn, không sợ sập nguồn.

Quy tắc vàng khi code SqlTransaction:

Phải gọi connection.Open() trước khi connection.BeginTransaction().

Mọi SqlCommand tham gia phiên đều phải gán cmd.Transaction = transaction.

Chỉ gọi transaction.Commit() ở dòng lệnh cuối cùng của khối try.

Luôn bọc transaction.Rollback() trong khối catch.

Tiết 5: Khép kín luồng POS Checkout & Kịch bản Rollback

Xây dựng CheckoutController.cs giải quyết bài toán giao dịch POS liên hoàn:

[BẮT ĐẦU TRANSACTION]
  │
  ├── 1. Tạo hóa đơn trong bảng `Orders` (OUTPUT INSERTED.OrderId -> ExecuteScalar)
  │
  ├── 2. Lặp từng món hàng trong giỏ:
  │      ├─ Kiểm tra tồn kho (SELECT StockQuantity FROM Products)
  │      ├─ Nếu Stock < Request -> Throw Exception (Kích hoạt Rollback)
  │      ├─ Thêm dòng chi tiết vào `OrderDetails`
  │      └─ Cập nhật trừ tồn kho bảng `Products`
  │
  ├── 3. Cộng điểm thưởng cho khách hàng trong bảng `Customers`
  │
[COMMIT THÀNH CÔNG]  hoặc  [CATCH EXCEPTION -> ROLLBACK SẠCH SẼ]


4. HƯỚNG DẪN CÀI ĐẶT & CHẠY KIỂM THỬ

Bước 1: Khởi tạo Database & Stored Procedure

Mở SQL Server Management Studio (SSMS).

Đảm bảo CSDL MiniSupermarketDb đã tồn tại dữ liệu mẫu.

Chạy script tạo thủ tục sp_GetRevenueByCashier trong cơ sở dữ liệu MiniSupermarketDb.

Bước 2: Cấu hình chuỗi kết nối

Mở MiniSupermarket.API/appsettings.json, kiểm tra connection string:

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=MiniSupermarketDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}


(Nếu dùng SQL Express: Đổi Server=localhost thành Server=.\\SQLEXPRESS).

Bước 3: Khởi chạy dự án

Trong Visual Studio, chọn Startup Project là MiniSupermarket.API $\rightarrow$ Nhấn F5 hoặc Ctrl + F5.

Trình duyệt sẽ mở giao diện Swagger UI tại đường dẫn: https://localhost:7xxx/swagger/index.html.

5. KỊCH BẢN KIỂM THỬ API (SWAGGER UI)

Kịch bản 1: Quét mã vạch sản phẩm (Tiết 2)

Endpoint: GET /api/ado-products/barcode/893456789001

Kết quả mong đợi: HTTP 200 OK

{
  "productId": 1,
  "barcode": "893456789001",
  "productName": "Snack Khoai Tây O'Star Gói 60g",
  "price": 12500,
  "stockQuantity": 150
}


Kịch bản 2: Báo cáo doanh thu theo thu ngân (Tiết 3)

Endpoint: GET /api/Reports/revenue-by-cashier

Kết quả mong đợi: HTTP 200 OK

{
  "success": true,
  "totalCashiers": 5,
  "grandTotalRevenue": 4822000.00,
  "data": [
    {
      "cashierUsername": "cashier03",
      "totalOrders": 2,
      "totalRevenue": 1435000.00,
      "totalPointsAwarded": 143
    }
  ]
}


Kịch bản 3: Thanh toán POS thành công (Tiết 5 - Commit)

Endpoint: POST /api/Checkout

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


Kết quả mong đợi: HTTP 200 OK

Đối soát SSMS: Bảng Orders tăng 1 bản ghi, OrderDetails có 2 dòng, tồn kho SP 1 giảm 2 và SP 5 giảm 3.

Kịch bản 4: Mua hàng vượt tồn kho (Tiết 5 - Rollback)

Endpoint: POST /api/Checkout

Request Body (Đặt 999 đơn vị sản phẩm chỉ còn tồn kho 40):

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


Kết quả mong đợi: HTTP 400 Bad Request

{
  "success": false,
  "message": "Giao dịch thanh toán bị hủy và đã được Rollback an toàn!",
  "errorDetail": "Sản phẩm 'Bánh Quy Bơ Danisa Hộp Thiếc 454g' (ID: 2) không đủ tồn kho! (Tồn kho thực tế: 40, Số lượng yêu cầu mua: 999)"
}


Đối soát SSMS: Không có Order mới nào được tạo, sản phẩm ID 1 không hề bị trừ tồn kho.

6. CHECKLIST NGHIỆM THU THỰC HÀNH

[ ] 1. Kiến trúc cơ bản: Khởi tạo kết nối an toàn với using (SqlConnection ...), sử dụng SqlParameter ngăn ngừa SQL Injection.

[ ] 2. Phân biệt thực thi: Chọn chuẩn xác giữa ExecuteScalar(), ExecuteNonQuery() và ExecuteReader().

[ ] 3. Tra cứu nhanh: Endpoint /api/ado-products/barcode/{barcode} hoạt động ổn định, trả về dữ liệu nhanh chóng.

[ ] 4. Stored Procedure: Viết và chạy thành công sp_GetRevenueByCashier trên SSMS, map dữ liệu đúng qua ReportsController.

[ ] 5. Cơ chế ACID: Giải thích được 4 đặc tính ACID và cấu trúc bọc SqlTransaction.

[ ] 6. Giao dịch Checkout: API /api/checkout hoàn tất đủ 4 bước (Order, OrderDetails, trừ tồn kho, tích điểm).

[ ] 7. Kiểm chứng Rollback: Đảm bảo khi phát sinh lỗi quá tồn kho, toàn bộ dữ liệu trở về trạng thái toàn vẹn ban đầu.
