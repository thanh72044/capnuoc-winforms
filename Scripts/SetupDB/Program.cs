using Microsoft.Data.SqlClient;

string[] connectionStrings = new[]
{
    "Server=localhost,50905;Trusted_Connection=true;TrustServerCertificate=true;",
    "Server=localhost\\MSSQLSERVER1;Trusted_Connection=true;TrustServerCertificate=true;",
    "Server=.\\MSSQLSERVER1;Trusted_Connection=true;TrustServerCertificate=true;",
    "Server=localhost;Trusted_Connection=true;TrustServerCertificate=true;",
};

string workingConnection = null;

Console.WriteLine("=== Tim ket noi SQL Server ===");
foreach (var cs in connectionStrings)
{
    try
    {
        using var testConn = new SqlConnection(cs);
        testConn.Open();
        Console.WriteLine("OK: " + cs);
        workingConnection = cs;
        break;
    }
    catch (Exception ex)
    {
        Console.WriteLine("Loi: " + cs + " -> " + ex.Message.Split('\n')[0]);
    }
}

if (workingConnection == null)
{
    Console.WriteLine("\nKhong tim duoc ket noi SQL Server!");
    Console.ReadKey();
    return;
}

Console.WriteLine("\n=== Tao Database CapNuocDB ===");
using var conn = new SqlConnection(workingConnection);
conn.Open();

var createDb = new SqlCommand("IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'CapNuocDB') CREATE DATABASE CapNuocDB", conn);
createDb.ExecuteNonQuery();
Console.WriteLine("Da tao database CapNuocDB");

conn.ChangeDatabase("CapNuocDB");

string sql = @"
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='KhachHang' AND xtype='U')
CREATE TABLE KhachHang (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    MaKhachHang     NVARCHAR(20)    NOT NULL UNIQUE,
    HoTen           NVARCHAR(100)   NOT NULL,
    DiaChi          NVARCHAR(255)   NOT NULL,
    SoDienThoai     NVARCHAR(15)    NOT NULL,
    Email           NVARCHAR(100),
    MaSoDongHo      NVARCHAR(20)    NOT NULL UNIQUE,
    NgayDangKy      DATETIME        DEFAULT GETDATE(),
    DangHoatDong    BIT             DEFAULT 1
);

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ChiSoDongHo' AND xtype='U')
CREATE TABLE ChiSoDongHo (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    KhachHangId     INT             NOT NULL REFERENCES KhachHang(Id),
    MaSoDongHo      NVARCHAR(20)    NOT NULL,
    ChiSoDau        INT             NOT NULL DEFAULT 0,
    ChiSoCuoi       INT             NOT NULL DEFAULT 0,
    NgayDoc         DATETIME        DEFAULT GETDATE(),
    Thang           INT             NOT NULL,
    Nam             INT             NOT NULL,
    DonGia          DECIMAL(10,2)   DEFAULT 7000,
    GhiChu          NVARCHAR(255)
);
";

new SqlCommand(sql, conn).ExecuteNonQuery();
Console.WriteLine("Da tao cac bang du lieu");

var checkData = new SqlCommand("SELECT COUNT(*) FROM KhachHang", conn);
int count = (int)checkData.ExecuteScalar();

if (count == 0)
{
    string insertSql = @"
    INSERT INTO KhachHang (MaKhachHang, HoTen, DiaChi, SoDienThoai, Email, MaSoDongHo) VALUES
    (N'KH-001', N'Nguyễn Văn An', N'12 Lý Tự Trọng, Q.1, TP.HCM', N'0901234567', N'an@email.com', N'DH-2024-001'),
    (N'KH-002', N'Trần Thị Bình', N'45 Nguyễn Huệ, Q.1, TP.HCM', N'0912345678', N'binh@email.com', N'DH-2024-002'),
    (N'KH-003', N'Lê Văn Cường', N'78 Đinh Tiên Hoàng, Q.Bình Thạnh', N'0923456789', N'', N'DH-2024-003'),
    (N'KH-004', N'Phạm Thị Dung', N'99 Cách Mạng Tháng 8, Q.3', N'0934567890', N'dung@email.com', N'DH-2024-004'),
    (N'KH-005', N'Hoàng Văn Em', N'23 Võ Văn Tần, Q.3, TP.HCM', N'0945678901', N'', N'DH-2024-005');";
    new SqlCommand(insertSql, conn).ExecuteNonQuery();
    Console.WriteLine("Da them du lieu mau (5 khach hang)");
}
else
{
    Console.WriteLine("Du lieu mau da ton tai, bo qua.");
}

// Tính connection string cho app chính
string appConnStr = workingConnection.Replace("Trusted_Connection=true;", "Database=CapNuocDB;Trusted_Connection=true;");

Console.WriteLine("\n========================================");
Console.WriteLine("THANH CONG! Database da san sang.");
Console.WriteLine("Connection string de dung trong app:");
Console.WriteLine(appConnStr);
Console.WriteLine("========================================");
Console.WriteLine("\nNhan Enter de thoat...");
Console.ReadLine();
