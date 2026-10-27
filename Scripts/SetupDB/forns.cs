// //Console.WriteLine("--- THÔNG TIN TIÊU THỤ NƯỚC ---");
// string khachHang = "Nguyễn văn A";
// string Ma = "KH001";
// int ChiSoCu = 150;
// int ChiSoMoi = 175;
// int TieuThu = ChiSoMoi - ChiSoCu;
// decimal dongia = 7500m;

// Console.WriteLine("--- THÔNG TIN TIÊU THỤ NƯỚC ---");
// Console.WriteLine($"khách hàng:{khachHang}(Mã:{Ma})");
// Console.WriteLine($"chỉ số cũ: {ChiSoCu} | chỉ số mới:{ChiSoMoi}");
// Console.WriteLine($"tiêu thụ: {TieuThu}m3");

// Console.WriteLine("--- THÔNG TIN TIÊU THỤ NƯỚC BẬC THANG ---");

// int soM3 = 25;
// decimal ketQua = 0m;
// if (soM3 <= 10){
//     ketQua = soM3 * 6869m;
// }
// else if (soM3 <= 20){
//     ketQua = (10*6869m) + ((soM3 -10) * 8110m);
// }

// else if (soM3 <= 30) {
//     ketQua = (10 * 6869m) + (10 * 8110m) + ((soM3 -20) * 9969m);
// }
// else
// {
//     ketQua = (10 * 6869m) + (10 * 8110m) + (10 * 15315m) + ((soM3 - 30) * 15315m);
// }
// Console.WriteLine($"số m3 tiêu thụ: {soM3}m3");
// Console.WriteLine($"tổng tiền tiêu thụ thanh toán:{ketQua}");

public class BaiTap3
{
    public static bool KiemTraSDT(string sdt)
    {
        if (sdt.Length == 10 && sdt.StartsWith("0")&& sdt.All(char.IsDigit))
        {
            return true;
        }
        else { return false; }
    }

    public static string TaoMaKhachHang()
    {
        string ngayHienTai = DateTime.Now.ToString("yyyyMMdd");
        int soNgauNhien = new random().next(100, 999);
        return $"{ngayHienTai}-{soNgauNhien}";
    }

    public static void Main()
    {
        string maKH = TaoMaKhachHang();
        Console.WriteLine($"thông tin khách hàng:{maKH}");
        string sdt1 = "0901234567";
        string sdt2 = "090123";
        string sdt3 = "8901234567";

        Console.WriteLine($"SĐT {sdt1} hợp lệ? {KiemTraSDT(sdt1)}"); // true
        Console.WriteLine($"SĐT {sdt2} hợp lệ? {KiemTraSDT(sdt2)}"); // false (thiếu số)
        Console.WriteLine($"SĐT {sdt3} hợp lệ? {KiemTraSDT(sdt3)}"); // false (không bắt đầu bằng 0)
    }
}