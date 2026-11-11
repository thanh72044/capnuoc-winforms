using System;

namespace CapNuocApp.Models;

/// <summary>
/// Model đại diện cho 1 Hóa Đơn tiền nước (bảng HoaDon trong SQL Server)
/// </summary>
public class HoaDon
{
    // ==========================================
    // 1. CÁC TRƯỜNG TƯƠNG ỨNG CỘT BẢNG HOADON TRONG DATABASE
    // ==========================================
    public int Id { get; set; }
    public string MaHoaDon { get; set; } = string.Empty;
    public int KhachHangId { get; set; }
    public int? ChiSoDongHoId { get; set; } // int? cho phép null nếu tạo hóa đơn trực tiếp
    public int Thang { get; set; }
    public int Nam { get; set; }
    public int SoM3TieuThu { get; set; }
    public decimal DonGia { get; set; }
    public decimal ThanhTien { get; set; }
    public DateTime NgayLap { get; set; } = DateTime.Now;
    public bool DaThanhToan { get; set; } = false;
    public DateTime? NgayThanhToan { get; set; } // DateTime? null khi chưa thanh toán

    // ==========================================
    // 2. CÁC TRƯỜNG MƯỢN TỪ BẢNG KHACHHANG (KHI INNER JOIN)
    // Giúp hiển thị trực tiếp lên DataGridView mà không cần truy vấn lần 2
    // ==========================================
    public string MaKhachHang { get; set; } = string.Empty;
    public string TenKhachHang { get; set; } = string.Empty;
    public string SoDienThoai { get; set; } = string.Empty;

    // ==========================================
    // 3. COMPUTED PROPERTY (Thuộc tính tự động tính chữ tiếng Việt)
    // ==========================================
    public string TrangThaiText => DaThanhToan ? "Đã thanh toán" : "Chưa thanh toán";
}
