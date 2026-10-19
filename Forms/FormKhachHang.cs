using CapNuocApp.Models;
using CapNuocApp.Data;

namespace CapNuocApp.Forms
{
    /// <summary>
    /// Form chính quản lý danh sách khách hàng
    /// </summary>
    public partial class FormKhachHang : Form
    {
        private List<KhachHang> _danhSachKhachHang = new();

        public FormKhachHang()
        {
            InitializeComponent();
            ConfigureDataGridView();
        }

        private void FormKhachHang_Load(object sender, EventArgs e)
        {
            LoadDanhSachKhachHang();
        }

        /// <summary>
        /// Tải danh sách khách hàng vào DataGridView
        /// </summary>
        private void LoadDanhSachKhachHang()
        {
            try
            {
                _danhSachKhachHang = DatabaseHelper.GetAllKhachHang();
                dgvKhachHang.DataSource = null;
                dgvKhachHang.DataSource = _danhSachKhachHang;
                lblTongSo.Text = $"Tổng số: {_danhSachKhachHang.Count} khách hàng";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối database:\n{ex.Message}\n\nVui lòng kiểm tra SQL Server.", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tìm kiếm khách hàng theo từ khóa
        /// </summary>
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadDanhSachKhachHang();
                return;
            }

            try
            {
                var ketQua = DatabaseHelper.SearchKhachHang(keyword);
                dgvKhachHang.DataSource = null;
                dgvKhachHang.DataSource = ketQua;
                lblTongSo.Text = $"Tìm thấy: {ketQua.Count} khách hàng";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tìm kiếm:\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Mở form thêm khách hàng mới
        /// </summary>
        private void btnThem_Click(object sender, EventArgs e)
        {
            var formThem = new FormThemSuaKhachHang();
            if (formThem.ShowDialog() == DialogResult.OK)
            {
                LoadDanhSachKhachHang();
                MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Mở form sửa thông tin khách hàng được chọn
        /// </summary>
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvKhachHang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var khachHang = (KhachHang)dgvKhachHang.SelectedRows[0].DataBoundItem;
            var formSua = new FormThemSuaKhachHang(khachHang);
            if (formSua.ShowDialog() == DialogResult.OK)
            {
                LoadDanhSachKhachHang();
                MessageBox.Show("Cập nhật thành công!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Xóa khách hàng được chọn
        /// </summary>
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvKhachHang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var khachHang = (KhachHang)dgvKhachHang.SelectedRows[0].DataBoundItem;
            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn xóa khách hàng:\n{khachHang.HoTen}?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    DatabaseHelper.XoaKhachHang(khachHang.Id);
                    LoadDanhSachKhachHang();
                    MessageBox.Show("Xóa thành công!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi xóa:\n{ex.Message}", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Làm mới danh sách
        /// </summary>
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            LoadDanhSachKhachHang();
        }

        /// <summary>
        /// Cấu hình hiển thị cột cho DataGridView
        /// </summary>
        private void ConfigureDataGridView()
        {
            dgvKhachHang.AutoGenerateColumns = false;
            dgvKhachHang.Columns.Clear();

            dgvKhachHang.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { DataPropertyName = "MaKhachHang", HeaderText = "Mã KH", Width = 80 },
                new DataGridViewTextBoxColumn { DataPropertyName = "HoTen", HeaderText = "Họ Tên", Width = 180 },
                new DataGridViewTextBoxColumn { DataPropertyName = "DiaChi", HeaderText = "Địa Chỉ", Width = 220 },
                new DataGridViewTextBoxColumn { DataPropertyName = "SoDienThoai", HeaderText = "Số ĐT", Width = 110 },
                new DataGridViewTextBoxColumn { DataPropertyName = "MaSoDongHo", HeaderText = "Mã Đồng Hồ", Width = 120 },
                new DataGridViewTextBoxColumn { DataPropertyName = "NgayDangKy", HeaderText = "Ngày ĐK", Width = 100,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" } },
            });

            dgvKhachHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKhachHang.ReadOnly = true;
            dgvKhachHang.AllowUserToAddRows = false;
        }

        // Cho phép tìm kiếm bằng phím Enter
        private void txtTimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                btnTimKiem_Click(sender, e);
        }

        private void InitializeComponent()
        {
            this.dgvKhachHang = new DataGridView();
            this.txtTimKiem = new TextBox();
            this.btnTimKiem = new Button();
            this.btnThem = new Button();
            this.btnSua = new Button();
            this.btnXoa = new Button();
            this.btnLamMoi = new Button();
            this.lblTongSo = new Label();
            var panelTop = new Panel();
            var panelBottom = new Panel();
            var lblTimKiem = new Label();

            // Panel trên (tìm kiếm + nút)
            panelTop.Dock = DockStyle.Top;
            panelTop.Height = 55;
            panelTop.Padding = new Padding(10, 10, 10, 5);
            panelTop.BackColor = Color.FromArgb(30, 136, 229);

            lblTimKiem.Text = "🔍 Tìm kiếm:";
            lblTimKiem.ForeColor = Color.White;
            lblTimKiem.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblTimKiem.Location = new Point(10, 15);
            lblTimKiem.AutoSize = true;

            txtTimKiem.Location = new Point(100, 12);
            txtTimKiem.Size = new Size(280, 30);
            txtTimKiem.Font = new Font("Segoe UI", 10);
            txtTimKiem.PlaceholderText = "Nhập tên, mã KH, mã đồng hồ...";
            txtTimKiem.KeyPress += txtTimKiem_KeyPress;

            btnTimKiem.Text = "Tìm";
            btnTimKiem.Location = new Point(390, 11);
            btnTimKiem.Size = new Size(75, 30);
            btnTimKiem.BackColor = Color.White;
            btnTimKiem.ForeColor = Color.FromArgb(30, 136, 229);
            btnTimKiem.FlatStyle = FlatStyle.Flat;
            btnTimKiem.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnTimKiem.Click += btnTimKiem_Click;

            panelTop.Controls.AddRange(new Control[] { lblTimKiem, txtTimKiem, btnTimKiem });

            // Panel dưới (các nút thao tác)
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Height = 50;
            panelBottom.Padding = new Padding(10, 8, 10, 8);
            panelBottom.BackColor = Color.FromArgb(245, 245, 245);

            lblTongSo.Location = new Point(10, 15);
            lblTongSo.AutoSize = true;
            lblTongSo.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            lblTongSo.ForeColor = Color.Gray;

            // Nút Thêm
            btnThem.Text = "+ Thêm";
            btnThem.Size = new Size(90, 34);
            btnThem.BackColor = Color.FromArgb(76, 175, 80);
            btnThem.ForeColor = Color.White;
            btnThem.FlatStyle = FlatStyle.Flat;
            btnThem.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnThem.Dock = DockStyle.Right;
            btnThem.Click += btnThem_Click;

            // Nút Sửa
            btnSua.Text = "✏ Sửa";
            btnSua.Size = new Size(90, 34);
            btnSua.BackColor = Color.FromArgb(255, 152, 0);
            btnSua.ForeColor = Color.White;
            btnSua.FlatStyle = FlatStyle.Flat;
            btnSua.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnSua.Dock = DockStyle.Right;
            btnSua.Click += btnSua_Click;

            // Nút Xóa
            btnXoa.Text = "🗑 Xóa";
            btnXoa.Size = new Size(90, 34);
            btnXoa.BackColor = Color.FromArgb(244, 67, 54);
            btnXoa.ForeColor = Color.White;
            btnXoa.FlatStyle = FlatStyle.Flat;
            btnXoa.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnXoa.Dock = DockStyle.Right;
            btnXoa.Click += btnXoa_Click;

            // Nút Làm mới
            btnLamMoi.Text = "🔄 Làm mới";
            btnLamMoi.Size = new Size(100, 34);
            btnLamMoi.BackColor = Color.FromArgb(96, 125, 139);
            btnLamMoi.ForeColor = Color.White;
            btnLamMoi.FlatStyle = FlatStyle.Flat;
            btnLamMoi.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnLamMoi.Dock = DockStyle.Right;
            btnLamMoi.Click += btnLamMoi_Click;

            panelBottom.Controls.AddRange(new Control[] { lblTongSo, btnThem, btnSua, btnXoa, btnLamMoi });

            // DataGridView
            ((System.ComponentModel.ISupportInitialize)dgvKhachHang).BeginInit();
            dgvKhachHang.Dock = DockStyle.Fill;
            dgvKhachHang.BackgroundColor = Color.White;
            dgvKhachHang.BorderStyle = BorderStyle.None;
            dgvKhachHang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvKhachHang.RowHeadersVisible = false;
            dgvKhachHang.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(245, 248, 255)
            };
            dgvKhachHang.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(30, 136, 229),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            dgvKhachHang.DefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Segoe UI", 9.5f),
                SelectionBackColor = Color.FromArgb(187, 222, 251),
                SelectionForeColor = Color.Black
            };
            ((System.ComponentModel.ISupportInitialize)dgvKhachHang).EndInit();

            // Form chính
            this.Text = "💧 Quản Lý Khách Hàng — Cấp Nước Bến Thành";
            this.Size = new Size(900, 580);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 9.5f);
            this.BackColor = Color.White;

            this.Controls.AddRange(new Control[] { dgvKhachHang, panelTop, panelBottom });
            this.Load += FormKhachHang_Load;
        }

        // Controls
        private DataGridView dgvKhachHang;
        private TextBox txtTimKiem;
        private Button btnTimKiem, btnThem, btnSua, btnXoa, btnLamMoi;
        private Label lblTongSo;
    }
}
