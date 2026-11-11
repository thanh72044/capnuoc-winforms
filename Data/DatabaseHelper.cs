using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using CapNuocApp.Models;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2016.Drawing;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Data.SqlClient;

namespace CapNuocApp.Data;

public class DatabaseHelper
{
    private static string _connectionString =
        @"Server=localhost\MSSQLSERVER1;Database=CapNuocDB;Trusted_Connection=true;TrustServerCertificate=true;";

    public static List<KhachHang> GetAllKhachHang()
    {
        var danhSach = new List<KhachHang>();
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        string sql = "SELECT * FROM  KhachHang WHERE DangHoatDong = 1 ORDER BY HoTen  DESC ";

        using var cmd = new SqlCommand(sql, conn);

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var kh = new KhachHang
            {
                Id = (int)reader["Id"],
                MaKhachHang = reader["MaKhachHang"].ToString() ?? "",
                HoTen = reader["HoTen"].ToString() ?? "",
                DiaChi = reader["DiaChi"].ToString() ?? "",
                SoDienThoai = reader["SoDienThoai"].ToString() ?? "",
                Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() ?? "" : "",
                MaSoDongHo = reader["MaSoDongHo"].ToString() ?? "",
                NgayDangKy = (DateTime)reader["NgayDangKy"],
                DangHoatDong = (bool)reader["DangHoatDong"],
            };
            danhSach.Add(kh);
        }
        return danhSach;
    }

    public static bool ThemKhachHang(KhachHang kh)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        string sql =
            @"INSERT INTO KhachHang (MaKhachHang, HoTen, DiaChi, SoDienThoai, Email, MaSoDongHo) VALUES (@Ma, @HoTen, @DiaChi, @SDT, @Email, @MaDH)";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Ma", kh.MaKhachHang);
        cmd.Parameters.AddWithValue("@HoTen", kh.HoTen);
        cmd.Parameters.AddWithValue("@DiaChi", kh.DiaChi);
        cmd.Parameters.AddWithValue("@SDT", kh.SoDienThoai);
        cmd.Parameters.AddWithValue("@Email", kh.Email ?? "");
        cmd.Parameters.AddWithValue("@MaDH", kh.MaSoDongHo);
        int soDongBiAnhHuong = cmd.ExecuteNonQuery();
        return soDongBiAnhHuong > 0;
    }

    public static bool CapNhatKhacHang(KhachHang kh)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        string sql =
            @"UPDATE KhachHang 
                       SET HoTen = @HoTen, DiaChi = @DiaChi, SoDienThoai = @SDT, Email = @Email 
                       WHERE Id = @Id";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", kh.Id);
        cmd.Parameters.AddWithValue("@HoTen", kh.HoTen);
        cmd.Parameters.AddWithValue("@DiaChi", kh.DiaChi);
        cmd.Parameters.AddWithValue("@SDT", kh.SoDienThoai);
        cmd.Parameters.AddWithValue("@Email", kh.Email ?? "");
        int soDong = cmd.ExecuteNonQuery();
        return soDong > 0;
    }

    public static bool XoaKhachHang(KhachHang kh)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        string sql = @"UPDATE KhachHang SET DangHoatDong = 0 WHERE Id = @Id";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue(@"Id", kh.Id);
        int soDong = cmd.ExecuteNonQuery();
        return soDong > 0;
    }

    public static int DemSoKhachHang()
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        string sql = @"SELECT COUNT(*) FROM KhachHang WHERE DangHoatDong = 1";
        using var cmd = new SqlCommand(sql, conn);
        object result = cmd.ExecuteScalar();
        return result != null ? Convert.ToInt32(result) : 0;
    }

    public static decimal TinhTongDoanhThu()
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        string sql = @"SELECT SUM((ChiSoCuoi - ChiSoDau) * DonGia) FROM ChiSoDongHo";
        using var cmd = new SqlCommand(sql, conn);
        object result = cmd.ExecuteScalar();
        if (result != null && result != DBNull.Value)
        {
            return Convert.ToDecimal(result);
        }
        return 0m;
    }

    public static List<KhachHang> SearchKhachHang(string tuKhoa)
    {
        var danhSach = new List<KhachHang>();
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        string sql =
            @"SELECT * FROM KhachHang 
                       WHERE (HoTen LIKE @kw OR MaKhachHang LIKE @kw OR SoDienThoai LIKE @kw) 
                       AND DangHoatDong = 1 
                       ORDER BY HoTen ASC";

        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@kw", $"%{tuKhoa}%");
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var kh = new KhachHang
            {
                Id = (int)reader["Id"],
                MaKhachHang = reader["MaKhachHang"].ToString() ?? "",
                HoTen = reader["HoTen"].ToString() ?? "",
                DiaChi = reader["DiaChi"].ToString() ?? "",
                SoDienThoai = reader["SoDienThoai"].ToString() ?? "",
                Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() ?? "" : "",
                MaSoDongHo = reader["MaSoDongHo"].ToString() ?? "",
                NgayDangKy = (DateTime)reader["NgayDangKy"],
                DangHoatDong = (bool)reader["DangHoatDong"],
            };
            danhSach.Add(kh);
        }
        return danhSach;
    }

    public static bool ThemChiSoNuoc(ChiSoDongHo cs)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        string sql =
            @"INSERT INTO ChiSoDongHo (KhachHangId, MaSoDongHo, ChiSoDau, ChiSoCuoi, Thang, Nam, DonGia)
                       VALUES (@KhachHangId, @MaDH, @Dau, @Cuoi, @Thang, @Nam, @DonGia)";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@KhachHangId", cs.KhachHangId);
        cmd.Parameters.AddWithValue("@MaDH", cs.MaSoDongHo);
        cmd.Parameters.AddWithValue("@Dau", cs.ChiSoDau);
        cmd.Parameters.AddWithValue("@Cuoi", cs.ChiSoCuoi);
        cmd.Parameters.AddWithValue("@Thang", cs.Thang);
        cmd.Parameters.AddWithValue("@Nam", cs.Nam > 0 ? cs.Nam : DateTime.Now.Year);
        cmd.Parameters.AddWithValue("@DonGia", cs.DonGia);
        int soDong = cmd.ExecuteNonQuery();
        return soDong > 0;
    }

    public static List<ChiSoDongHo> GetChiSoDongHo(int khachHangId)
    {
        var danhSach = new List<ChiSoDongHo>();
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        string sql =
            @"SELECT * FROM ChiSoDongHo 
                       WHERE KhachHangId = @KhId 
                       ORDER BY Nam DESC, Thang DESC";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@KhId", khachHangId);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var cs = new ChiSoDongHo
            {
                Id = (int)reader["Id"],
                KhachHangId = (int)reader["KhachHangId"],
                MaSoDongHo = reader["MaSoDongHo"].ToString() ?? "",
                ChiSoDau = (int)reader["ChiSoDau"],
                ChiSoCuoi = (int)reader["ChiSoCuoi"],
                Thang = (int)reader["Thang"],
                Nam = (int)reader["Nam"],
                DonGia = reader["DonGia"] != DBNull.Value ? (decimal)reader["DonGia"] : 7000m,
            };
            danhSach.Add(cs);
        }
        return danhSach;
    }

    public static List<NhanVien> GetAllNhanVien()
    {
        var danhsach = new List<NhanVien>();
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        var sql = $"SELECT * FROM NhanVien WHERE DangHoatDong = 1 ORDER BY MaNhanVien ASC";
        using var cmd = new SqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var nv = new NhanVien
            {
                Id = (int)reader["Id"],
                MaNhanVien = reader["MaNhanVien"].ToString() ?? "",
                HoTen = reader["HoTen"].ToString() ?? "",
                SoDienThoai = reader["SoDienThoai"].ToString() ?? "",
                KhuVucPhuTrach = reader["KhuVucPhuTrach"].ToString() ?? "",
                DangHoatDong = (bool)reader["DangHoatDong"],
            };
            danhsach.Add(nv);
        }
        return danhsach;
    }

    public static bool ThemNhanVien(NhanVien nv)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        var sql =
            $"INSERT INTO NhanVien (MaNhanVien, HoTen, SoDienThoai, KhuVucPhuTrach) VALUES (@Ma, @HoTen, @SDT, @KhuVuc)";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Ma", nv.MaNhanVien);
        cmd.Parameters.AddWithValue("@HoTen", nv.HoTen);
        cmd.Parameters.AddWithValue("@SDT", nv.SoDienThoai);
        cmd.Parameters.AddWithValue("@KhuVuc", nv.KhuVucPhuTrach);
        int soDong = cmd.ExecuteNonQuery();
        return soDong > 0;
    }

    public static bool CapNhatNhanVien(NhanVien nv)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        string sql =
            @"UPDATE NhanVien SET HoTen = @HoTen, SoDienThoai = @SDT, KhuVucPhuTrach = @KhuVuc WHERE Id = @Id";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", nv.Id);
        cmd.Parameters.AddWithValue("@HoTen", nv.HoTen);
        cmd.Parameters.AddWithValue("@SDT", nv.SoDienThoai);
        cmd.Parameters.AddWithValue("@KhuVuc", nv.KhuVucPhuTrach);
        int soDong = cmd.ExecuteNonQuery();
        return soDong > 0;
    }

    public static bool XoaNhanVien(NhanVien nv)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        string sql = @"UPDATE NhanVien SET DangHoatDong = 0 WHERE Id = @Id";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", nv.Id);
        int soDong = cmd.ExecuteNonQuery();
        return soDong > 0;
    }

    public static bool XoaNhanVien(int id) => XoaNhanVien(new NhanVien { Id = id });

    public static bool TaoHoaDon(HoaDon hd)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        string sql =
            @"INSERT INTO HoaDon 
                   (MaHoaDon, KhachHangId, ChiSoDongHoId, Thang, Nam, SoM3TieuThu, DonGia, ThanhTien)
                   VALUES 
                   (@MaHD, @KhId, @CsId, @Thang, @Nam, @SoM3, @DonGia, @ThanhTien)";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@MaHD", hd.MaHoaDon);
        cmd.Parameters.AddWithValue("@KhId", hd.KhachHangId);
        // Nếu ChiSoDongHoId null thì lưu DBNull.Value vào database
        cmd.Parameters.AddWithValue("@CsId", (object?)hd.ChiSoDongHoId ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Thang", hd.Thang);
        cmd.Parameters.AddWithValue("@Nam", hd.Nam);
        cmd.Parameters.AddWithValue("@SoM3", hd.SoM3TieuThu);
        cmd.Parameters.AddWithValue("@DonGia", hd.DonGia);
        cmd.Parameters.AddWithValue("@ThanhTien", hd.ThanhTien);
        return cmd.ExecuteNonQuery() > 0;
    }

    public static List<HoaDon> GetAllHoaDon()
    {
        var list = new List<HoaDon>();
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        string sql =
            @"SELECT hd.*, kh.MaKhachHang, kh.HoTen, kh.SoDienThoai
                   FROM HoaDon hd
                   INNER JOIN KhachHang kh ON hd.KhachHangId = kh.Id
                   ORDER BY hd.Nam DESC, hd.Thang DESC, hd.Id DESC";
        using var cmd = new SqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(
                new HoaDon
                {
                    Id = (int)reader["Id"],
                    MaHoaDon = reader["MaHoaDon"].ToString() ?? "",
                    KhachHangId = (int)reader["KhachHangId"],
                    ChiSoDongHoId =
                        reader["ChiSoDongHoId"] != DBNull.Value
                            ? (int?)reader["ChiSoDongHoId"]
                            : null,
                    Thang = (int)reader["Thang"],
                    Nam = (int)reader["Nam"],
                    SoM3TieuThu = (int)reader["SoM3TieuThu"],
                    DonGia = (decimal)reader["DonGia"],
                    ThanhTien = (decimal)reader["ThanhTien"],
                    NgayLap = (DateTime)reader["NgayLap"],
                    DaThanhToan = (bool)reader["DaThanhToan"],
                    NgayThanhToan =
                        reader["NgayThanhToan"] != DBNull.Value
                            ? (DateTime?)reader["NgayThanhToan"]
                            : null,

                    // Dữ liệu lấy từ bảng KhachHang qua INNER JOIN
                    MaKhachHang = reader["MaKhachHang"].ToString() ?? "",
                    TenKhachHang = reader["HoTen"].ToString() ?? "",
                    SoDienThoai = reader["SoDienThoai"].ToString() ?? "",
                }
            );
        }
        return list;
    }

    public static bool ThanhToanHoaDon(int hoaDonId)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        string sql =
            @"UPDATE HoaDon 
                   SET DaThanhToan = 1, NgayThanhToan = GETDATE() 
                   WHERE Id = @Id";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", hoaDonId);
        return cmd.ExecuteNonQuery() > 0;
    }

    public static bool ThanhToanHoaDon(HoaDon hd) => ThanhToanHoaDon(hd.Id);
}
