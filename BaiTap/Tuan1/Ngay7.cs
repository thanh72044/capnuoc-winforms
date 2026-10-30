using System.Linq;
using CapNuocApp.Models;

public class BaiTap7
{
    // Dữ liệu mẫu — không cần sửa
    private static List<ChiSoDongHo> TaoDuLieu() =>
        new List<ChiSoDongHo>
        {
            new ChiSoDongHo
            {
                Id = 1,
                MaSoDongHo = "DH-001",
                ChiSoDau = 1000,
                ChiSoCuoi = 1008,
                Thang = 10,
                DonGia = 5973m,
            },
            new ChiSoDongHo
            {
                Id = 2,
                MaSoDongHo = "DH-002",
                ChiSoDau = 500,
                ChiSoCuoi = 518,
                Thang = 10,
                DonGia = 7052m,
            },
            new ChiSoDongHo
            {
                Id = 3,
                MaSoDongHo = "DH-003",
                ChiSoDau = 200,
                ChiSoCuoi = 225,
                Thang = 10,
                DonGia = 8669m,
            },
            new ChiSoDongHo
            {
                Id = 4,
                MaSoDongHo = "DH-004",
                ChiSoDau = 300,
                ChiSoCuoi = 312,
                Thang = 10,
                DonGia = 7052m,
            },
            new ChiSoDongHo
            {
                Id = 5,
                MaSoDongHo = "DH-005",
                ChiSoDau = 750,
                ChiSoCuoi = 795,
                Thang = 10,
                DonGia = 8669m,
            },
        };

    private static List<ChiSoDongHo> DanhSach = TaoDuLieu();

    public static void Test1_Where()
    {
        var KetQua = DanhSach.Where(cs => cs.SoLuongTieuThu > 20).ToList();
        var ThongBao = "CÁC ĐỒNG HỒ TIÊU THỤ > 20m³:\n";
        foreach (var cs in KetQua)
        {
            ThongBao += cs.ToString() + "\n";
        }
        MessageBox.Show(ThongBao, "kết quả test 1");
    }

    public static void Test2_OrderBy()
    {
        var KetQuahai = DanhSach.OrderBy(cs => cs.SoLuongTieuThu).ToList();
        var ThongBaohai = "DANH SÁCH SẮP XẾP THEO TIÊU THỤ (TĂNG DẦN):\n";
        foreach (var cs in KetQuahai)
        {
            ThongBaohai += cs.ToString() + "\n";
        }
        MessageBox.Show(ThongBaohai, "kết quả test 2");
    }

    public static void Test3_Sum()
    {
        int KetQuaBa = DanhSach.Sum(cs => cs.SoLuongTieuThu);
        string ThongBaoBa =
            $"TỔNG KẾT THÁNG 10:\n"
            + $"━━━━━━━━━━━━━━━━━━━\n"
            + $"• Tổng tiền thu được: {KetQuaBa:N0} VNĐ";
        MessageBox.Show(ThongBaoBa);
    }

    public static void Test4_Find()
    {
        var TimThay = DanhSach.FirstOrDefault(cs => cs.MaSoDongHo == "DH-001");
        if (TimThay != null)
        {
            MessageBox.Show($"TÌM THẤY ĐỒNG HỒ:\n{TimThay}", "Kết quả Test 4");
        }
        else
        {
            // Không tìm thấy
            MessageBox.Show("Không tìm thấy đồng hồ với mã này!", "Kết quả Test 4");
        }
    }
}
