using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using CapNuocApp.Models;
using DocumentFormat.OpenXml.Drawing.Charts;
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
}
