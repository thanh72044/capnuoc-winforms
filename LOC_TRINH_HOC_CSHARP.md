# 📘 LỘ TRÌNH HỌC C# TOÀN DIỆN — CẤP NƯỚC BẾN THÀNH
> File này note lại **TOÀN BỘ** kiến thức C# cần học theo thứ tự
> Đọc từ trên xuống — mỗi phần build trên phần trước

---

# GIAI ĐOẠN 1: C# CĂN BẢN (Tuần 1)

---

## 📌 BƯỚC 1: Cấu Trúc Chương Trình C#

```csharp
// Mỗi file .cs có cấu trúc như sau:

// 1. using — import thư viện cần dùng
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

// 2. namespace — nhóm chứa code (giống folder)
namespace CapNuocApp.Models
{
    // 3. class — bản thiết kế
    public class KhachHang
    {
        // 4. properties, fields, methods...
    }
}
```

**Quy tắc đặt tên (phải theo đúng):**
| Loại | Quy tắc | Ví dụ |
|------|---------|-------|
| Class | PascalCase | `KhachHang`, `DatabaseHelper` |
| Method | PascalCase | `GetAllKhachHang()`, `TinhTienNuoc()` |
| Property | PascalCase | `HoTen`, `SoDienThoai` |
| Variable (local) | camelCase | `tenKhachHang`, `soM3` |
| Private field | _camelCase | `_connectionString`, `_khachHang` |
| Constant | UPPER_CASE | `DON_GIA_MAC_DINH` |

---

## 📌 BƯỚC 2: Kiểu Dữ Liệu (Data Types)

```csharp
// ===== KIỂU SỐ =====
int soNguyen = 1500;           // -2 tỷ đến 2 tỷ → dùng cho: Id, chỉ số, tháng, năm
long soLon = 9_999_999_999L;   // Số rất lớn (L ở cuối)
double soThapPhan = 3.14;      // KHÔNG dùng cho tiền (mất chính xác)
decimal tienTe = 75_000.50m;   // ✅ DÙNG cho tiền (m ở cuối) — chính xác nhất
float soNho = 3.14f;           // Ít dùng (f ở cuối)

// ===== KIỂU VĂN BẢN =====
string ten = "Nguyễn Văn An";  // Chuỗi ký tự
char kyTu = 'A';               // 1 ký tự duy nhất

// ===== KIỂU ĐÚNG/SAI =====
bool dangHoatDong = true;      // true hoặc false → tương ứng BIT trong SQL

// ===== KIỂU NGÀY GIỜ =====
DateTime ngayHienTai = DateTime.Now;        // Ngày + giờ hiện tại
DateTime ngayMai = DateTime.Today.AddDays(1); // Ngày mai
string chuoiNgay = DateTime.Now.ToString("dd/MM/yyyy"); // "20/10/2026"
string chuoiGio  = DateTime.Now.ToString("HH:mm:ss");   // "22:57:00"

// ===== NULLABLE — cho phép null =====
int? maybeId = null;           // int? = có thể null
string? maybeEmail = null;     // string? = có thể null
```

**Chuyển đổi kiểu dữ liệu:**
```csharp
// String → Int
string chuoi = "1500";
int soNguyen = int.Parse(chuoi);          // Lỗi nếu không phải số
int soNguyen2 = Convert.ToInt32(chuoi);   // An toàn hơn một chút
bool ok = int.TryParse(chuoi, out int ketQua); // AN TOÀN NHẤT — không throw exception

// Int → String
int so = 1500;
string s = so.ToString();                 // "1500"
string s2 = so.ToString("N0");            // "1,500" (có dấu phẩy)

// Decimal format tiền
decimal tien = 75000m;
string tienFormat = tien.ToString("N0");  // "75,000"
string tienFormat2 = tien.ToString("C0"); // "₫75,000" (có ký hiệu tiền)
```

---

## 📌 BƯỚC 3: Khai Báo Biến + var

```csharp
// Cách 1: Khai báo rõ kiểu
int soKhachHang = 100;
string hoTen = "Nguyễn An";
List<KhachHang> danhSach = new List<KhachHang>();

// Cách 2: var — C# tự suy luận kiểu (type inference)
var soKhachHang = 100;          // C# biết là int
var hoTen = "Nguyễn An";        // C# biết là string
var danhSach = new List<KhachHang>(); // C# biết là List<KhachHang>

// ❌ Không dùng var khi không rõ kiểu
var ketQua = DatabaseHelper.GetData(); // Không biết trả về gì

// ✅ Dùng var khi kiểu rõ ràng từ vế phải
var conn = new SqlConnection(connStr); // Rõ ràng là SqlConnection
```

---

## 📌 BƯỚC 4: String — Xử Lý Chuỗi

```csharp
string hoTen = "  Nguyễn Văn An  ";

// Các method thường dùng
string trimmed = hoTen.Trim();          // Xóa khoảng trắng 2 đầu: "Nguyễn Văn An"
string upper = hoTen.ToUpper();         // CHỮ HOA
string lower = hoTen.ToLower();         // chữ thường
bool isEmpty = string.IsNullOrWhiteSpace(hoTen); // Kiểm tra rỗng/null/khoảng trắng
bool contains = hoTen.Contains("Văn"); // Có chứa "Văn" không?
string replaced = hoTen.Replace("An", "Anh"); // Thay thế
int length = hoTen.Length;             // Độ dài chuỗi

// String interpolation (DÙNG NHIỀU NHẤT)
string ma = "KH-001";
int soLuong = 25;
decimal tien = 175000m;
string thongBao = $"Khách {ma} dùng {soLuong}m³, tiền: {tien:N0}đ";
// Kết quả: "Khách KH-001 dùng 25m³, tiền: 175,000đ"

// Verbatim string — viết SQL dài nhiều dòng
string sql = @"SELECT kh.HoTen, cs.ChiSoCuoi
               FROM KhachHang kh
               INNER JOIN ChiSoDongHo cs ON kh.Id = cs.KhachHangId
               WHERE cs.Thang = 10";
```

---

## 📌 BƯỚC 5: Câu Lệnh Điều Kiện

```csharp
// ===== IF-ELSE =====
int soM3 = 25;
decimal donGia;

if (soM3 <= 10)
    donGia = 5973m;
else if (soM3 <= 20)
    donGia = 7052m;
else if (soM3 <= 30)
    donGia = 8669m;
else
    donGia = 15929m;

// ===== TERNARY — viết ngắn gọn (1 dòng) =====
string trangThai = dangHoatDong ? "Đang dùng" : "Đã ngưng";
// Tương đương: if (dangHoatDong) "Đang dùng" else "Đã ngưng"

// ===== SWITCH =====
int thang = DateTime.Now.Month;
string quy = thang switch
{
    1 or 2 or 3 => "Quý 1",
    4 or 5 or 6 => "Quý 2",
    7 or 8 or 9 => "Quý 3",
    _ => "Quý 4"             // _ = default (tất cả trường hợp còn lại)
};

// ===== NULL CHECK =====
string? email = null;
// Cách 1: if
if (email != null) Console.WriteLine(email);
// Cách 2: ?? (null-coalescing) — nếu null thì dùng giá trị mặc định
string emailHienThi = email ?? "Chưa có email";
// Cách 3: ?. (null-conditional) — nếu null thì trả null thay vì crash
int? emailLength = email?.Length; // null nếu email null
```

---

## 📌 BƯỚC 6: Vòng Lặp

```csharp
// ===== FOR — khi biết số lần lặp =====
for (int i = 1; i <= 12; i++)
{
    Console.WriteLine($"Tháng {i}");
}

// ===== FOREACH — duyệt qua collection (DÙNG NHIỀU NHẤT) =====
var danhSachKH = DatabaseHelper.GetAllKhachHang();
foreach (var kh in danhSachKH)
{
    Console.WriteLine($"{kh.MaKhachHang} - {kh.HoTen}");
}

// ===== WHILE — khi không biết số lần lặp =====
while (reader.Read())        // Đọc từng dòng từ SQL
{
    // Xử lý từng dòng
}

// ===== BREAK và CONTINUE =====
foreach (var kh in danhSachKH)
{
    if (!kh.DangHoatDong) continue; // Bỏ qua, tiếp tục vòng lặp tiếp theo
    if (kh.MaKhachHang == "KH-999") break; // Dừng hẳn vòng lặp
    Console.WriteLine(kh.HoTen);
}
```

---

## 📌 BƯỚC 7: Method (Hàm)

```csharp
public class TinhToanHelper
{
    // ===== Method cơ bản =====
    // [access] [static] KieuTraVe TenMethod(thamSo)
    
    // Hàm tính tiền nước — có tham số, có trả về
    public static decimal TinhTienNuoc(int soM3, decimal donGia)
    {
        return soM3 * donGia;
    }

    // Hàm không trả về — dùng void
    public static void HienThiLoi(string noiDung)
    {
        MessageBox.Show(noiDung, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    // Hàm tham số mặc định (optional parameter)
    public static decimal TinhTienNuocMacDinh(int soM3, decimal donGia = 7000m)
    {
        return soM3 * donGia;
    }
    // Gọi: TinhTienNuocMacDinh(25)         → 25 * 7000 = 175,000
    // Gọi: TinhTienNuocMacDinh(25, 9000m)  → 25 * 9000 = 225,000

    // Hàm trả về bool — kiểm tra hợp lệ
    public static bool KiemTraSoDT(string sdt)
    {
        if (string.IsNullOrWhiteSpace(sdt)) return false;
        if (sdt.Length != 10) return false;
        return sdt.All(char.IsDigit); // Tất cả ký tự đều là số?
    }

    // Out parameter — trả về nhiều giá trị
    public static bool ThuPhepChia(int a, int b, out int ketQua)
    {
        if (b == 0) { ketQua = 0; return false; }
        ketQua = a / b;
        return true;
    }
    // Gọi: if (ThuPhepChia(10, 2, out int kq)) Console.WriteLine(kq);

    // Expression body — viết ngắn (hàm 1 dòng)
    public static decimal TinhThanhTien(int soM3, decimal donGia) => soM3 * donGia;
}
```

---

# GIAI ĐOẠN 2: OOP — LẬP TRÌNH HƯỚNG ĐỐI TƯỢNG (Tuần 1-2)

---

## 📌 BƯỚC 8: Class và Object

```csharp
// ===== ĐỊNH NGHĨA CLASS =====
public class KhachHang
{
    // --- FIELDS (biến private, lưu trữ) ---
    private string _maKhachHang = string.Empty;

    // --- PROPERTIES (đọc/ghi có kiểm soát) ---
    
    // Auto-property: get;set; — C# tự tạo field bên trong
    public int Id { get; set; }
    public string HoTen { get; set; } = string.Empty;
    public string DiaChi { get; set; } = string.Empty;
    public string SoDienThoai { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string MaSoDongHo { get; set; } = string.Empty;
    public DateTime NgayDangKy { get; set; } = DateTime.Now;
    public bool DangHoatDong { get; set; } = true;

    // Read-only property — chỉ get, không set
    public string ThongTinNgan => $"{MaKhachHang} - {HoTen}";

    // Property với validation trong setter
    public string MaKhachHang
    {
        get => _maKhachHang;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Mã KH không được rỗng!");
            _maKhachHang = value.Trim().ToUpper();
        }
    }

    // --- CONSTRUCTOR (hàm khởi tạo) ---
    // Constructor không tham số
    public KhachHang() { }

    // Constructor có tham số
    public KhachHang(string maKH, string hoTen, string diaChi)
    {
        MaKhachHang = maKH;
        HoTen = hoTen;
        DiaChi = diaChi;
        NgayDangKy = DateTime.Now;
        DangHoatDong = true;
    }

    // --- METHODS ---
    public string TaoMaKhachHang()
    {
        return $"KH-{DateTime.Now:yyyyMMdd}-{new Random().Next(100, 999)}";
    }

    // Override ToString — khi print object sẽ in thông tin này
    public override string ToString()
    {
        return $"[{MaKhachHang}] {HoTen} - {SoDienThoai}";
    }
}

// ===== CÁCH TẠO VÀ DÙNG OBJECT =====
// Cách 1: Constructor thường
var kh1 = new KhachHang("KH-001", "Nguyễn An", "Q.1");

// Cách 2: Object initializer (phổ biến nhất)
var kh2 = new KhachHang
{
    MaKhachHang = "KH-002",
    HoTen = "Trần Bình",
    DiaChi = "Q.3",
    SoDienThoai = "0912345678"
};

// Đọc property
Console.WriteLine(kh2.HoTen);     // "Trần Bình"
Console.WriteLine(kh2.ToString()); // "[KH-002] Trần Bình - 0912345678"

// Thay đổi property
kh2.DiaChi = "Địa chỉ mới";
```

---

## 📌 BƯỚC 9: Computed Property + Expression Body

```csharp
public class ChiSoDongHo
{
    public int ChiSoDau { get; set; }
    public int ChiSoCuoi { get; set; }
    public decimal DonGia { get; set; } = 7000m;

    // ===== COMPUTED PROPERTY — tự tính, KHÔNG lưu vào DB =====
    // Mỗi lần đọc, nó tính lại từ các property khác

    // Dạng expression body (ngắn gọn)
    public int SoLuongTieuThu => ChiSoCuoi - ChiSoDau;
    public decimal ThanhTien => SoLuongTieuThu * DonGia;

    // Dạng đầy đủ (tương đương)
    public int SoLuongTieuThuDayDu
    {
        get { return ChiSoCuoi - ChiSoDau; }
    }

    // Computed property phức tạp hơn
    public string MoTaTieuThu
    {
        get
        {
            int sl = SoLuongTieuThu;
            if (sl <= 10) return $"{sl}m³ — Mức thấp";
            if (sl <= 20) return $"{sl}m³ — Mức trung bình";
            return $"{sl}m³ — Mức cao";
        }
    }
}

// Test:
var chiSo = new ChiSoDongHo { ChiSoDau = 1000, ChiSoCuoi = 1025, DonGia = 7000m };
Console.WriteLine(chiSo.SoLuongTieuThu); // 25
Console.WriteLine(chiSo.ThanhTien);       // 175000
Console.WriteLine(chiSo.MoTaTieuThu);     // "25m³ — Mức cao"
```

---

## 📌 BƯỚC 10: Static vs Instance

```csharp
public class DatabaseHelper
{
    // ===== STATIC — thuộc về CLASS, không cần new =====
    private static string _connectionString = "Server=..."; // static field
    
    public static List<KhachHang> GetAllKhachHang() { ... } // static method
    public static bool ThemKhachHang(KhachHang kh) { ... }

    // Gọi: DatabaseHelper.GetAllKhachHang()  ← không cần new DatabaseHelper()
}

public class FormKhachHang : Form
{
    // ===== INSTANCE — thuộc về từng OBJECT cụ thể =====
    private List<KhachHang> _danhSach; // instance field

    private void LoadDanhSach() { ... } // instance method
    
    // Gọi từ bên trong class: this.LoadDanhSach() hoặc LoadDanhSach()
}

// Khi nào dùng static?
// → Utility/Helper class (chỉ chứa hàm, không có state)
// → Factory method (tạo object)
// → Hằng số (const, static readonly)

// Khi nào dùng instance?
// → Form (mỗi form có state riêng)
// → Model (mỗi KhachHang có data riêng)
```

---

## 📌 BƯỚC 11: Access Modifiers

```csharp
public class Vi_Du_Access
{
    public string CongKhai { get; set; }     // Truy cập từ BẤT KỲ đâu
    private string RiengTu { get; set; }     // Chỉ trong class này
    protected string KeThua { get; set; }    // Trong class này + class con
    internal string NoiBo { get; set; }      // Trong cùng assembly (.dll/.exe)
    
    // Setter private — đọc được từ ngoài nhưng KHÔNG GÁN được từ ngoài
    public string ChiDocTuNgoai { get; private set; } = "Mặc định";
}

// Best practices trong project cấp nước:
public class KhachHang           // public — class dùng ở nhiều nơi
{
    public int Id { get; set; }  // public — Form cần đọc/ghi
    private string _cache;        // private — chỉ dùng nội bộ
}

public class DatabaseHelper      // public — Form gọi methods
{
    private static string _conn; // private — không ai cần biết connection string
    public static List<KhachHang> GetAll() { ... } // public — Form gọi
    private static KhachHang Map(SqlDataReader r) { ... } // private — chỉ dùng nội bộ
}
```

---

## 📌 BƯỚC 12: Inheritance (Kế Thừa) — Cơ Bản

```csharp
// BASE CLASS — lớp cha
public class NguoiDung
{
    public int Id { get; set; }
    public string HoTen { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    
    public virtual string GetThongTin() // virtual = cho phép override
    {
        return $"Người dùng: {HoTen}";
    }
}

// DERIVED CLASS — lớp con (kế thừa từ NguoiDung)
public class KhachHang : NguoiDung  // : = kế thừa
{
    public string MaSoDongHo { get; set; } = string.Empty;
    
    // override — ghi đè method của lớp cha
    public override string GetThongTin()
    {
        return $"Khách hàng: {HoTen} - Đồng hồ: {MaSoDongHo}";
    }
}

public class NhanVien : NguoiDung
{
    public string MaNhanVien { get; set; } = string.Empty;
    
    public override string GetThongTin()
    {
        return $"Nhân viên: {HoTen} - Mã: {MaNhanVien}";
    }
}

// Trong WinForms: Form kế thừa từ Form base
public class FormKhachHang : Form  // Form cũng là kế thừa!
{
    // FormKhachHang có tất cả tính năng của Form + thêm code của mình
}
```

---

# GIAI ĐOẠN 3: COLLECTIONS + LINQ (Tuần 2)

---

## 📌 BƯỚC 13: List<T> — Danh Sách Động

```csharp
// ===== TẠO LIST =====
var danhSach = new List<KhachHang>();          // List rỗng
var danhSach2 = new List<string> { "An", "Bình", "Cường" }; // Có sẵn

// ===== THÊM / XÓA =====
danhSach.Add(new KhachHang { HoTen = "Nguyễn An" });  // Thêm 1 phần tử
danhSach.AddRange(danhSachKhac);                        // Thêm nhiều từ list khác
danhSach.Remove(khachHang1);                            // Xóa phần tử cụ thể
danhSach.RemoveAt(0);                                   // Xóa theo vị trí (index)
danhSach.Clear();                                       // Xóa tất cả

// ===== ĐỌC =====
int soLuong = danhSach.Count;                           // Số phần tử
var kh = danhSach[0];                                   // Lấy phần tử đầu tiên (index 0)
bool coChua = danhSach.Contains(khachHang1);            // Có chứa không?

// ===== DUYỆT =====
foreach (var kh in danhSach)
{
    Console.WriteLine(kh.HoTen);
}
```

---

## 📌 BƯỚC 14: LINQ — Truy Vấn Dữ Liệu Trong C#

```csharp
// LINQ = Language Integrated Query — viết truy vấn giống SQL trong C#
// Dùng cho: lọc, sắp xếp, tìm kiếm, tổng hợp List/Array

var dsKH = DatabaseHelper.GetAllKhachHang(); // List<KhachHang>

// ===== WHERE — lọc theo điều kiện =====
var dangHoatDong = dsKH.Where(kh => kh.DangHoatDong == true).ToList();
var khachHangQ1  = dsKH.Where(kh => kh.DiaChi.Contains("Q.1")).ToList();

// ===== ORDER BY — sắp xếp =====
var sapXepTen   = dsKH.OrderBy(kh => kh.HoTen).ToList();          // A→Z
var sapXepTenGiamDan = dsKH.OrderByDescending(kh => kh.HoTen).ToList(); // Z→A
var sapXepNhieu = dsKH.OrderBy(kh => kh.DiaChi)
                       .ThenBy(kh => kh.HoTen).ToList(); // Sắp xếp 2 cột

// ===== FIRST / FIND — tìm 1 phần tử =====
var timTheo = dsKH.FirstOrDefault(kh => kh.MaKhachHang == "KH-001");
// Trả về null nếu không tìm thấy — KHÔNG throw exception
// (First() sẽ throw nếu không tìm thấy → tránh dùng)

// ===== COUNT / SUM / MAX / MIN — tổng hợp =====
int soKHDangDung = dsKH.Count(kh => kh.DangHoatDong);
decimal tongTien = dsChiSo.Sum(cs => cs.ThanhTien);
int chiSoCaoNhat = dsChiSo.Max(cs => cs.ChiSoCuoi);

// ===== SELECT — chọn / biến đổi dữ liệu =====
// Lấy chỉ danh sách tên
var danhSachTen = dsKH.Select(kh => kh.HoTen).ToList();
// Tạo object mới từ nhiều nguồn (anonymous type)
var thongTin = dsKH.Select(kh => new {
    kh.HoTen,
    kh.SoDienThoai,
    TenViet = kh.HoTen.ToUpper()
}).ToList();

// ===== KẾT HỢP NHIỀU PHÉP — giống viết SQL =====
var ketQua = dsKH
    .Where(kh => kh.DangHoatDong)              // Lọc đang hoạt động
    .Where(kh => kh.DiaChi.Contains("Q.1"))    // Ở Q.1
    .OrderBy(kh => kh.HoTen)                   // Sắp xếp A→Z
    .Take(10)                                  // Lấy 10 cái đầu
    .ToList();                                 // Chuyển về List

// ===== ANY / ALL — kiểm tra điều kiện =====
bool coKhachHangQ1 = dsKH.Any(kh => kh.DiaChi.Contains("Q.1")); // Có ít nhất 1?
bool tatCaDangDung  = dsKH.All(kh => kh.DangHoatDong);           // Tất cả đều...?
```

---

## 📌 BƯỚC 15: Dictionary<K,V> — Dùng Cho Bảng Giá

```csharp
// Dictionary = bảng tra cứu (key → value)
// Dùng khi cần tìm kiếm theo key nhanh (O(1))

// Bảng giá nước theo mức tiêu thụ
var bangGia = new Dictionary<string, decimal>
{
    { "Mức 1 (≤10m³)",  5973m },
    { "Mức 2 (≤20m³)",  7052m },
    { "Mức 3 (≤30m³)",  8669m },
    { "Mức 4 (>30m³)",  15929m }
};

// Đọc
decimal gia = bangGia["Mức 1 (≤10m³)"]; // 5973

// Kiểm tra tồn tại key trước khi đọc
if (bangGia.ContainsKey("Mức 1 (≤10m³)"))
{
    var giaTri = bangGia["Mức 1 (≤10m³)"];
}

// TryGetValue — an toàn hơn
if (bangGia.TryGetValue("Mức 1 (≤10m³)", out decimal donGia))
{
    Console.WriteLine(donGia);
}

// Duyệt
foreach (var kvp in bangGia)
{
    Console.WriteLine($"{kvp.Key}: {kvp.Value:N0}đ/m³");
}
```

---

# GIAI ĐOẠN 4: XỬ LÝ LỖI + BEST PRACTICES (Tuần 2)

---

## 📌 BƯỚC 16: Try-Catch-Finally

```csharp
// ===== CẤU TRÚC ĐẦY ĐỦ =====
try
{
    // Code có thể gây lỗi
    var danhSach = DatabaseHelper.GetAllKhachHang();
    dgvKhachHang.DataSource = danhSach;
}
catch (SqlException ex) when (ex.Number == -1)
{
    // Lỗi SQL cụ thể: không kết nối được server
    MessageBox.Show("Không thể kết nối SQL Server!\nKiểm tra lại service.", "Lỗi Kết Nối");
}
catch (SqlException ex)
{
    // Các lỗi SQL khác
    MessageBox.Show($"Lỗi Database:\n{ex.Message}", "Lỗi SQL");
}
catch (Exception ex)
{
    // Bắt tất cả lỗi còn lại
    MessageBox.Show($"Lỗi không xác định:\n{ex.Message}", "Lỗi");
}
finally
{
    // LUÔN LUÔN chạy — dù có lỗi hay không
    // Dùng để: đóng loading spinner, reset trạng thái UI
    btnTim.Enabled = true; // Bật lại nút dù có lỗi
}

// ===== THROW EXCEPTION tự tạo =====
public static bool ThemKhachHang(KhachHang kh)
{
    if (string.IsNullOrWhiteSpace(kh.HoTen))
        throw new ArgumentException("Họ tên không được rỗng!");
    // ...
}

// ===== USING STATEMENT — tự động đóng tài nguyên =====
// Khi ra khỏi block using, .Dispose() được gọi tự động
// SqlConnection, SqlCommand, SqlDataReader đều implement IDisposable

using var conn = new SqlConnection(_connectionString);
conn.Open();
// Khi ra khỏi scope của using → conn.Close() + conn.Dispose() tự gọi
// Dù có exception hay không!
```

---

## 📌 BƯỚC 17: Validation Pattern

```csharp
// Pattern validate input trước khi lưu DB
private bool ValidateInput()
{
    // Kiểm tra từng field
    if (string.IsNullOrWhiteSpace(txtHoTen.Text))
    {
        ShowWarning("Vui lòng nhập Họ Tên!", txtHoTen);
        return false;
    }
    if (string.IsNullOrWhiteSpace(txtDiaChi.Text))
    {
        ShowWarning("Vui lòng nhập Địa Chỉ!", txtDiaChi);
        return false;
    }
    if (!KiemTraSoDT(txtSDT.Text))
    {
        ShowWarning("Số điện thoại không hợp lệ (cần 10 số)!", txtSDT);
        return false;
    }
    return true; // Hợp lệ
}

private void ShowWarning(string message, Control focusControl)
{
    MessageBox.Show(message, "Thiếu thông tin",
        MessageBoxButtons.OK, MessageBoxIcon.Warning);
    focusControl.Focus(); // Đưa con trỏ vào ô bị lỗi
}

private bool KiemTraSoDT(string sdt)
{
    return !string.IsNullOrWhiteSpace(sdt)
        && sdt.Length == 10
        && sdt.All(char.IsDigit);
}

// Gọi trong btnLuu_Click:
private void btnLuu_Click(object sender, EventArgs e)
{
    if (!ValidateInput()) return; // Dừng lại nếu invalid
    // Tiếp tục lưu...
}
```

---

# GIAI ĐOẠN 5: ADO.NET — KẾT NỐI DATABASE (Tuần 2-3)

---

## 📌 BƯỚC 18: Connection String

```csharp
// ===== CÁC DẠNG CONNECTION STRING =====

// 1. Windows Authentication (không cần user/password)
"Server=.\MSSQLSERVER1;Database=CapNuocDB;Trusted_Connection=true;TrustServerCertificate=true;"

// 2. SQL Authentication (có user/password)
"Server=.\MSSQLSERVER1;Database=CapNuocDB;User Id=sa;Password=MatKhau123;TrustServerCertificate=true;"

// 3. LocalDB (Visual Studio built-in)
"Server=(localdb)\MSSQLLocalDB;Database=CapNuocDB;Trusted_Connection=true;"

// ===== GIẢI THÍCH TỪNG PHẦN =====
// Server=.\MSSQLSERVER1  → Máy local (.), instance tên MSSQLSERVER1
// Server=localhost         → SQL Server mặc định (port 1433)
// Server=192.168.1.100     → Máy chủ theo IP
// Database=CapNuocDB       → Tên database cần kết nối
// Trusted_Connection=true  → Dùng Windows Authentication
// TrustServerCertificate=true → Bỏ qua kiểm tra SSL (môi trường dev)
```

---

## 📌 BƯỚC 19: Ba Phương Thức Execute — PHẢI THUỘC LÒNG

```csharp
// ===== 1. ExecuteReader() — Dùng cho SELECT nhiều dòng =====
using var cmd = new SqlCommand("SELECT * FROM KhachHang", conn);
using var reader = cmd.ExecuteReader(); // Trả về SqlDataReader
while (reader.Read()) // Đọc từng dòng
{
    string ten = reader["HoTen"].ToString()!;    // Đọc theo tên cột
    string ten2 = reader.GetString(2);           // Đọc theo index cột
}

// ===== 2. ExecuteNonQuery() — Dùng cho INSERT/UPDATE/DELETE =====
using var cmd2 = new SqlCommand("INSERT INTO KhachHang (...) VALUES (...)", conn);
int soDoiTac = cmd2.ExecuteNonQuery(); // Trả về số dòng bị ảnh hưởng
bool thanhCong = soDoiTac > 0;        // true nếu có ít nhất 1 dòng thay đổi

// ===== 3. ExecuteScalar() — Dùng cho 1 giá trị duy nhất =====
using var cmd3 = new SqlCommand("SELECT COUNT(*) FROM KhachHang", conn);
int soLuong = (int)cmd3.ExecuteScalar()!; // Trả về object, phải cast

// Ví dụ thực tế:
using var cmd4 = new SqlCommand("SELECT SUM(ThanhTien) FROM HoaDon WHERE Thang=@Thang", conn);
cmd4.Parameters.AddWithValue("@Thang", 10);
decimal tongTien = (decimal)(cmd4.ExecuteScalar() ?? 0m); // ?? 0 phòng null
```

---

## 📌 BƯỚC 20: Parameterized Query — BẮT BUỘC PHẢI DÙNG

```csharp
// ❌ SAI — SQL Injection!!! KHÔNG BAO GIỜ làm thế này
string sql = "SELECT * FROM KhachHang WHERE HoTen = '" + txtTimKiem.Text + "'";
// Nếu user nhập: ' OR '1'='1 → lấy được toàn bộ dữ liệu!

// ✅ ĐÚNG — Parameterized Query
string sql = "SELECT * FROM KhachHang WHERE HoTen LIKE @keyword";
using var cmd = new SqlCommand(sql, conn);
cmd.Parameters.AddWithValue("@keyword", $"%{txtTimKiem.Text}%");
// .NET tự động escape — an toàn 100%

// ===== CÁC CÁCH THÊM PARAMETER =====

// Cách 1: AddWithValue (đơn giản, thường dùng)
cmd.Parameters.AddWithValue("@HoTen", kh.HoTen);

// Cách 2: Add + SqlDbType (chính xác hơn về kiểu)
cmd.Parameters.Add("@HoTen", SqlDbType.NVarChar, 100).Value = kh.HoTen;
cmd.Parameters.Add("@Tien",  SqlDbType.Decimal).Value = kh.ThanhTien;

// Xử lý NULL
cmd.Parameters.AddWithValue("@Email", kh.Email ?? (object)DBNull.Value);
// Hoặc đơn giản hơn:
cmd.Parameters.AddWithValue("@Email", kh.Email ?? "");
```

---

## 📌 BƯỚC 21: Full DatabaseHelper Pattern

```csharp
public class DatabaseHelper
{
    private static readonly string _connectionString =
        @"Server=.\MSSQLSERVER1;Database=CapNuocDB;Trusted_Connection=true;TrustServerCertificate=true;";

    // ===== HELPER: Tạo connection =====
    private static SqlConnection GetConnection()
    {
        var conn = new SqlConnection(_connectionString);
        conn.Open();
        return conn;
    }

    // ===== READ — lấy danh sách =====
    public static List<KhachHang> GetAllKhachHang()
    {
        var list = new List<KhachHang>();
        using var conn = GetConnection();
        using var cmd = new SqlCommand(
            "SELECT * FROM KhachHang WHERE DangHoatDong=1 ORDER BY HoTen", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            list.Add(MapKhachHang(reader));
        return list;
    }

    // ===== SEARCH =====
    public static List<KhachHang> SearchKhachHang(string keyword)
    {
        var list = new List<KhachHang>();
        using var conn = GetConnection();
        string sql = @"SELECT * FROM KhachHang
                       WHERE (HoTen LIKE @kw OR MaKhachHang LIKE @kw OR MaSoDongHo LIKE @kw)
                       AND DangHoatDong = 1 ORDER BY HoTen";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@kw", $"%{keyword}%");
        using var reader = cmd.ExecuteReader();
        while (reader.Read()) list.Add(MapKhachHang(reader));
        return list;
    }

    // ===== CREATE =====
    public static bool ThemKhachHang(KhachHang kh)
    {
        using var conn = GetConnection();
        string sql = @"INSERT INTO KhachHang (MaKhachHang,HoTen,DiaChi,SoDienThoai,Email,MaSoDongHo)
                       VALUES (@Ma,@Ten,@DC,@SDT,@Email,@MaDH)";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Ma",    kh.MaKhachHang);
        cmd.Parameters.AddWithValue("@Ten",   kh.HoTen);
        cmd.Parameters.AddWithValue("@DC",    kh.DiaChi);
        cmd.Parameters.AddWithValue("@SDT",   kh.SoDienThoai);
        cmd.Parameters.AddWithValue("@Email", kh.Email ?? "");
        cmd.Parameters.AddWithValue("@MaDH",  kh.MaSoDongHo);
        return cmd.ExecuteNonQuery() > 0;
    }

    // ===== UPDATE =====
    public static bool CapNhatKhachHang(KhachHang kh)
    {
        using var conn = GetConnection();
        string sql = @"UPDATE KhachHang SET HoTen=@Ten,DiaChi=@DC,SoDienThoai=@SDT,Email=@Email
                       WHERE Id=@Id";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Ten",   kh.HoTen);
        cmd.Parameters.AddWithValue("@DC",    kh.DiaChi);
        cmd.Parameters.AddWithValue("@SDT",   kh.SoDienThoai);
        cmd.Parameters.AddWithValue("@Email", kh.Email ?? "");
        cmd.Parameters.AddWithValue("@Id",    kh.Id);
        return cmd.ExecuteNonQuery() > 0;
    }

    // ===== SOFT DELETE =====
    public static bool XoaKhachHang(int id)
    {
        using var conn = GetConnection();
        using var cmd = new SqlCommand(
            "UPDATE KhachHang SET DangHoatDong=0 WHERE Id=@Id", conn);
        cmd.Parameters.AddWithValue("@Id", id);
        return cmd.ExecuteNonQuery() > 0;
    }

    // ===== PRIVATE HELPER: map reader → object =====
    private static KhachHang MapKhachHang(SqlDataReader r)
    {
        return new KhachHang
        {
            Id           = (int)r["Id"],
            MaKhachHang  = r["MaKhachHang"].ToString()!,
            HoTen        = r["HoTen"].ToString()!,
            DiaChi       = r["DiaChi"].ToString()!,
            SoDienThoai  = r["SoDienThoai"].ToString()!,
            Email        = r["Email"].ToString()!,
            MaSoDongHo   = r["MaSoDongHo"].ToString()!,
            NgayDangKy   = (DateTime)r["NgayDangKy"],
            DangHoatDong = (bool)r["DangHoatDong"]
        };
    }
}
```

---

# GIAI ĐOẠN 6: WINFORMS (Tuần 3)

---

## 📌 BƯỚC 22: Controls Hay Dùng

```
Controls quan trọng trong WinForms:

┌─────────────────────────────────────────────────────────┐
│ Form — cửa sổ chính                                     │
├─────────────────────────────────────────────────────────┤
│ TextBox — ô nhập văn bản                                │
│ Label — nhãn hiển thị text                              │
│ Button — nút bấm                                        │
│ DataGridView — bảng hiển thị danh sách                  │
│ ComboBox — dropdown list                                │
│ DateTimePicker — chọn ngày                              │
│ CheckBox — ô tick                                       │
│ Panel — container, chia vùng layout                     │
│ MessageBox — hộp thoại thông báo (không phải control)  │
└─────────────────────────────────────────────────────────┘
```

```csharp
// ===== TEXTBOX =====
txtHoTen.Text = "Nguyễn Văn An";   // Gán giá trị
string ten = txtHoTen.Text.Trim();  // Đọc và xóa khoảng trắng
txtHoTen.Clear();                    // Xóa nội dung
txtHoTen.Focus();                   // Đặt con trỏ vào đây
txtHoTen.ReadOnly = true;           // Chỉ đọc
txtHoTen.PlaceholderText = "Nhập tên..."; // Hint text

// ===== DATETIMEPICKER =====
dtpNgay.Value = DateTime.Now;        // Gán ngày
DateTime ngayChon = dtpNgay.Value;   // Đọc ngày

// ===== COMBOBOX =====
cboLoai.Items.Add("Khách hàng thường");
cboLoai.Items.Add("Khách hàng ưu tiên");
cboLoai.SelectedIndex = 0;          // Chọn phần tử đầu tiên
string loaiChon = cboLoai.SelectedItem?.ToString() ?? "";

// ===== MESSAGEBOX =====
// Thông báo đơn giản
MessageBox.Show("Lưu thành công!");

// Đầy đủ tham số
MessageBox.Show(
    "Bạn có chắc muốn xóa?",          // Nội dung
    "Xác nhận",                        // Tiêu đề
    MessageBoxButtons.YesNo,           // Nút bấm
    MessageBoxIcon.Question);          // Icon

// Kiểm tra kết quả
var result = MessageBox.Show("Xóa?", "XN", MessageBoxButtons.YesNo);
if (result == DialogResult.Yes) { /* Xóa */ }
```

---

## 📌 BƯỚC 23: Events — Sự Kiện

```csharp
public class FormKhachHang : Form
{
    public FormKhachHang()
    {
        // Gán sự kiện — 3 cách
        
        // Cách 1: += với method riêng (thường dùng nhất)
        btnThem.Click += btnThem_Click;
        this.Load += FormKhachHang_Load;
        
        // Cách 2: += với lambda (nhanh, dùng cho logic ngắn)
        btnLamMoi.Click += (sender, e) => LoadDanhSachKhachHang();
        
        // Cách 3: += với anonymous method
        txtTimKiem.TextChanged += delegate(object s, EventArgs e)
        {
            if (txtTimKiem.Text.Length == 0) LoadDanhSachKhachHang();
        };
    }

    // Event handler — method xử lý sự kiện
    // Luôn có signature: (object sender, EventArgs e)
    private void FormKhachHang_Load(object sender, EventArgs e)
    {
        LoadDanhSachKhachHang(); // Load data khi form mở
    }

    private void btnThem_Click(object sender, EventArgs e)
    {
        // Code xử lý khi click nút Thêm
    }

    // KeyPress event — phát hiện phím bấm
    private void txtTimKiem_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
            btnTimKiem_Click(sender, e); // Gọi tìm kiếm khi nhấn Enter
    }
}
```

---

## 📌 BƯỚC 24: DataGridView Hoàn Chỉnh

```csharp
// ===== CẤU HÌNH =====
dgvKhachHang.AutoGenerateColumns = false;   // Tự chọn cột
dgvKhachHang.Columns.Clear();
dgvKhachHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Chọn cả dòng
dgvKhachHang.ReadOnly = true;               // Không cho sửa trực tiếp
dgvKhachHang.AllowUserToAddRows = false;    // Không cho thêm dòng trực tiếp
dgvKhachHang.Dock = DockStyle.Fill;         // Chiếm toàn bộ không gian

// ===== THÊM CỘT =====
dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn
{
    DataPropertyName = "MaKhachHang",    // Tên property trong class
    HeaderText = "Mã KH",               // Tiêu đề cột
    Width = 80
});
dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn
{
    DataPropertyName = "NgayDangKy",
    HeaderText = "Ngày ĐK",
    Width = 100,
    DefaultCellStyle = new DataGridViewCellStyle
    {
        Format = "dd/MM/yyyy" // Format hiển thị ngày
    }
});

// ===== BIND DATA =====
dgvKhachHang.DataSource = null;         // Reset trước (tránh bug)
dgvKhachHang.DataSource = danhSach;    // Gán List<KhachHang>

// ===== LẤY OBJECT TỪ DÒNG ĐƯỢC CHỌN =====
if (dgvKhachHang.SelectedRows.Count > 0)
{
    var kh = (KhachHang)dgvKhachHang.SelectedRows[0].DataBoundItem;
    // Bây giờ kh là object KhachHang của dòng đang chọn
}

// ===== STYLE ĐẸP =====
dgvKhachHang.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
{
    BackColor = Color.FromArgb(30, 136, 229), // Xanh dương
    ForeColor = Color.White,
    Font = new Font("Segoe UI", 10, FontStyle.Bold)
};
dgvKhachHang.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
{
    BackColor = Color.FromArgb(245, 248, 255) // Xen kẽ màu nhạt
};
```

---

## 📌 BƯỚC 25: Form Dialog Pattern

```csharp
// ===== FORM CHA: Mở form con =====
private void btnThem_Click(object sender, EventArgs e)
{
    var formCon = new FormThemSuaKhachHang(); // Tạo form con
    
    // ShowDialog() = modal — chặn form cha, chờ form con đóng
    if (formCon.ShowDialog() == DialogResult.OK)
    {
        // Form con đóng và báo thành công
        LoadDanhSachKhachHang(); // Reload data
        MessageBox.Show("Thêm thành công!");
    }
    // Nếu Cancel — không làm gì
}

// Mở form sửa — truyền object vào
private void btnSua_Click(object sender, EventArgs e)
{
    var kh = (KhachHang)dgvKhachHang.SelectedRows[0].DataBoundItem;
    using var formSua = new FormThemSuaKhachHang(kh); // using = tự dispose sau
    if (formSua.ShowDialog() == DialogResult.OK)
        LoadDanhSachKhachHang();
}

// ===== FORM CON: Báo kết quả cho form cha =====
private void btnLuu_Click(object sender, EventArgs e)
{
    if (!ValidateInput()) return;
    try
    {
        // Lưu DB...
        this.DialogResult = DialogResult.OK; // Báo thành công → form cha nhận được
        this.Close(); // Đóng form con
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Lỗi: {ex.Message}");
    }
}

private void btnHuy_Click(object sender, EventArgs e)
{
    this.DialogResult = DialogResult.Cancel; // Báo hủy
    // Form tự đóng khi DialogResult được set
}
```

---

# GIAI ĐOẠN 7: TỔNG HỢP — PATTERN & BEST PRACTICES

---

## 📌 BƯỚC 26: Kiến Trúc Project Chuẩn

```
capnuoc-project/
│
├── Models/                    ← Dữ liệu thuần túy
│   ├── KhachHang.cs           → Class model, chỉ có properties
│   └── ChiSoDongHo.cs         → Class model, có computed property
│
├── Data/                      ← Tầng truy cập database
│   └── DatabaseHelper.cs      → Static class, tất cả SQL ở đây
│
├── Forms/                     ← Tầng giao diện
│   ├── FormKhachHang.cs       → Form chính: danh sách + CRUD
│   └── FormThemSuaKhachHang.cs → Form con: thêm/sửa
│
├── Database/
│   └── create_database.sql    → Script tạo DB + dữ liệu mẫu
│
└── Program.cs                 → Entry point: mở FormKhachHang

NGUYÊN TẮC:
- Form KHÔNG viết SQL trực tiếp → gọi qua DatabaseHelper
- DatabaseHelper KHÔNG dùng Controls của Form
- Models KHÔNG biết gì về UI hay DB
```

---

## 📌 BƯỚC 27: Những Lỗi Phổ Biến Cần Tránh

```csharp
// ❌ LỖI 1: Quên đóng connection
SqlConnection conn = new SqlConnection(_cs);
conn.Open();
// ... code ... Nếu có exception → connection không bao giờ đóng!

// ✅ ĐÚNG: Dùng using
using var conn = new SqlConnection(_cs);
conn.Open(); // Tự đóng dù có exception

// ---

// ❌ LỖI 2: Nối chuỗi SQL trực tiếp
string sql = "SELECT * FROM KhachHang WHERE HoTen = '" + input + "'";

// ✅ ĐÚNG: Dùng parameter
string sql = "SELECT * FROM KhachHang WHERE HoTen = @ten";
cmd.Parameters.AddWithValue("@ten", input);

// ---

// ❌ LỖI 3: Cast mà không kiểm tra null
string email = (string)reader["Email"]; // Crash nếu Email = NULL trong DB

// ✅ ĐÚNG: Kiểm tra DBNull
string email = reader["Email"] == DBNull.Value ? "" : reader["Email"].ToString()!;
// Hoặc:
string email = reader["Email"].ToString() ?? "";

// ---

// ❌ LỖI 4: Không dùng try-catch khi thao tác DB
DatabaseHelper.GetAllKhachHang(); // App crash nếu DB lỗi

// ✅ ĐÚNG: Bọc try-catch
try { DatabaseHelper.GetAllKhachHang(); }
catch (Exception ex) { MessageBox.Show(ex.Message); }

// ---

// ❌ LỖI 5: Dùng double thay decimal cho tiền
double tien = 75000.1 + 75000.2; // = 150000.30000000001 (sai!)

// ✅ ĐÚNG: Dùng decimal
decimal tien = 75000.1m + 75000.2m; // = 150000.3 (chính xác)
```

---

## 📌 BƯỚC 28: Tóm Tắt — Bản Đồ Kiến Thức C#

```
C# CĂN BẢN
├── Kiểu dữ liệu (int, decimal, string, bool, DateTime)
├── Khai báo biến + var
├── String interpolation ($"...{biến}...")
├── If-else + switch
├── Vòng lặp (for, foreach, while)
└── Method (void, return, params, static)

OOP
├── Class + Properties (get;set;)
├── Computed property (=>)
├── Constructor + Overloading
├── Access Modifiers (public, private, static)
└── Inheritance cơ bản (:)

COLLECTIONS
├── List<T> (Add, Remove, Count, foreach)
├── Dictionary<K,V> (key-value lookup)
└── LINQ (Where, OrderBy, FirstOrDefault, Select, Sum)

XỬ LÝ LỖI
├── try-catch-finally
├── using statement (IDisposable)
└── Validation pattern

ADO.NET
├── SqlConnection (kết nối)
├── SqlCommand (câu SQL)
├── ExecuteReader (SELECT nhiều dòng)
├── ExecuteNonQuery (INSERT/UPDATE/DELETE)
├── ExecuteScalar (1 giá trị)
└── Parameterized Query (@param)

WINFORMS
├── Controls (TextBox, Button, DataGridView, Label)
├── Events (Click, Load, KeyPress)
├── DataGridView Binding (DataSource = List<T>)
├── DataBoundItem (lấy object từ selected row)
└── Form Dialog Pattern (ShowDialog, DialogResult)
```

---

## 📌 BƯỚC 29: Câu Hỏi Phỏng Vấn + Câu Trả Lời Chuẩn

**Q: `using` trong C# có mấy nghĩa?**
> A: 2 nghĩa: (1) `using System;` — import namespace. (2) `using var conn = ...;` — tự động gọi Dispose() khi ra khỏi scope, đảm bảo đóng connection dù có exception.

**Q: Sự khác nhau giữa `int` và `decimal`?**
> A: `int` lưu số nguyên không có phần thập phân. `decimal` lưu số thập phân với độ chính xác cao — PHẢI dùng cho tiền tệ. `double` cũng lưu số thập phân nhưng dùng nhị phân nên có thể mất chính xác (0.1 + 0.2 ≠ 0.3).

**Q: `List<T>` và `Array` khác nhau thế nào?**
> A: Array có kích thước cố định, khai báo một lần không thể thêm bớt. `List<T>` động — có thể Add/Remove thoải mái. Trong project thường dùng `List<T>` vì số lượng khách hàng thay đổi.

**Q: Tại sao trong `DatabaseHelper` tất cả đều `static`?**
> A: Vì DatabaseHelper là utility class thuần túy — không có trạng thái riêng. Static giúp gọi trực tiếp `DatabaseHelper.GetAllKhachHang()` mà không cần `new DatabaseHelper()`, gọn hơn và tiết kiệm bộ nhớ.

**Q: `FirstOrDefault` khác `First` thế nào?**
> A: `First()` throw `InvalidOperationException` nếu không tìm thấy. `FirstOrDefault()` trả về `null` (với reference type) — an toàn hơn. Luôn dùng `FirstOrDefault` trong production code.

---

*📌 Lưu ý: Đây là file tham chiếu — không cần thuộc hết ngay mà học dần theo từng bước trong README.md*
