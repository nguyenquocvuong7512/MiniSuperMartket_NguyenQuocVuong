# MiniSupermarket – Thực hành .NET Core Buổi 2

## 1. Thông tin sinh viên

- **Họ và tên:** Nguyễn Quốc Vương
- **Mã sinh viên:** 2124110272
- **Lớp học phần:** CCQ2411
- **Đề tài:** Thiết kế Hệ thống Quản lý Bán lẻ ứng dụng Lập trình Bất đồng bộ trong .NET Core – Cửa hàng Thực phẩm Sạch Organic Farm.

## 2. Nội dung thực hành

Buổi 2 tập trung vào **bảo mật và phân quyền JWT cho Web API**, đồng thời tích hợp chức năng đăng nhập giữa ASP.NET Core Web API và ứng dụng Windows Forms.

Các nội dung đã thực hành theo bài giảng:

- Xây dựng `AuthController` để xử lý đăng nhập và cấp phát JWT Token.
- Cấu hình xác thực JWT Bearer trong `Program.cs`.
- Sử dụng `[Authorize]` để bảo vệ API, yêu cầu người dùng phải đăng nhập mới được truy cập.
- Sử dụng `[Authorize(Roles = "...")]` để phân quyền truy cập theo vai trò người dùng.
- Xây dựng giao diện đăng nhập `FormLogin` trên Windows Forms.
- Sử dụng `SessionManager` để lưu JWT Token và vai trò người dùng trong phiên làm việc.
- Sử dụng `HttpClient` để gửi yêu cầu từ WinForms đến Web API.
- Đính kèm JWT Token vào HTTP Header theo định dạng `Authorization: Bearer <token>`.
- Kiểm thử xác thực và phân quyền API bằng Swagger UI.

## 3. Công nghệ và công cụ sử dụng

- **Ngôn ngữ lập trình:** C#.
- **Nền tảng:** .NET / ASP.NET Core Web API.
- **Giao diện desktop:** Windows Forms (WinForms).
- **Bảo mật:** JWT (JSON Web Token), JWT Bearer Authentication.
- **Thư viện:** `System.IdentityModel.Tokens.Jwt`, `Microsoft.AspNetCore.Authentication.JwtBearer`.
- **Giao tiếp API:** `HttpClient`, HTTP và JSON.
- **Kiểm thử API:** Swagger UI.
- **Quản lý thư viện:** NuGet.

## 4. Cấu trúc project

```text
MiniSupermarket/
│
├── MiniSupermarket.API/
│   │
│   ├── Connected Services/
│   ├── Dependencies/
│   ├── Properties/
│   │
│   ├── Controllers/
│   │   ├── AuthController.cs
│   │   ├── CategoriesController.cs
│   │   ├── RolesController.cs
│   │   └── WeatherForecastController.cs
│   │
│   ├── Models/
│   │   ├── Category.cs
│   │   └── Roles.cs
│   │
│   ├── appsettings.json
│   ├── MiniSupermarket.API.http
│   ├── Program.cs
│   └── WeatherForecast.cs
│
└── MiniSupermarket.WinForm/
    │
    ├── Dependencies/
    │
    ├── Form1.cs
    ├── FormCategoryManagement.cs
    ├── FormLogin.cs
    ├── FormRoleManagement.cs
    ├── Program.cs
    └── SessionManager.cs
```

### Mô tả các thành phần

- **MiniSupermarket.API:** Project backend xây dựng bằng ASP.NET Core Web API.
- **Controllers:** Chứa các controller xử lý yêu cầu API.
- **Models:** Chứa các lớp mô hình dữ liệu như danh mục và vai trò.
- **Program.cs:** Cấu hình và khởi chạy ứng dụng API.
- **appsettings.json:** Chứa cấu hình ứng dụng.
- **MiniSupermarket.WinForm:** Project giao diện desktop sử dụng Windows Forms.
- **FormLogin.cs:** Giao diện đăng nhập.
- **FormCategoryManagement.cs:** Giao diện quản lý danh mục.
- **FormRoleManagement.cs:** Giao diện quản lý vai trò.
- **SessionManager.cs:** Quản lý thông tin phiên đăng nhập, bao gồm JWT Token và vai trò người dùng.
- **Form1.cs:** Form giao diện chính được định nghĩa trong project.

## 5. Kiến thức đạt được

Sau buổi thực hành, sinh viên hiểu được sự khác nhau giữa xác thực (Authentication) và phân quyền (Authorization), nắm được nguyên lý hoạt động của JWT, biết cách bảo vệ API bằng thuộc tính `[Authorize]`, phân quyền theo vai trò và tích hợp xác thực giữa ứng dụng WinForms với ASP.NET Core Web API.

Ngoài ra, sinh viên được thực hành sử dụng `HttpClient` để giao tiếp với API và kiểm tra quyền truy cập bằng Swagger UI.

## 6. Tài liệu tham khảo

- Bài giảng Thực hành Buổi 2 – Lập trình Ứng dụng .NET Core: Bảo mật và phân quyền JWT cho Web API.
- Repository: https://github.com/nguyenquocvuong7512/MiniSuperMartket_NguyenQuocVuong
