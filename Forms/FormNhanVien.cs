using System;
using System.Drawing;
using System.Windows.Forms;
using CapNuocApp.Data;
using CapNuocApp.Models;

namespace CapNuocApp;

public class FormNhanVien : Form
{
    private TextBox txtMaNV;
    private TextBox txtHoTen;
    private TextBox txtSDT;
    private TextBox txtKhuVuc;
    private DataGridView dgvNhanVien;

    public FormNhanVien()
    {
        InitializeUI();
        LoadData(); // Vừa mở form là kéo 3 nhân viên trong SQL Server lên ngay
    }

    private void InitializeUI()
    {
        this.Text = "Quản Lý Danh Sách Nhân Viên Ghi Nước";
        this.Size = new Size(950, 520);
        this.StartPosition = FormStartPosition.CenterParent;
        this.BackColor = Color.FromArgb(240, 244, 248);

        // Tiêu đề
        var lblTitle = new Label
        {
            Text = "👥 DANH SÁCH NHÂN VIÊN GHI CHỈ SỐ NƯỚC",
            Font = new Font("Segoe UI", 14, FontStyle.Bold),
            ForeColor = Color.FromArgb(30, 100, 200),
            Location = new Point(25, 15),
            AutoSize = true,
        };
        this.Controls.Add(lblTitle);

        // --- CỘT TRÁI: KHUNG NHẬP LIỆU ---
        var grp = new GroupBox
        {
            Text = "Thêm Nhân Viên Mới",
            Location = new Point(25, 55),
            Size = new Size(320, 400),
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
        };

        int lx = 15,
            ix = 110,
            y = 35,
            gap = 50;

        grp.Controls.Add(
            new Label
            {
                Text = "Mã NV:",
                Location = new Point(lx, y),
                AutoSize = true,
            }
        );
        txtMaNV = new TextBox
        {
            Location = new Point(ix, y - 3),
            Size = new Size(185, 27),
            Font = new Font("Segoe UI", 10),
        };
        grp.Controls.Add(txtMaNV);

        grp.Controls.Add(
            new Label
            {
                Text = "Họ Tên:",
                Location = new Point(lx, y + gap),
                AutoSize = true,
            }
        );
        txtHoTen = new TextBox
        {
            Location = new Point(ix, y + gap - 3),
            Size = new Size(185, 27),
            Font = new Font("Segoe UI", 10),
        };
        grp.Controls.Add(txtHoTen);

        grp.Controls.Add(
            new Label
            {
                Text = "Số ĐT:",
                Location = new Point(lx, y + gap * 2),
                AutoSize = true,
            }
        );
        txtSDT = new TextBox
        {
            Location = new Point(ix, y + gap * 2 - 3),
            Size = new Size(185, 27),
            Font = new Font("Segoe UI", 10),
        };
        grp.Controls.Add(txtSDT);

        grp.Controls.Add(
            new Label
            {
                Text = "Khu vực:",
                Location = new Point(lx, y + gap * 3),
                AutoSize = true,
            }
        );
        txtKhuVuc = new TextBox
        {
            Location = new Point(ix, y + gap * 3 - 3),
            Size = new Size(185, 27),
            Font = new Font("Segoe UI", 10),
        };
        grp.Controls.Add(txtKhuVuc);

        var btnThem = new Button
        {
            Text = "➕ Thêm Nhân Viên",
            Location = new Point(20, y + gap * 4 + 10),
            Size = new Size(275, 40),
            BackColor = Color.FromArgb(40, 167, 69),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand,
        };
        btnThem.FlatAppearance.BorderSize = 0;
        btnThem.Click += (s, e) => ActionThem();
        grp.Controls.Add(btnThem);

        this.Controls.Add(grp);

        // --- CỘT PHẢI: BẢNG DANH SÁCH NHÂN VIÊN ---
        dgvNhanVien = new DataGridView
        {
            Location = new Point(365, 65),
            Size = new Size(545, 390),
            BackgroundColor = Color.White,
            ReadOnly = true,
            AllowUserToAddRows = false,
            RowHeadersVisible = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        };
        this.Controls.Add(dgvNhanVien);
    }

    private void LoadData()
    {
        try
        {
            // Gọi hàm GetAllNhanVien mà bạn vừa viết!
            var ds = DatabaseHelper.GetAllNhanVien();
            dgvNhanVien.DataSource = ds;

            // Ẩn các cột kỹ thuật
            if (dgvNhanVien.Columns["Id"] != null)
                dgvNhanVien.Columns["Id"].Visible = false;
            if (dgvNhanVien.Columns["DangHoatDong"] != null)
                dgvNhanVien.Columns["DangHoatDong"].Visible = false;

            // Đặt tên tiếng Việt cho các cột
            if (dgvNhanVien.Columns["MaNhanVien"] != null)
                dgvNhanVien.Columns["MaNhanVien"].HeaderText = "Mã NV";
            if (dgvNhanVien.Columns["HoTen"] != null)
                dgvNhanVien.Columns["HoTen"].HeaderText = "Họ và Tên";
            if (dgvNhanVien.Columns["SoDienThoai"] != null)
                dgvNhanVien.Columns["SoDienThoai"].HeaderText = "Số Điện Thoại";
            if (dgvNhanVien.Columns["KhuVucPhuTrach"] != null)
                dgvNhanVien.Columns["KhuVucPhuTrach"].HeaderText = "Khu Vực Phụ Trách";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi tải nhân viên: {ex.Message}", "Lỗi");
        }
    }

    private void ActionThem()
    {
        if (string.IsNullOrWhiteSpace(txtMaNV.Text) || string.IsNullOrWhiteSpace(txtHoTen.Text))
        {
            MessageBox.Show("Vui lòng nhập ít nhất Mã NV và Họ Tên!", "Nhắc nhở");
            return;
        }

        // Đóng gói vào Model NhanVien
        var nv = new NhanVien
        {
            MaNhanVien = txtMaNV.Text.Trim(),
            HoTen = txtHoTen.Text.Trim(),
            SoDienThoai = txtSDT.Text.Trim(),
            KhuVucPhuTrach = txtKhuVuc.Text.Trim(),
        };

        // Gọi hàm ThemNhanVien mà bạn vừa viết!
        if (DatabaseHelper.ThemNhanVien(nv))
        {
            MessageBox.Show("Thêm nhân viên thành công! 🎉", "Thông báo");
            LoadData(); // Nạp lại bảng để nhân viên mới xuất hiện
            txtMaNV.Clear();
            txtHoTen.Clear();
            txtSDT.Clear();
            txtKhuVuc.Clear();
            txtMaNV.Focus();
        }
    }
}
