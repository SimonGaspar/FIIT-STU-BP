using System.Collections.Generic;
using System.IO;
using Bachelor_app;
using Bakalárska_práca.Enumerate;
using Bakalárska_práca.Extension;
using Bakalárska_práca.Manager;
using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace Bakalárska_práca.Model
{
    /// <summary>
    /// Descriptor matches model
    /// </summary>
    public class MatchModel
    {
        public DescriptorModel LeftDescriptor;
        public DescriptorModel RightDescriptor;
        public List<MDMatch[]> MatchesList;
        public Mat PerspectiveMatrix;
        public Mat Mask;
        public string FileFormatMatch;
        public List<MDMatch[]> FilteredMatchesList;
        public bool FilteredMatch;
    }

    public static class MatchExtension
    {
        public static void DrawAndSave(this MatchModel model, FileManager fileManager)
        {
            Mat output = new Mat();
            var fileName = $"{Path.GetFileNameWithoutExtension(model.RightDescriptor.KeyPoint.InputFile.fileInfo.Name)}_{Path.GetFileNameWithoutExtension(model.LeftDescriptor.KeyPoint.InputFile.fileInfo.Name)}.JPG";
            var filePath = Path.Combine(Configuration.TempDrawMatches, fileName);

            Features2DToolbox.DrawMatches(new Mat(model.LeftDescriptor.KeyPoint.InputFile.fileInfo.FullName), model.LeftDescriptor.KeyPoint.DetectedKeyPoints, new Mat(model.RightDescriptor.KeyPoint.InputFile.fileInfo.FullName), model.RightDescriptor.KeyPoint.DetectedKeyPoints, new VectorOfVectorOfDMatch(model.FilteredMatchesList.ToArray()), output, new MCvScalar(0, 0, 255), new MCvScalar(0, 255, 0), model.Mask);
            output.Save(filePath);

            fileManager.listViewerModel._lastDrawnMatches = output.Image2ImageBGR();
            fileManager.AddInputFileToList(filePath, EListViewGroup.DrawnMatches);
        }
    }
}
