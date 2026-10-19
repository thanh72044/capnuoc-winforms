namespace CapNuocApp.Models
{
    /// <summary>
    /// Model đại diện cho một khách hàng sử dụng nước
    /// </summary>
    public class KhachHang
    {
        public int Id { get; set; }
        public string MaKhachHang { get; set; } = string.Empty;
        public string HoTen { get; set; } = string.Empty;
        public string DiaChi { get; set; } = string.Empty;
        public string SoDienThoai { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MaSoDongHo { get; set; } = string.Empty;  // Mã số đồng hồ nước
        public DateTime NgayDangKy { get; set; } = DateTime.Now;
        public bool DangHoatDong { get; set; } = true;
    }
}
