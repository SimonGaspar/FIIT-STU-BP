using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
            try
            {
                Mat output = new Mat();
                var fileName = $"{Path.GetFileNameWithoutExtension(model.RightDescriptor.KeyPoint.InputFile.fileInfo.Name)}_{Path.GetFileNameWithoutExtension(model.LeftDescriptor.KeyPoint.InputFile.fileInfo.Name)}.JPG";
                var filePath = Path.Combine(Configuration.TempDirectoryPath, fileName);
                var savePath = Path.Combine(Configuration.TempDrawMatches, fileName);

                Features2DToolbox.DrawMatches(new Mat(model.LeftDescriptor.KeyPoint.InputFile.fileInfo.FullName), model.LeftDescriptor.KeyPoint.DetectedKeyPoints, new Mat(model.RightDescriptor.KeyPoint.InputFile.fileInfo.FullName), model.RightDescriptor.KeyPoint.DetectedKeyPoints, new VectorOfVectorOfDMatch(model.FilteredMatchesList.ToArray()), output, new MCvScalar(0, 0, 255), new MCvScalar(0, 255, 0), model.Mask);
                output.Save(savePath);

                fileManager.listViewerModel._lastDrawnMatches = output.Image2ImageBGR();
                fileManager.AddInputFileToList(savePath, EListViewGroup.DrawnMatches);
            }
            catch (Exception e)
            {
                throw new Exception($"Problem with {System.Reflection.MethodBase.GetCurrentMethod().Name}\n\n{e.Message}\n\n{e.StackTrace}\n", e);
            }
        }

        public static int SaveMatchString(this MatchModel model, bool UseMask = true)
        {
            if (model.Mask == null || model.FilteredMatchesList.Count == 0)
            {
                model.FileFormatMatch = null;
                return 0;
            }


            var leftImageName = model.LeftDescriptor.KeyPoint.InputFile.fileInfo.Name;
            var rightImageName = model.RightDescriptor.KeyPoint.InputFile.fileInfo.Name;
            var matchesList = model.FilteredMatch ? model.FilteredMatchesList : model.MatchesList;
            
            int countMaskMatches = 0;
            if (UseMask)
            {
                for (int m = 0; m < matchesList.Count; m++)
                {
                    if (model.Mask.GetValue(0, m) > 0)
                        countMaskMatches++;
                }
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Path.Combine(Configuration.TempDirectoryPath, leftImageName));
            sb.AppendLine(Path.Combine(Configuration.TempDirectoryPath, rightImageName));
            sb.AppendLine($"{(UseMask ? countMaskMatches : matchesList.Count)}");
            for (int m = 0; m < matchesList.Count; m++)
            {
                if (!UseMask || model.Mask.GetValue(0, m) > 0)
                    sb.Append($"{matchesList[m][0].TrainIdx} ");
            }
            sb.AppendLine();
            for (int m = 0; m < matchesList.Count; m++)
            {
                if (!UseMask || model.Mask.GetValue(0, m) > 0)
                    sb.Append($"{matchesList[m][0].QueryIdx} ");
            }

            model.FileFormatMatch = sb.ToString();
            return 0;
        }
    }
}
