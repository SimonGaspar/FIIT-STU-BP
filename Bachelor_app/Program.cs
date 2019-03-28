using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Bachelor_app.Extension;
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

            //string tempDirectory = Path.GetFullPath($"..\\..\\..\\Temp");

            //try
            //{
            //    while (true)
            //    {
            //        var cuda = new CudaORBDetector(30000, scoreType: Emgu.CV.Features2D.ORBDetector.ScoreType.Harris);
            //        var x = new Mat(@"C:\Users\Notebook\Desktop\VisualSFM_windows_cuda_64bit\Example\VisualStudioCodeTest\100_7101.JPG");
            //        var xx = new Mat(@"C:\Users\Notebook\Desktop\VisualSFM_windows_cuda_64bit\Example\VisualStudioCodeTest\100_7102.JPG");
            //        GpuMat y = new GpuMat(x);
            //        GpuMat z = new GpuMat(xx);
            //        CudaInvoke.CvtColor(y, y, ColorConversion.Bgr2Gray);
            //        CudaInvoke.CvtColor(z, z, ColorConversion.Bgr2Gray);
            //        var KeyPoins1 = cuda.Detect(y);
            //        var KeyPoins2 = cuda.Detect(z);
            //        var vectorKeyPoint1 = new VectorOfKeyPoint(KeyPoins1);
            //        var vectorKeyPoint2 = new VectorOfKeyPoint(KeyPoins2);
            //        GpuMat Descriptor1 = new GpuMat();
            //        GpuMat Descriptor2 = new GpuMat();
            //        cuda.Compute(y, vectorKeyPoint1, Descriptor1);
            //        cuda.Compute(z, vectorKeyPoint2, Descriptor2);

            //        var matcher = new CudaBFMatcher(Emgu.CV.Features2D.DistanceType.Hamming);
            //        var vector = new Emgu.CV.Util.VectorOfVectorOfDMatch();
            //        matcher.KnnMatch(Descriptor1, Descriptor2, vector, 1, null, false);

            //        Mat output = new Mat();
            //        Directory.CreateDirectory($@"{tempDirectory}\DrawMatches");
            //        Features2DToolbox.DrawMatches(x, vectorKeyPoint1, xx, vectorKeyPoint2, vector, output, new MCvScalar(0, 0, 255), new MCvScalar(0, 255, 0));
            //        output.Save(Path.Combine($@"{tempDirectory}\DrawMatches", $"Pokus.JPG"));

            //    }
            //}
            //catch (Exception e) {
            //    Console.WriteLine("");
            //}



            Localizer.InitLocalizedResource("en-EN", "Bachelor_app.Language.Resources.Resources");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
