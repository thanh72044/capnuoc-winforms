using CapNuocApp.Models;

public class BaiTap5{
public static void Test1(){
    var kh1 = new KhachHang{
        Id = 1,
        MaKhachHang = "11-sn",
        HoTen = "THANH",
        DiaChi = "123cbs",
        SoDienThoai = "12345679",
        Email = "abc@gmail.com",
        MaSoDongHo = "abc123",
        NgayDangKy = DateTime.Now,
        DangHoatDong  = true,
    };
    var kh2 = new KhachHang{
        Id = 2,
        MaKhachHang = "123-ccc",
        HoTen = "THANH",
        DiaChi = "123cbs",
        SoDienThoai = "333333",
        Email = "abc@gmail.com",
        MaSoDongHo = "abc123",
        NgayDangKy = DateTime.Now,
        DangHoatDong  = true,
    };
    var kh3 = new KhachHang{
        Id = 3,
        MaKhachHang = "123-ccc",
        HoTen = "THANH",
        DiaChi = "123cbs",
        SoDienThoai = "333333",
        Email = "abc@gmail.com",
        MaSoDongHo = "abc123",
        NgayDangKy = DateTime.Now,
        DangHoatDong  = true,
    };
        var kh4 = new KhachHang{
        Id = 4,
        MaKhachHang = "123-ccc",
        HoTen = "THANH",
        DiaChi = "123cbs",
        SoDienThoai = "333333",
        Email = "abc@gmail.com",
        MaSoDongHo = "abc123",
        NgayDangKy = DateTime.Now,
        DangHoatDong  = true,
    };
        var kh5 = new KhachHang{
        Id = 5,
        MaKhachHang = "123-ccc",
        HoTen = "THANH",
        DiaChi = "123cbs",
        SoDienThoai = "333333",
        Email = "abc@gmail.com",
        MaSoDongHo = "abc123",
        NgayDangKy = DateTime.Now,
        DangHoatDong  = true,
    };
   var danhSach = new List<KhachHang>{kh1,kh2,kh3,kh4,kh5};
   var ketQua = "";
    foreach(var khachHang in danhSach){
        ketQua += khachHang.ToString() + "\n";
    }
    MessageBox.Show(ketQua, "Danh sách khách hàng");

}
}