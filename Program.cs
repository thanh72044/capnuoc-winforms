namespace CapNuocApp;

static class Program
{
    [STAThread]
    static void Main()
    {
        BaiTap5.Test1();
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}
