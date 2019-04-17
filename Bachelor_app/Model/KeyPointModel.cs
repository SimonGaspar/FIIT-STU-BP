using System;
using System.IO;
using Bachelor_app.Enumerate;
using Bachelor_app.Extension;
using Bachelor_app.Manager;
using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using static Emgu.CV.Features2D.Features2DToolbox;

namespace Bachelor_app.Model
{
    public class KeyPointModel
    {
        public VectorOfKeyPoint DetectedKeyPoints { get; set; }
        public InputFileModel InputFile { get; set; }
        public int ID { get; set; }
    }


    public static class KeyPointExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="fileManager">Save in FileManager ListViewGroup.</param>
        public static void DrawAndSave(this KeyPointModel model, FileManager fileManager)
        {
            try
            {
                var fileName = $"{model.InputFile.FileNameWithoutExtension}.JPG";
                var filePath = model.InputFile.FullPath;
                var savePath = Path.Combine(Configuration.TempDrawKeypoint, fileName);

                Mat output = new Mat();
                Features2DToolbox.DrawKeypoints(new Mat(filePath), model.DetectedKeyPoints, output, new Bgr(0, 0, 255), KeypointDrawType.DrawRichKeypoints);
                output.Save(savePath);

                fileManager.ListViewModel._lastDrawnKeypoint = output.ToImageBGR();
                fileManager.AddInputFileToList(savePath, EListViewGroup.DrawnKeyPoint);
            }
            catch (Exception e)
            {
                throw new Exception($"Problem with {System.Reflection.MethodBase.GetCurrentMethod().Name}\n\n{e.Message}\n\n{e.StackTrace}\n", e);
            }
        }
    }
}
