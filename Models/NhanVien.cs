namespace CapNuocApp.Models;

public class NhanVien
{
    public int Id { get; set; }
    public string MaNhanVien { get; set; } = string.Empty;
    public string HoTen { get; set; } = string.Empty;
    public string SoDienThoai { get; set; } = string.Empty;
    public string KhuVucPhuTrach { get; set; } = string.Empty;
    public bool DangHoatDong { get; set; } = true;

    public override string ToString()
    {
        return $"[{MaNhanVien}] {HoTen} - {KhuVucPhuTrach}";
    }
}
