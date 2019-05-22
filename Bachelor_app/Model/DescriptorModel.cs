using System.IO;
using System.Text;

namespace Bachelor_app.Model
{
    public class DescriptorModel
    {
        public KeyPointModel KeyPoint { get; private set; }
        public Mat Descriptor { get; private set; }
        public string FileFormatSIFT { get; private set; }

        public DescriptorModel(KeyPointModel keyPointModel, Mat descriptors)
        {
            KeyPoint = keyPointModel;
            Descriptor = descriptors;
        }

        /// <summary>
        /// Save SIFT format text in model.
        /// </summary>
        /// <param name="SiftText">Text in SIFT format.</param>
        public void SaveSiftInModel(string SiftText)
        {
            FileFormatSIFT = SiftText;
        }
    }

    public static class DescriptorExtension
    {
        /// <summary>
        /// Save descriptor in txt file for VisualSFM.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="SaveInTempDirectory"></param>
        /// <param name="SaveInDescriptorNode"></param>
        public static void SaveSiftFile(this DescriptorModel model, bool SaveInTempDirectory = true, bool SaveInDescriptorNode = true)
        {
            var descriptor = model.Descriptor;
            var keyPoints = model.KeyPoint.DetectedKeyPoints;
            var fileName = $"{model.KeyPoint.InputFile.FileNameWithoutExtension}.SIFT";
            var descriptorSavePath = Path.Combine(Configuration.TempDirectoryPath, fileName);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{keyPoints.Size} 128");

            for (int i = 0; i < descriptor.Rows; i++)
            {
                // X and Y are switched, now it's good
                sb.AppendLine($"{keyPoints[i].Point.Y} {keyPoints[i].Point.X} {keyPoints[i].Size} {keyPoints[i].Angle}");

                for (int j = 0; j < 128; j++)
                    if (j < descriptor.Cols)
                        sb.Append($"{descriptor.GetValue(i, j)} ");
                    else
                        sb.Append("0 ");
                sb.AppendLine();
            }

            if (SaveInTempDirectory)
                File.WriteAllText(descriptorSavePath, sb.ToString());

            if (SaveInDescriptorNode)
                model.SaveSiftInModel(sb.ToString());
        }
    }
}
