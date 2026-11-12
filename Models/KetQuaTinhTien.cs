using System;

namespace CapNuocApp.Models;

public class KetQuaTinhTien
{
    // 1. Đơn giá từng bậc
    public const decimal GIA_BAC_1 = 6700m;
    public const decimal GIA_BAC_2 = 7100m;
    public const decimal GIA_BAC_3 = 8700m;
    public const decimal GIA_BAC_4 = 16000m;

    // 2. Thuộc tính lưu trữ
    public int SoM3 { get; set; }

    public int M3Bac1 { get; set; }
    public decimal TienBac1 { get; set; }

    public int M3Bac2 { get; set; }
    public decimal TienBac2 { get; set; }

    public int M3Bac3 { get; set; }
    public decimal TienBac3 { get; set; }

    public int M3Bac4 { get; set; }
    public decimal TienBac4 { get; set; }

    // 3. Các thuộc tính tự tính (Expression-bodied property)
    public decimal TienNuocChuaThue => TienBac1 + TienBac2 + TienBac3 + TienBac4;
    public decimal ThueVAT => Math.Round(TienNuocChuaThue * 0.05m, MidpointRounding.AwayFromZero);
    public decimal PhiBVMT => Math.Round(TienNuocChuaThue * 0.10m, MidpointRounding.AwayFromZero);
    public decimal TongThanhTien => TienNuocChuaThue + ThueVAT + PhiBVMT;

    // 4. Hàm thuật toán tính toán bậc thang
    public static KetQuaTinhTien TinhTienNuoc(int soM3)
    {
        var kq = new KetQuaTinhTien { SoM3 = soM3 };
        if (soM3 <= 0)
            return kq;

        int conLai = soM3;

        // BẬC 1: Tối đa 10 m³ đầu
        kq.M3Bac1 = Math.Min(conLai, 10);
        kq.TienBac1 = kq.M3Bac1 * GIA_BAC_1;
        conLai -= kq.M3Bac1;

        // BẬC 2: Tối đa 10 m³ kế tiếp (từ m³ thứ 11 đến 20)
        if (conLai > 0)
        {
            kq.M3Bac2 = Math.Min(conLai, 10);
            kq.TienBac2 = kq.M3Bac2 * GIA_BAC_2;
            conLai -= kq.M3Bac2;
        }

        // BẬC 3: Tối đa 10 m³ kế tiếp (từ m³ thứ 21 đến 30)
        if (conLai > 0)
        {
            kq.M3Bac3 = Math.Min(conLai, 10);
            kq.TienBac3 = kq.M3Bac3 * GIA_BAC_3;
            conLai -= kq.M3Bac3;
        }

        // BẬC 4: Toàn bộ số m³ vượt trên 30 m³
        if (conLai > 0)
        {
            kq.M3Bac4 = conLai;
            kq.TienBac4 = kq.M3Bac4 * GIA_BAC_4;
        }

        return kq;
    }
}
