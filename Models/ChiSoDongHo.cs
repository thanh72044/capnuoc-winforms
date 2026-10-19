namespace CapNuocApp.Models
{
    /// <summary>
    /// Model đại diện cho một kỳ đọc chỉ số đồng hồ nước
    /// </summary>
    public class ChiSoDongHo
    {
        public int Id { get; set; }
        public int KhachHangId { get; set; }
        public string MaSoDongHo { get; set; } = string.Empty;
        public int ChiSoDau { get; set; }    // Chỉ số đầu kỳ (m³)
        public int ChiSoCuoi { get; set; }   // Chỉ số cuối kỳ (m³)
        public int SoLuongTieuThu => ChiSoCuoi - ChiSoDau;  // Lượng nước dùng
        public DateTime NgayDoc { get; set; } = DateTime.Now;
        public int Thang { get; set; }
        public int Nam { get; set; }
        public decimal DonGia { get; set; } = 7000; // Giá mặc định 7,000đ/m³
        public decimal ThanhTien => SoLuongTieuThu * DonGia;

        // Navigation property
        public KhachHang? KhachHang { get; set; }
    }
}
