namespace CapNuocApp.Models;

/// <summary>
/// Class đại diện cho 1 khách hàng trong hệ thống cấp nước
/// Tương ứng với bảng KhachHang trong SQL Server
/// </summary>
public class KhachHang
{
    // ===== PROPERTIES =====
    // Mỗi property = 1 cột trong bảng KhachHang của database

    public int Id { get; set; }                                     // ID tự tăng (PRIMARY KEY)

    public string MaKhachHang { get; set; } = string.Empty;        // VD: "KH-001"

    public string HoTen { get; set; } = string.Empty;              // VD: "Nguyễn Văn An"

    public string DiaChi { get; set; } = string.Empty;             // VD: "12 Lý Tự Trọng, Q.1"

    public string SoDienThoai { get; set; } = string.Empty;        // VD: "0901234567"

    public string Email { get; set; } = string.Empty;              // Có thể rỗng

    public string MaSoDongHo { get; set; } = string.Empty;         // VD: "DH-001"

    public DateTime NgayDangKy { get; set; } = DateTime.Now;       // Mặc định = hôm nay

    public bool DangHoatDong { get; set; } = true;                 // true = đang dùng, false = đã nghỉ

    // ===== COMPUTED PROPERTY =====
    // Tự tính từ property khác, KHÔNG lưu vào database

    /// <summary>Thông tin ngắn gọn để hiển thị trong danh sách</summary>
    public string ThongTinNgan => $"{MaKhachHang} - {HoTen}";

    // ===== OVERRIDE TOSTRING =====
    // Khi Console.WriteLine(khachHang) sẽ in ra cái này
    public override string ToString()
    {
        return $"[{MaKhachHang}] {HoTen} | ĐT: {SoDienThoai} | Đồng hồ: {MaSoDongHo}";
    }
}