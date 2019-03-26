using System;
using System.Windows.Forms;
using Bachelor_app.Extension;
using Bachelor_app.Resources;
using Bakalárska_práca.Enumerate;
using Emgu.CV.Cuda;

namespace Bakalárska_práca
{
    static class Program
    {
        //Na konci odkomentovat 
        //
        //ACTIVATEFORFULLFORM

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Localizer.InitLocalizedResource("en-EN", "Bachelor_app.Language.Resources.Resources");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
