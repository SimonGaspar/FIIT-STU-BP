using Bachelor_app;
using Bakalárska_práca.Extension;
using Emgu.CV;
using System.IO;
using System.Text;

namespace Bakalárska_práca.Model
{
    /// <summary>
    /// Model for descriptor
    /// </summary>
    public class DescriptorModel
    {
        public KeyPointModel KeyPoint;
        public Mat Descriptors;
        public string FileFormatSIFT;
    }

    public static class DescriptorExtension {
        public static void SaveSiftFile(this DescriptorModel model, bool SaveInTempDirectory = true, bool SaveInDescriptorNode = true)
        {
            var descriptor = model.Descriptors;
            var keyPoints = model.KeyPoint.DetectedKeyPoints;
            var fileName = $"{Path.GetFileNameWithoutExtension(model.KeyPoint.InputFile.fileInfo.Name)}.SIFT";

            var countKeypoint = keyPoints.Size;
            var countDescriptor = descriptor.Cols;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{countKeypoint} 128");

            for (int i = 0; i < descriptor.Rows; i++)
            {
                // X a Y su prehodene, teraz je to dobre
                sb.AppendLine($"{keyPoints[i].Point.Y} {keyPoints[i].Point.X} {keyPoints[i].Size} {keyPoints[i].Angle}");

                for (int j = 0; j < 128; j++)
                    if (j < descriptor.Cols)
                        sb.Append($"{descriptor.GetValue(i, j)} ");
                    else
                        sb.Append("0 ");
                sb.AppendLine();
            }

            if (SaveInTempDirectory)
                File.WriteAllText(Path.Combine(Configuration.TempDirectoryPath, fileName), sb.ToString());

            if (SaveInDescriptorNode)
                model.FileFormatSIFT = sb.ToString();
        }
    }
}
