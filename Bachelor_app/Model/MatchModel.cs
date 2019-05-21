using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Bachelor_app.Enumerate;
using Bachelor_app.Extension;
using Bachelor_app.Manager;
using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace Bachelor_app.Model
{
    public class MatchModel
    {
        public DescriptorModel LeftDescriptor { get; private set; }
        public DescriptorModel RightDescriptor { get; private set; }
        public List<MDMatch[]> MatchesList { get; private set; }
        public Mat PerspectiveMatrix { get; private set; }
        public Mat Mask { get; private set; }
        public string FileFormatMatch { get; private set; }
        public List<MDMatch[]> FilteredMatchesList { get; private set; }
        public bool FilteredMatch { get; private set; }

        public MatchModel(DescriptorModel leftDescriptor, DescriptorModel rightDescriptor, List<MDMatch[]> matchesList, Mat perspectiveMatrix, Mat mask, string fileFormatMatch, List<MDMatch[]> filteredMatchesList, bool filteredMatch)
        {
            LeftDescriptor = leftDescriptor;
            RightDescriptor = rightDescriptor;
            MatchesList = matchesList;
            PerspectiveMatrix = perspectiveMatrix;
            Mask = mask;
            FileFormatMatch = fileFormatMatch;
            FilteredMatchesList = filteredMatchesList;
            FilteredMatch = filteredMatch;
        }

        /// <summary>
        /// Save all founded matches in model.
        /// </summary>
        /// <param name="text">Text in correct format</param>
        public void SaveMatchInModel(string text)
        {
            FileFormatMatch = text;
        }
    }

    public static class MatchExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="fileManager">Save in FileManager ListViewGroup.</param>
        public static void DrawAndSave(this MatchModel model, FileManager fileManager)
        {
            try
            {
                var fileName = $"{model.RightDescriptor.KeyPoint.InputFile.FileNameWithoutExtension}_{model.LeftDescriptor.KeyPoint.InputFile.FileNameWithoutExtension}.JPG";
                var savePath = Path.Combine(Configuration.TempDrawMatches, fileName);

                using (Mat output = new Mat())
                {
                    Features2DToolbox.DrawMatches(new Mat(model.LeftDescriptor.KeyPoint.InputFile.FullPath), model.LeftDescriptor.KeyPoint.DetectedKeyPoints, new Mat(model.RightDescriptor.KeyPoint.InputFile.FullPath), model.RightDescriptor.KeyPoint.DetectedKeyPoints, new VectorOfVectorOfDMatch(model.FilteredMatchesList.ToArray()), output, new MCvScalar(0, 0, 255), new MCvScalar(0, 255, 0), model.Mask);
                    output.Save(savePath);

                    fileManager.ListViewModel._lastDrawnMatches = output.ToImageBGR();
                    fileManager.AddInputFileToList(savePath, EListViewGroup.DrawnMatches);
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Problem with {System.Reflection.MethodBase.GetCurrentMethod().Name}\n\n{e.Message}\n\n{e.StackTrace}\n", e);
            }
        }

        // <summary>
        /// Save string with all matches in model.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UseMask"></param>
        /// <returns></returns>
        public static string SaveMatchString(this MatchModel model, bool UseMask = true, bool SaveInNode=false)
        {
            if (model.Mask == null || model.FilteredMatchesList.Count == 0)
            {
                model.SaveMatchInModel(null);
                return null;
            }


            var leftImageName = model.LeftDescriptor.KeyPoint.InputFile.FileName;
            var rightImageName = model.RightDescriptor.KeyPoint.InputFile.FileName;
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

            if(SaveInNode)
                model.SaveMatchInModel(sb.ToString());

            return sb.ToString();
        }
    }
}
