using System;
using System.Drawing;
using System.Windows.Forms;
using CapNuocApp.Data;
using CapNuocApp.Models;
using Microsoft.Data.SqlClient;

namespace CapNuocApp;

public class FormGhiNuoc : Form
{
    private int _khachHangId;
    private string _maSoDongHo;

    // Các ô nhập liệu trên popup
    private TextBox txtChiSoDau;
    private TextBox txtChiSoCuoi;
    private ComboBox cboThang;
    private Label lblTieuThu;
    private Label lblThanhTien;
    private DataGridView dgvLichSu;

    private const decimal DON_GIA = 7000m; // 7.000đ/m3 nước sinh hoạt

    // Constructor nhận 3 thông tin từ MainForm truyền sang
    public FormGhiNuoc(int khachHangId, string hoTen, string maDH)
    {
        _khachHangId = khachHangId;
        _maSoDongHo = maDH;

        InitializeUI(hoTen, maDH);
    }

    private void InitializeUI(string hoTen, string maDH)
    {
        // Cấu hình cửa sổ popup
        this.Text = "Ghi Nhận Chỉ Số Đồng Hồ Nước";
        this.Size = new Size(820, 480);
        this.StartPosition = FormStartPosition.CenterParent; // Mở ra ngay giữa màn hình cha
        this.FormBorderStyle = FormBorderStyle.FixedDialog; // Cố định kích thước, không cho kéo dãn
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.BackColor = Color.FromArgb(245, 248, 252);

        // Tiêu đề và thông tin khách hàng
        var lblHeader = new Label
        {
            Text = $"💧 KHÁCH HÀNG: {hoTen}\n📌 Mã ĐH: {maDH}",
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            ForeColor = Color.FromArgb(0, 102, 204),
            Location = new Point(25, 20),
            Size = new Size(360, 45),
        };
        this.Controls.Add(lblHeader);

        int labelX = 30,
            inputX = 160,
            startY = 85,
            gapY = 45;

        // 1. Chọn Tháng
        this.Controls.Add(
            new Label
            {
                Text = "Tháng ghi:",
                Location = new Point(labelX, startY),
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5f),
            }
        );
        cboThang = new ComboBox
        {
            Location = new Point(inputX, startY - 3),
            Size = new Size(200, 26),
            DropDownStyle = ComboBoxStyle.DropDownList,
        };
        for (int i = 1; i <= 12; i++)
            cboThang.Items.Add($"Tháng {i}");
        cboThang.SelectedIndex = DateTime.Now.Month - 1; // Mặc định chọn tháng hiện tại
        this.Controls.Add(cboThang);

        // 2. Chỉ số đầu (Số cũ)
        this.Controls.Add(
            new Label
            {
                Text = "Chỉ số ĐẦU (m³):",
                Location = new Point(labelX, startY + gapY),
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5f),
            }
        );
        txtChiSoDau = new TextBox
        {
            Location = new Point(inputX, startY + gapY - 3),
            Size = new Size(200, 26),
            Text = "0",
            Font = new Font("Segoe UI", 10),
        };
        txtChiSoDau.TextChanged += (s, e) => TinhTien();
        this.Controls.Add(txtChiSoDau);

        // 3. Chỉ số cuối (Số mới vừa ghi)
        this.Controls.Add(
            new Label
            {
                Text = "Chỉ số CUỐI (m³):",
                Location = new Point(labelX, startY + gapY * 2),
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5f),
            }
        );
        txtChiSoCuoi = new TextBox
        {
            Location = new Point(inputX, startY + gapY * 2 - 3),
            Size = new Size(200, 26),
            Text = "0",
            Font = new Font("Segoe UI", 10),
        };
        txtChiSoCuoi.TextChanged += (s, e) => TinhTien();
        this.Controls.Add(txtChiSoCuoi);

        // 4. Kết quả tính toán tự động
        lblTieuThu = new Label
        {
            Text = "Tiêu thụ: 0 m³",
            Location = new Point(labelX, startY + gapY * 3),
            AutoSize = true,
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            ForeColor = Color.DarkSlateGray,
        };
        this.Controls.Add(lblTieuThu);

        lblThanhTien = new Label
        {
            Text = "Thành tiền: 0 VNĐ",
            Location = new Point(labelX, startY + gapY * 4),
            AutoSize = true,
            Font = new Font("Segoe UI", 11, FontStyle.Bold),
            ForeColor = Color.FromArgb(220, 53, 69),
        };
        this.Controls.Add(lblThanhTien);

        // 5. Nút bấm Lưu và Hủy
        var btnLuu = new Button
        {
            Text = "💾 Lưu Chỉ Số",
            Location = new Point(60, 350),
            Size = new Size(135, 40),
            BackColor = Color.FromArgb(40, 167, 69),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            Cursor = Cursors.Hand,
        };
        btnLuu.FlatAppearance.BorderSize = 0;
        btnLuu.Click += (s, e) => ActionLuu();
        this.Controls.Add(btnLuu);

        var btnDong = new Button
        {
            Text = "❌ Đóng",
            Location = new Point(215, 350),
            Size = new Size(135, 40),
            BackColor = Color.FromArgb(108, 117, 125),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 10),
            Cursor = Cursors.Hand,
        };
        btnDong.FlatAppearance.BorderSize = 0;
        btnDong.Click += (s, e) => this.Close();
        this.Controls.Add(btnDong);

        // Tính toán ban đầu
        TinhTien();

        // Tiêu đề bảng lịch sử
        var lblLichSu = new Label
        {
            Text = "📋 LỊCH SỬ DÙNG NƯỚC CÁC THÁNG TRƯỚC",
            Font = new Font("Segoe UI", 10.5f, FontStyle.Bold),
            ForeColor = Color.FromArgb(0, 102, 204),
            Location = new Point(400, 20),
            AutoSize = true,
        };
        this.Controls.Add(lblLichSu);

        // Bảng DataGridView hiển thị lịch sử
        dgvLichSu = new DataGridView
        {
            Location = new Point(400, 55),
            Size = new Size(380, 340),
            BackgroundColor = Color.White,
            ReadOnly = true,
            AllowUserToAddRows = false,
            RowHeadersVisible = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        };
        this.Controls.Add(dgvLichSu);

        // Gọi hàm nạp dữ liệu lịch sử lên bảng
        LoadLichSu();
    }

    public void LoadLichSu()
    {
        var danhSach = DatabaseHelper.GetChiSoDongHo(_khachHangId);
        dgvLichSu.DataSource = danhSach;
        if (danhSach.Count > 0)
        {
            txtChiSoDau.Text = danhSach[0].ChiSoCuoi.ToString();
        }
    }

    /// <summary>
    /// Tự động tính số nước tiêu thụ và thành tiền mỗi khi người dùng gõ số
    /// </summary>
    private void TinhTien()
    {
        int.TryParse(txtChiSoDau?.Text.Trim(), out int dau);
        int.TryParse(txtChiSoCuoi?.Text.Trim(), out int cuoi);

        int tieuThu = cuoi - dau;
        if (tieuThu < 0)
            tieuThu = 0;

        decimal tien = tieuThu * DON_GIA;
        if (lblTieuThu != null)
            lblTieuThu.Text = $"💧 Tiêu thụ: {tieuThu} m³";
        if (lblThanhTien != null)
            lblThanhTien.Text = $"💰 Thành tiền: {tien:N0} VNĐ";
    }

    /// <summary>
    /// Đóng gói dữ liệu vào Model ChiSoDongHo và gọi DatabaseHelper lưu xuống SQL
    /// </summary>
    private void ActionLuu()
    {
        if (
            !int.TryParse(txtChiSoDau.Text.Trim(), out int dau)
            || !int.TryParse(txtChiSoCuoi.Text.Trim(), out int cuoi)
        )
        {
            MessageBox.Show("Chỉ số đầu và cuối phải là số nguyên hợp lệ!", "Lỗi nhập liệu");
            return;
        }

        if (cuoi < dau)
        {
            MessageBox.Show("Chỉ số cuối không được nhỏ hơn chỉ số đầu!", "Cảnh báo");
            return;
        }

        var cs = new ChiSoDongHo
        {
            KhachHangId = _khachHangId,
            MaSoDongHo = _maSoDongHo,
            ChiSoDau = dau,
            ChiSoCuoi = cuoi,
            Thang = cboThang.SelectedIndex + 1,
            Nam = DateTime.Now.Year,
            DonGia = DON_GIA,
        };

        try
        {
            if (DatabaseHelper.ThemChiSoNuoc(cs))
            {
                // ==============================================================
                // 🌟 TỰ ĐỘNG SINH HÓA ĐƠN TIỀN NƯỚC KHI GHI CHỈ SỐ THÀNH CÔNG
                // ==============================================================
                int soM3 = cuoi - dau;
                decimal thanhTien = soM3 * DON_GIA;
                // Mã hóa đơn format: HD-NămTháng-MãKH (VD: HD-202611-1)
                string maHD = $"HD-{cs.Nam}{cs.Thang:D2}-{cs.KhachHangId}";

                var hd = new HoaDon
                {
                    MaHoaDon = maHD,
                    KhachHangId = cs.KhachHangId,
                    Thang = cs.Thang,
                    Nam = cs.Nam,
                    SoM3TieuThu = soM3,
                    DonGia = DON_GIA,
                    ThanhTien = thanhTien,
                    DaThanhToan = false, // Mới tạo hóa đơn thì chưa thanh toán
                };

                bool taoHoaDonThanhCong = false;
                bool daCoHoaDonTruocDo = false;

                try
                {
                    taoHoaDonThanhCong = DatabaseHelper.TaoHoaDon(hd);
                }
                catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
                {
                    // Bắt đúng mã lỗi 2627 / 2601 của SQL Server: Trùng mã hóa đơn (UNIQUE)
                    daCoHoaDonTruocDo = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Đã lưu chỉ số nhưng không thể tạo hóa đơn: {ex.Message}",
                        "Lỗi tạo hóa đơn",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }

                // Thông báo rõ ràng cho người dùng biết kết quả
                if (taoHoaDonThanhCong)
                {
                    MessageBox.Show(
                        $"Ghi nhận chỉ số nước thành công!\nĐã tự động tạo hóa đơn [{maHD}] ({thanhTien:N0} VNĐ) 🎉",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                else if (daCoHoaDonTruocDo)
                {
                    MessageBox.Show(
                        $"Ghi nhận chỉ số nước thành công!\n⚠️ Lưu ý: Hóa đơn kỳ tháng {cs.Thang}/{cs.Nam} của khách hàng này đã tồn tại trước đó.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Lỗi lưu cơ sở dữ liệu: {ex.Message}",
                "Lỗi SQL",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
    }
}
