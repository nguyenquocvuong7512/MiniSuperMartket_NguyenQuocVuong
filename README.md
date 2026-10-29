# 🛒 HỆ THỐNG QUẢN LÝ SIÊU THỊ MINI (MINISUPERMARKET SYSTEM)

**Môn học:** Lập trình Ứng dụng .NET Core (Mã môn: 229162)  
**Buổi thực hành:** Buổi 4 – Thiết kế kiến trúc giao diện WinForms Shell và điều hướng phân quyền người dùng.

---

## 🏗️ 1. Mô hình kiến trúc hệ thống (Client – Server)

Dự án được xây dựng theo mô hình Client – Server, gồm hai thành phần chính:

- **MiniSupermarket.API (Backend):** Ứng dụng ASP.NET Core Web API chịu trách nhiệm xử lý nghiệp vụ, quản lý dữ liệu và cung cấp API cho ứng dụng Client.
- **MiniSupermarket.WinForm (Frontend Client):** Ứng dụng Windows Forms cung cấp giao diện quản lý siêu thị, hỗ trợ điều hướng giữa các màn hình nghiệp vụ và tổ chức giao diện theo vai trò người dùng.

Trong buổi thực hành 4, trọng tâm là xây dựng kiến trúc giao diện trung tâm `FormMainShell`, tổ chức thanh điều hướng Sidebar, vùng hiển thị nội dung và cơ chế phân quyền giao diện theo ba vai trò: Admin, Cashier và Warehouse.

## 🛠️ 2. Công nghệ sử dụng

- **Ngôn ngữ lập trình:** C#
- **Nền tảng:** .NET 8.0
- **Backend:** ASP.NET Core Web API
- **Frontend:** Windows Forms
- **Giao tiếp Client – Server:** HTTP API
- **Thành phần giao diện:** Form, Panel, Button, Label, DataGridView
- **Kỹ thuật điều hướng:** Nhúng Form con vào Panel trung tâm
- **Kỹ thuật phân quyền:** Role-Based Access Control (RBAC)

## 📂 3. Cấu trúc Solution

Cấu trúc dưới đây được tổng hợp từ các thư mục và tệp hiển thị trong Solution Explorer.

```text
MiniSupermarket.API/
│
├── Controllers/
│   ├── CustomerController.cs
│   ├── OrdersController.cs
│   ├── ProductsController.cs
│   ├── RolesController.cs
│   ├── UsersController.cs
│   └── WeatherForecastController.cs
│
├── Data/
│
├── Migrations/
│
├── Models/
│
├── appsettings.json
├── MiniSupermarket.API.http
├── Program.cs
└── WeatherForecast.cs
│
└── MiniSupermarket.WinForm/
    │
    ├── Dependencies/
    ├── Form1.cs
    ├── FormCategoryManagement.cs
    ├── FormCustomerManagement.cs
    ├── FormLogin.cs
    ├── FormMainShell.cs
    ├── FormPOS.cs
    ├── FormProductManagement.cs
    ├── FormQuickReport.cs
    ├── FormRoleManagement.cs
    ├── FormUserManagement.cs
    ├── Program.cs
    └── SessionManager.cs
```

**Chức năng các thành phần chính:**

### Backend – MiniSupermarket.API

| Thành phần | Chức năng |
|---|---|
| `Controllers/` | Chứa các Controller xử lý những nhóm chức năng của API. |
| `CustomerController.cs` | Xử lý các yêu cầu liên quan đến khách hàng. |
| `OrdersController.cs` | Xử lý các yêu cầu liên quan đến đơn hàng. |
| `ProductsController.cs` | Xử lý các yêu cầu liên quan đến sản phẩm. |
| `RolesController.cs` | Xử lý các yêu cầu liên quan đến vai trò. |
| `UsersController.cs` | Xử lý các yêu cầu liên quan đến tài khoản người dùng. |
| `Data/` | Chứa các thành phần làm việc với dữ liệu. |
| `Migrations/` | Chứa các tệp Migration của Entity Framework Core. |
| `Models/` | Chứa các lớp mô hình dữ liệu. |
| `Program.cs` | Điểm khởi chạy và cấu hình ứng dụng API. |
| `appsettings.json` | Chứa các cấu hình của ứng dụng. |

### Frontend – MiniSupermarket.WinForm

| Tệp | Chức năng |
|---|---|
| `FormMainShell.cs` | Giao diện khung chính, tổ chức Sidebar, Header và vùng hiển thị nội dung. |
| `FormLogin.cs` | Giao diện đăng nhập người dùng. |
| `SessionManager.cs` | Quản lý thông tin phiên đăng nhập và vai trò người dùng. |
| `FormCategoryManagement.cs` | Giao diện quản lý danh mục nhóm hàng. |
| `FormProductManagement.cs` | Giao diện quản lý sản phẩm. |
| `FormCustomerManagement.cs` | Giao diện quản lý khách hàng. |
| `FormPOS.cs` | Giao diện quầy bán hàng POS. |
| `FormQuickReport.cs` | Giao diện báo cáo nhanh. |
| `FormUserManagement.cs` | Giao diện quản lý tài khoản người dùng. |
| `FormRoleManagement.cs` | Giao diện quản lý vai trò. |
| `Form1.cs` | Form Windows Forms bổ sung trong dự án. |
| `Program.cs` | Điểm khởi chạy ứng dụng Windows Forms. |

*Lưu ý: Cây thư mục thể hiện các thành phần nhìn thấy trong Solution Explorer; không đại diện cho toàn bộ tệp bên trong từng thư mục.*

## 🖥️ 4. Nội dung thực hành

### 4.1. Thiết kế giao diện khung chính FormMainShell

Xây dựng giao diện điều khiển trung tâm theo kiến trúc Single-Form, gồm ba khu vực:

- **Sidebar:** Thanh điều hướng bên trái, chứa logo MiniMart POS và các nút chức năng.
- **Top Header:** Thanh tiêu đề, hiển thị tên màn hình và thông tin người dùng.
- **Main Content:** Vùng nội dung trung tâm dùng để hiển thị các màn hình nghiệp vụ.

Các chức năng điều hướng chính gồm:

- Bán hàng (POS).
- Quản lý danh mục.
- Quản lý sản phẩm.
- Quản lý khách hàng.
- Báo cáo doanh thu.
- Quản trị tài khoản.
- Đăng xuất.

### 4.2. Nhúng Form con vào vùng nội dung trung tâm

Tổ chức các màn hình nghiệp vụ trong `panelMainContent`, hạn chế việc mở nhiều cửa sổ độc lập.

Các thuộc tính quan trọng khi nhúng Form con:

- `TopLevel = false`: Chuyển Form con thành thành phần có thể nhúng vào Form cha.
- `FormBorderStyle = None`: Loại bỏ đường viền cửa sổ.
- `Dock = Fill`: Cho Form con lấp đầy vùng nội dung.
- `Controls.Clear()`: Xóa các điều khiển cũ trong vùng hiển thị trước khi nạp màn hình mới.
- `BringToFront()` và `Show()`: Đưa Form con lên trước và hiển thị giao diện.

### 4.3. Điều hướng và làm nổi bật chức năng đang chọn

Khi người dùng chọn một chức năng trên Sidebar:

- Mở Form nghiệp vụ tương ứng trong vùng nội dung trung tâm.
- Cập nhật tiêu đề màn hình trên Header.
- Đổi màu nút chức năng đang được chọn để người dùng dễ nhận biết.
- Đóng Form con đang hoạt động trước khi chuyển sang Form khác.

### 4.4. Phân quyền giao diện theo vai trò người dùng

Sử dụng cơ chế Role-Based Access Control (RBAC) để điều chỉnh các chức năng hiển thị theo vai trò đăng nhập.

| Vai trò | Chức năng được phép truy cập |
|---|---|
| **Admin** | Bán hàng POS, quản lý danh mục, sản phẩm, khách hàng, báo cáo doanh thu và tài khoản người dùng. |
| **Cashier** | Bán hàng POS và quản lý khách hàng. |
| **Warehouse** | Quản lý danh mục và sản phẩm, phục vụ công việc kho hàng. |

Cơ chế phân quyền giao diện sử dụng thuộc tính `Visible` của các nút chức năng để ẩn hoặc hiện menu theo vai trò.

Đối với vai trò không hợp lệ, ứng dụng hiển thị cảnh báo và đóng giao diện khung chính.

### 4.5. Hiển thị thông tin phiên đăng nhập

Sử dụng `SessionManager` để truy xuất thông tin phiên đăng nhập, bao gồm:

- Tên tài khoản đang đăng nhập.
- Vai trò hiện tại của người dùng.
- Thông tin phiên phục vụ điều hướng và phân quyền giao diện.

Thông tin người dùng được hiển thị trên Header của `FormMainShell`.

### 4.6. Xử lý chức năng đăng xuất

Tổ chức chức năng đăng xuất trên Sidebar:

- Hiển thị hộp thoại xác nhận đăng xuất.
- Xóa thông tin phiên đăng nhập hiện tại.
- Ẩn giao diện khung chính.
- Mở lại màn hình đăng nhập.
- Đóng giao diện khung chính sau khi hoàn tất quá trình chuyển màn hình.

## 🚀 5. Hướng dẫn chạy và kiểm thử

### Bước 1: Khởi chạy Backend

1. Mở Solution bằng Visual Studio.
2. Chọn `MiniSupermarket.API` làm Startup Project.
3. Nhấn `F5` để chạy ứng dụng API.
4. Kiểm tra địa chỉ và cổng API đang hoạt động.

### Bước 2: Khởi chạy WinForms Client

1. Đảm bảo địa chỉ API được cấu hình chính xác trong ứng dụng WinForms.
2. Chọn `MiniSupermarket.WinForm` làm Startup Project.
3. Nhấn `F5` để chạy ứng dụng.
4. Đăng nhập bằng tài khoản phù hợp với vai trò cần kiểm thử.

### Bước 3: Kiểm thử phân quyền giao diện

Có thể sử dụng bộ tài khoản mẫu trong tài liệu thực hành để kiểm tra:

| Tài khoản mẫu | Vai trò | Kết quả mong đợi |
|---|---|---|
| `admin01` | Admin | Hiển thị đầy đủ các chức năng. |
| `cashier01` | Cashier | Chỉ hiển thị Bán hàng POS và Khách hàng. |
| `ware01` | Warehouse | Chỉ hiển thị Quản lý danh mục và Quản lý sản phẩm. |

Mật khẩu mẫu theo tài liệu thực hành: `123456`.

*Các tài khoản trên là dữ liệu kiểm thử được cung cấp trong tài liệu; cần bảo đảm chúng đã được tạo trong hệ thống trước khi đăng nhập.*

### Bước 4: Kiểm thử điều hướng

- Chọn từng nút trên Sidebar.
- Kiểm tra Form nghiệp vụ được hiển thị trong vùng nội dung trung tâm.
- Kiểm tra tiêu đề Header thay đổi tương ứng.
- Kiểm tra màu nút chức năng đang được chọn.
- Kiểm tra chức năng đăng xuất và quay lại màn hình đăng nhập.

## 👨‍💻 6. Thông tin sinh viên

- **Họ và tên:** Nguyễn Quốc Vương
- **Mã sinh viên:** 2124110272
- **Lớp học phần:** CCQ2411D
