using CapNuocApp.Models;
using CapNuocApp.Data;

namespace CapNuocApp.Forms
{
    /// <summary>
    /// Form thêm mới hoặc chỉnh sửa thông tin khách hàng
    /// </summary>
    public partial class FormThemSuaKhachHang : Form
    {
        private KhachHang? _khachHang; // null = thêm mới, có giá trị = sửa

        // Constructor thêm mới
        public FormThemSuaKhachHang()
        {
            InitializeComponent();
            this.Text = "➕ Thêm Khách Hàng Mới";
            lblTitle.Text = "THÊM KHÁCH HÀNG MỚI";
            txtMaKH.Text = TaoMaKhachHang(); // Tự động tạo mã
        }

        // Constructor sửa
        public FormThemSuaKhachHang(KhachHang kh)
        {
            InitializeComponent();
            _khachHang = kh;
            this.Text = "✏ Chỉnh Sửa Khách Hàng";
            lblTitle.Text = "CHỈNH SỬA KHÁCH HÀNG";
            DieuChinhDuLieu();
        }

        // Điền dữ liệu vào form khi sửa
        private void DieuChinhDuLieu()
        {
            txtMaKH.Text = _khachHang!.MaKhachHang;
            txtMaKH.ReadOnly = true; // Không cho sửa mã
            txtHoTen.Text = _khachHang.HoTen;
            txtDiaChi.Text = _khachHang.DiaChi;
            txtSDT.Text = _khachHang.SoDienThoai;
            txtEmail.Text = _khachHang.Email;
            txtMaDongHo.Text = _khachHang.MaSoDongHo;
        }

        // Tự động tạo mã khách hàng theo format KH-YYYYMMDD-XXX
        private string TaoMaKhachHang()
        {
            return $"KH-{DateTime.Now:yyyyMMdd}-{new Random().Next(100, 999)}";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Validate dữ liệu
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Thiếu thông tin", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtSDT.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return;
            }

            try
            {
                bool thanhCong;
                if (_khachHang == null)
                {
                    // Thêm mới
                    var kh = new KhachHang
                    {
                        MaKhachHang = txtMaKH.Text.Trim(),
                        HoTen = txtHoTen.Text.Trim(),
                        DiaChi = txtDiaChi.Text.Trim(),
                        SoDienThoai = txtSDT.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        MaSoDongHo = txtMaDongHo.Text.Trim(),
                        NgayDangKy = DateTime.Now
                    };
                    thanhCong = DatabaseHelper.ThemKhachHang(kh);
                }
                else
                {
                    // Cập nhật
                    _khachHang.HoTen = txtHoTen.Text.Trim();
                    _khachHang.DiaChi = txtDiaChi.Text.Trim();
                    _khachHang.SoDienThoai = txtSDT.Text.Trim();
                    _khachHang.Email = txtEmail.Text.Trim();
                    _khachHang.MaSoDongHo = txtMaDongHo.Text.Trim();
                    thanhCong = DatabaseHelper.CapNhatKhachHang(_khachHang);
                }

                if (thanhCong)
                    this.DialogResult = DialogResult.OK;
                else
                    MessageBox.Show("Không thể lưu dữ liệu. Vui lòng thử lại.", 
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi:\n{ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.txtMaKH = new TextBox();
            this.txtHoTen = new TextBox();
            this.txtDiaChi = new TextBox();
            this.txtSDT = new TextBox();
            this.txtEmail = new TextBox();
            this.txtMaDongHo = new TextBox();
            this.btnLuu = new Button();
            this.btnHuy = new Button();

            int labelX = 20, inputX = 140, inputW = 280, rowH = 45, startY = 80;
            Font labelFont = new Font("Segoe UI", 10);
            Font inputFont = new Font("Segoe UI", 10);

            // Tiêu đề
            lblTitle.Text = "THÊM KHÁCH HÀNG MỚI";
            lblTitle.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(30, 136, 229);
            lblTitle.Location = new Point(20, 20);
            lblTitle.AutoSize = true;

            // Hàm tạo label + textbox
            void AddRow(string labelText, TextBox txt, int row, bool multiLine = false)
            {
                var lbl = new Label
                {
                    Text = labelText,
                    Location = new Point(labelX, startY + row * rowH + (multiLine ? 3 : 10)),
                    AutoSize = true,
                    Font = labelFont
                };
                txt.Location = new Point(inputX, startY + row * rowH + 5);
                txt.Size = new Size(inputW, multiLine ? 60 : 30);
                txt.Font = inputFont;
                if (multiLine) { txt.Multiline = true; }
                this.Controls.AddRange(new Control[] { lbl, txt });
            }

            AddRow("Mã KH (*)", txtMaKH, 0);
            AddRow("Họ Tên (*)", txtHoTen, 1);
            AddRow("Địa Chỉ (*)", txtDiaChi, 2);
            AddRow("Số ĐT (*)", txtSDT, 3);
            AddRow("Email", txtEmail, 4);
            AddRow("Mã Đồng Hồ", txtMaDongHo, 5);

            // Nút Lưu
            btnLuu.Text = "💾 Lưu";
            btnLuu.Location = new Point(140, startY + 6 * rowH + 15);
            btnLuu.Size = new Size(110, 36);
            btnLuu.BackColor = Color.FromArgb(30, 136, 229);
            btnLuu.ForeColor = Color.White;
            btnLuu.FlatStyle = FlatStyle.Flat;
            btnLuu.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnLuu.Click += btnLuu_Click;

            // Nút Hủy
            btnHuy.Text = "✕ Hủy";
            btnHuy.Location = new Point(260, startY + 6 * rowH + 15);
            btnHuy.Size = new Size(110, 36);
            btnHuy.BackColor = Color.FromArgb(244, 67, 54);
            btnHuy.ForeColor = Color.White;
            btnHuy.FlatStyle = FlatStyle.Flat;
            btnHuy.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnHuy.Click += btnHuy_Click;

            // Form
            this.Text = "Thêm/Sửa Khách Hàng";
            this.Size = new Size(460, startY + 7 * rowH + 50);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 9.5f);

            this.Controls.AddRange(new Control[] { lblTitle, btnLuu, btnHuy });
        }

        private Label lblTitle;
        private TextBox txtMaKH, txtHoTen, txtDiaChi, txtSDT, txtEmail, txtMaDongHo;
        private Button btnLuu, btnHuy;
    }
}
