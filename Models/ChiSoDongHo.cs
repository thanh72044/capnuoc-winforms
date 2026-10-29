namespace CapNuocApp.Models;

/// <summary>
/// Class đại diện cho 1 khách hàng trong hệ thống cấp nước
/// Tương ứng với bảng KhachHang trong SQL Server
/// </summary>
public class ChiSoDongHo
{
    // ===== PROPERTIES =====
    // Mỗi property = 1 cột trong bảng KhachHang của database

    public int Id { get; set; }                                    

    public int KhachHangId { get; set; }

    public string  MaSoDongHo { get; set; } = string.Empty;              

    public int ChiSoDau { get; set; }  

    public int ChiSoCuoi { get; set; }    

    public int Thang { get; set; }           

    public decimal DonGia { get; set; } = 7000m;       

    public int SoLuongTieuThu => ChiSoCuoi - ChiSoDau;

    public decimal ThanhTien => SoLuongTieuThu * DonGia;           

    // ===== COMPUTED PROPERTY =====
    // Tự tính từ property khác, KHÔNG lưu vào database

    /// <summary>Thông tin ngắn gọn để hiển thị trong danh sách</summary>
    public string ThongTinNgan => $"{MaSoDongHo} - {KhachHangId}";

    // ===== OVERRIDE TOSTRING =====
    // Khi Console.WriteLine(khachHang) sẽ in ra cái này
    public override string ToString()
    {
        return $"[{ThanhTien}] | chỉ số đầu: {ChiSoDau} | chỉ số cuối: {ChiSoCuoi} | số lượng tiêu thụ: {SoLuongTieuThu}";
    }
}