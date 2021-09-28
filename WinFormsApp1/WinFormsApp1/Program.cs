using System;
using System.Windows.Forms;

namespace WinFormsApp1
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var view = new View();
            _ = new Presenter(new Model(), view);
            Application.Run(view);
        }
    }
}
