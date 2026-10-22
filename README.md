# Hệ Thống Quản Lý Bán Lẻ & Quầy POS Siêu Thị Nông Sản Hữu Cơ (MiniSupermarket)

> **Giải pháp kiến trúc kết hợp giữa ASP.NET Core Web API và Windows Forms Desktop App, tích hợp sâu ADO.NET thuần, T-SQL Stored Procedure và điều khiển giao dịch nguyên tử đa bảng với `SqlTransaction` theo chuẩn ACID.**

## 📌 1. Giới thiệu tổng quan

Dự án **MiniSupermarket** được xây dựng phục vụ mô hình vận hành chuỗi siêu thị bán lẻ nông sản sạch và thực phẩm hữu cơ (**Organic Farm**). Hệ thống giải quyết các bài toán trọng tâm trong kỹ thuật phần mềm doanh nghiệp:

* **Hiệu năng cao tầng thấp (High Performance):** Ứng dụng **ADO.NET thuần** kết hợp `SqlDataReader` (Forward-only, Connected Mode) để tra cứu mã vạch sản phẩm tại quầy POS với độ trễ phản hồi cực thấp (< 100ms), tránh chi phí tài nguyên (overhead) của ORM trong giờ cao điểm.

* **Bộ đệm dữ liệu ngoại tuyến (Disconnected Cache):** Sử dụng `DataTable` và `SqlDataAdapter` để tải danh mục hàng hóa lưu đệm trực tiếp lên bộ nhớ RAM, phục vụ hiển thị lưới giao diện kể cả khi ngắt kết nối.

* **Tối ưu báo cáo tổng hợp:** Đóng gói logic thống kê ca bán lẻ phức tạp vào **Stored Procedure** (`sp_GetRevenueByCashier`) trên SQL Server, giải phóng băng thông mạng và tận dụng kế hoạch thực thi biên dịch sẵn (Compiled Execution Plan).

* **An toàn giao dịch đa bảng (ACID Compliance):** Toàn bộ quy trình thanh toán hóa đơn (`Orders` $\rightarrow$ `OrderDetails` $\rightarrow$ trừ tồn kho `Products` $\rightarrow$ tích điểm `Customers`) được bọc kín trong `SqlTransaction`. Cơ chế đảm bảo nguyên tắc: **Hoàn tất 100% (Commit) hoặc khôi phục nguyên trạng (Rollback), triệt tiêu hoàn toàn rác dữ liệu**.

* **Bảo mật hệ thống:** Sử dụng 100% `SqlParameter` cho toàn bộ tham số đầu vào, triệt tiêu nguy cơ tấn công **SQL Injection**.

## 📂 2. Cấu trúc Solution & Cây thư mục dự án

Hệ thống được tổ chức theo kiến trúc phân tách rõ ràng giữa Backend API và Desktop Client:

```
MiniSuperMarket.sln
│
├── 📁 MiniSupermarket.API                     # Backend Web API (.NET Core)
│   ├── 📁 Connected Services
│   ├── 📁 Dependencies
│   ├── 📁 Properties
│   ├── 📁 Controllers                         # Bộ điều hướng xử lý nghiệp vụ
│   │   ├── AdoProductController.cs           # Tra cứu Barcode & DataTable Cache (ADO.NET)
│   │   ├── AuthController.cs                 # Xác thực & Phân quyền đăng nhập
│   │   ├── CategoriesController.cs           # Quản lý danh mục hàng
│   │   ├── CheckoutController.cs             # Thanh toán POS bọc SqlTransaction (ACID)
│   │   ├── CustomerController.cs             # Quản lý thông tin & điểm thưởng khách hàng
│   │   ├── OrdersController.cs               # Quản lý lịch sử hóa đơn bán buôn/lẻ
│   │   ├── ProductsController.cs             # CRUD danh mục sản phẩm (EF Core)
│   │   ├── ReportsController.cs              # Gọi Stored Procedure xuất báo cáo doanh thu
│   │   ├── RolesController.cs                # Quản lý nhóm vai trò
│   │   ├── UsersController.cs                # Quản lý tài khoản nhân viên / thu ngân
│   │   └── WeatherForecastController.cs
│   ├── 📁 Data
│   │   └── SupermarketDbContext.cs          # EF Core DbContext & Seed Data (Organic Farm)
│   ├── 📁 Dto                                # Data Transfer Objects
│   │   ├── CashierRevenueDto.cs              # DTO nhận kết quả từ sp_GetRevenueByCashier
│   │   ├── CheckoutDtos.cs                   # CartItem & CheckoutRequest DTO
│   │   ├── ProductLookupDto.cs               # DTO tra cứu nhanh sản phẩm POS
│   │   └── TopSellingProductDto.cs           # DTO báo cáo mặt hàng bán chạy
│   ├── 📁 Migrations                         # Lịch sử cập nhật Schema CSDL
│   ├── 📁 Models                             # Định nghĩa Entities thực thể CSDL
│   ├── appsettings.json                      # Cấu hình ConnectionString & Service
│   ├── MiniSupermarket.API.http              # HTTP Client Test Script
│   └── Program.cs                            # Pipeline khởi tạo Web Host & DI Container
│
└── 📁 MiniSupermarket.WinForm                 # Desktop Client giao diện quầy POS (WinForms)
    ├── 📁 Dependencies
    ├── Form1.cs
    ├── FormCategoryManagement.cs             # Màn hình quản lý danh mục
    ├── FormCustomerManagement.cs             # Màn hình chăm sóc khách hàng & tích điểm
    ├── FormLogin.cs                          # Đăng nhập hệ thống thu ngân
    ├── FormMainShell.cs                      # Giao diện khung điều hướng chính
    ├── FormPOS.cs                            # Giao diện màn hình thanh toán quầy thu ngân
    ├── FormProductManagement.cs              # Màn hình quản lý kho & sản phẩm
    ├── FormQuickReport.cs                    # Màn hình xem báo cáo nhanh doanh thu ca
    ├── FormRoleManagement.cs                 # Quản lý phân quyền chức vụ
    ├── FormUserManagement.cs                 # Quản lý danh sách nhân viên
    ├── SessionManager.cs                     # Quản lý phiên làm việc & tài khoản hiện hành
    └── Program.cs                            # Điểm khởi chạy ứng dụng Desktop

```

## 🗄️ 3. Cơ sở dữ liệu mẫu (Organic Farm Seed Data)

Hệ thống được khởi tạo sẵn với **15 danh mục nông sản hữu cơ** và **15 sản phẩm đại diện** có mã vạch (Barcode EAN-13 chuẩn):

### Danh mục & Sản phẩm đại diện

| ID | Mã vạch (Barcode) | Tên sản phẩm | Danh mục | Đơn giá | Đơn vị | Tồn kho | 
| ----- | ----- | ----- | ----- | ----- | ----- | ----- | 
| **101** | `8930000001010` | Cải bó xôi hữu cơ Farm | Rau lá hữu cơ Farm | 35.000 đ | Bó 500g | 80 | 
| **201** | `8930000002017` | Cà rốt giống củ bản địa | Củ quả tươi sạch | 38.000 đ | Kg | 100 | 
| **301** | `8930000003014` | Ổi lê hữu cơ chín cây | Trái cây vườn sinh thái | 45.000 đ | Kg | 60 | 
| **401** | `8930000004011` | Nấm đùi gà tươi Organic | Nấm tươi & Nấm ăn hữu cơ | 42.000 đ | Gói 250g | 50 | 
| **501** | `8930000005018` | Thịt ba chỉ heo thảo mộc | Thịt sạch chăn thả tự nhiên | 165.000 đ | Khay 500g | 40 | 
| **601** | `8930000006015` | Cá lăng sinh thái sông Đà | Thủy hải sản sinh thái | 195.000 đ | Kg | 30 | 
| **701** | `8930000007012` | Trứng gà ta thảo mộc tự nhiên | Trứng gia cầm thả vườn | 48.000 đ | Hộp 10 quả | 120 | 
| **801** | `8930000008019` | Sữa bò tươi thanh trùng Organic | Sữa tươi thanh trùng & Sữa hạt | 52.000 đ | Chai 900ml | 45 | 
| **901** | `8930000009016` | Gạo ST25 hữu cơ ruộng rươi | Gạo sạch & Nông sản ngũ cốc | 125.000 đ | Túi 2kg | 70 | 
| **1001** | `8930000010013` | Mật ong hoa rừng nguyên chất | Mật ong rừng & Dược liệu vườn | 220.000 đ | Hũ 500ml | 35 | 
| **1101** | `8930000011010` | Nước mắm truyền thống cá cơm 40N | Gia vị & Nước mắm thủ công | 95.000 đ | Chai 500ml | 50 | 
| **1201** | `8930000012017` | Dầu mè vàng ép lạnh thủ công | Dầu ép lạnh nguyên chất | 110.000 đ | Chai 250ml | 40 | 
| **1301** | `8930000013014` | Trà hoa cúc sấy lạnh tự nhiên | Rau củ sấy & Trà thảo mộc Farm | 85.000 đ | Hũ 80g | 55 | 
| **1401** | `8930000014011` | Dưa cải củ lên men tự nhiên | Thực phẩm lên men tự nhiên | 32.000 đ | Hũ 450g | 60 | 
| **1501** | `8930000015018` | Combo Rau Củ Xanh Dinh Dưỡng Tuần | Combo Nông Sản Tuần | 250.000 đ | Set 5kg | 20 | 

## ⚙️ 4. Hướng dẫn thiết lập & Khởi chạy dự án

### 4.1. Yêu cầu môi trường

* **Hệ điều hành:** Windows 10/11

* **IDE:** Visual Studio 2022 (phiên bản 17.4 trở lên) có cài đặt workload **ASP.NET and web development** và **.NET desktop development**.

* **SDK:** .NET Core 6.0 / 8.0 SDK

* **Hệ quản trị CSDL:** Microsoft SQL Server 2019/2022 hoặc SQL Server Express.

### 4.2. Cấu hình chuỗi kết nối (`appsettings.json`)

Kiểm tra tệp `MiniSupermarket.API/appsettings.json`, chỉnh sửa chuỗi kết nối cho phù hợp với máy trạm:

```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=MiniSupermarketDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}

```

*(Nếu sử dụng SQL Express, thay `localhost` bằng `.\\SQLEXPRESS`).*

### 4.3. Tạo Stored Procedure trên SQL Server

Mở **SQL Server Management Studio (SSMS)**, chọn CSDL `MiniSupermarketDb` và thực thi kịch bản tạo thủ tục báo cáo:

```
USE MiniSupermarketDb;
GO

IF OBJECT_ID('sp_GetRevenueByCashier', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetRevenueByCashier;
GO

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

```

## 🧪 5. Hướng dẫn kiểm tra và đối soát hệ thống (Test Guide)

Khởi chạy đồng thời hoặc độc lập dự án Web API từ Visual Studio (**F5** hoặc **Ctrl + F5**). Trình duyệt sẽ mở giao diện Swagger UI tại đường dẫn: `https://localhost:7xxx/swagger/index.html`.

### Cách 1: Kiểm thử trực tiếp qua Swagger UI

#### 1. Tra cứu thông tin sản phẩm bằng Barcode (Connected Mode - `SqlDataReader`)

* **Endpoint:** `GET /api/ado-products/barcode/8930000001010`

* **Mô tả:** Quét mã vạch nông sản **Cải bó xôi hữu cơ Farm** (ID: 101, Giá: 35.000đ).

* **Kết quả kỳ vọng (Status 200 OK):**

```
{
  "productId": 101,
  "barcode": "8930000001010",
  "productName": "Cải bó xôi hữu cơ Farm",
  "price": 35000,
  "stockQuantity": 80
}

```

#### 2. Tải danh sách bộ đệm ngoại tuyến (Disconnected Mode - `DataTable` & `SqlDataAdapter`)

* **Endpoint:** `GET /api/ado-products/disconnected-cache`

* **Mô tả:** Lấy toàn bộ 15 sản phẩm đại diện đã nạp vào bộ nhớ RAM ngắt kết nối.

* **Kết quả kỳ vọng (Status 200 OK):**

```
{
  "totalLoaded": 15,
  "cachedAt": "2026-10-22 15:30:00",
  "data": [
    {
      "productId": 101,
      "barcode": "8930000001010",
      "productName": "Cải bó xôi hữu cơ Farm",
      "price": 35000,
      "stockQuantity": 80
    },
    {
      "productId": 201,
      "barcode": "8930000002017",
      "productName": "Cà rốt giống củ bản địa",
      "price": 38000,
      "stockQuantity": 100
    }
  ]
}

```

#### 3. Báo cáo doanh thu ca theo thu ngân (`Stored Procedure`)

* **Endpoint:** `GET /api/reports/revenue-by-cashier`

* **Mô tả:** Thực thi thủ tục lưu trữ `sp_GetRevenueByCashier` để tổng hợp doanh thu theo 5 tài khoản thu ngân (`cashier01` $\rightarrow$ `cashier05`) từ 15 hóa đơn bán hàng có sẵn.

* **Kết quả kỳ vọng (Status 200 OK):**

```
{
  "success": true,
  "generatedAt": "2026-10-22 15:31:10",
  "totalCashiers": 5,
  "grandTotalRevenue": 4822000.00,
  "data": [
    {
      "cashierUsername": "cashier03",
      "totalOrders": 2,
      "totalRevenue": 1435000.00,
      "totalPointsAwarded": 143
    },
    {
      "cashierUsername": "cashier05",
      "totalOrders": 3,
      "totalRevenue": 1328000.00,
      "totalPointsAwarded": 132
    },
    {
      "cashierUsername": "cashier01",
      "totalOrders": 4,
      "totalRevenue": 1322000.00,
      "totalPointsAwarded": 131
    },
    {
      "cashierUsername": "cashier04",
      "totalOrders": 3,
      "totalRevenue": 484000.00,
      "totalPointsAwarded": 48
    },
    {
      "cashierUsername": "cashier02",
      "totalOrders": 3,
      "totalRevenue": 253000.00,
      "totalPointsAwarded": 24
    }
  ]
}

```

#### 4. Kịch bản thanh toán thành công (Transaction Commit)

* **Endpoint:** `POST /api/checkout`

* **Mô tả:** Mua 2 bó **Cải bó xôi hữu cơ Farm** (ID: 101, tồn kho ban đầu 80) và 1 kg **Cá lăng sinh thái sông Đà** (ID: 601, tồn kho ban đầu 30).

* **Request Body:**

```
{
  "cashierUsername": "cashier01",
  "customerId": 1,
  "items": [
    { "productId": 101, "quantity": 2, "unitPrice": 35000 },
    { "productId": 601, "quantity": 1, "unitPrice": 195000 }
  ]
}

```

* **Kết quả kỳ vọng (Status 200 OK):**

  * Hệ thống tính tổng tiền $2 \times 35.000 + 1 \times 195.000 = 265.000$ đ, tích $26$ điểm thưởng.

  * Tồn kho SP 101 giảm còn **78**, SP 601 giảm còn **29**.

  * Điểm thưởng khách hàng ID 1 được cộng dồn.

#### 5. Kịch bản thiếu kho - Kích hoạt hoàn tác (Transaction Rollback)

* **Endpoint:** `POST /api/checkout`

* **Mô tả:** Mua 1 bó **Cải bó xôi hữu cơ Farm** (ID: 101) và cố tình mua **999 kg** **Cà rốt giống củ bản địa** (ID: 201, tồn kho chỉ có 100).

* **Request Body:**

```
{
  "cashierUsername": "cashier01",
  "customerId": 1,
  "items": [
    { "productId": 101, "quantity": 1, "unitPrice": 35000 },
    { "productId": 201, "quantity": 999, "unitPrice": 38000 }
  ]
}

```

* **Kết quả kỳ vọng (Status 400 Bad Request):**

```
{
  "success": false,
  "message": "Giao dịch thanh toán bị hủy và đã được Rollback an toàn!",
  "errorDetail": "Sản phẩm 'Cà rốt giống củ bản địa' (ID: 201) không đủ tồn kho! (Tồn kho thực tế: 100, Số lượng yêu cầu mua: 999)"
}

```

* **Đối soát thực tế trên SQL Server (Chứng minh chuẩn ACID):**

  * Tồn kho của sản phẩm ID 101 **vẫn giữ nguyên 78**, không bị trừ 1 bó dù đứng trước sản phẩm lỗi.

  * Không có bản ghi hóa đơn rác nào được chèn vào bảng `Orders` hay `OrderDetails`.

### Cách 2: Kiểm thử trên ứng dụng máy trạm Windows Forms (`MiniSupermarket.WinForm`)

1. Đặt startup project là **MiniSupermarket.WinForm** (hoặc cấu hình Multiple Startup Projects chạy cả API và WinForm).

2. Đăng nhập bằng tài khoản thu ngân (ví dụ: `cashier01`).

3. Mở màn hình **FormPOS**:

   * Nhập mã vạch `8930000001010` và nhấn **Enter** $\rightarrow$ Dữ liệu sản phẩm Cải bó xôi hữu cơ hiển thị lập tức lên danh sách mua hàng.

   * Thử nghiệm bấm nút **Thanh toán** để kích hoạt luồng gọi Web API transaction khép kín.

4. Mở màn hình **FormQuickReport** để xem dữ liệu doanh thu tự động load từ Stored Procedure.

## 🛡️ 6. Tổng kết kiến thức kỹ thuật đạt được

* \[x\] **ADO.NET Cơ bản:** Quản lý vòng đời kết nối với `using(SqlConnection)`, phân định chính xác `ExecuteScalar()`, `ExecuteNonQuery()`, `ExecuteReader()`.

* \[x\] **Chống SQL Injection:** Tuyệt đối không cộng chuỗi SQL, tham số hóa toàn diện với `SqlParameter`.

* \[x\] **Connected vs Disconnected:** Nắm vững sự khác biệt giữa stream đọc nhanh một chiều `SqlDataReader` và bảng dữ liệu RAM ngoại tuyến `DataTable`/`SqlDataAdapter`.

* \[x\] **Tối ưu Stored Procedure:** Giảm phân tích cú pháp SQL lặp lại, tối ưu băng thông và bảo mật tầng dữ liệu.

* \[x\] **Toàn vẹn giao dịch ACID:** Kiểm soát luồng ghi dữ liệu phức tạp nhiều bảng bằng `SqlTransaction` (`BeginTransaction`, `Commit`, `Rollback`).