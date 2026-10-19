-- ============================================================
-- Script tạo Database cho hệ thống Quản lý Cấp Nước Bến Thành
-- Chạy script này trên SQL Server Management Studio (SSMS)
-- ============================================================

-- Tạo database
CREATE DATABASE CapNuocDB;
GO

USE CapNuocDB;
GO

-- Bảng khách hàng
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

-- Bảng chỉ số đồng hồ
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

-- Bảng hóa đơn
CREATE TABLE HoaDon (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    MaHoaDon        NVARCHAR(20)    NOT NULL UNIQUE,
    KhachHangId     INT             NOT NULL FOREIGN KEY REFERENCES KhachHang(Id),
    ChiSoId         INT             FOREIGN KEY REFERENCES ChiSoDongHo(Id),
    SoLuong         INT             NOT NULL,
    DonGia          DECIMAL(10,2)   NOT NULL,
    ThanhTien       DECIMAL(12,2)   NOT NULL,
    NgayLap         DATETIME        DEFAULT GETDATE(),
    HanThanhToan    DATETIME,
    DaThanhToan     BIT             DEFAULT 0,
    NgayThanhToan   DATETIME
);

-- Dữ liệu mẫu để test
INSERT INTO KhachHang (MaKhachHang, HoTen, DiaChi, SoDienThoai, Email, MaSoDongHo) VALUES
('KH-001', N'Nguyễn Văn An', N'12 Lý Tự Trọng, Q.1, TP.HCM', '0901234567', 'an@email.com', 'DH-2024-001'),
('KH-002', N'Trần Thị Bình', N'45 Nguyễn Huệ, Q.1, TP.HCM', '0912345678', 'binh@email.com', 'DH-2024-002'),
('KH-003', N'Lê Văn Cường', N'78 Đinh Tiên Hoàng, Q.Bình Thạnh', '0923456789', '', 'DH-2024-003'),
('KH-004', N'Phạm Thị Dung', N'99 Cách Mạng Tháng 8, Q.3', '0934567890', 'dung@email.com', 'DH-2024-004'),
('KH-005', N'Hoàng Văn Em', N'23 Võ Văn Tần, Q.3, TP.HCM', '0945678901', '', 'DH-2024-005');

-- Dữ liệu chỉ số mẫu
INSERT INTO ChiSoDongHo (KhachHangId, MaSoDongHo, ChiSoDau, ChiSoCuoi, Thang, Nam) VALUES
(1, 'DH-2024-001', 1000, 1018, 10, 2026),
(2, 'DH-2024-002', 500, 522, 10, 2026),
(3, 'DH-2024-003', 2300, 2315, 10, 2026);

PRINT N'Database CapNuocDB đã được tạo thành công!';
GO
