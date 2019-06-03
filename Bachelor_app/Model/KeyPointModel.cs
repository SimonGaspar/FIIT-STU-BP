using Bachelor_app.Enumerate;
using Bachelor_app.Extension;
using Bachelor_app.Manager;
using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.IO;
using System.Threading.Tasks;
using static Emgu.CV.Features2D.Features2DToolbox;

namespace Bachelor_app.Model
{
    public class KeyPointModel
    {
        public VectorOfKeyPoint DetectedKeyPoints { get; private set; }
        public InputFileModel InputFile { get; private set; }
        public int ID { get; private set; }

        public KeyPointModel(VectorOfKeyPoint detectedKeyPoints, InputFileModel inputFile, int id)
        {
            DetectedKeyPoints = detectedKeyPoints;
            InputFile = inputFile;
            ID = id;
        }
    }


    public static class KeyPointExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="fileManager">Save in FileManager ListViewGroup.</param>
        public async static Task DrawAndSaveAsync(this KeyPointModel model, FileManager fileManager)
        {
            try
            {
                var fileName = $"{model.InputFile.FileNameWithoutExtension}.JPG";
                var filePath = model.InputFile.FullPath;
                var savePath = Path.Combine(Configuration.TempDrawKeypoint, fileName);

                using (Mat output = new Mat())
                {
                    Features2DToolbox.DrawKeypoints(new Mat(filePath), model.DetectedKeyPoints, output, new Bgr(0, 0, 255), KeypointDrawType.DrawRichKeypoints);
                    output.Save(savePath);

                    fileManager.ListViewModel._lastDrawnKeypoint = output.ToImageBGR();
                    fileManager.AddInputFileToList(savePath, EListViewGroup.DrawnKeyPoint);
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Problem with {System.Reflection.MethodBase.GetCurrentMethod().Name}\n\n{e.Message}\n\n{e.StackTrace}\n", e);
            }
        }
    }
}
