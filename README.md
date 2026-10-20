# 💧 LỘ TRÌNH HỌC 30 NGÀY — CHUẨN BỊ PHỎNG VẤN CẤP NƯỚC BẾN THÀNH
> **Mục tiêu:** Nắm vững C# WinForms + SQL Server để tự viết lại toàn bộ project quản lý cấp nước
> 
> **Công nghệ cần học:** C# .NET | WinForms | ADO.NET | SQL Server
>
> **Cách dùng file này:** Mỗi ngày mở ra, đọc bài hôm đó, tự code theo hướng dẫn, rồi tick vào ô [ ]

---

## 📅 TỔNG QUAN 4 TUẦN

| Tuần | Chủ đề | Kết quả đạt được |
|------|--------|-----------------|
| Tuần 1 (Ngày 1–7) | **C# Nền tảng** | Viết được code C# cơ bản, hiểu OOP |
| Tuần 2 (Ngày 8–14) | **SQL Server + ADO.NET** | Kết nối DB, thực hiện CRUD |
| Tuần 3 (Ngày 15–21) | **WinForms** | Xây dựng giao diện desktop |
| Tuần 4 (Ngày 22–30) | **Tổng hợp + Luyện phỏng vấn** | Viết lại toàn bộ project, trả lời câu hỏi |

---

# 📗 TUẦN 1: C# NỀN TẢNG (Ngày 1–7)

---

## 🗓️ NGÀY 1 — Cài Đặt Môi Trường + Hello World

### Mục tiêu: Cài đủ tool, viết được chương trình C# đầu tiên

### ✅ Checklist công việc:
- [ ] Cài [Visual Studio 2022 Community](https://visualstudio.microsoft.com/) (chọn ".NET Desktop Development")
- [ ] Cài [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (miễn phí)
- [ ] Cài [SQL Server Management Studio – SSMS](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)
- [ ] Tạo project WinForms mới: Visual Studio → New Project → Windows Forms App (.NET)
- [ ] Chạy được Hello World:

```csharp
// Trong Form1.cs, thêm vào constructor
MessageBox.Show("Xin chào Cấp Nước Bến Thành!");
```

### 📝 Ghi nhớ hôm nay:
- Visual Studio là IDE (phần mềm lập trình)
- `.cs` là đuôi file C#
- `namespace` = nhóm chứa code, `class` = bản thiết kế, `method` = hành động

---

## 🗓️ NGÀY 2 — Biến, Kiểu Dữ Liệu, In Ra Màn Hình

### Mục tiêu: Khai báo biến, hiểu các kiểu dữ liệu cơ bản

### Lý thuyết cần đọc:
```csharp
// Khai báo biến — kiểu_dữ_liệu tên_biến = giá_trị;
int soKhachHang = 100;          // Số nguyên
decimal tienNuoc = 75000.50m;   // Số thập phân (dùng 'm' ở cuối)
string tenKhach = "Nguyễn Văn An"; // Chuỗi ký tự
bool dangHoatDong = true;       // Đúng/Sai
DateTime ngayDangKy = DateTime.Now; // Ngày giờ hiện tại

// In ra (dùng trong Console App hoặc MessageBox)
Console.WriteLine($"Khách hàng: {tenKhach}");
Console.WriteLine($"Tiền nước: {tienNuoc:N0} đồng"); // :N0 = định dạng số có dấu phẩy
MessageBox.Show($"Có {soKhachHang} khách hàng");
```

### Bảng kiểu dữ liệu quan trọng:
| Kiểu C# | Dùng cho | Ví dụ |
|---------|---------|-------|
| `int` | Số nguyên (mã KH, chỉ số đồng hồ) | `int chiSo = 1500;` |
| `decimal` | Tiền tệ (quan trọng!) | `decimal tien = 75000m;` |
| `string` | Chuỗi (tên, địa chỉ, mã) | `string maKH = "KH-001";` |
| `bool` | Đúng/Sai (đang hoạt động?) | `bool ok = true;` |
| `DateTime` | Ngày giờ | `DateTime ngay = DateTime.Now;` |

### ✅ Bài tập hôm nay:
- [ ] Khai báo 5 biến: maKhachHang, hoTen, diaChi, soDienThoai, tienNuoc
- [ ] Dùng MessageBox.Show để in ra thông tin 1 khách hàng giả tưởng
- [ ] Dùng `DateTime.Now.ToString("dd/MM/yyyy")` để in ngày hôm nay

### 📝 Ghi nhớ hôm nay:
- Dùng `decimal` (KHÔNG dùng `double`) cho tiền tệ — `decimal` chính xác hơn
- String interpolation: `$"Tên: {tenKhach}"` — dấu `$` trước nháy kép
- `m` sau số thập phân: `75000.5m` = kiểu decimal

---

## 🗓️ NGÀY 3 — Câu Lệnh Điều Kiện + Vòng Lặp

### Mục tiêu: Xử lý logic, lặp qua danh sách dữ liệu

### Lý thuyết cần đọc:

**If-Else (điều kiện):**
```csharp
int chiSoDau = 1000;
int chiSoCuoi = 1025;
int soM3 = chiSoCuoi - chiSoDau; // = 25

if (soM3 > 20)
{
    MessageBox.Show("Dùng nhiều nước — trên 20m³");
}
else if (soM3 > 10)
{
    MessageBox.Show("Dùng vừa — 10 đến 20m³");
}
else
{
    MessageBox.Show("Dùng ít — dưới 10m³");
}
```

**Switch (nhiều nhánh):**
```csharp
int thang = DateTime.Now.Month;
switch (thang)
{
    case 1: case 2: case 3:
        MessageBox.Show("Quý 1");
        break;
    case 4: case 5: case 6:
        MessageBox.Show("Quý 2");
        break;
    default:
        MessageBox.Show("Quý 3 hoặc 4");
        break;
}
```

**Vòng lặp for:**
```csharp
// In ra chỉ số từ tháng 1 đến tháng 12
for (int thang = 1; thang <= 12; thang++)
{
    Console.WriteLine($"Tháng {thang}");
}
```

**Vòng lặp foreach (duyệt danh sách):**
```csharp
// Đây là vòng lặp quan trọng nhất khi làm việc với database!
var danhSachKH = new List<string> { "Nguyễn An", "Trần Bình", "Lê Cường" };

foreach (var ten in danhSachKH)
{
    Console.WriteLine($"Khách hàng: {ten}");
}
```

### ✅ Bài tập hôm nay:
- [ ] Viết hàm tính tiền nước: nếu dùng ≤10m³ thì 7000đ/m³, nếu >10m³ thì 9000đ/m³
- [ ] Dùng `for` loop in ra danh sách 5 khách hàng giả tưởng
- [ ] Dùng `foreach` để tính tổng tiền nước của danh sách

### 📝 Ghi nhớ hôm nay:
- `for` = biết trước số lần lặp
- `foreach` = duyệt qua từng phần tử trong danh sách (thường dùng hơn)
- `break` = thoát khỏi vòng lặp ngay lập tức

---

## 🗓️ NGÀY 4 — Method (Hàm / Phương Thức)

### Mục tiêu: Viết và gọi hàm, hiểu tham số và giá trị trả về

### Lý thuyết cần đọc:
```csharp
// Cấu trúc: [access] [static] kiểuTraVe TenHam(thamSo1, thamSo2)
// Hàm tính tiền nước
private decimal TinhTienNuoc(int soM3, decimal donGia)
{
    decimal thanhTien = soM3 * donGia;
    return thanhTien; // Trả về kết quả
}

// Hàm không trả về giá trị (void)
private void HienThiThongBao(string noiDung)
{
    MessageBox.Show(noiDung, "Thông báo", 
        MessageBoxButtons.OK, MessageBoxIcon.Information);
}

// Gọi hàm
decimal tien = TinhTienNuoc(25, 7000m); // = 175,000
HienThiThongBao($"Tiền nước: {tien:N0} đồng");
```

**Hàm với giá trị mặc định:**
```csharp
// donGia có giá trị mặc định = 7000 nếu không truyền vào
private decimal TinhTienNuoc(int soM3, decimal donGia = 7000m)
{
    return soM3 * donGia;
}

// Gọi với 2 tham số
decimal tien1 = TinhTienNuoc(25, 9000m);
// Gọi với 1 tham số — donGia tự dùng 7000
decimal tien2 = TinhTienNuoc(25);
```

### ✅ Bài tập hôm nay:
- [ ] Viết hàm `TaoMaKhachHang()` trả về chuỗi format `"KH-YYYYMMDD-XXX"`
- [ ] Viết hàm `KiemTraSoDienThoai(string sdt)` trả về `bool` — đúng nếu đủ 10 số
- [ ] Viết hàm `TinhTienNuocTheoThang(int chiSoDau, int chiSoCuoi)` tính thành tiền

### 📝 Ghi nhớ hôm nay:
- `void` = hàm không trả về gì (thường dùng để hiển thị, lưu DB)
- `return` = trả kết quả về nơi gọi hàm, kết thúc hàm ngay lập tức
- `private` = chỉ dùng trong class hiện tại; `public` = dùng được từ nơi khác

---

## 🗓️ NGÀY 5 — Class và Properties (OOP phần 1)

### Mục tiêu: Tạo class đầu tiên, hiểu property là gì

### Lý thuyết cần đọc:
```csharp
// Class = khuôn mẫu để tạo ra các object
// Đây chính là file Models/KhachHang.cs bạn sẽ viết lại!
public class KhachHang
{
    // Properties = thuộc tính của khách hàng
    public int Id { get; set; }                              // ID tự tăng
    public string MaKhachHang { get; set; } = string.Empty; // Mã KH
    public string HoTen { get; set; } = string.Empty;       // Họ tên
    public string DiaChi { get; set; } = string.Empty;      // Địa chỉ
    public string SoDienThoai { get; set; } = string.Empty; // Số ĐT
    public string Email { get; set; } = string.Empty;       // Email
    public string MaSoDongHo { get; set; } = string.Empty;  // Mã đồng hồ
    public DateTime NgayDangKy { get; set; } = DateTime.Now;// Ngày đăng ký
    public bool DangHoatDong { get; set; } = true;          // Đang hoạt động
}
```

**Cách dùng class:**
```csharp
// Tạo object từ class (dùng từ khóa new)
var khachHang1 = new KhachHang
{
    MaKhachHang = "KH-001",
    HoTen = "Nguyễn Văn An",
    DiaChi = "12 Lý Tự Trọng, Q.1",
    SoDienThoai = "0901234567"
};

// Đọc giá trị
Console.WriteLine(khachHang1.HoTen);      // "Nguyễn Văn An"
Console.WriteLine(khachHang1.NgayDangKy); // Ngày hôm nay

// Thay đổi giá trị
khachHang1.DiaChi = "Địa chỉ mới";
```

**List — danh sách nhiều object:**
```csharp
var danhSach = new List<KhachHang>();
danhSach.Add(khachHang1);
danhSach.Add(new KhachHang { HoTen = "Trần Thị Bình" });

// Duyệt qua danh sách
foreach (var kh in danhSach)
{
    Console.WriteLine($"{kh.MaKhachHang} - {kh.HoTen}");
}

// Đếm số lượng
Console.WriteLine($"Có {danhSach.Count} khách hàng");
```

### ✅ Bài tập hôm nay:
- [ ] Tạo file `Models/KhachHang.cs` với class KhachHang đầy đủ properties
- [ ] Tạo 5 object KhachHang, thêm vào List, dùng foreach để in ra màn hình
- [ ] Tạo file `Models/ChiSoDongHo.cs` với class ChiSoDongHo

### 📝 Ghi nhớ hôm nay:
- `{ get; set; }` = auto-property (C# tự tạo biến lưu trữ bên trong)
- `= string.Empty` = giá trị mặc định, tránh lỗi null
- `List<T>` = danh sách động, có thể thêm/xóa phần tử

---

## 🗓️ NGÀY 6 — Constructor + OOP nâng cao

### Mục tiêu: Hiểu constructor, computed property, kế thừa cơ bản

### Lý thuyết cần đọc:

**Constructor (hàm khởi tạo):**
```csharp
public class ChiSoDongHo
{
    public int Id { get; set; }
    public int KhachHangId { get; set; }
    public int ChiSoDau { get; set; }
    public int ChiSoCuoi { get; set; }
    public decimal DonGia { get; set; } = 7000m;

    // Computed property — tự tính, không lưu vào DB
    // Mỗi lần đọc, nó tự tính lại từ các property khác
    public int SoLuongTieuThu => ChiSoCuoi - ChiSoDau;
    public decimal ThanhTien => SoLuongTieuThu * DonGia;
}
```

**Constructor overloading (nhiều cách khởi tạo):**
```csharp
public class KhachHang
{
    public string HoTen { get; set; } = string.Empty;
    public DateTime NgayDangKy { get; set; }

    // Constructor 1: không tham số
    public KhachHang()
    {
        NgayDangKy = DateTime.Now;
    }

    // Constructor 2: có tham số
    public KhachHang(string hoTen)
    {
        HoTen = hoTen;
        NgayDangKy = DateTime.Now;
    }
}

// Cách dùng:
var kh1 = new KhachHang();                // Dùng constructor 1
var kh2 = new KhachHang("Nguyễn An");    // Dùng constructor 2
```

### ✅ Bài tập hôm nay:
- [ ] Thêm computed property `SoLuongTieuThu` và `ThanhTien` vào `ChiSoDongHo`
- [ ] Thêm constructor vào `KhachHang` tự set `NgayDangKy = DateTime.Now`
- [ ] Tạo 3 object `ChiSoDongHo`, in ra màn hình ThanhTien của từng cái

### 📝 Ghi nhớ hôm nay:
- `=>` trong property = expression body, tính toán khi được đọc
- Constructor trùng tên class, không có kiểu trả về
- Constructor chạy 1 lần duy nhất khi `new`

---

## 🗓️ NGÀY 7 — Xử Lý Lỗi Try-Catch + LINQ cơ bản

### Mục tiêu: Viết code an toàn, lọc/sắp xếp danh sách

### Lý thuyết cần đọc:

**Try-Catch (bẫy lỗi):**
```csharp
// Không dùng try-catch — app crash nếu lỗi
// DatabaseHelper.GetAllKhachHang();

// Dùng try-catch — xử lý lỗi an toàn
try
{
    var danhSach = DatabaseHelper.GetAllKhachHang();
    // Code chạy bình thường
}
catch (SqlException ex)
{
    // Chạy khi có lỗi SQL (không kết nối được DB)
    MessageBox.Show($"Lỗi database: {ex.Message}");
}
catch (Exception ex)
{
    // Bắt TẤT CẢ loại lỗi còn lại
    MessageBox.Show($"Lỗi không xác định: {ex.Message}");
}
finally
{
    // Luôn chạy dù có lỗi hay không
    // Dùng để dọn dẹp tài nguyên
}
```

**LINQ — lọc và sắp xếp danh sách:**
```csharp
var danhSach = new List<KhachHang> { /* ... */ };

// Where = lọc
var dangHoatDong = danhSach.Where(kh => kh.DangHoatDong == true).ToList();

// OrderBy = sắp xếp
var sapXep = danhSach.OrderBy(kh => kh.HoTen).ToList();

// FirstOrDefault = tìm 1 phần tử (trả null nếu không có)
var timKiem = danhSach.FirstOrDefault(kh => kh.MaKhachHang == "KH-001");

// Count = đếm
int soKhachHang = danhSach.Count(kh => kh.DangHoatDong);

// Select = chọn 1 thuộc tính (lấy chỉ tên)
var danhSachTen = danhSach.Select(kh => kh.HoTen).ToList();
```

### ✅ Bài tập hôm nay:
- [ ] Bọc tất cả code database bằng try-catch từ hôm nay
- [ ] Dùng LINQ: lọc khách hàng đang hoạt động, sắp xếp theo tên
- [ ] Dùng `FirstOrDefault` để tìm khách hàng theo mã KH

### 📝 Ghi nhớ hôm nay:
- Luôn dùng `try-catch` khi thao tác với database!
- LINQ dùng `=>` (lambda) để viết điều kiện ngắn gọn
- `ToList()` ở cuối LINQ để chuyển về `List<T>`

---

# 📘 TUẦN 2: SQL SERVER + ADO.NET (Ngày 8–14)

---

## 🗓️ NGÀY 8 — SQL Server Cơ Bản

### Mục tiêu: Tạo database, viết câu truy vấn SELECT cơ bản

### ✅ Checklist công việc:
- [ ] Mở SSMS (SQL Server Management Studio)
- [ ] Kết nối đến SQL Server (server name: `.\MSSQLSERVER1` hoặc `localhost`)
- [ ] Tạo database và bảng:

```sql
-- Tạo database mới
CREATE DATABASE CapNuocDB;
GO
USE CapNuocDB;
GO

-- Tạo bảng khách hàng
CREATE TABLE KhachHang (
    Id           INT IDENTITY(1,1) PRIMARY KEY, -- ID tự tăng
    MaKhachHang  NVARCHAR(20)  NOT NULL UNIQUE, -- Mã KH, không trùng
    HoTen        NVARCHAR(100) NOT NULL,         -- Họ tên (tiếng Việt dùng NVARCHAR)
    DiaChi       NVARCHAR(255) NOT NULL,
    SoDienThoai  NVARCHAR(15)  NOT NULL,
    Email        NVARCHAR(100),                  -- Có thể null (không bắt buộc)
    MaSoDongHo   NVARCHAR(20)  NOT NULL UNIQUE,
    NgayDangKy   DATETIME      DEFAULT GETDATE(),-- Mặc định = ngày hôm nay
    DangHoatDong BIT           DEFAULT 1         -- 1=đang dùng, 0=đã nghỉ
);

-- Chèn dữ liệu mẫu
INSERT INTO KhachHang (MaKhachHang, HoTen, DiaChi, SoDienThoai, Email, MaSoDongHo)
VALUES
('KH-001', N'Nguyễn Văn An',  N'12 Lý Tự Trọng, Q.1',    '0901234567', 'an@gmail.com', 'DH-001'),
('KH-002', N'Trần Thị Bình',  N'45 Nguyễn Huệ, Q.1',      '0912345678', '',             'DH-002'),
('KH-003', N'Lê Văn Cường',   N'78 Đinh Tiên Hoàng, Q.BT','0923456789', '',             'DH-003');
```

**Câu SELECT cơ bản:**
```sql
-- Lấy tất cả
SELECT * FROM KhachHang;

-- Chỉ lấy cột cần thiết
SELECT MaKhachHang, HoTen, SoDienThoai FROM KhachHang;

-- Lọc điều kiện
SELECT * FROM KhachHang WHERE DangHoatDong = 1;

-- Sắp xếp
SELECT * FROM KhachHang ORDER BY HoTen ASC;

-- Tìm kiếm (LIKE với %)
SELECT * FROM KhachHang WHERE HoTen LIKE N'%Nguyễn%';
```

### ✅ Bài tập hôm nay:
- [ ] Tạo database `CapNuocDB` và bảng `KhachHang` trong SSMS
- [ ] Chèn 5 bản ghi khách hàng
- [ ] Thực hiện 5 câu SELECT: lấy tất cả, lọc đang hoạt động, tìm theo tên

### 📝 Ghi nhớ hôm nay:
- `NVARCHAR` (không phải `VARCHAR`) cho tiếng Việt
- `IDENTITY(1,1)` = tự tăng, bắt đầu từ 1, tăng 1 mỗi lần
- Tiền tố `N` trước chuỗi: `N'Nguyễn Văn An'` để lưu đúng tiếng Việt
- `LIKE '%từ%'` = tìm kiếm chứa từ đó bất kỳ vị trí nào

---

## 🗓️ NGÀY 9 — SQL INSERT, UPDATE, DELETE + Bảng ChiSoDongHo

### Mục tiêu: Thao tác CRUD hoàn chỉnh trong SQL

```sql
-- Tạo bảng chỉ số đồng hồ (có Foreign Key liên kết với KhachHang)
CREATE TABLE ChiSoDongHo (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    KhachHangId INT NOT NULL FOREIGN KEY REFERENCES KhachHang(Id), -- Khóa ngoại!
    MaSoDongHo  NVARCHAR(20) NOT NULL,
    ChiSoDau    INT NOT NULL DEFAULT 0,   -- Chỉ số đầu kỳ (m³)
    ChiSoCuoi   INT NOT NULL DEFAULT 0,   -- Chỉ số cuối kỳ (m³)
    NgayDoc     DATETIME DEFAULT GETDATE(),
    Thang       INT NOT NULL,
    Nam         INT NOT NULL,
    DonGia      DECIMAL(10,2) DEFAULT 7000 -- Giá tiền mỗi m³
);

-- INSERT: thêm chỉ số tháng 10
INSERT INTO ChiSoDongHo (KhachHangId, MaSoDongHo, ChiSoDau, ChiSoCuoi, Thang, Nam)
VALUES (1, 'DH-001', 1000, 1025, 10, 2026);

-- UPDATE: cập nhật địa chỉ khách hàng
UPDATE KhachHang
SET DiaChi = N'Địa chỉ mới, Q.3, TP.HCM'
WHERE Id = 1;

-- DELETE: xóa thật (NGUY HIỂM — nên dùng soft delete thay thế)
DELETE FROM KhachHang WHERE Id = 5;

-- SOFT DELETE: xóa mềm (ĐÚNG cách — không mất dữ liệu)
UPDATE KhachHang SET DangHoatDong = 0 WHERE Id = 5;
```

**JOIN — lấy dữ liệu từ 2 bảng:**
```sql
-- INNER JOIN: lấy khách hàng CÓ chỉ số tháng 10
SELECT 
    kh.HoTen,
    kh.MaSoDongHo,
    cs.ChiSoDau,
    cs.ChiSoCuoi,
    (cs.ChiSoCuoi - cs.ChiSoDau) AS SoM3TieuThu,
    (cs.ChiSoCuoi - cs.ChiSoDau) * cs.DonGia AS ThanhTien
FROM KhachHang kh
INNER JOIN ChiSoDongHo cs ON kh.Id = cs.KhachHangId
WHERE cs.Thang = 10 AND cs.Nam = 2026;

-- LEFT JOIN: lấy TẤT CẢ khách hàng (kể cả chưa có chỉ số)
SELECT kh.HoTen, cs.ChiSoCuoi
FROM KhachHang kh
LEFT JOIN ChiSoDongHo cs ON kh.Id = cs.KhachHangId
    AND cs.Thang = 10;
```

### ✅ Bài tập hôm nay:
- [ ] Tạo bảng `ChiSoDongHo` với Foreign Key đúng
- [ ] Chèn 3 bản ghi chỉ số đồng hồ
- [ ] Viết câu INNER JOIN tính tiền nước tháng hiện tại
- [ ] Thực hành soft delete cho 1 khách hàng

---

## 🗓️ NGÀY 10 — ADO.NET Phần 1: Kết Nối + SELECT

### Mục tiêu: Viết C# kết nối SQL Server, đọc dữ liệu vào List

### ⭐ Đây là bài QUAN TRỌNG NHẤT — phải thuộc!

```csharp
// File: Data/DatabaseHelper.cs
using Microsoft.Data.SqlClient;
using CapNuocApp.Models;

public class DatabaseHelper
{
    // Connection string — đường dẫn kết nối database
    private static string _connectionString =
        @"Server=.\MSSQLSERVER1;Database=CapNuocDB;Trusted_Connection=true;TrustServerCertificate=true;";

    // Lấy toàn bộ danh sách khách hàng
    public static List<KhachHang> GetAllKhachHang()
    {
        var list = new List<KhachHang>(); // List rỗng để chứa kết quả

        // using = tự động đóng/giải phóng kết nối khi xong (quan trọng!)
        using var conn = new SqlConnection(_connectionString);
        conn.Open(); // Mở kết nối

        string sql = "SELECT * FROM KhachHang WHERE DangHoatDong = 1 ORDER BY HoTen";
        using var cmd = new SqlCommand(sql, conn); // Command chứa câu SQL
        using var reader = cmd.ExecuteReader();    // Thực thi SELECT, lấy reader

        // Duyệt từng dòng kết quả
        while (reader.Read())
        {
            list.Add(new KhachHang
            {
                Id          = (int)reader["Id"],
                MaKhachHang = reader["MaKhachHang"].ToString()!,
                HoTen       = reader["HoTen"].ToString()!,
                DiaChi      = reader["DiaChi"].ToString()!,
                SoDienThoai = reader["SoDienThoai"].ToString()!,
                Email       = reader["Email"].ToString()!,
                MaSoDongHo  = reader["MaSoDongHo"].ToString()!,
                NgayDangKy  = (DateTime)reader["NgayDangKy"],
                DangHoatDong = (bool)reader["DangHoatDong"]
            });
        }
        return list; // Trả về danh sách đã đọc
    }
}
```

**Sơ đồ luồng ADO.NET (phải thuộc lòng):**
```
SqlConnection (kết nối)
    ↓ .Open()
SqlCommand (câu lệnh SQL)
    ↓ .ExecuteReader()    → Dùng cho SELECT
    ↓ .ExecuteNonQuery()  → Dùng cho INSERT/UPDATE/DELETE
    ↓ .ExecuteScalar()    → Dùng cho COUNT/SUM (1 giá trị)
SqlDataReader
    ↓ .Read() (lặp từng dòng)
Object / List
```

### ✅ Bài tập hôm nay:
- [ ] Cài NuGet package: `Microsoft.Data.SqlClient` (Tools → NuGet → Manage)
- [ ] Tạo file `Data/DatabaseHelper.cs` với method `GetAllKhachHang()`
- [ ] Gọi trong Form và in ra MessageBox: "Có X khách hàng"

---

## 🗓️ NGÀY 11 — ADO.NET Phần 2: INSERT, UPDATE, DELETE + Parameterized Query

### Mục tiêu: Viết đầy đủ CRUD, hiểu và dùng Parameterized Query

```csharp
// THÊM khách hàng mới
public static bool ThemKhachHang(KhachHang kh)
{
    using var conn = new SqlConnection(_connectionString);
    conn.Open();

    // Dùng @thamso thay vì nối chuỗi — QUAN TRỌNG (chống SQL Injection)
    string sql = @"INSERT INTO KhachHang 
                   (MaKhachHang, HoTen, DiaChi, SoDienThoai, Email, MaSoDongHo)
                   VALUES (@Ma, @HoTen, @DiaChi, @SDT, @Email, @MaDH)";
    using var cmd = new SqlCommand(sql, conn);

    // Thêm giá trị cho từng tham số
    cmd.Parameters.AddWithValue("@Ma",    kh.MaKhachHang);
    cmd.Parameters.AddWithValue("@HoTen", kh.HoTen);
    cmd.Parameters.AddWithValue("@DiaChi",kh.DiaChi);
    cmd.Parameters.AddWithValue("@SDT",   kh.SoDienThoai);
    cmd.Parameters.AddWithValue("@Email", kh.Email ?? ""); // ?? = nếu null thì dùng ""
    cmd.Parameters.AddWithValue("@MaDH",  kh.MaSoDongHo);

    int soDoiTac = cmd.ExecuteNonQuery(); // Trả về số dòng bị thay đổi
    return soDoiTac > 0; // true nếu thêm thành công
}

// CẬP NHẬT thông tin
public static bool CapNhatKhachHang(KhachHang kh)
{
    using var conn = new SqlConnection(_connectionString);
    conn.Open();
    string sql = @"UPDATE KhachHang SET 
                   HoTen=@HoTen, DiaChi=@DiaChi, SoDienThoai=@SDT, Email=@Email
                   WHERE Id=@Id";
    using var cmd = new SqlCommand(sql, conn);
    cmd.Parameters.AddWithValue("@HoTen", kh.HoTen);
    cmd.Parameters.AddWithValue("@DiaChi",kh.DiaChi);
    cmd.Parameters.AddWithValue("@SDT",   kh.SoDienThoai);
    cmd.Parameters.AddWithValue("@Email", kh.Email ?? "");
    cmd.Parameters.AddWithValue("@Id",    kh.Id);
    return cmd.ExecuteNonQuery() > 0;
}

// XÓA MỀM
public static bool XoaKhachHang(int id)
{
    using var conn = new SqlConnection(_connectionString);
    conn.Open();
    string sql = "UPDATE KhachHang SET DangHoatDong = 0 WHERE Id = @Id";
    using var cmd = new SqlCommand(sql, conn);
    cmd.Parameters.AddWithValue("@Id", id);
    return cmd.ExecuteNonQuery() > 0;
}

// TÌM KIẾM với LIKE
public static List<KhachHang> SearchKhachHang(string keyword)
{
    var list = new List<KhachHang>();
    using var conn = new SqlConnection(_connectionString);
    conn.Open();
    string sql = @"SELECT * FROM KhachHang 
                   WHERE (HoTen LIKE @kw OR MaKhachHang LIKE @kw OR MaSoDongHo LIKE @kw)
                   AND DangHoatDong = 1 ORDER BY HoTen";
    using var cmd = new SqlCommand(sql, conn);
    cmd.Parameters.AddWithValue("@kw", $"%{keyword}%"); // % ở 2 đầu = tìm bất kỳ vị trí
    using var reader = cmd.ExecuteReader();
    while (reader.Read()) { list.Add(MapKhachHang(reader)); }
    return list;
}
```

### ✅ Bài tập hôm nay:
- [ ] Thêm đủ 4 method: Get, Them, CapNhat, Xoa vào DatabaseHelper
- [ ] Test từng method — gọi từ Form và kiểm tra DB trong SSMS
- [ ] Giải thích được: TẠI SAO KHÔNG nối chuỗi SQL trực tiếp?

---

## 🗓️ NGÀY 12 — SQL Nâng Cao: GROUP BY, Thống Kê

### Mục tiêu: Viết câu báo cáo, thống kê doanh thu

```sql
-- Thống kê tiêu thụ nước theo tháng
SELECT 
    Thang,
    Nam,
    COUNT(*) AS SoKhachHang,
    SUM(ChiSoCuoi - ChiSoDau) AS TongM3,
    SUM((ChiSoCuoi - ChiSoDau) * DonGia) AS TongDoanhThu
FROM ChiSoDongHo
GROUP BY Thang, Nam
ORDER BY Nam DESC, Thang DESC;

-- Tìm khách hàng dùng nhiều nhất
SELECT TOP 5
    kh.HoTen,
    SUM(cs.ChiSoCuoi - cs.ChiSoDau) AS TongM3
FROM KhachHang kh
INNER JOIN ChiSoDongHo cs ON kh.Id = cs.KhachHangId
WHERE cs.Nam = 2026
GROUP BY kh.Id, kh.HoTen
ORDER BY TongM3 DESC;

-- Khách hàng chưa có chỉ số tháng này
SELECT kh.HoTen, kh.MaSoDongHo
FROM KhachHang kh
LEFT JOIN ChiSoDongHo cs ON kh.Id = cs.KhachHangId
    AND cs.Thang = 10 AND cs.Nam = 2026
WHERE cs.Id IS NULL AND kh.DangHoatDong = 1;
```

---

## 🗓️ NGÀY 13 — Tạo Bảng HoaDon + Stored Procedure

```sql
-- Bảng hóa đơn
CREATE TABLE HoaDon (
    Id           INT IDENTITY(1,1) PRIMARY KEY,
    MaHoaDon     NVARCHAR(20)   NOT NULL UNIQUE,
    KhachHangId  INT            NOT NULL FOREIGN KEY REFERENCES KhachHang(Id),
    ChiSoId      INT            FOREIGN KEY REFERENCES ChiSoDongHo(Id),
    SoLuong      INT            NOT NULL,
    DonGia       DECIMAL(10,2)  NOT NULL,
    ThanhTien    DECIMAL(12,2)  NOT NULL,
    NgayLap      DATETIME       DEFAULT GETDATE(),
    HanThanhToan DATETIME,
    DaThanhToan  BIT            DEFAULT 0,
    NgayThanhToan DATETIME
);

-- Stored Procedure: tự động tạo hóa đơn từ chỉ số
CREATE PROCEDURE sp_TaoHoaDon
    @KhachHangId INT,
    @Thang INT,
    @Nam INT
AS
BEGIN
    DECLARE @ChiSoId INT, @SoLuong INT, @DonGia DECIMAL(10,2), @ThanhTien DECIMAL(12,2)
    
    SELECT @ChiSoId = Id, 
           @SoLuong = ChiSoCuoi - ChiSoDau,
           @DonGia = DonGia,
           @ThanhTien = (ChiSoCuoi - ChiSoDau) * DonGia
    FROM ChiSoDongHo
    WHERE KhachHangId = @KhachHangId AND Thang = @Thang AND Nam = @Nam
    
    INSERT INTO HoaDon (MaHoaDon, KhachHangId, ChiSoId, SoLuong, DonGia, ThanhTien, HanThanhToan)
    VALUES (
        'HD-' + FORMAT(@Thang,'00') + '-' + CAST(@KhachHangId AS NVARCHAR),
        @KhachHangId, @ChiSoId, @SoLuong, @DonGia, @ThanhTien,
        DATEADD(day, 15, GETDATE())
    )
END
```

---

## 🗓️ NGÀY 14 — Ôn Tập Tuần 2 + Viết Lại DatabaseHelper Hoàn Chỉnh

### ✅ Checklist ôn tập:
- [ ] Thuộc luồng ADO.NET: Connection → Command → Execute → Reader
- [ ] Giải thích được Parameterized Query vs nối chuỗi
- [ ] Viết được SELECT, INSERT, UPDATE có WHERE, INNER JOIN
- [ ] Giải thích Soft Delete và tại sao dùng
- [ ] Thêm method `GetChiSoTheoThang(int thang, int nam)` vào DatabaseHelper

---

# 📙 TUẦN 3: WINFORMS — GIAO DIỆN DESKTOP (Ngày 15–21)

---

## 🗓️ NGÀY 15 — Form Cơ Bản + Controls

### Mục tiêu: Tạo Form, thêm Button, TextBox, Label

```csharp
// Form chính — tạo bằng code (không dùng Designer)
public class FormKhachHang : Form
{
    // Khai báo controls
    private DataGridView dgvKhachHang;
    private TextBox txtTimKiem;
    private Button btnThem, btnSua, btnXoa, btnTimKiem;
    private Label lblTongSo;

    public FormKhachHang()
    {
        InitializeComponent(); // Khởi tạo controls
        ConfigureDataGridView(); // Cấu hình bảng
    }

    private void InitializeComponent()
    {
        // Tạo controls
        this.dgvKhachHang = new DataGridView();
        this.txtTimKiem = new TextBox();
        this.btnThem = new Button();

        // Cấu hình Form
        this.Text = "💧 Quản Lý Khách Hàng — Cấp Nước Bến Thành";
        this.Size = new Size(900, 580);
        this.StartPosition = FormStartPosition.CenterScreen;

        // Cấu hình Button
        btnThem.Text = "+ Thêm";
        btnThem.Size = new Size(90, 34);
        btnThem.BackColor = Color.FromArgb(76, 175, 80); // Màu xanh lá
        btnThem.ForeColor = Color.White;
        btnThem.FlatStyle = FlatStyle.Flat;
        btnThem.Click += btnThem_Click; // Gán sự kiện

        // Thêm controls vào Form
        this.Controls.Add(btnThem);
        this.Controls.Add(txtTimKiem);
        this.Controls.Add(dgvKhachHang);
    }
}
```

---

## 🗓️ NGÀY 16 — DataGridView + Data Binding

### Mục tiêu: Hiển thị danh sách khách hàng lên DataGridView

```csharp
private void ConfigureDataGridView()
{
    dgvKhachHang.AutoGenerateColumns = false; // Tự chọn cột hiển thị
    dgvKhachHang.Columns.Clear();

    // Thêm cột — DataPropertyName phải trùng tên property trong class KhachHang
    dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn
    {
        DataPropertyName = "MaKhachHang", // Tên property
        HeaderText = "Mã KH",             // Tiêu đề cột
        Width = 80
    });
    dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn
    {
        DataPropertyName = "HoTen",
        HeaderText = "Họ Tên",
        Width = 180
    });
    // ... thêm các cột khác

    dgvKhachHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Chọn cả dòng
    dgvKhachHang.ReadOnly = true; // Không cho sửa trực tiếp
    dgvKhachHang.Dock = DockStyle.Fill; // Chiếm toàn bộ không gian còn lại
}

private void LoadDanhSachKhachHang()
{
    try
    {
        var danhSach = DatabaseHelper.GetAllKhachHang();
        dgvKhachHang.DataSource = null;      // Reset trước
        dgvKhachHang.DataSource = danhSach;  // Bind dữ liệu
        lblTongSo.Text = $"Tổng: {danhSach.Count} khách hàng";
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi kết nối",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
```

---

## 🗓️ NGÀY 17 — Form Thêm/Sửa + Validation

```csharp
public class FormThemSuaKhachHang : Form
{
    private KhachHang? _khachHang; // null = thêm mới; có giá trị = sửa

    // Constructor thêm mới
    public FormThemSuaKhachHang()
    {
        InitializeComponent();
        this.Text = "➕ Thêm Khách Hàng";
    }

    // Constructor sửa (nhận object cần sửa)
    public FormThemSuaKhachHang(KhachHang kh)
    {
        InitializeComponent();
        _khachHang = kh;
        this.Text = "✏ Sửa Khách Hàng";
        // Điền dữ liệu vào TextBox
        txtHoTen.Text = kh.HoTen;
        txtDiaChi.Text = kh.DiaChi;
        txtSDT.Text = kh.SoDienThoai;
    }

    private void btnLuu_Click(object sender, EventArgs e)
    {
        // Validation — kiểm tra dữ liệu trước khi lưu
        if (string.IsNullOrWhiteSpace(txtHoTen.Text))
        {
            MessageBox.Show("Vui lòng nhập Họ Tên!", "Thiếu thông tin",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtHoTen.Focus(); // Đặt con trỏ vào ô bị thiếu
            return;
        }

        try
        {
            if (_khachHang == null)
            {
                // Thêm mới
                var kh = new KhachHang { HoTen = txtHoTen.Text.Trim(), /* ... */ };
                DatabaseHelper.ThemKhachHang(kh);
            }
            else
            {
                // Cập nhật
                _khachHang.HoTen = txtHoTen.Text.Trim();
                DatabaseHelper.CapNhatKhachHang(_khachHang);
            }
            this.DialogResult = DialogResult.OK; // Báo thành công cho form cha
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi");
        }
    }
}
```

---

## 🗓️ NGÀY 18–19 — Kết Nối Form với Database (Tích Hợp)

### Mục tiêu: Hoàn thiện luồng CRUD đầy đủ trên giao diện

```csharp
// Nút Thêm — mở form con
private void btnThem_Click(object sender, EventArgs e)
{
    var formThem = new FormThemSuaKhachHang();
    // ShowDialog = mở dạng modal (chặn form cha)
    if (formThem.ShowDialog() == DialogResult.OK)
    {
        LoadDanhSachKhachHang(); // Reload danh sách sau khi thêm
        MessageBox.Show("Thêm thành công!", "Thông báo",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}

// Nút Sửa
private void btnSua_Click(object sender, EventArgs e)
{
    if (dgvKhachHang.SelectedRows.Count == 0)
    {
        MessageBox.Show("Vui lòng chọn khách hàng cần sửa!", "Chưa chọn");
        return;
    }
    // Lấy object từ dòng đang chọn
    var kh = (KhachHang)dgvKhachHang.SelectedRows[0].DataBoundItem;
    var formSua = new FormThemSuaKhachHang(kh); // Truyền object vào
    if (formSua.ShowDialog() == DialogResult.OK)
        LoadDanhSachKhachHang();
}

// Nút Xóa
private void btnXoa_Click(object sender, EventArgs e)
{
    if (dgvKhachHang.SelectedRows.Count == 0) { return; }
    var kh = (KhachHang)dgvKhachHang.SelectedRows[0].DataBoundItem;

    var confirm = MessageBox.Show(
        $"Xóa khách hàng {kh.HoTen}?", "Xác nhận",
        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

    if (confirm == DialogResult.Yes)
    {
        DatabaseHelper.XoaKhachHang(kh.Id);
        LoadDanhSachKhachHang();
    }
}
```

---

## 🗓️ NGÀY 20 — Panel Layout + Thiết Kế Giao Diện Đẹp

```csharp
// Dùng Panel để chia vùng giao diện
var panelTop = new Panel
{
    Dock = DockStyle.Top,
    Height = 55,
    BackColor = Color.FromArgb(30, 136, 229) // Màu xanh dương
};
var panelBottom = new Panel
{
    Dock = DockStyle.Bottom,
    Height = 50,
    BackColor = Color.FromArgb(245, 245, 245) // Màu xám nhạt
};

// Font chuyên nghiệp
this.Font = new Font("Segoe UI", 9.5f);

// Màu sắc nút
btnThem.BackColor = Color.FromArgb(76, 175, 80);   // Xanh lá
btnSua.BackColor  = Color.FromArgb(255, 152, 0);   // Cam
btnXoa.BackColor  = Color.FromArgb(244, 67, 54);   // Đỏ
btnTimKiem.BackColor = Color.White;

// DataGridView đẹp
dgvKhachHang.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
{
    BackColor = Color.FromArgb(30, 136, 229),
    ForeColor = Color.White,
    Font = new Font("Segoe UI", 10, FontStyle.Bold)
};
dgvKhachHang.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
{
    BackColor = Color.FromArgb(245, 248, 255) // Xen kẽ màu nhạt
};
```

---

## 🗓️ NGÀY 21 — Ôn Tập Tuần 3 + Thêm Tính Năng Tìm Kiếm

```csharp
// Tìm kiếm khi nhấn Enter
private void txtTimKiem_KeyPress(object sender, KeyPressEventArgs e)
{
    if (e.KeyChar == (char)Keys.Enter)
        btnTimKiem_Click(sender, e); // Gọi hàm tìm kiếm
}

// Tìm kiếm
private void btnTimKiem_Click(object sender, EventArgs e)
{
    string keyword = txtTimKiem.Text.Trim();
    if (string.IsNullOrEmpty(keyword))
    {
        LoadDanhSachKhachHang(); // Nếu rỗng → load lại tất cả
        return;
    }
    var ketQua = DatabaseHelper.SearchKhachHang(keyword);
    dgvKhachHang.DataSource = ketQua;
    lblTongSo.Text = $"Tìm thấy: {ketQua.Count} kết quả";
}
```

---

# 📕 TUẦN 4: TỔNG HỢP + LUYỆN PHỎNG VẤN (Ngày 22–30)

---

## 🗓️ NGÀY 22–24 — Viết Lại Toàn Bộ Project Từ Đầu (Không Nhìn Tài Liệu)

### Thứ tự viết lại:
1. `Models/KhachHang.cs` — 10 phút
2. `Models/ChiSoDongHo.cs` — 10 phút
3. `Database/create_database.sql` — 20 phút (tạo 3 bảng)
4. `Data/DatabaseHelper.cs` — 45 phút (CRUD + Search)
5. `Forms/FormKhachHang.cs` — 60 phút (giao diện chính)
6. `Forms/FormThemSuaKhachHang.cs` — 45 phút (thêm/sửa)
7. `Program.cs` — 2 phút

> Nếu bị kẹt mới được mở lại tài liệu. Mục tiêu là tự viết được!

---

## 🗓️ NGÀY 25–26 — Thêm Tính Năng: Quản Lý Chỉ Số Đồng Hồ

### Tạo Form mới: FormChiSoDongHo
- [ ] Hiển thị danh sách chỉ số theo tháng
- [ ] Nhập chỉ số đầu kỳ / cuối kỳ
- [ ] Tự động tính tiền và tạo hóa đơn

---

## 🗓️ NGÀY 27 — Ôn Tập Câu Hỏi Phỏng Vấn

### ❓ 15 Câu hỏi hay gặp nhất — phải trả lời được:

1. **ADO.NET là gì? Các class chính trong ADO.NET?**
   > SqlConnection, SqlCommand, SqlDataReader, SqlDataAdapter

2. **Sự khác nhau giữa ExecuteReader, ExecuteNonQuery, ExecuteScalar?**
   > Reader=SELECT nhiều dòng | NonQuery=INSERT/UPDATE/DELETE | Scalar=1 giá trị

3. **Parameterized Query là gì? Tại sao quan trọng?**
   > Dùng @param thay vì nối chuỗi — chống SQL Injection

4. **SQL Injection là gì?**
   > Lỗ hổng bảo mật khi input độc hại được nhúng vào SQL

5. **`using` keyword trong C# dùng để làm gì?**
   > Tự động gọi Dispose() — đóng connection, giải phóng bộ nhớ

6. **Soft Delete là gì? Tại sao dùng thay vì DELETE?**
   > Set cột DangHoatDong=0 thay vì xóa thật — giữ lịch sử, tránh vi phạm FK

7. **INNER JOIN và LEFT JOIN khác nhau thế nào?**
   > INNER=chỉ lấy khớp cả 2 bảng | LEFT=lấy tất cả bên trái, NULL nếu không khớp

8. **Property và Field khác nhau?**
   > Field=biến trực tiếp | Property={get;set;} có thể thêm logic

9. **Constructor là gì? Constructor Overloading?**
   > Hàm khởi tạo, chạy khi new. Overloading=nhiều constructor khác tham số

10. **List<T> là gì? Kể các method hay dùng?**
    > Add, Remove, Count, Contains, Where (LINQ), OrderBy (LINQ)

11. **DataGridView.DataSource hoạt động thế nào?**
    > Bind List<T> — tự hiển thị data, cập nhật khi gán lại

12. **ShowDialog() khác Application.Run() thế nào?**
    > ShowDialog=modal (chặn form cha) | Run=chạy form chính

13. **Tại sao dùng NVARCHAR thay VARCHAR?**
    > NVARCHAR lưu Unicode — hỗ trợ tiếng Việt

14. **IDENTITY(1,1) là gì?**
    > Tự tăng: bắt đầu từ 1, mỗi lần tăng thêm 1

15. **Computed Property (expression body) là gì?**
    > `public int SoLuong => ChiSoCuoi - ChiSoDau;` — tính toán khi đọc, không lưu DB

---

## 🗓️ NGÀY 28 — Giả Lập Phỏng Vấn

> Nhờ bạn bè hoặc nhờ AI hỏi lần lượt 15 câu trên.  
> Trả lời bằng lời nói, không nhìn tài liệu.  
> Ghi lại câu nào còn yếu để ôn lại.

---

## 🗓️ NGÀY 29 — Dọn Dẹp Project + Git

```bash
# Các lệnh Git cơ bản cần biết
git init                    # Khởi tạo repo
git add .                   # Thêm tất cả file vào staging
git commit -m "Mô tả thay đổi"  # Lưu snapshot
git status                  # Xem trạng thái hiện tại
git log --oneline           # Xem lịch sử commit
git diff                    # Xem thay đổi chưa commit
```

### ✅ Checklist project:
- [ ] Code có comment tiếng Việt đầy đủ
- [ ] Tất cả thao tác DB đều bọc try-catch
- [ ] Validation input đầy đủ (không để null/rỗng)
- [ ] Xóa mềm (không xóa thật)
- [ ] Connection string đúng

---

## 🗓️ NGÀY 30 — Ngày Cuối: Tổng Ôn Tập

### ✅ Checklist cuối cùng trước phỏng vấn:

**C# cần thuộc:**
- [ ] Thuộc 3 phương thức Execute của SqlCommand
- [ ] Giải thích được Parameterized Query + SQL Injection
- [ ] Hiểu `using` và tại sao cần thiết
- [ ] Constructor overloading — cách dùng 2 constructor (thêm/sửa)
- [ ] Soft Delete — giải thích được lý do

**SQL cần thuộc:**
- [ ] SELECT + WHERE + ORDER BY + LIKE
- [ ] INSERT INTO VALUES (...)
- [ ] UPDATE SET ... WHERE Id = ...
- [ ] INNER JOIN / LEFT JOIN
- [ ] GROUP BY + SUM / COUNT

**WinForms cần thuộc:**
- [ ] DataGridView bind List<T> + lấy selected row
- [ ] ShowDialog() + DialogResult
- [ ] Event handling: Click, Load, KeyPress

**Câu hỏi hỏi ngược nhà tuyển dụng:**
- [ ] "Stack công nghệ đang dùng là gì? C# phiên bản mấy?"
- [ ] "Team có dùng Entity Framework hay ADO.NET thuần?"
- [ ] "Quy trình onboarding cho nhân viên mới thế nào?"

---

## 💡 HƯỚNG DẪN SỬ DỤNG FILE NÀY

1. **Mỗi ngày:** Đọc bài tương ứng → code theo → tick [ ] hoàn thành
2. **Bị kẹt:** Hỏi AI: *"Giải thích cho tôi phần [TÊN PHẦN] trong README"*
3. **Muốn thực hành thêm:** Hỏi AI: *"Ra cho tôi 5 bài tập về [TÊN CHỦ ĐỀ]"*
4. **Trước phỏng vấn:** Hỏi AI: *"Hỏi tôi 10 câu phỏng vấn về ADO.NET"*

---

## 📞 THÔNG TIN PROJECT

- **Ứng viên:** thanh72044
- **Vị trí:** Lập trình viên
- **Công ty:** Cấp Nước Bến Thành
- **Tech stack:** C# .NET 10 | WinForms | SQL Server | ADO.NET

---
*Chúc bạn phỏng vấn thành công! 💪*
