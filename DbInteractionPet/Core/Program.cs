using System;
using System.Windows.Forms;
using System.IO;

namespace DbInteractionPet
{
    static class Program
    {
        private const string root = @"C:\Projects\DbInteractionPet\DbInteractionPet";

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", root);

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainMenu());
        }
    }
}
