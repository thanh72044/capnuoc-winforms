namespace CapNuocApp;

static class Program
{
    [STAThread]
    static void Main()
    {
        BaiTap7.Test1_Where();
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}
