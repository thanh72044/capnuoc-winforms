namespace CapNuocApp;

static class Program
{
    [STAThread]
    static void Main()
    {
        BaiTap7.Test5_TryCatch();
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}
