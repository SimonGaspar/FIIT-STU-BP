using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Bachelor_app.Extension;
using Bachelor_app.Helper;
using Bachelor_app.Manager;
using Bachelor_app.Resources;
using Bakalárska_práca.Enumerate;
using Emgu.CV;
using Emgu.CV.Cuda;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;

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
