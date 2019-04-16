using System.IO;
using Bachelor_app;
using Bakalárska_práca.Enumerate;
using Bakalárska_práca.Extension;
using Bakalárska_práca.Manager;
using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using static Emgu.CV.Features2D.Features2DToolbox;

namespace Bakalárska_práca.Model
{
    /// <summary>
    /// KeyPoint model
    /// </summary>
    public class KeyPointModel
    {
        public VectorOfKeyPoint DetectedKeyPoints;
        public InputFileModel InputFile;
        public int ID;
    }


    public static class KeyPointExtension
    {
        public static void DrawAndSave(this KeyPointModel model, FileManager fileManager)
        {
            var fileName = $"{Path.GetFileNameWithoutExtension(model.InputFile.fileInfo.Name)}.JPG";
            var filePath = Path.Combine(Configuration.TempDrawKeypoint, fileName);

            Mat output = new Mat();
            Features2DToolbox.DrawKeypoints(new Mat(fileName), model.DetectedKeyPoints, output, new Bgr(0, 0, 255), KeypointDrawType.DrawRichKeypoints);
            output.Save(filePath);

            fileManager.listViewerModel._lastDrawnKeypoint = output.Image2ImageBGR();
            fileManager.AddInputFileToList(filePath, EListViewGroup.DrawnKeyPoint);
        }
    }
}
