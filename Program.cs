using CapNuocApp.Forms;

namespace CapNuocApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new FormKhachHang());
        }
    }
}
