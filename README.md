# MiniSupermarket – Hệ thống Quản lý Bán lẻ Cửa hàng Thực phẩm Sạch Organic Farm

<p align="center">
  <strong>Đồ án thực hành môn Lập trình Ứng dụng .NET Core</strong><br>
  Ứng dụng Windows Forms kết nối ASP.NET Core Web API
</p>

## 1. Thông tin sinh viên

| Thông tin | Nội dung |
|---|---|
| **Họ và tên sinh viên** | Nguyễn Quốc Vương |
| **Mã sinh viên** | 2124110272 |
| **Lớp học phần** | CCQ2411 |
| **Đề tài** | Thiết kế Hệ thống Quản lý Bán lẻ ứng dụng Lập trình Bất đồng bộ trong .NET Core – Cửa hàng Thực phẩm Sạch Organic Farm |

## 2. Giới thiệu dự án

**MiniSupermarket** là dự án thực hành xây dựng ứng dụng quản lý bán lẻ theo mô hình Client–Server bằng nền tảng .NET. Giải pháp gồm hai project chính:

- **MiniSupermarket.API**: Backend xây dựng bằng ASP.NET Core Web API, cung cấp các endpoint để xử lý yêu cầu từ ứng dụng khách.
- **MiniSupermarket.WinForm**: Ứng dụng giao diện máy tính Windows Forms, cho phép người dùng đăng nhập và thao tác với các chức năng quản lý thông qua API.

Dựa trên nội dung thực hành Buổi 2, dự án tập trung vào xác thực và phân quyền bằng **JWT (JSON Web Token)**, quản lý phiên đăng nhập ở phía WinForms và gửi Bearer Token khi gọi các API được bảo vệ.

## 3. Mục tiêu

- Xây dựng ứng dụng theo mô hình tách biệt giao diện WinForms và backend Web API.
- Thực hành xây dựng các API quản lý danh mục và vai trò.
- Tích hợp chức năng đăng nhập và cấp JWT Token.
- Quản lý token và vai trò người dùng trong phiên làm việc phía WinForms.
- Sử dụng `HttpClient` để gửi yêu cầu HTTP từ WinForms đến Web API.
- Tìm hiểu cơ chế xác thực (`Authentication`) và phân quyền (`Authorization`) theo vai trò.
- Làm quen với lập trình bất đồng bộ khi giao tiếp với API.

## 4. Công nghệ sử dụng

| Công nghệ / thành phần | Vai trò |
|---|---|
| C# | Ngôn ngữ lập trình |
| .NET / ASP.NET Core Web API | Xây dựng backend và các endpoint API |
| Windows Forms (WinForms) | Xây dựng giao diện ứng dụng desktop |
| JWT Bearer Authentication | Xác thực yêu cầu bằng token |
| `HttpClient` | Gửi yêu cầu HTTP từ WinForms đến API |
| JSON | Định dạng dữ liệu trao đổi giữa client và server |
| Swagger / OpenAPI | Khám phá và kiểm thử API trong môi trường phát triển |
| NuGet | Quản lý thư viện phụ thuộc |

Các gói NuGet liên quan đến phần thực hành JWT theo tài liệu Buổi 2:
- `System.IdentityModel.Tokens.Jwt`
- `Microsoft.AspNetCore.Authentication.JwtBearer`

> **Lưu ý:** Hãy kiểm tra phiên bản .NET Target Framework và các gói NuGet thực tế trong project trước khi cài đặt hoặc nâng cấp thư viện.

## 5. Cấu trúc dự án

```text
MiniSupermarket/
├── MiniSupermarket.API/
│   ├── Controllers/
│   │   ├── AuthController.cs
│   │   ├── CategoriesController.cs
│   │   ├── RolesController.cs
│   │   └── WeatherForecastController.cs
│   ├── Models/
│   │   ├── Category.cs
│   │   └── Roles.cs
│   ├── Properties/
│   ├── appsettings.json
│   ├── MiniSupermarket.API.http
│   ├── Program.cs
│   └── WeatherForecast.cs
│
└── MiniSupermarket.WinForm/
    ├── Form1.cs
    ├── FormCategoryManagement.cs
    ├── FormLogin.cs
    ├── FormRoleManagement.cs
    ├── Program.cs
    └── SessionManager.cs
```

### Vai trò các thành phần chính

**Backend – `MiniSupermarket.API`**

- `AuthController.cs`: xử lý endpoint xác thực/đăng nhập và trả kết quả đăng nhập theo cấu hình hiện tại.
- `CategoriesController.cs`: cung cấp API liên quan đến danh mục.
- `RolesController.cs`: cung cấp API liên quan đến vai trò.
- `Models/Category.cs`: model dữ liệu danh mục.
- `Models/Roles.cs`: model dữ liệu vai trò.
- `Program.cs`: cấu hình và khởi chạy ứng dụng Web API; các middleware xác thực/phân quyền cần được cấu hình tại đây.
- `appsettings.json`: nơi lưu cấu hình ứng dụng. Không đưa khóa bí mật hoặc mật khẩu dùng thật lên kho mã nguồn công khai.

**Client – `MiniSupermarket.WinForm`**

- `FormLogin.cs`: giao diện đăng nhập.
- `FormCategoryManagement.cs`: giao diện quản lý danh mục.
- `FormRoleManagement.cs`: giao diện quản lý vai trò.
- `SessionManager.cs`: lưu thông tin phiên đăng nhập trong bộ nhớ ứng dụng, gồm JWT Token và vai trò người dùng theo thiết kế của bài thực hành.
- `Program.cs`: điểm khởi chạy ứng dụng WinForms.

Cấu trúc trên được tổng hợp từ Solution Explorer được cung cấp. Các chức năng thực tế có thể thay đổi tùy theo mã nguồn hiện tại.

## 6. Chức năng chính

### 6.1. Xác thực và phiên đăng nhập

- Gửi thông tin tài khoản và mật khẩu từ WinForms đến API đăng nhập.
- Nhận JWT Token và vai trò người dùng khi đăng nhập thành công.
- Lưu token trong `SessionManager` để sử dụng trong phiên hiện tại.
- Hiển thị thông báo khi đăng nhập thành công hoặc thất bại.

### 6.2. Quản lý danh mục

- Gọi API danh mục từ ứng dụng WinForms.
- Hiển thị dữ liệu danh mục trên giao diện quản lý.
- Là nền tảng để thực hiện các thao tác CRUD (thêm, xem, sửa, xóa) nếu các endpoint tương ứng đã được triển khai.

### 6.3. Quản lý vai trò

- Tách riêng giao diện quản lý vai trò.
- Kết nối với `RolesController` để trao đổi dữ liệu vai trò qua API, tùy theo các endpoint đã triển khai.

### 6.4. Bảo mật JWT và phân quyền

Theo nội dung thực hành Buổi 2, quy trình dự kiến gồm:

1. Người dùng gửi thông tin đăng nhập đến API.
2. Backend kiểm tra thông tin và phát hành JWT Token nếu hợp lệ.
3. WinForms lưu token và vai trò vào `SessionManager`.
4. Khi gọi endpoint được bảo vệ, client gửi token trong HTTP Header:

   ```http
   Authorization: Bearer <JWT_TOKEN>
   ```

5. Backend kiểm tra token trước khi cho phép truy cập.
6. Endpoint có thể giới hạn quyền theo vai trò bằng `[Authorize(Roles = "...")]`.

Các mã phản hồi thường gặp khi kiểm thử bảo mật:
- `200 OK`: yêu cầu được xử lý thành công.
- `400 Bad Request`: dữ liệu đầu vào không hợp lệ.
- `401 Unauthorized`: chưa đăng nhập hoặc token không hợp lệ/hết hạn.
- `403 Forbidden`: đã xác thực nhưng không đủ quyền truy cập.

> Các mục trên mô tả luồng và kiến thức theo bài thực hành. Hãy đối chiếu mã nguồn thực tế để xác nhận endpoint, vai trò và chính sách bảo mật đã được triển khai đầy đủ.

## 7. Yêu cầu môi trường

- Windows có hỗ trợ chạy ứng dụng .NET tương ứng.
- Visual Studio có workload **.NET desktop development** và **ASP.NET and web development**.
- .NET SDK/Runtime tương thích với `TargetFramework` trong hai project.
- Các gói NuGet được khai báo trong project.
- Nếu API sử dụng cơ sở dữ liệu, cần cài đặt/cấu hình hệ quản trị và connection string tương ứng. Cấu hình cơ sở dữ liệu cụ thể cần kiểm tra trong mã nguồn hiện tại.

## 8. Cách tải và chạy dự án

### Bước 1: Clone repository

```bash
git clone https://github.com/nguyenquocvuong7512/MiniSuperMartket_NguyenQuocVuong.git
cd MiniSuperMartket_NguyenQuocVuong
```

### Bước 2: Mở solution

Mở file solution `MiniSupermarket.sln` bằng Visual Studio. Nếu tên file solution thực tế khác, hãy chọn file `.sln` có trong thư mục repository.

### Bước 3: Khôi phục NuGet và build

Trong Visual Studio, chọn **Restore NuGet Packages**, sau đó build solution.

Có thể dùng Terminal tại thư mục chứa solution:

```bash
dotnet restore
dotnet build
```

### Bước 4: Khởi chạy Web API

Đặt `MiniSupermarket.API` làm project khởi chạy và chạy ứng dụng. Kiểm tra URL/port thực tế được hiển thị trong console hoặc cấu hình launch profile.

Mở đường dẫn Swagger nếu Swagger được bật trong môi trường Development. Không nên mặc định port trong tài liệu mẫu là port của máy bạn; hãy dùng đúng địa chỉ API đang chạy.

### Bước 5: Cấu hình địa chỉ API ở WinForms

Kiểm tra các địa chỉ `BaseAddress` hoặc URL API trong mã nguồn WinForms. Đảm bảo chúng trùng với địa chỉ và giao thức HTTP/HTTPS mà backend đang sử dụng.

### Bước 6: Khởi chạy WinForms

Đặt `MiniSupermarket.WinForm` làm project khởi chạy và chạy ứng dụng. Nếu muốn bắt đầu từ màn hình đăng nhập, kiểm tra `Program.cs` để bảo đảm form khởi chạy là `FormLogin`.

## 9. Kiểm thử API và JWT

Có thể dùng Swagger UI để kiểm thử theo trình tự:

1. Gọi endpoint đăng nhập để nhận token.
2. Sao chép JWT Token từ kết quả trả về.
3. Dùng nút **Authorize** của Swagger nếu cấu hình OpenAPI hỗ trợ Bearer Authentication.
4. Gọi endpoint được bảo vệ để kiểm tra kết quả khi có token.
5. Nếu có endpoint giới hạn vai trò, đăng nhập bằng các tài khoản có vai trò khác nhau để kiểm tra quyền truy cập.

Khi kiểm thử, cần sử dụng tài khoản được cấu hình trong mã nguồn/môi trường của dự án. Không xem tài khoản mẫu trong tài liệu thực hành là tài khoản chắc chắn tồn tại trong phiên bản repository hiện tại.

## 10. Nguyên lý xác thực và phân quyền

- **Authentication (Xác thực):** xác định người dùng là ai.
- **Authorization (Phân quyền):** xác định người dùng được phép thực hiện hành động nào.
- **JWT:** token chứa các claim cần thiết và được ký bằng khóa bí mật để backend xác minh tính hợp lệ.
- **Stateless Authentication:** server xác thực từng yêu cầu dựa trên token thay vì phải duy trì session đăng nhập trong bộ nhớ server theo cách truyền thống.
- **Data Annotations / ModelState:** có thể dùng để kiểm tra dữ liệu đầu vào, ví dụ `[Required]`, `[StringLength]`, `[Range]`, `[EmailAddress]`.
- **Middleware:** `UseAuthentication()` cần được đặt trước `UseAuthorization()` trong pipeline ASP.NET Core.

## 11. Bảo mật và lưu ý khi phát triển

- Không commit mật khẩu thật, JWT Secret, connection string có thông tin xác thực hoặc dữ liệu nhạy cảm lên GitHub.
- Dùng biến môi trường hoặc cơ chế quản lý secrets phù hợp cho môi trường phát triển/triển khai.
- Không dùng tài khoản và mật khẩu mẫu của bài giảng làm thông tin đăng nhập trong môi trường thực tế.
- Bảo đảm thời hạn token, chữ ký, issuer/audience (nếu sử dụng) được kiểm tra theo cấu hình hệ thống.
- Chỉ mở endpoint công khai khi có chủ đích; những endpoint cần đăng nhập nên được bảo vệ bằng chính sách phù hợp.
- Không coi việc ẩn nút trên giao diện WinForms là cơ chế phân quyền duy nhất; backend phải tự kiểm tra quyền truy cập.

## 12. Định hướng phát triển

Các hướng phát triển có thể cân nhắc cho những giai đoạn tiếp theo:

- Tích hợp SQL Server và Entity Framework Core nếu chưa hoàn thiện.
- Chuyển dữ liệu mẫu sang dữ liệu được lưu trữ trong cơ sở dữ liệu.
- Hoàn thiện CRUD cho danh mục và vai trò.
- Hoàn thiện phân quyền theo vai trò ở cấp endpoint và giao diện.
- Chuẩn hóa xử lý lỗi, kiểm tra dữ liệu đầu vào và thông báo cho người dùng.
- Bổ sung các nghiệp vụ bán hàng như sản phẩm, khách hàng, đơn hàng và chi tiết đơn hàng khi phù hợp với phạm vi đồ án.

## 13. Tài liệu tham khảo

- **Bài giảng Thực hành Buổi 2 – Lập trình Ứng dụng .NET Core:** Bảo mật và phân quyền JWT cho Web API.
- Tài liệu ASP.NET Core Web API, Windows Forms và JWT Bearer Authentication của Microsoft.

## 14. Repository

Mã nguồn dự án:  
https://github.com/nguyenquocvuong7512/MiniSuperMartket_NguyenQuocVuong

---

<p align="center">
  <sub>MiniSupermarket – Đồ án thực hành .NET Core | Nguyễn Quốc Vương | CCQ2411</sub>
</p>
