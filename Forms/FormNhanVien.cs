using System;
using System.Drawing;
using System.Windows.Forms;
using CapNuocApp.Data;
using CapNuocApp.Models;
using Microsoft.Data.SqlClient;

namespace CapNuocApp;

public class FormNhanVien : Form
{
    private TextBox txtId; // Ẩn - lưu Id khi sửa/xóa
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
            Text = "Thông Tin Nhân Viên",
            Location = new Point(25, 55),
            Size = new Size(320, 410),
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
        };

        // Ô Id ẩn
        txtId = new TextBox { Visible = false };
        grp.Controls.Add(txtId);

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

        // --- CÁC NÚT BẤM (Hàng 1: Thêm + Cập nhật, Hàng 2: Xóa + Xóa trắng) ---
        int btnY = y + gap * 4 + 5;

        // Nút Thêm
        var btnThem = new Button
        {
            Text = "➕ Thêm",
            Location = new Point(15, btnY),
            Size = new Size(135, 38),
            BackColor = Color.FromArgb(40, 167, 69),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand,
        };
        btnThem.FlatAppearance.BorderSize = 0;
        btnThem.Click += (s, e) => ActionThem();
        grp.Controls.Add(btnThem);

        // Nút Sửa / Cập nhật
        var btnSua = new Button
        {
            Text = "✏️ Cập Nhật",
            Location = new Point(165, btnY),
            Size = new Size(135, 38),
            BackColor = Color.FromArgb(243, 156, 18),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand,
        };
        btnSua.FlatAppearance.BorderSize = 0;
        btnSua.Click += (s, e) => ActionSua();
        grp.Controls.Add(btnSua);

        // Nút Xóa
        var btnXoa = new Button
        {
            Text = "🗑️ Xóa NV",
            Location = new Point(15, btnY + 48),
            Size = new Size(135, 38),
            BackColor = Color.FromArgb(220, 53, 69),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand,
        };
        btnXoa.FlatAppearance.BorderSize = 0;
        btnXoa.Click += (s, e) => ActionXoa();
        grp.Controls.Add(btnXoa);

        // Nút Xóa trắng
        var btnClear = new Button
        {
            Text = "🧹 Xóa Trắng",
            Location = new Point(165, btnY + 48),
            Size = new Size(135, 38),
            BackColor = Color.FromArgb(108, 117, 125),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand,
        };
        btnClear.FlatAppearance.BorderSize = 0;
        btnClear.Click += (s, e) => XoaTrangForm();
        grp.Controls.Add(btnClear);

        this.Controls.Add(grp);

        // --- CỘT PHẢI: BẢNG DANH SÁCH NHÂN VIÊN ---
        dgvNhanVien = new DataGridView
        {
            Location = new Point(365, 65),
            Size = new Size(545, 400),
            BackgroundColor = Color.White,
            ReadOnly = true,
            AllowUserToAddRows = false,
            RowHeadersVisible = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        };

        // 🌟 SỰ KIỆN CELLCLICK: Khi click chọn dòng trên bảng, đổ dữ liệu sang các ô bên trái
        dgvNhanVien.CellClick += (s, e) =>
        {
            if (dgvNhanVien.CurrentRow?.DataBoundItem is NhanVien nv)
            {
                txtId.Text = nv.Id.ToString();
                txtMaNV.Text = nv.MaNhanVien;
                txtHoTen.Text = nv.HoTen;
                txtSDT.Text = nv.SoDienThoai;
                txtKhuVuc.Text = nv.KhuVucPhuTrach;

                txtMaNV.ReadOnly = true; // Khóa mã nhân viên khi đang xem/cập nhật
            }
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
        if (!string.IsNullOrEmpty(txtId.Text))
        {
            MessageBox.Show(
                "Bạn đang chọn một nhân viên sẵn có trên bảng!\n\n"
                    + "• Nếu muốn lưu thay đổi cho nhân viên này, hãy bấm '✏️ Cập Nhật'.\n"
                    + "• Nếu muốn thêm nhân viên mới, hãy bấm '🧹 Xóa Trắng' trước rồi nhập mã mới.",
                "Nhắc nhở",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
            return;
        }

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

        try
        {
            if (DatabaseHelper.ThemNhanVien(nv))
            {
                MessageBox.Show("Thêm nhân viên thành công! 🎉", "Thông báo");
                LoadData();
                XoaTrangForm();
            }
        }
        catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
        {
            MessageBox.Show(
                $"Mã nhân viên '{nv.MaNhanVien}' đã tồn tại trong hệ thống!\nVui lòng nhập mã nhân viên khác.",
                "Trùng mã nhân viên",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
            txtMaNV.Focus();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Lỗi khi thêm nhân viên: {ex.Message}",
                "Lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
    }

    private void ActionSua()
    {
        if (string.IsNullOrEmpty(txtId.Text))
        {
            MessageBox.Show("Vui lòng click chọn 1 nhân viên trên bảng trước khi sửa!", "Nhắc nhở");
            return;
        }

        if (string.IsNullOrWhiteSpace(txtHoTen.Text))
        {
            MessageBox.Show("Họ tên nhân viên không được để trống!", "Nhắc nhở");
            return;
        }

        var nv = new NhanVien
        {
            Id = int.Parse(txtId.Text),
            HoTen = txtHoTen.Text.Trim(),
            SoDienThoai = txtSDT.Text.Trim(),
            KhuVucPhuTrach = txtKhuVuc.Text.Trim(),
        };

        try
        {
            if (DatabaseHelper.CapNhatNhanVien(nv))
            {
                MessageBox.Show("Cập nhật thông tin nhân viên thành công! 🎉", "Thông báo");
                LoadData();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Lỗi khi cập nhật nhân viên: {ex.Message}",
                "Lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
    }

    private void ActionXoa()
    {
        if (string.IsNullOrEmpty(txtId.Text))
        {
            MessageBox.Show("Vui lòng click chọn 1 nhân viên trên bảng trước khi xóa!", "Nhắc nhở");
            return;
        }

        var confirm = MessageBox.Show(
            $"Bạn có chắc chắn muốn xóa nhân viên [{txtMaNV.Text}] {txtHoTen.Text}?",
            "Xác nhận xóa",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        );

        if (confirm == DialogResult.Yes)
        {
            try
            {
                int id = int.Parse(txtId.Text);
                if (DatabaseHelper.XoaNhanVien(id))
                {
                    MessageBox.Show("Đã xóa nhân viên thành công!", "Thông báo");
                    LoadData();
                    XoaTrangForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi khi xóa nhân viên: {ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }

    private void XoaTrangForm()
    {
        txtId.Clear();
        txtMaNV.Clear();
        txtHoTen.Clear();
        txtSDT.Clear();
        txtKhuVuc.Clear();
        txtMaNV.ReadOnly = false; // Mở lại ô Mã NV để nhập mã mới
        txtMaNV.Focus();
    }
}
