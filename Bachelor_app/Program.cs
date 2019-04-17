using System;
using System.Windows.Forms;
using Bachelor_app.Resources;

namespace Bachelor_app
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Localizer.InitLocalizedResource("en-EN", "Bachelor_app.Language.Resources.Resources");
            Configuration.GenerateFolders();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
