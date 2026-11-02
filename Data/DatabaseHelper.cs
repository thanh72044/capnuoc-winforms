using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
}
