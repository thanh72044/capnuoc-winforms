using CapNuocApp.Models;
using Microsoft.Data.SqlClient;

namespace CapNuocApp.Data
{
    /// <summary>
    /// Lớp kết nối và thao tác cơ sở dữ liệu SQL Server
    /// </summary>
    public class DatabaseHelper
    {
        // TODO: Thay đổi connection string cho phù hợp với môi trường của bạn
        private static string _connectionString =
            "Server=localhost,50905;Database=CapNuocDB;Trusted_Connection=true;TrustServerCertificate=true;";

        /// <summary>
        /// Lấy toàn bộ danh sách khách hàng
        /// </summary>
        public static List<KhachHang> GetAllKhachHang()
        {
            var list = new List<KhachHang>();
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            string sql = "SELECT * FROM KhachHang WHERE DangHoatDong = 1 ORDER BY HoTen";
            using var cmd = new SqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(MapKhachHang(reader));
            }
            return list;
        }

        /// <summary>
        /// Tìm kiếm khách hàng theo tên hoặc mã số đồng hồ
        /// </summary>
        public static List<KhachHang> SearchKhachHang(string keyword)
        {
            var list = new List<KhachHang>();
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            string sql = @"SELECT * FROM KhachHang 
                           WHERE (HoTen LIKE @keyword OR MaKhachHang LIKE @keyword 
                                  OR MaSoDongHo LIKE @keyword)
                           AND DangHoatDong = 1
                           ORDER BY HoTen";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@keyword", $"%{keyword}%");
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(MapKhachHang(reader));
            }
            return list;
        }

        /// <summary>
        /// Thêm khách hàng mới vào database
        /// </summary>
        public static bool ThemKhachHang(KhachHang kh)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            string sql = @"INSERT INTO KhachHang 
                          (MaKhachHang, HoTen, DiaChi, SoDienThoai, Email, MaSoDongHo, NgayDangKy, DangHoatDong)
                          VALUES (@Ma, @HoTen, @DiaChi, @SDT, @Email, @MaDH, @Ngay, 1)";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Ma", kh.MaKhachHang);
            cmd.Parameters.AddWithValue("@HoTen", kh.HoTen);
            cmd.Parameters.AddWithValue("@DiaChi", kh.DiaChi);
            cmd.Parameters.AddWithValue("@SDT", kh.SoDienThoai);
            cmd.Parameters.AddWithValue("@Email", kh.Email ?? "");
            cmd.Parameters.AddWithValue("@MaDH", kh.MaSoDongHo);
            cmd.Parameters.AddWithValue("@Ngay", kh.NgayDangKy);

            return cmd.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Cập nhật thông tin khách hàng
        /// </summary>
        public static bool CapNhatKhachHang(KhachHang kh)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            string sql = @"UPDATE KhachHang SET 
                          HoTen = @HoTen, DiaChi = @DiaChi, SoDienThoai = @SDT, 
                          Email = @Email, MaSoDongHo = @MaDH
                          WHERE Id = @Id";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@HoTen", kh.HoTen);
            cmd.Parameters.AddWithValue("@DiaChi", kh.DiaChi);
            cmd.Parameters.AddWithValue("@SDT", kh.SoDienThoai);
            cmd.Parameters.AddWithValue("@Email", kh.Email ?? "");
            cmd.Parameters.AddWithValue("@MaDH", kh.MaSoDongHo);
            cmd.Parameters.AddWithValue("@Id", kh.Id);

            return cmd.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Xóa mềm khách hàng (set DangHoatDong = 0)
        /// </summary>
        public static bool XoaKhachHang(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            string sql = "UPDATE KhachHang SET DangHoatDong = 0 WHERE Id = @Id";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);

            return cmd.ExecuteNonQuery() > 0;
        }

        // Map từ SqlDataReader sang KhachHang object
        private static KhachHang MapKhachHang(SqlDataReader reader)
        {
            return new KhachHang
            {
                Id = (int)reader["Id"],
                MaKhachHang = reader["MaKhachHang"].ToString()!,
                HoTen = reader["HoTen"].ToString()!,
                DiaChi = reader["DiaChi"].ToString()!,
                SoDienThoai = reader["SoDienThoai"].ToString()!,
                Email = reader["Email"].ToString()!,
                MaSoDongHo = reader["MaSoDongHo"].ToString()!,
                NgayDangKy = (DateTime)reader["NgayDangKy"],
                DangHoatDong = (bool)reader["DangHoatDong"]
            };
        }
    }
}
