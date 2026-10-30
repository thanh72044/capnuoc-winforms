namespace CapNuocApp;

static class Program
{
    [STAThread]
    static void Main()
    {
        BaiTap7.Test4_Find();
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}
