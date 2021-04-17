using System;
using System.Windows.Forms;

namespace MusicPlayerForOBS
{
    static class Program
    {
        static Program()
        {
            LibrariesLoader.RegisterDependencyResolver();
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
