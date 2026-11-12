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
    private Label lblBac1;
    private Label lblBac2;
    private Label lblBac3;
    private Label lblBac4;
    private Label lblThuePhi;
    private Label lblThanhTien;
    private DataGridView dgvLichSu;

    // Lưu kết quả tính tiền bậc thang gần nhất
    private KetQuaTinhTien _ketQuaHienTai = new();

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
        this.Text = "Ghi Nhận Chỉ Số & Tính Tiền Nước Bậc Thang";
        this.Size = new Size(1000, 620);
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
            Location = new Point(25, 15),
            Size = new Size(365, 45),
        };
        this.Controls.Add(lblHeader);

        int labelX = 25,
            inputX = 150,
            startY = 70,
            gapY = 38;

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
            Size = new Size(220, 26),
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
            Size = new Size(220, 26),
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
            Size = new Size(220, 26),
            Text = "0",
            Font = new Font("Segoe UI", 10),
        };
        txtChiSoCuoi.TextChanged += (s, e) => TinhTien();
        this.Controls.Add(txtChiSoCuoi);

        // 4. Khung chi tiết tính tiền nước bậc thang
        var grpChiTiet = new GroupBox
        {
            Text = "📊 Bảng Kê Tiền Nước Lũy Tiến",
            Location = new Point(25, 190),
            Size = new Size(365, 305),
            Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
            ForeColor = Color.FromArgb(40, 40, 40),
            BackColor = Color.White,
        };

        lblTieuThu = new Label
        {
            Text = "💧 Tổng tiêu thụ: 0 m³",
            Location = new Point(15, 28),
            AutoSize = true,
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            ForeColor = Color.DarkSlateBlue,
        };
        grpChiTiet.Controls.Add(lblTieuThu);

        lblBac1 = new Label
        {
            Text = "• Bậc 1 (≤ 10 m³): 0 m³ × 6.700đ = 0đ",
            Location = new Point(15, 58),
            AutoSize = true,
            Font = new Font("Segoe UI", 9, FontStyle.Regular),
            ForeColor = Color.FromArgb(50, 50, 50),
        };
        grpChiTiet.Controls.Add(lblBac1);

        lblBac2 = new Label
        {
            Text = "• Bậc 2 (11 - 20 m³): 0 m³ × 7.100đ = 0đ",
            Location = new Point(15, 83),
            AutoSize = true,
            Font = new Font("Segoe UI", 9, FontStyle.Regular),
            ForeColor = Color.FromArgb(50, 50, 50),
        };
        grpChiTiet.Controls.Add(lblBac2);

        lblBac3 = new Label
        {
            Text = "• Bậc 3 (21 - 30 m³): 0 m³ × 8.700đ = 0đ",
            Location = new Point(15, 108),
            AutoSize = true,
            Font = new Font("Segoe UI", 9, FontStyle.Regular),
            ForeColor = Color.FromArgb(50, 50, 50),
        };
        grpChiTiet.Controls.Add(lblBac3);

        lblBac4 = new Label
        {
            Text = "• Bậc 4 (> 30 m³): 0 m³ × 16.000đ = 0đ",
            Location = new Point(15, 133),
            AutoSize = true,
            Font = new Font("Segoe UI", 9, FontStyle.Regular),
            ForeColor = Color.FromArgb(50, 50, 50),
        };
        grpChiTiet.Controls.Add(lblBac4);

        var lblDivider = new Label
        {
            Text = "────────────────────────────────────",
            Location = new Point(15, 155),
            AutoSize = true,
            ForeColor = Color.LightGray,
        };
        grpChiTiet.Controls.Add(lblDivider);

        lblThuePhi = new Label
        {
            Text = "Thuế VAT (5%): 0đ  |  Phí BVMT (10%): 0đ",
            Location = new Point(15, 180),
            AutoSize = true,
            Font = new Font("Segoe UI", 9, FontStyle.Regular),
            ForeColor = Color.DimGray,
        };
        grpChiTiet.Controls.Add(lblThuePhi);

        lblThanhTien = new Label
        {
            Text = "💰 TỔNG CỘNG: 0 VNĐ",
            Location = new Point(15, 215),
            AutoSize = true,
            Font = new Font("Segoe UI", 11.5f, FontStyle.Bold),
            ForeColor = Color.FromArgb(220, 53, 69),
        };
        grpChiTiet.Controls.Add(lblThanhTien);

        this.Controls.Add(grpChiTiet);

        // 5. Nút bấm Lưu và Hủy
        var btnLuu = new Button
        {
            Text = "💾 Lưu Chỉ Số",
            Location = new Point(35, 515),
            Size = new Size(165, 42),
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
            Location = new Point(215, 515),
            Size = new Size(165, 42),
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
            Location = new Point(415, 15),
            AutoSize = true,
        };
        this.Controls.Add(lblLichSu);

        // Bảng DataGridView hiển thị lịch sử
        dgvLichSu = new DataGridView
        {
            Location = new Point(415, 45),
            Size = new Size(545, 512),
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

        // Ẩn các cột kỹ thuật không cần thiết để bảng gọn gàng, thoáng mắt
        if (dgvLichSu.Columns["Id"] != null) dgvLichSu.Columns["Id"].Visible = false;
        if (dgvLichSu.Columns["KhachHangId"] != null) dgvLichSu.Columns["KhachHangId"].Visible = false;
        if (dgvLichSu.Columns["MaSoDongHo"] != null) dgvLichSu.Columns["MaSoDongHo"].Visible = false;
        if (dgvLichSu.Columns["DonGia"] != null) dgvLichSu.Columns["DonGia"].Visible = false;
        if (dgvLichSu.Columns["ThongTinNgan"] != null) dgvLichSu.Columns["ThongTinNgan"].Visible = false;

        // Đổi tên tiêu đề tiếng Việt và định dạng cột
        if (dgvLichSu.Columns["Thang"] != null) { dgvLichSu.Columns["Thang"].HeaderText = "Tháng"; dgvLichSu.Columns["Thang"].FillWeight = 50; }
        if (dgvLichSu.Columns["Nam"] != null) { dgvLichSu.Columns["Nam"].HeaderText = "Năm"; dgvLichSu.Columns["Nam"].FillWeight = 55; }
        if (dgvLichSu.Columns["ChiSoDau"] != null) { dgvLichSu.Columns["ChiSoDau"].HeaderText = "Số Đầu"; dgvLichSu.Columns["ChiSoDau"].FillWeight = 65; }
        if (dgvLichSu.Columns["ChiSoCuoi"] != null) { dgvLichSu.Columns["ChiSoCuoi"].HeaderText = "Số Cuối"; dgvLichSu.Columns["ChiSoCuoi"].FillWeight = 65; }
        if (dgvLichSu.Columns["SoLuongTieuThu"] != null) { dgvLichSu.Columns["SoLuongTieuThu"].HeaderText = "Tiêu Thụ"; dgvLichSu.Columns["SoLuongTieuThu"].FillWeight = 65; }
        if (dgvLichSu.Columns["ThanhTien"] != null)
        {
            dgvLichSu.Columns["ThanhTien"].HeaderText = "Thành Tiền (VNĐ)";
            dgvLichSu.Columns["ThanhTien"].FillWeight = 90;
            dgvLichSu.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
            dgvLichSu.Columns["ThanhTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        if (danhSach.Count > 0)
        {
            txtChiSoDau.Text = danhSach[0].ChiSoCuoi.ToString();
        }
    }

    /// <summary>
    /// Tự động tính số nước tiêu thụ và thành tiền theo bậc thang mỗi khi người dùng gõ số
    /// </summary>
    private void TinhTien()
    {
        int.TryParse(txtChiSoDau?.Text.Trim(), out int dau);
        int.TryParse(txtChiSoCuoi?.Text.Trim(), out int cuoi);

        int tieuThu = cuoi - dau;
        if (tieuThu < 0)
            tieuThu = 0;

        // Gọi nghiệp vụ tính toán bậc thang
        _ketQuaHienTai = KetQuaTinhTien.TinhTienNuoc(tieuThu);

        if (lblTieuThu != null)
            lblTieuThu.Text = $"💧 Tổng tiêu thụ: {tieuThu} m³";

        if (lblBac1 != null)
            lblBac1.Text = $"• Bậc 1 (≤ 10 m³): {_ketQuaHienTai.M3Bac1} m³ × {KetQuaTinhTien.GIA_BAC_1:N0}đ = {_ketQuaHienTai.TienBac1:N0}đ";

        if (lblBac2 != null)
            lblBac2.Text = $"• Bậc 2 (11 - 20 m³): {_ketQuaHienTai.M3Bac2} m³ × {KetQuaTinhTien.GIA_BAC_2:N0}đ = {_ketQuaHienTai.TienBac2:N0}đ";

        if (lblBac3 != null)
            lblBac3.Text = $"• Bậc 3 (21 - 30 m³): {_ketQuaHienTai.M3Bac3} m³ × {KetQuaTinhTien.GIA_BAC_3:N0}đ = {_ketQuaHienTai.TienBac3:N0}đ";

        if (lblBac4 != null)
            lblBac4.Text = $"• Bậc 4 (> 30 m³): {_ketQuaHienTai.M3Bac4} m³ × {KetQuaTinhTien.GIA_BAC_4:N0}đ = {_ketQuaHienTai.TienBac4:N0}đ";

        if (lblThuePhi != null)
            lblThuePhi.Text = $"VAT (5%): {_ketQuaHienTai.ThueVAT:N0}đ  |  BVMT (10%): {_ketQuaHienTai.PhiBVMT:N0}đ";

        if (lblThanhTien != null)
            lblThanhTien.Text = $"💰 TỔNG CỘNG: {_ketQuaHienTai.TongThanhTien:N0} VNĐ";
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

        int tieuThu = cuoi - dau;
        var kq = KetQuaTinhTien.TinhTienNuoc(tieuThu);

        // Đơn giá bình quân = Tiền chưa thuế / Số m3 (nếu dùng 0m3 thì lấy giá bậc 1)
        decimal donGiaBinhQuan = tieuThu > 0
            ? Math.Round(kq.TienNuocChuaThue / tieuThu, 2)
            : KetQuaTinhTien.GIA_BAC_1;

        var cs = new ChiSoDongHo
        {
            KhachHangId = _khachHangId,
            MaSoDongHo = _maSoDongHo,
            ChiSoDau = dau,
            ChiSoCuoi = cuoi,
            Thang = cboThang.SelectedIndex + 1,
            Nam = DateTime.Now.Year,
            DonGia = donGiaBinhQuan,
        };

        try
        {
            if (DatabaseHelper.ThemChiSoNuoc(cs))
            {
                // ==============================================================
                // 🌟 TỰ ĐỘNG SINH HÓA ĐƠN TIỀN NƯỚC THEO GIÁ BẬC THANG LŨY TIẾN
                // ==============================================================
                // Mã hóa đơn format: HD-NămTháng-MãKH (VD: HD-202611-1)
                string maHD = $"HD-{cs.Nam}{cs.Thang:D2}-{cs.KhachHangId}";

                var hd = new HoaDon
                {
                    MaHoaDon = maHD,
                    KhachHangId = cs.KhachHangId,
                    Thang = cs.Thang,
                    Nam = cs.Nam,
                    SoM3TieuThu = tieuThu,
                    DonGia = donGiaBinhQuan,
                    ThanhTien = kq.TongThanhTien, // Tổng tiền đã gồm VAT 5% + BVMT 10%
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
                        $"Ghi nhận chỉ số nước thành công!\n"
                        + $"• Tiêu thụ: {tieuThu} m³\n"
                        + $"• Đã tạo hóa đơn [{maHD}]\n"
                        + $"• Tổng tiền (gồm VAT 5% + BVMT 10%): {kq.TongThanhTien:N0} VNĐ 🎉",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    LoadLichSu();
                }
                else if (daCoHoaDonTruocDo)
                {
                    MessageBox.Show(
                        $"Ghi nhận chỉ số nước thành công!\n⚠️ Lưu ý: Hóa đơn kỳ tháng {cs.Thang}/{cs.Nam} của khách hàng này đã tồn tại trước đó.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    LoadLichSu();
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
