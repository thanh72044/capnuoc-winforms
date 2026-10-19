# 💧 Hệ Thống Quản Lý Cấp Nước Bến Thành

Dự án thực hành C# WinForms để chuẩn bị phỏng vấn vị trí **Lập trình viên** tại Công ty Cấp nước Bến Thành.

## 🛠 Công nghệ sử dụng

| Công nghệ | Mục đích |
|-----------|----------|
| C# .NET 8 | Ngôn ngữ lập trình chính |
| WinForms | Giao diện desktop |
| SQL Server | Cơ sở dữ liệu |
| ADO.NET | Kết nối database |
| ClosedXML | Xuất Excel |

## 📋 Tính năng

- ✅ **Quản lý khách hàng**: Thêm, sửa, xóa, tìm kiếm
- ✅ **Quản lý đồng hồ nước**: Ghi nhận chỉ số hàng tháng
- ✅ **Tính tiền nước**: Tự động tính theo bảng giá
- 🔄 **Xuất báo cáo Excel** *(đang phát triển)*
- 🔄 **In hóa đơn PDF** *(đang phát triển)*

## 🚀 Hướng dẫn cài đặt

### Yêu cầu hệ thống
- Windows 10/11
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server 2019+ (hoặc SQL Server Express - miễn phí)
- Visual Studio 2022 (hoặc VS Code)

### Bước 1: Clone project
```bash
git clone https://github.com/thanh72044/capnuoc-winforms.git
cd capnuoc-winforms
```

### Bước 2: Tạo database
1. Mở **SQL Server Management Studio (SSMS)**
2. Chạy file `Database/create_database.sql`

### Bước 3: Cấu hình kết nối
Mở file `Data/DatabaseHelper.cs` và chỉnh sửa connection string:
```csharp
private static string _connectionString =
    "Server=TEN_SERVER;Database=CapNuocDB;Trusted_Connection=true;TrustServerCertificate=true;";
```

### Bước 4: Chạy ứng dụng
```bash
dotnet run
```

## 📂 Cấu trúc project

```
capnuoc-project/
├── Models/
│   ├── KhachHang.cs          # Model khách hàng
│   └── ChiSoDongHo.cs        # Model chỉ số đồng hồ
├── Data/
│   └── DatabaseHelper.cs     # Lớp thao tác database (ADO.NET)
├── Forms/
│   ├── FormKhachHang.cs      # Form danh sách khách hàng
│   └── FormThemSuaKhachHang.cs # Form thêm/sửa
├── Database/
│   └── create_database.sql   # Script tạo DB + dữ liệu mẫu
├── Program.cs                # Điểm vào ứng dụng
└── CapNuocApp.csproj         # File cấu hình project
```

## 📚 Kiến thức được thực hành

- OOP: Class, Properties, Methods
- ADO.NET: SqlConnection, SqlCommand, SqlDataReader
- WinForms: DataGridView, Form, Controls
- SQL: SELECT, INSERT, UPDATE, Stored Procedures
- Git: Version control cơ bản

## 👤 Tác giả

**thanh72044** — Ứng viên vị trí Lập trình viên, Công ty Cấp nước Bến Thành
