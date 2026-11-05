using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CapNuocApp.Data;
using CapNuocApp.Models;

namespace CapNuocApp;

public class MainForm : Form
{
    // =========================================================================
    // 1. KHAI BÁO CÁC Ô NHẬP LIỆU (TextBox) VÀ BẢNG (DataGridView)
    // Các biến này đặt ở cấp Class để mọi hàm bên dưới đều đọc/ghi được
    // =========================================================================
    private TextBox txtId; // Lưu ID ngầm của khách hàng (dùng khi Sửa / Xóa)
    private TextBox txtMaKH; // Ô gõ Mã khách hàng (VD: KH-001)
    private TextBox txtHoTen; // Ô gõ Họ và tên
    private TextBox txtDiaChi; // Ô gõ Địa chỉ nhà
    private TextBox txtSDT; // Ô gõ Số điện thoại
    private TextBox txtEmail; // Ô gõ Email
    private TextBox txtMaDH; // Ô gõ Mã số đồng hồ nước (VD: DH-001)

    private DataGridView dgvKhachHang; // Bảng lưới hiển thị danh sách từ SQL Server
    private TextBox txtTimKiem;
    private Label lblThongKe;

    public MainForm()
    {
        InitializeComponent(); // Dựng giao diện (nút bấm, ô chữ, bảng...)
        LoadData(); // Vừa bật app lên là tự động kéo dữ liệu từ SQL Server về
    }

    // =========================================================================
    // 2. HÀM DỰNG GIAO DIỆN (Chia làm 2 cột: Trái nhập liệu, Phải xem bảng)
    // =========================================================================
    private void InitializeComponent()
    {
        // Cấu hình kích thước và màu nền cho cửa sổ chính
        this.Text = "Hệ Thống Quản Lý Cấp Nước Bến Thành";
        this.Size = new Size(1150, 680);
        this.StartPosition = FormStartPosition.CenterScreen; // Mở ra giữa màn hình
        this.BackColor = Color.FromArgb(240, 244, 248); // Màu xanh nhạt dễ nhìn

        // --- TIÊU ĐỀ PHÍA TRÊN CÙNG ---
        var lblTitle = new Label
        {
            Text = "💧 QUẢN LÝ THÔNG TIN KHÁCH HÀNG CẤP NƯỚC",
            Font = new Font("Segoe UI", 16, FontStyle.Bold),
            ForeColor = Color.FromArgb(30, 100, 200),
            Location = new Point(30, 15),
            AutoSize = true,
        };
        this.Controls.Add(lblTitle);

        // --- CỘT BÊN TRÁI: KHUNG CHỨA CÁC Ô NHẬP LIỆU (GroupBox) ---
        var grpInfo = new GroupBox
        {
            Text = "Thông Tin Khách Hàng",
            Location = new Point(30, 60),
            Size = new Size(350, 550),
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            ForeColor = Color.FromArgb(50, 50, 50),
        };

        // Tọa độ để xếp các ô nhập liệu thẳng hàng từ trên xuống dưới
        int labelX = 20,
            inputX = 130,
            startY = 35,
            gapY = 45;

        // Ô Id này ẩn đi, người dùng không cần thấy, nhưng code cần để biết đang sửa/xóa ai
        txtId = new TextBox { Visible = false };

        // 1. Ô nhập Mã KH
        grpInfo.Controls.Add(
            new Label
            {
                Text = "Mã KH:",
                Location = new Point(labelX, startY),
                AutoSize = true,
            }
        );
        txtMaKH = new TextBox
        {
            Location = new Point(inputX, startY - 3),
            Size = new Size(195, 27),
            Font = new Font("Segoe UI", 10),
        };
        grpInfo.Controls.Add(txtMaKH);

        // 2. Ô nhập Họ Tên
        grpInfo.Controls.Add(
            new Label
            {
                Text = "Họ Tên:",
                Location = new Point(labelX, startY + gapY),
                AutoSize = true,
            }
        );
        txtHoTen = new TextBox
        {
            Location = new Point(inputX, startY + gapY - 3),
            Size = new Size(195, 27),
            Font = new Font("Segoe UI", 10),
        };
        grpInfo.Controls.Add(txtHoTen);

        // 3. Ô nhập Địa Chỉ
        grpInfo.Controls.Add(
            new Label
            {
                Text = "Địa Chỉ:",
                Location = new Point(labelX, startY + gapY * 2),
                AutoSize = true,
            }
        );
        txtDiaChi = new TextBox
        {
            Location = new Point(inputX, startY + gapY * 2 - 3),
            Size = new Size(195, 27),
            Font = new Font("Segoe UI", 10),
        };
        grpInfo.Controls.Add(txtDiaChi);

        // 4. Ô nhập Số Điện Thoại
        grpInfo.Controls.Add(
            new Label
            {
                Text = "Số ĐT:",
                Location = new Point(labelX, startY + gapY * 3),
                AutoSize = true,
            }
        );
        txtSDT = new TextBox
        {
            Location = new Point(inputX, startY + gapY * 3 - 3),
            Size = new Size(195, 27),
            Font = new Font("Segoe UI", 10),
        };
        grpInfo.Controls.Add(txtSDT);

        // 5. Ô nhập Email
        grpInfo.Controls.Add(
            new Label
            {
                Text = "Email:",
                Location = new Point(labelX, startY + gapY * 4),
                AutoSize = true,
            }
        );
        txtEmail = new TextBox
        {
            Location = new Point(inputX, startY + gapY * 4 - 3),
            Size = new Size(195, 27),
            Font = new Font("Segoe UI", 10),
        };
        grpInfo.Controls.Add(txtEmail);

        // 6. Ô nhập Mã Đồng Hồ
        grpInfo.Controls.Add(
            new Label
            {
                Text = "Mã Đồng Hồ:",
                Location = new Point(labelX, startY + gapY * 5),
                AutoSize = true,
            }
        );
        txtMaDH = new TextBox
        {
            Location = new Point(inputX, startY + gapY * 5 - 3),
            Size = new Size(195, 27),
            Font = new Font("Segoe UI", 10),
        };
        grpInfo.Controls.Add(txtMaDH);

        // --- CÁC NÚT BẤM THAO TÁC (Nằm dưới các ô nhập liệu) ---
        int btnY = startY + gapY * 6 + 10;

        // Nút Thêm Mới (Màu xanh lá)
        var btnAdd = new Button
        {
            Text = "➕ Thêm Mới",
            Location = new Point(20, btnY),
            Size = new Size(145, 38),
            BackColor = Color.FromArgb(40, 167, 69),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand,
        };
        btnAdd.Click += (s, e) => ActionThem(); // Bấm nút này sẽ gọi hàm ActionThem bên dưới

        // Nút Cập Nhật (Màu cam)
        var btnUpdate = new Button
        {
            Text = "✏️ Cập Nhật",
            Location = new Point(180, btnY),
            Size = new Size(145, 38),
            BackColor = Color.FromArgb(243, 156, 18),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand,
        };
        btnUpdate.Click += (s, e) => ActionSua(); // Bấm nút này sẽ gọi hàm ActionSua bên dưới

        // Nút Xóa (Màu đỏ)
        var btnDelete = new Button
        {
            Text = "🗑️ Xóa KH",
            Location = new Point(20, btnY + 50),
            Size = new Size(145, 38),
            BackColor = Color.FromArgb(220, 53, 69),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand,
        };
        btnDelete.Click += (s, e) => ActionXoa(); // Bấm nút này sẽ gọi hàm ActionXoa bên dưới

        // Nút Xóa Trắng (Màu xám) - Để xóa trắng các ô và gõ người mới
        var btnClear = new Button
        {
            Text = "🧹 Xóa Trắng",
            Location = new Point(180, btnY + 50),
            Size = new Size(145, 38),
            BackColor = Color.FromArgb(108, 117, 125),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand,
        };
        btnClear.Click += (s, e) => XoaTrangForm();

        // Nút Ghi Số Nước (Màu xanh dương đậm)
        var btnGhiNuoc = new Button
        {
            Text = "💧 Ghi Số Nước",
            Location = new Point(20, btnY + 100),
            Size = new Size(305, 42),
            BackColor = Color.FromArgb(0, 102, 204),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 10.5f, FontStyle.Bold),
            Cursor = Cursors.Hand
        };
        btnGhiNuoc.FlatAppearance.BorderSize = 0;
        btnGhiNuoc.Click += (s, e) => ActionMoFormGhiNuoc();

        grpInfo.Controls.Add(btnAdd);
        grpInfo.Controls.Add(btnUpdate);
        grpInfo.Controls.Add(btnDelete);
        grpInfo.Controls.Add(btnClear);
        grpInfo.Controls.Add(btnGhiNuoc);
        this.Controls.Add(grpInfo);
        // --- THANH TÌM KIẾM VÀ THỐNG KÊ (Nằm phía trên bảng) ---
        txtTimKiem = new TextBox
        {
            Location = new Point(400, 60),
            Size = new Size(250, 27),
            Font = new Font("Segoe UI", 10),
            PlaceholderText = "Nhập tên, mã KH hoặc SĐT...",
        };

        var btnTimKiem = new Button
        {
            Text = "🔍 Tìm",
            Location = new Point(660, 58),
            Size = new Size(80, 30),
            BackColor = Color.FromArgb(0, 123, 255),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand,
        };
        btnTimKiem.Click += (s, e) => ActionTimKiem();

        var btnLamMoi = new Button
        {
            Text = "🔄 Tất cả",
            Location = new Point(750, 58),
            Size = new Size(85, 30),
            BackColor = Color.FromArgb(108, 117, 125),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand,
        };
        btnLamMoi.Click += (s, e) =>
        {
            txtTimKiem.Clear();
            LoadData();
        };

        lblThongKe = new Label
        {
            Location = new Point(850, 63),
            AutoSize = true,
            Font = new Font("Segoe UI", 10, FontStyle.Italic),
            ForeColor = Color.DarkSlateGray,
        };

        this.Controls.Add(txtTimKiem);
        this.Controls.Add(btnTimKiem);
        this.Controls.Add(btnLamMoi);
        this.Controls.Add(lblThongKe);

        // --- CỘT BÊN PHẢI: BẢNG LƯỚI HIỂN THỊ DANH SÁCH (DataGridView) ---
        dgvKhachHang = new DataGridView
        {
            Location = new Point(400, 105),
            Size = new Size(710, 505),
            BackgroundColor = Color.White,
            BorderStyle = BorderStyle.Fixed3D,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            ReadOnly = true, // Chỉ cho xem trên bảng, muốn sửa thì sửa qua ô bên trái
            SelectionMode = DataGridViewSelectionMode.FullRowSelect, // Click vào là chọn nguyên dòng
            AllowUserToAddRows = false,
            RowHeadersVisible = false,
        };

        // 🌟 TÍNH NĂNG QUAN TRỌNG NHẤT:
        // Khi người dùng click chuột vào bất kỳ dòng nào trên bảng:
        // Lập tức bốc dữ liệu dòng đó ném sang các ô nhập liệu bên trái!
        dgvKhachHang.CellClick += (s, e) =>
        {
            if (dgvKhachHang.CurrentRow?.DataBoundItem is KhachHang kh)
            {
                txtId.Text = kh.Id.ToString();
                txtMaKH.Text = kh.MaKhachHang;
                txtHoTen.Text = kh.HoTen;
                txtDiaChi.Text = kh.DiaChi;
                txtSDT.Text = kh.SoDienThoai;
                txtEmail.Text = kh.Email;
                txtMaDH.Text = kh.MaSoDongHo;
            }
        };

        this.Controls.Add(dgvKhachHang);
    }

    // =========================================================================
    // 3. CÁC HÀM XỬ LÝ NGHIỆP VỤ (Gọi sang DatabaseHelper)
    // =========================================================================

    /// <summary>
    /// Đọc toàn bộ khách hàng từ DatabaseHelper và nạp vào DataGridView
    /// </summary>
    private void LoadData()
    {
        try
        {
            dgvKhachHang.DataSource = DatabaseHelper.GetAllKhachHang();
            lblThongKe.Text = $"Tổng số{DatabaseHelper.DemSoKhachHang()} khách hàng";
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Lỗi tải dữ liệu: {ex.Message}",
                "Lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
    }

    private void ActionTimKiem()
    {
        string tuKhoa = txtTimKiem.Text.Trim();
        if (string.IsNullOrEmpty(tuKhoa))
        {
            LoadData();
            return;
        }
        var KetQua = DatabaseHelper.SearchKhachHang(tuKhoa);
        dgvKhachHang.DataSource = KetQua;
        lblThongKe.Text = $"đã tìm thấy{KetQua.Count} khách hàng";
    }

    private void ActionMoFormGhiNuoc()
    {
        if (string.IsNullOrEmpty(txtId.Text))
        {
            MessageBox.Show(
                "Vui lòng click chọn 1 khách hàng trên bảng trước khi ghi nước!",
                "Nhắc nhở"
            );
            return;
        }
        int KhachHangId = int.Parse(txtId.Text);
        string HoTen = txtHoTen.Text;
        string MaDH = txtMaDH.Text;
        var formGhiNuoc = new FormGhiNuoc(KhachHangId, HoTen, MaDH);
        formGhiNuoc.ShowDialog();
    }

    /// <summary>
    /// Lấy chữ từ các ô bên trái -> Đóng vào Model KhachHang -> Gửi cho SQL Server thêm mới
    /// </summary>
    private void ActionThem()
    {
        // 1. Kiểm tra xem người dùng đã nhập thông tin tối thiểu chưa
        if (string.IsNullOrWhiteSpace(txtMaKH.Text) || string.IsNullOrWhiteSpace(txtHoTen.Text))
        {
            MessageBox.Show("Vui lòng nhập ít nhất Mã KH và Họ tên!", "Nhắc nhở");
            return;
        }

        // 2. Thu thập dữ liệu từ các ô TextBox vào chiếc hộp Model KhachHang
        var kh = new KhachHang
        {
            MaKhachHang = txtMaKH.Text.Trim(),
            HoTen = txtHoTen.Text.Trim(),
            DiaChi = txtDiaChi.Text.Trim(),
            SoDienThoai = txtSDT.Text.Trim(),
            Email = txtEmail.Text.Trim(),
            MaSoDongHo = txtMaDH.Text.Trim(),
        };

        // 3. Gọi DatabaseHelper.ThemKhachHang để lưu vào SQL Server
        if (DatabaseHelper.ThemKhachHang(kh))
        {
            MessageBox.Show("Thêm khách hàng mới thành công! 🎉", "Thông báo");
            LoadData(); // Tải lại bảng để khách hàng mới xuất hiện ngay
            XoaTrangForm(); // Dọn sạch ô nhập liệu để sẵn sàng nhập người tiếp theo
        }
    }

    /// <summary>
    /// Lấy thông tin đã sửa từ các ô bên trái -> Cập nhật xuống SQL Server
    /// </summary>
    private void ActionSua()
    {
        if (string.IsNullOrEmpty(txtId.Text))
        {
            MessageBox.Show(
                "Vui lòng click chọn 1 khách hàng trên bảng trước khi sửa!",
                "Nhắc nhở"
            );
            return;
        }

        // Tạo object KhachHang mang thông tin mới cùng với Id của người đang chọn
        var kh = new KhachHang
        {
            Id = int.Parse(txtId.Text),
            HoTen = txtHoTen.Text.Trim(),
            DiaChi = txtDiaChi.Text.Trim(),
            SoDienThoai = txtSDT.Text.Trim(),
            Email = txtEmail.Text.Trim(),
        };

        // Gọi DatabaseHelper cập nhật vào SQL Server
        if (DatabaseHelper.CapNhatKhacHang(kh))
        {
            MessageBox.Show("Cập nhật thông tin thành công! 🎉", "Thông báo");
            LoadData(); // Tải lại bảng để thấy thông tin mới đổi
        }
    }

    /// <summary>
    /// Xóa mềm khách hàng đang chọn trên bảng
    /// </summary>
    private void ActionXoa()
    {
        if (string.IsNullOrEmpty(txtId.Text))
        {
            MessageBox.Show(
                "Vui lòng click chọn 1 khách hàng trên bảng trước khi xóa!",
                "Nhắc nhở"
            );
            return;
        }

        // Hỏi xác nhận trước khi xóa để tránh người dùng bấm nhầm
        var confirm = MessageBox.Show(
            $"Bạn có chắc chắn muốn xóa khách hàng: [{txtMaKH.Text}] {txtHoTen.Text}?",
            "Xác nhận xóa",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        );

        if (confirm == DialogResult.Yes)
        {
            var kh = new KhachHang { Id = int.Parse(txtId.Text) };

            // Gọi DatabaseHelper để đánh dấu DangHoatDong = 0
            if (DatabaseHelper.XoaKhachHang(kh))
            {
                MessageBox.Show("Đã xóa khách hàng thành công!", "Thông báo");
                LoadData(); // Tải lại bảng, khách hàng này sẽ biến mất khỏi bảng
                XoaTrangForm(); // Xóa trắng các ô nhập
            }
        }
    }

    /// <summary>
    /// Làm sạch tất cả các ô nhập liệu về trạng thái rỗng
    /// </summary>
    private void XoaTrangForm()
    {
        txtId.Clear();
        txtMaKH.Clear();
        txtHoTen.Clear();
        txtDiaChi.Clear();
        txtSDT.Clear();
        txtEmail.Clear();
        txtMaDH.Clear();
        txtMaKH.Focus(); // Đặt con trỏ chuột nhấp nháy sẵn ở ô Mã KH
    }
}
