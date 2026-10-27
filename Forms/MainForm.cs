namespace CapNuocApp;

public class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        // === Cấu hình cửa sổ chính ===
        this.Text = "Quản Lý Cấp Nước Bến Thành";
        this.Size = new Size(900, 600);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.BackColor = Color.FromArgb(240, 244, 248);

        // === Tiêu đề ===
        var lblTitle = new Label
        {
            Text = "💧 Hệ Thống Quản Lý Cấp Nước",
            Font = new Font("Segoe UI", 20, FontStyle.Bold),
            ForeColor = Color.FromArgb(30, 100, 200),
            Location = new Point(30, 30),
            AutoSize = true
        };

        // === Nút ví dụ ===
        var btnHello = new Button
        {
            Text = "Xin chào WinForms! 👋",
            Location = new Point(30, 100),
            Size = new Size(220, 45),
            BackColor = Color.FromArgb(30, 100, 200),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 11),
            Cursor = Cursors.Hand
        };
        btnHello.Click += (s, e) =>
            MessageBox.Show("Xin chào Cấp Nước Bến Thành!\nBạn đang học WinForms với Antigravity 🚀",
                            "Chào mừng",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

        this.Controls.Add(lblTitle);
        this.Controls.Add(btnHello);
    }
}
