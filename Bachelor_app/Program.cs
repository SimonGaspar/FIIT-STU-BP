using System;
using System.IO;
using System.Windows.Forms;
using Bachelor_app.Resources;
using Bachelor_app.Tools;
using Emgu.CV;
using Emgu.CV.Structure;

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
            //var path = $@"C:\Users\Notebook\Desktop\Testovacie data\Calibrated NotreDame";
            //var savePath = $@"C:\Users\Notebook\Desktop\Testovacie data\Little NotreDame";
            //var directoryInfo = new DirectoryInfo(path);

            //    foreach (var item in directoryInfo.GetFiles()) {
            //    var image = new Image<Bgr, byte>(item.FullName);
            //    var size = image.Size.Width > image.Size.Height ? image.Size.Width : image.Size.Height;
            //    while (size > 1000) {
            //        var x = image.Resize(0.50, Emgu.CV.CvEnum.Inter.Linear);
            //        image = x;
            //        size = (int)Math.Floor(size * 0.5);
            //    }

            //    image.Save(Path.Combine(savePath, item.Name));
            //}


            Localizer.InitLocalizedResource("en-EN", "Bachelor_app.Language.Resources.Resources");
            Configuration.GenerateFolders();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
