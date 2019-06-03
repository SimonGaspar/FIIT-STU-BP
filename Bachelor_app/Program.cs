using System;
using System.Windows.Forms;
using Bachelor_app.Resources;

namespace Bachelor_app
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Localizer.InitLocalizedResource("en-EN", "Bachelor_app.Language.Resources.Resources");
            Configuration.GenerateFolders();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
