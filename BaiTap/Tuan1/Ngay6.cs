using CapNuocApp.Models;

/// <summary>
/// NGÀY 6 — THỰC HÀNH VỚI ChiSoDongHo
/// Giống Ngay5 nhưng dùng ChiSoDongHo thay vì KhachHang
/// Mục tiêu: thấy computed property (SoLuongTieuThu, ThanhTien) tự tính
/// </summary>
public class BaiTap6
{
    public static void Test1()
    {
        // Tạo 5 chỉ số đồng hồ — giống Ngay5 tạo 5 KhachHang
        var cs1 = new ChiSoDongHo
        {
            Id = 1,
            KhachHangId = 1,
            MaSoDongHo = "DH-001",
            ChiSoDau = 1000,
            ChiSoCuoi = 1008, // tiêu thụ 8m³
            Thang = 10,
            DonGia = 5973m,
        };
        var cs2 = new ChiSoDongHo
        {
            Id = 2,
            KhachHangId = 2,
            MaSoDongHo = "DH-002",
            ChiSoDau = 500,
            ChiSoCuoi = 518, // tiêu thụ 18m³
            Thang = 10,
            DonGia = 7052m,
        };
        var cs3 = new ChiSoDongHo
        {
            Id = 3,
            KhachHangId = 3,
            MaSoDongHo = "DH-003",
            ChiSoDau = 200,
            ChiSoCuoi = 225, // tiêu thụ 25m³
            Thang = 10,
            DonGia = 8669m,
        };
        var cs4 = new ChiSoDongHo
        {
            Id = 4,
            KhachHangId = 4,
            MaSoDongHo = "DH-004",
            ChiSoDau = 300,
            ChiSoCuoi = 312, // tiêu thụ 12m³
            Thang = 10,
            DonGia = 7052m,
        };
        var cs5 = new ChiSoDongHo
        {
            Id = 5,
            KhachHangId = 5,
            MaSoDongHo = "DH-005",
            ChiSoDau = 750,
            ChiSoCuoi = 795, // tiêu thụ 45m³
            Thang = 10,
            DonGia = 8669m,
        };

        // Bỏ vào danh sách — giống Ngay5
        var danhSach = new List<ChiSoDongHo> { cs1, cs2, cs3, cs4, cs5 };

        // Duyệt và hiển thị — cs.ToString() gọi method đã viết trong ChiSoDongHo
        var ketQua = "BẢNG CHỈ SỐ THÁNG 10:\n";
        ketQua += "━━━━━━━━━━━━━━━━━━━━━━━━━━\n";
        foreach (var cs in danhSach)
        {
            ketQua += cs.ToString() + "\n";
        }

        MessageBox.Show(ketQua, "Ngày 6 — Danh sách ChiSoDongHo");
    }
}
