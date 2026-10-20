// Script tạo database CapNuocDB
// Chạy: dotnet script Scripts/CreateDatabase.cs (hoặc copy vào Program.cs tạm thời)

using Microsoft.Data.SqlClient;

string[] connectionStrings = new[]
{
    "Server=localhost,50905;Trusted_Connection=true;TrustServerCertificate=true;",
    "Server=localhost\\MSSQLSERVER1;Trusted_Connection=true;TrustServerCertificate=true;",
    "Server=.\\MSSQLSERVER1;Trusted_Connection=true;TrustServerCertificate=true;",
    "Server=localhost;Trusted_Connection=true;TrustServerCertificate=true;",
};

string? workingConnection = null;

Console.WriteLine("=== Tìm kết nối SQL Server ===");
foreach (var cs in connectionStrings)
{
    try
    {
        using var testConn = new SqlConnection(cs);
        testConn.Open();
        Console.WriteLine($"✅ Kết nối thành công: {cs}");
        workingConnection = cs;
        break;
    }
    catch
    {
        Console.WriteLine($"❌ Thất bại: {cs}");
    }
}

if (workingConnection == null)
{
    Console.WriteLine("Không tìm được kết nối SQL Server!");
    return;
}

// Tạo database
Console.WriteLine("\n=== Tạo Database CapNuocDB ===");
using var conn = new SqlConnection(workingConnection);
conn.Open();

string[] sqlCommands = {
    "IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'CapNuocDB') CREATE DATABASE CapNuocDB",
};

foreach (var sql in sqlCommands)
{
    using var cmd = new SqlCommand(sql, conn);
    cmd.ExecuteNonQuery();
}

// Chuyển sang CapNuocDB
conn.ChangeDatabase("CapNuocDB");

string createTables = @"
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='KhachHang')
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

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ChiSoDongHo')
CREATE TABLE ChiSoDongHo (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    KhachHangId     INT             NOT NULL FOREIGN KEY REFERENCES KhachHang(Id),
    MaSoDongHo      NVARCHAR(20)    NOT NULL,
    ChiSoDau        INT             NOT NULL DEFAULT 0,
    ChiSoCuoi       INT             NOT NULL DEFAULT 0,
    NgayDoc         DATETIME        DEFAULT GETDATE(),
    Thang           INT             NOT NULL,
    Nam             INT             NOT NULL,
    DonGia          DECIMAL(10,2)   DEFAULT 7000,
    GhiChu          NVARCHAR(255)
);

IF NOT EXISTS (SELECT TOP 1 1 FROM KhachHang)
BEGIN
    INSERT INTO KhachHang (MaKhachHang, HoTen, DiaChi, SoDienThoai, Email, MaSoDongHo) VALUES
    (N'KH-001', N'Nguyễn Văn An', N'12 Lý Tự Trọng, Q.1, TP.HCM', N'0901234567', N'an@email.com', N'DH-2024-001'),
    (N'KH-002', N'Trần Thị Bình', N'45 Nguyễn Huệ, Q.1, TP.HCM', N'0912345678', N'binh@email.com', N'DH-2024-002'),
    (N'KH-003', N'Lê Văn Cường', N'78 Đinh Tiên Hoàng, Q.Bình Thạnh', N'0923456789', N'', N'DH-2024-003'),
    (N'KH-004', N'Phạm Thị Dung', N'99 Cách Mạng Tháng 8, Q.3', N'0934567890', N'dung@email.com', N'DH-2024-004'),
    (N'KH-005', N'Hoàng Văn Em', N'23 Võ Văn Tần, Q.3, TP.HCM', N'0945678901', N'', N'DH-2024-005');
END
";

using var cmdTables = new SqlCommand(createTables, conn);
cmdTables.ExecuteNonQuery();

Console.WriteLine("✅ Database CapNuocDB đã được tạo với dữ liệu mẫu!");
Console.WriteLine($"\n📌 Connection string của bạn:\nServer=localhost,50905;Database=CapNuocDB;Trusted_Connection=true;TrustServerCertificate=true;");
