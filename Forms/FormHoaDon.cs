using System;
using System.Drawing;
using System.Windows.Forms;
using CapNuocApp.Data;
using CapNuocApp.Models;

namespace CapNuocApp;

/// <summary>
/// Giao diện Quản Lý Hóa Đơn & Thu Tiền Nước
/// </summary>
public class FormHoaDon : Form
{
    private DataGridView dgvHoaDon;
    private Label lblTongTien;
    private Label lblSoLuong;
    private ComboBox cboLocTrangThai;

    public FormHoaDon()
    {
        InitializeUI();
        LoadData(); // Nạp danh sách hóa đơn từ SQL Server lên bảng
    }

    private void InitializeUI()
    {
        this.Text = "Quản Lý Hóa Đơn Tiền Nước";
        this.Size = new Size(1150, 620);
        this.StartPosition = FormStartPosition.CenterParent;
        this.BackColor = Color.FromArgb(240, 244, 248);

        // --- 1. TIÊU ĐỀ ---
        var lblTitle = new Label
        {
            Text = "🧾 DANH SÁCH HÓA ĐƠN TIỀN NƯỚC",
            Font = new Font("Segoe UI", 15, FontStyle.Bold),
            ForeColor = Color.FromArgb(30, 100, 200),
            Location = new Point(25, 15),
            AutoSize = true,
        };
        this.Controls.Add(lblTitle);

        // --- 2. THANH LỌC TRẠNG THÁI ---
        var lblLoc = new Label
        {
            Text = "Lọc trạng thái:",
            Location = new Point(25, 60),
            AutoSize = true,
            Font = new Font("Segoe UI", 10),
        };
        this.Controls.Add(lblLoc);

        cboLocTrangThai = new ComboBox
        {
            Location = new Point(130, 57),
            Size = new Size(180, 28),
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font = new Font("Segoe UI", 9.5f),
        };
        cboLocTrangThai.Items.AddRange(new object[] { "Tất cả hóa đơn", "Chưa thanh toán", "Đã thanh toán" });
        cboLocTrangThai.SelectedIndex = 0;
        cboLocTrangThai.SelectedIndexChanged += (s, e) => LoadData();
        this.Controls.Add(cboLocTrangThai);

        // --- 3. NÚT THU TIỀN ---
        var btnThanhToan = new Button
        {
            Text = "💵 Thu Tiền (Đã Thanh Toán)",
            Location = new Point(330, 54),
            Size = new Size(220, 34),
            BackColor = Color.FromArgb(40, 167, 69),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
            Cursor = Cursors.Hand,
        };
        btnThanhToan.FlatAppearance.BorderSize = 0;
        btnThanhToan.Click += (s, e) => ActionThanhToan();
        this.Controls.Add(btnThanhToan);

        // --- 4. THỐNG KÊ TỔNG QUAN ---
        lblSoLuong = new Label
        {
            Location = new Point(570, 62),
            AutoSize = true,
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            ForeColor = Color.DarkSlateGray,
        };
        this.Controls.Add(lblSoLuong);

        lblTongTien = new Label
        {
            Location = new Point(800, 62),
            AutoSize = true,
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            ForeColor = Color.FromArgb(220, 53, 69),
        };
        this.Controls.Add(lblTongTien);

        // --- 5. BẢNG LƯỚI DATAGRIDVIEW ---
        dgvHoaDon = new DataGridView
        {
            Location = new Point(25, 100),
            Size = new Size(1085, 460),
            BackgroundColor = Color.White,
            ReadOnly = true,
            AllowUserToAddRows = false,
            RowHeadersVisible = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        };
        dgvHoaDon.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
        this.Controls.Add(dgvHoaDon);
    }

    /// <summary>
    /// Đọc dữ liệu từ SQL Server và đổ lên bảng DataGridView
    /// </summary>
    private void LoadData()
    {
        try
        {
            var ds = DatabaseHelper.GetAllHoaDon();

            // Lọc danh sách theo ComboBox
            if (cboLocTrangThai.SelectedIndex == 1) // Chưa thanh toán
                ds = ds.FindAll(h => !h.DaThanhToan);
            else if (cboLocTrangThai.SelectedIndex == 2) // Đã thanh toán
                ds = ds.FindAll(h => h.DaThanhToan);

            dgvHoaDon.DataSource = ds;

            // Ẩn các cột khóa ngoại / kỹ thuật không cần hiển thị cho kế toán
            if (dgvHoaDon.Columns["Id"] != null) dgvHoaDon.Columns["Id"].Visible = false;
            if (dgvHoaDon.Columns["KhachHangId"] != null) dgvHoaDon.Columns["KhachHangId"].Visible = false;
            if (dgvHoaDon.Columns["ChiSoDongHoId"] != null) dgvHoaDon.Columns["ChiSoDongHoId"].Visible = false;
            if (dgvHoaDon.Columns["DaThanhToan"] != null) dgvHoaDon.Columns["DaThanhToan"].Visible = false;

            // Đặt tên tiếng Việt và định dạng số/ngày tháng cho từng cột
            if (dgvHoaDon.Columns["MaHoaDon"] != null) dgvHoaDon.Columns["MaHoaDon"].HeaderText = "Mã HĐ";
            if (dgvHoaDon.Columns["MaKhachHang"] != null) dgvHoaDon.Columns["MaKhachHang"].HeaderText = "Mã KH";
            if (dgvHoaDon.Columns["TenKhachHang"] != null) dgvHoaDon.Columns["TenKhachHang"].HeaderText = "Họ và Tên";
            if (dgvHoaDon.Columns["SoDienThoai"] != null) dgvHoaDon.Columns["SoDienThoai"].HeaderText = "SĐT";
            if (dgvHoaDon.Columns["Thang"] != null) dgvHoaDon.Columns["Thang"].HeaderText = "Tháng";
            if (dgvHoaDon.Columns["Nam"] != null) dgvHoaDon.Columns["Nam"].HeaderText = "Năm";
            if (dgvHoaDon.Columns["SoM3TieuThu"] != null) dgvHoaDon.Columns["SoM3TieuThu"].HeaderText = "Tiêu thụ (m³)";

            if (dgvHoaDon.Columns["DonGia"] != null)
            {
                dgvHoaDon.Columns["DonGia"].HeaderText = "Đơn Giá";
                dgvHoaDon.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            }
            if (dgvHoaDon.Columns["ThanhTien"] != null)
            {
                dgvHoaDon.Columns["ThanhTien"].HeaderText = "Thành Tiền (VNĐ)";
                dgvHoaDon.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
            }
            if (dgvHoaDon.Columns["TrangThaiText"] != null) dgvHoaDon.Columns["TrangThaiText"].HeaderText = "Trạng Thái";

            if (dgvHoaDon.Columns["NgayLap"] != null)
            {
                dgvHoaDon.Columns["NgayLap"].HeaderText = "Ngày Lập";
                dgvHoaDon.Columns["NgayLap"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            if (dgvHoaDon.Columns["NgayThanhToan"] != null)
            {
                dgvHoaDon.Columns["NgayThanhToan"].HeaderText = "Ngày Thu Tiền";
                dgvHoaDon.Columns["NgayThanhToan"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }

            // Tính tổng tiền các hóa đơn đang hiển thị
            decimal tongTien = 0;
            foreach (var h in ds) tongTien += h.ThanhTien;

            lblSoLuong.Text = $"📊 Số lượng: {ds.Count} hóa đơn";
            lblTongTien.Text = $"💰 Tổng tiền: {tongTien:N0} VNĐ";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi tải danh sách hóa đơn: {ex.Message}", "Lỗi");
        }
    }

    /// <summary>
    /// Xử lý khi bấm nút Thu Tiền
    /// </summary>
    private void ActionThanhToan()
    {
        // 1. Kiểm tra đã chọn dòng nào trên bảng chưa
        if (dgvHoaDon.CurrentRow?.DataBoundItem is not HoaDon hd)
        {
            MessageBox.Show("Vui lòng click chọn 1 hóa đơn trên bảng để thu tiền!", "Nhắc nhở");
            return;
        }

        // 2. Nếu hóa đơn đã trả rồi thì không thu lại
        if (hd.DaThanhToan)
        {
            MessageBox.Show(
                $"Hóa đơn [{hd.MaHoaDon}] đã được thanh toán trước đó rồi!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
            return;
        }

        // 3. Hỏi xác nhận thu tiền
        var xacNhan = MessageBox.Show(
            $"Xác nhận thu tiền cho hóa đơn:\n\n"
                + $"• Khách hàng: {hd.TenKhachHang} ({hd.MaKhachHang})\n"
                + $"• Kỳ hóa đơn: Tháng {hd.Thang}/{hd.Nam}\n"
                + $"• Số khối nước: {hd.SoM3TieuThu} m³\n"
                + $"• Số tiền cần thu: {hd.ThanhTien:N0} VNĐ",
            "Xác nhận thanh toán",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        );

        if (xacNhan == DialogResult.Yes)
        {
            try
            {
                if (DatabaseHelper.ThanhToanHoaDon(hd.Id))
                {
                    MessageBox.Show("Đã thu tiền và cập nhật trạng thái hóa đơn thành công! 🎉", "Thành công");
                    LoadData(); // Nạp lại bảng để thấy trạng thái "Đã thanh toán"
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật thanh toán: {ex.Message}", "Lỗi");
            }
        }
    }

}

