using System;
using System.Drawing;
using System.Windows.Forms;
using CapNuocApp.Data;

namespace CapNuocApp;

public class MainForm : Form
{
    private DataGridView dgvKhachHang;

    public MainForm()
    {
        InitializeComponent();
        LoadData();
    }

    private void InitializeComponent()
    {
        // === Cấu hình cửa sổ chính ===
        this.Text = "Quản Lý Cấp Nước Bến Thành";
        this.Size = new Size(1000, 620);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.BackColor = Color.FromArgb(240, 244, 248);

        // === Tiêu đề ===
        var lblTitle = new Label
        {
            Text = "💧 Danh Sách Khách Hàng (Dữ Liệu Thật Từ SQL Server)",
            Font = new Font("Segoe UI", 16, FontStyle.Bold),
            ForeColor = Color.FromArgb(30, 100, 200),
            Location = new Point(30, 20),
            AutoSize = true
        };

        // === Nút tải lại dữ liệu ===
        var btnReload = new Button
        {
            Text = "🔄 Tải lại dữ liệu",
            Location = new Point(30, 65),
            Size = new Size(160, 36),
            BackColor = Color.FromArgb(30, 100, 200),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            Cursor = Cursors.Hand
        };
        btnReload.Click += (s, e) => LoadData();

        // === Bảng hiển thị danh sách khách hàng ===
        dgvKhachHang = new DataGridView
        {
            Location = new Point(30, 115),
            Size = new Size(925, 430),
            BackgroundColor = Color.White,
            BorderStyle = BorderStyle.Fixed3D,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            ReadOnly = true,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            AllowUserToAddRows = false,
            RowHeadersVisible = false
        };

        this.Controls.Add(lblTitle);
        this.Controls.Add(btnReload);
        this.Controls.Add(dgvKhachHang);
    }

    /// <summary>
    /// Gọi DatabaseHelper để lấy dữ liệu thật từ SQL Server và hiển thị lên bảng
    /// </summary>
    private void LoadData()
    {
        try
        {
            var ds = DatabaseHelper.GetAllKhachHang();
            dgvKhachHang.DataSource = ds;
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Lỗi kết nối cơ sở dữ liệu:\n{ex.Message}",
                "Thông báo lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
    }
}
