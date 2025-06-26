# SalesManagementSystem

## 📝 Giới thiệu

**SalesManagementSystem** là một hệ thống quản lý bán hàng được xây dựng với kiến trúc nhiều lớp (multi-layer architecture) sử dụng .NET (C#) và WPF cho giao diện người dùng. Dự án hướng tới việc quản lý sản phẩm, khách hàng, đơn hàng và nhân viên một cách hiệu quả, dễ mở rộng và bảo trì.

## 🚀 Tính năng nổi bật

- Quản lý sản phẩm: Thêm, sửa, xóa, tìm kiếm sản phẩm.
- Quản lý khách hàng: Thêm, sửa, xóa, tìm kiếm khách hàng.
- Quản lý đơn hàng: Tạo đơn hàng, xem lịch sử đơn hàng.
- Quản lý nhân viên: Đăng nhập, phân quyền.
- Giao diện người dùng hiện đại với WPF, hỗ trợ đăng nhập cho cả nhân viên và khách hàng.
- Kiến trúc phân lớp rõ ràng: Data Access Layer, Repository, Service, Presentation (WPF).

## 🏗️ Kiến trúc dự án

```
SalesManagementSystem/
│
├── DataAccessLayer/           # Tầng truy xuất dữ liệu, chứa các entity (Product, Customer, Order, ...), context, mapping với database
│   └── Entities/              # Định nghĩa các lớp thực thể (entity) tương ứng với bảng dữ liệu
│   └── LucySalesDataContext.cs# DbContext quản lý kết nối và truy vấn database
│
├── Repositories/              # Tầng repository, định nghĩa interface và hiện thực thao tác CRUD với dữ liệu
│   └── Interfaces/            # Định nghĩa các interface repository (ICustomerRepository, ...)
│   └── Implementations/       # Hiện thực các repository (CustomerRepository, ...)
│
├── Services/                  # Tầng service, xử lý logic nghiệp vụ, giao tiếp giữa repository và UI
│   └── Interfaces/            # Định nghĩa các interface service (ICustomerService, ...)
│   └── Implementations/       # Hiện thực các service (CustomerService, ...)
│
├── WPF.SalesManagementSystem/ # Tầng giao diện người dùng (UI) sử dụng WPF
│   └── *.xaml, *.xaml.cs      # Các cửa sổ (window) giao diện: đăng nhập, quản lý sản phẩm, khách hàng, đơn hàng...
│   └── appsettings.json       # Cấu hình ứng dụng (kết nối DB, ...)
│
├── SalesManagementSystem.sln  # Solution file, quản lý toàn bộ các project con
```

### Mô hình dữ liệu (ví dụ)

- **Product**: ProductId, ProductName, CategoryId, UnitPrice, UnitsInStock, ...
- **Customer**: CustomerId, CompanyName, ContactName, Address, Phone, ...
- **Order**: OrderId, CustomerId, EmployeeId, OrderDate, ...
- **Employee**: EmployeeId, Name, Title, ...

### Giao diện người dùng

- **Đăng nhập**: Phân biệt nhân viên và khách hàng.
- **Quản lý sản phẩm/khách hàng**: Danh sách, tìm kiếm, thêm, sửa, xóa.
- **Quản lý đơn hàng**: Tạo mới, xem lịch sử.
- **Chỉnh sửa thông tin cá nhân** (cho khách hàng).

## ⚙️ Cài đặt & chạy thử

### Yêu cầu

- Windows, .NET 6.0 trở lên
- Visual Studio 2022 hoặc mới hơn

### Hướng dẫn

1. **Clone repo:**
   ```bash
   git clone <repo-url>
   cd SalesManagementSystem
   ```
2. **Mở solution bằng Visual Studio:**  
   Mở file `SalesManagementSystem.sln`.
3. **Khôi phục package & build:**  
   Visual Studio sẽ tự động khôi phục các package cần thiết khi build.
4. **Chạy project WPF:**  
   Đặt `WPF.SalesManagementSystem` làm project khởi động và nhấn F5 để chạy.

## 🧑‍💻 Đóng góp

Chúng tôi hoan nghênh mọi đóng góp!  
Vui lòng tạo issue hoặc pull request nếu bạn muốn đề xuất tính năng, sửa lỗi hoặc cải thiện tài liệu.

## 📄 License

Dự án này sử dụng [MIT License](LICENSE) (hoặc cập nhật theo license thực tế của bạn).

## 💬 Liên hệ

- Tác giả: Nguyễn Ngọc Viên
- Email: zienkdev@gmail.com