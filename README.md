# 🛒 HỆ THỐNG QUẢN LÝ SIÊU THỊ MINI (MINISUPERMARKET SYSTEM)

**Môn học:** Lập trình Ứng dụng .NET Core (Mã môn: 229162)  
**Buổi thực hành:** Buổi 5 - ADO.NET nâng cao, Stored Procedure và giao dịch POS Checkout

---

## 🏗️ 1. Mục tiêu thực hành

Buổi thực hành 5 tập trung nâng cao khả năng truy xuất dữ liệu SQL Server bằng ADO.NET và xây dựng các API phục vụ nghiệp vụ bán hàng tại quầy POS.

Các nội dung chính:

- Sử dụng ADO.NET để kết nối và truy vấn cơ sở dữ liệu SQL Server.
- Sử dụng `SqlConnection`, `SqlCommand`, `SqlParameter`, `SqlDataReader` và `DataTable`.
- Phân biệt và sử dụng các phương thức `ExecuteScalar()`, `ExecuteNonQuery()` và `ExecuteReader()`.
- Xây dựng API tra cứu sản phẩm theo mã vạch.
- Sử dụng `SqlDataAdapter` và `DataTable` để tải dữ liệu theo mô hình ngắt kết nối.
- Sử dụng Stored Procedure để tổng hợp và thống kê doanh thu theo nhân viên thu ngân.
- Sử dụng `SqlTransaction` để đảm bảo tính toàn vẹn dữ liệu trong quá trình thanh toán POS.

## 🛠️ 2. Công nghệ sử dụng

- **Ngôn ngữ lập trình:** C#
- **Nền tảng:** ASP.NET Core Web API, .NET 8.0
- **Truy xuất dữ liệu:** ADO.NET
- **Thư viện kết nối SQL Server:** `Microsoft.Data.SqlClient`
- **Cơ sở dữ liệu:** Microsoft SQL Server
- **Công cụ quản lý cơ sở dữ liệu:** SQL Server Management Studio (SSMS)
- **Công cụ kiểm thử API:** Swagger UI
- **Định dạng dữ liệu trao đổi:** JSON

## 📂 3. Cấu trúc Solution

```text
Solution 'MiniSupermarket'
│
├── MiniSupermarket.API/                    # Backend - ASP.NET Core Web API
│   │
│   ├── Controllers/
│   │   ├── AdoProductController.cs         # ADO.NET: Tra cứu mã vạch sản phẩm
│   │   ├── CheckoutController.cs           # API thanh toán POS bằng SqlTransaction
│   │   ├── ReportsController.cs             # API báo cáo doanh thu bằng Stored Procedure
│   │   ├── ProductsController.cs            # API quản lý sản phẩm
│   │   ├── OrdersController.cs              # API quản lý hóa đơn
│   │   └── ...                              # Các Controller khác
│   │
│   ├── Dto/
│   │   ├── ProductLookupDto.cs              # DTO tra cứu sản phẩm
│   │   ├── CheckoutDtos.cs                  # DTO thanh toán POS
│   │   ├── CashierRevenueDto.cs             # DTO báo cáo doanh thu thu ngân
│   │   ├── TopSellingProductDto.cs           # DTO thống kê sản phẩm bán chạy
│   │   └── ...
│   │
│   ├── Models/
│   │   ├── Product.cs                       # Mô hình sản phẩm
│   │   ├── Customer.cs                      # Mô hình khách hàng
│   │   ├── Order.cs                         # Mô hình hóa đơn
│   │   ├── OrderDetail.cs                   # Mô hình chi tiết hóa đơn
│   │   ├── Category.cs                      # Mô hình danh mục
│   │   └── ...                              # Các Model khác
│   │
│   ├── Data/                                # Thành phần dữ liệu
│   ├── Migrations/                          # Các Migration
│   ├── appsettings.json                     # Cấu hình kết nối cơ sở dữ liệu
│   ├── Program.cs                           # Khởi tạo và cấu hình Web API
│   └── ...
│
└── MiniSupermarket.WinForm/                 # Frontend - Windows Forms
    ├── FormPOS.cs                           # Giao diện bán hàng POS
    ├── FormQuickReport.cs                   # Giao diện báo cáo nhanh
    ├── FormProductManagement.cs             # Giao diện quản lý sản phẩm
    ├── FormCustomerManagement.cs            # Giao diện quản lý khách hàng
    ├── FormMainShell.cs                     # Giao diện chính
    ├── Program.cs                           # Điểm khởi chạy ứng dụng
    └── ...                                  # Các Form và thành phần khác
```

**Các thành phần trọng tâm của Buổi 5:**

- `AdoProductController.cs`: Thực hành truy vấn SQL Server bằng ADO.NET và tra cứu sản phẩm theo mã vạch.
- `ProductLookupDto.cs`: Định nghĩa dữ liệu trả về khi tra cứu sản phẩm.
- `ReportsController.cs`: Thực hiện báo cáo doanh thu thông qua Stored Procedure.
- `CashierRevenueDto.cs`: Định nghĩa cấu trúc dữ liệu báo cáo doanh thu theo thu ngân.
- `CheckoutController.cs`: Xử lý thanh toán POS và sử dụng `SqlTransaction` để đảm bảo tính toàn vẹn dữ liệu.
- `CheckoutDtos.cs`: Định nghĩa dữ liệu đầu vào cho nghiệp vụ thanh toán.
- `Order.cs`, `OrderDetail.cs`, `Product.cs`, `Customer.cs`: Các mô hình dữ liệu liên quan đến nghiệp vụ bán hàng.

## 🔌 4. Nội dung thực hành

### 4.1. Truy xuất dữ liệu bằng ADO.NET

Tìm hiểu và sử dụng các thành phần cốt lõi của ADO.NET để kết nối ứng dụng C# với SQL Server.

Các nội dung đã được đưa vào bài thực hành:

- `SqlConnection`: Thiết lập kết nối đến cơ sở dữ liệu.
- `SqlCommand`: Thực thi câu lệnh SQL hoặc Stored Procedure.
- `SqlParameter`: Truyền tham số vào câu lệnh SQL, hạn chế nguy cơ SQL Injection.
- `SqlDataReader`: Đọc dữ liệu tuần tự theo mô hình kết nối.
- `DataTable`: Lưu trữ dữ liệu trong bộ nhớ để xử lý theo mô hình ngắt kết nối.
- `ExecuteScalar()`: Lấy một giá trị đơn.
- `ExecuteNonQuery()`: Thực thi các câu lệnh thêm, sửa và xóa.
- `ExecuteReader()`: Đọc tập kết quả truy vấn.

Các đối tượng kết nối và đọc dữ liệu được quản lý bằng `using` để tự động giải phóng tài nguyên khi hoàn thành công việc.

### 4.2. Xây dựng API tra cứu mã vạch sản phẩm

Tệp xử lý: `AdoProductController.cs`

API sử dụng ADO.NET để truy vấn thông tin sản phẩm trực tiếp từ SQL Server.

Các chức năng chính:

- Tra cứu sản phẩm bằng mã vạch.
- Sử dụng `SqlDataReader` để đọc thông tin sản phẩm.
- Truyền mã vạch thông qua tham số SQL.
- Trả về thông tin sản phẩm theo định dạng JSON.
- Xử lý trường hợp mã vạch không hợp lệ hoặc không tìm thấy sản phẩm.
- Sử dụng `SqlDataAdapter` và `DataTable` để tải danh sách sản phẩm theo mô hình ngắt kết nối.

### 4.3. Xây dựng API báo cáo doanh thu

Tệp xử lý: `ReportsController.cs`

Sử dụng Stored Procedure kết hợp ADO.NET để tổng hợp dữ liệu doanh thu theo nhân viên thu ngân.

Nội dung thực hiện:

- Xây dựng Stored Procedure `sp_GetRevenueByCashier` trên SQL Server.
- Sử dụng `COUNT()` để thống kê số lượng hóa đơn.
- Sử dụng `SUM()` để tính tổng doanh thu và điểm thưởng.
- Sử dụng `GROUP BY` để nhóm dữ liệu theo tài khoản thu ngân.
- Sử dụng `ORDER BY` để sắp xếp kết quả theo doanh thu.
- Gọi Stored Procedure bằng `SqlCommand` với `CommandType.StoredProcedure`.
- Ánh xạ dữ liệu truy vấn vào `CashierRevenueDto`.
- Trả về dữ liệu thống kê dưới dạng JSON.

### 4.4. Xây dựng API thanh toán POS

Tệp xử lý: `CheckoutController.cs`

Sử dụng ADO.NET và `SqlTransaction` để xử lý quy trình thanh toán hóa đơn bán lẻ.

Các bước nghiệp vụ:

1. Tiếp nhận thông tin thanh toán và danh sách sản phẩm.
2. Tạo hóa đơn trong bảng `Orders`.
3. Lưu chi tiết hóa đơn vào bảng `OrderDetails`.
4. Cập nhật số lượng tồn kho trong bảng `Products`.
5. Cập nhật điểm thưởng cho khách hàng trong bảng `Customers`.
6. Xác nhận giao dịch khi các thao tác thành công.
7. Hoàn tác giao dịch khi phát sinh lỗi để bảo vệ tính toàn vẹn dữ liệu.

Sử dụng `CheckoutDtos.cs` để định nghĩa dữ liệu đầu vào phục vụ nghiệp vụ thanh toán.

### 4.5. Đảm bảo tính toàn vẹn dữ liệu với SqlTransaction

Tìm hiểu cơ chế giao dịch trong SQL Server và nguyên tắc ACID.

- **Atomicity:** Các thao tác trong giao dịch được thực hiện toàn bộ hoặc hoàn tác.
- **Consistency:** Dữ liệu phải duy trì tính hợp lệ sau giao dịch.
- **Isolation:** Các giao dịch được kiểm soát để hạn chế ảnh hưởng lẫn nhau.
- **Durability:** Dữ liệu đã được xác nhận giao dịch được lưu bền vững.

Sử dụng `Commit()` để xác nhận giao dịch thành công và `Rollback()` để hoàn tác khi xảy ra lỗi.

## 🧪 5. Hướng dẫn chạy và kiểm thử API

### Bước 1: Khởi chạy Web API

1. Mở solution `MiniSupermarket` bằng Visual Studio.
2. Chọn `MiniSupermarket.API` làm Startup Project.
3. Kiểm tra chuỗi kết nối SQL Server trong `appsettings.json`.
4. Đảm bảo SQL Server đang hoạt động và cơ sở dữ liệu đã được chuẩn bị.
5. Nhấn `F5` hoặc `Ctrl + F5` để chạy API.
6. Mở Swagger UI để kiểm thử các endpoint.

### Bước 2: Kiểm thử API tra cứu mã vạch

Endpoint:

```http
GET /api/ado-products/barcode/{barcode}
```

Ví dụ:

```http
GET /api/ado-products/barcode/893456789001
```

Kết quả mong đợi:

- `200 OK`: Trả về thông tin sản phẩm tìm thấy.
- `400 Bad Request`: Tham số mã vạch không hợp lệ.
- `404 Not Found`: Không tìm thấy sản phẩm tương ứng.

### Bước 3: Kiểm thử tải danh sách sản phẩm

Endpoint:

```http
GET /api/ado-products/disconnected-cache
```

Kiểm tra danh sách sản phẩm được tải bằng `SqlDataAdapter`, lưu trong `DataTable` và trả về dưới dạng JSON.

### Bước 4: Kiểm thử báo cáo doanh thu

Endpoint:

```http
GET /api/Reports/revenue-by-cashier
```

Kiểm tra kết quả thống kê doanh thu, số lượng hóa đơn và điểm thưởng của từng nhân viên thu ngân.

Trước khi kiểm thử, cần tạo Stored Procedure `sp_GetRevenueByCashier` trong đúng cơ sở dữ liệu SQL Server.

### Bước 5: Kiểm thử thanh toán POS

Endpoint:

```http
POST /api/Checkout
```

Sử dụng Swagger UI để gửi thông tin thu ngân, khách hàng và danh sách sản phẩm cần thanh toán.

Kiểm tra hai tình huống:

- **Thanh toán thành công:** Hóa đơn, chi tiết hóa đơn, tồn kho và điểm thưởng được cập nhật theo nghiệp vụ.
- **Thanh toán thất bại:** Giao dịch được hoàn tác khi phát sinh lỗi, chẳng hạn số lượng mua vượt quá tồn kho.

Có thể sử dụng SQL Server Management Studio để đối chiếu dữ liệu trước và sau khi thực hiện giao dịch.

*Lưu ý: Các endpoint và tình huống trên là hướng dẫn kiểm thử theo nội dung thực hành; cần chạy thử trên môi trường thực tế để xác nhận kết quả.*

## 👨‍💻 6. Thông tin sinh viên

- **Họ tên sinh viên:** Nguyễn Quốc Vương
- **Mã sinh viên:** 2124110272
- **Lớp học phần:** CCQ2411D
