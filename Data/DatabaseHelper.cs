using System;
using System.Collections.Generic;
using CapNuocApp.Models;
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
}
