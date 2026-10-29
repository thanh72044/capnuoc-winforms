using CapNuocApp.Models;

public class BaiTap7
{
    // Dữ liệu mẫu — không cần sửa
    private static List<ChiSoDongHo> TaoDuLieu() =>
        new List<ChiSoDongHo>
        {
            new ChiSoDongHo { Id = 1, MaSoDongHo = "DH-001", ChiSoDau = 1000, ChiSoCuoi = 1008, Thang = 10, DonGia = 5973m },
            new ChiSoDongHo { Id = 2, MaSoDongHo = "DH-002", ChiSoDau = 500,  ChiSoCuoi = 518,  Thang = 10, DonGia = 7052m },
            new ChiSoDongHo { Id = 3, MaSoDongHo = "DH-003", ChiSoDau = 200,  ChiSoCuoi = 225,  Thang = 10, DonGia = 8669m },
            new ChiSoDongHo { Id = 4, MaSoDongHo = "DH-004", ChiSoDau = 300,  ChiSoCuoi = 312,  Thang = 10, DonGia = 7052m },
            new ChiSoDongHo { Id = 5, MaSoDongHo = "DH-005", ChiSoDau = 750,  ChiSoCuoi = 795,  Thang = 10, DonGia = 8669m },
        };

    public static void Test1_Where() { 
        var DanhSach = TaoDuLieu(); 
        var KetQua = DanhSach.Where(cs => cs.SoLuongTieuThu >20).toList()
    }

    public static void Test2_OrderBy() { }

    public static void Test3_Sum() { }

    public static void Test4_Find() { }
}
