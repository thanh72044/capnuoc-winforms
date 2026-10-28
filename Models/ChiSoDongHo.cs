namespace CapNuocApp.Models;

/// <summary>
/// Class đại diện cho 1 khách hàng trong hệ thống cấp nước
/// Tương ứng với bảng KhachHang trong SQL Server
/// </summary>
public class ChiSoDongHo
{
    // ===== PROPERTIES =====
    // Mỗi property = 1 cột trong bảng KhachHang của database

    public int Id { get; set; }                                     // ID tự tăng (PRIMARY KEY)

    public int KhachHangId { get; set; };        // VD: "KH-001"

    public string  MaSoDongHo { get; set; };              // VD: "Nguyễn Văn An"

    public int ChiSoDau { get; set; };             // VD: "12 Lý Tự Trọng, Q.1"

    public int ChiSoCuoi { get; set; };        // VD: "0901234567"

    public int Thang { get; set; };              // Có thể rỗng

    public decimal DonGia { get; set; } = 7000m;         // VD: "DH-001"

    public int SoLuongTieuThu => ChiSoCuoi - ChiSoDau;

    public decimal ThanhTien =?> SoLuongTieuThu + DonGia;           // true = đang dùng, false = đã nghỉ

    // ===== COMPUTED PROPERTY =====
    // Tự tính từ property khác, KHÔNG lưu vào database

    /// <summary>Thông tin ngắn gọn để hiển thị trong danh sách</summary>
    public string ThongTinNgan => $"{ChiSoDongHo} - {KhachHangId}";

    // ===== OVERRIDE TOSTRING =====
    // Khi Console.WriteLine(khachHang) sẽ in ra cái này
    public override string ToString()
    {
        return $"[{ThanhTien}] | chỉ số đầu: {ChiSoDau} | chỉ số cuối: {ChiSoCuoi} | số lượng tiêu thụ: {SoLuongTieuThu}";
    }
}