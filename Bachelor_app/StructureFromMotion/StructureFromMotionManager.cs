using Bakalárska_práca.Extension;
using Bakalárska_práca.Manager;
using Bakalárska_práca.Model;
using Bakalárska_práca.StructureFromMotion;
using Bakalárska_práca.StructureFromMotion.FeatureDetectionDescription;
using Bakalárska_práca.StructureFromMotion.FeatureMatcher;
using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakalárska_práca
{
    public class SfM
    {
        const string leftImageName = "100_7100";
        const string centerImageName = "100_7102";
        const string rightImageName = "100_7104";

        const string path = @"C:\Users\Notebook\Desktop\VisualSFM_windows_cuda_64bit\Example\images";
        const string pathVisualSFM = @"C:\Users\Notebook\Desktop\VisualSFM_windows_cuda_64bit";

        string tempDirectory = Path.GetFullPath($"..\\..\\..\\Temp");
        string matchFileName = $"AllFoundedMatches.txt";

        public IFeatureDetector _detector;
        public IFeatureDescriptor _descriptor;
        public IFeatureMatcher _matcher;
        float ms_MAX_DIST = 0, ms_MIN_DIST = float.MaxValue;

        private List<KeyPoint> DetectedKeyPoints;
        private List<Descriptor> ComputedDescriptors;
        private List<DescriptorsMatch> FoundedMatches;

        private FileManager fileManager;
        private DisplayManager displayManager;

        public SfM(FileManager fileManager, DisplayManager displayManager) {

            Directory.CreateDirectory(tempDirectory);
            DetectedKeyPoints = new List<KeyPoint>();
            ComputedDescriptors = new List<Descriptor>();
            FoundedMatches = new List<DescriptorsMatch>();

            this.fileManager = fileManager;
            this.displayManager = displayManager;


            //var list = new List<InputFile>();

            //var files = Directory.GetFiles(@"C:\Users\Notebook\Desktop\VisualSFM_windows_cuda_64bit\Example\VisualStudioCodeTest");
            //foreach (var node in files)
            //{
            //    var input = new InputFile(new FileInfo(node));
            //    list.Add(input);
            //    File.Copy(node, Path.Combine(tempDirectory, input.fileInfo.Name),true);
            //}

            //ComputeSfM(new OrientedFastAndRotatedBrief(), new OrientedFastAndRotatedBrief(), new BruteForce(), list);



            //FindAndMatch();

            //var orb = new ORBDetector(1000, edgeThreshold: 240);
            //var imageLeft = new Mat(Path.Combine(path, $"{leftImageName}.JPG"));
            //var imageRight = new Mat(Path.Combine(path, $"{centerImageName}.JPG"));

            //var leftKeyPoints = new VectorOfKeyPoint();
            //var rightKeyPoints = new VectorOfKeyPoint();
            //var leftDescriptor = new Mat();
            //var rightDescriptor = new Mat();

            //orb.DetectAndCompute(imageLeft, null, leftKeyPoints, leftDescriptor, false);
            //orb.DetectAndCompute(imageRight, null, rightKeyPoints, rightDescriptor, false);

            //var matcher = new BFMatcher(DistanceType.Hamming2, true);
            //var matches = new VectorOfVectorOfDMatch();
            //matcher.Add(leftDescriptor);
            //matcher.KnnMatch(rightDescriptor, matches, 1, null);

            //var Mask = new Mat();
            //var PerspectiveMatrix = new Mat();
            //PerspectiveMatrix = CvInvoke.FindHomography(leftKeyPoints, rightKeyPoints, Emgu.CV.CvEnum.HomographyMethod.Ransac, 3, Mask);


            //var drawMatches = new Mat();
            //Features2DToolbox.DrawMatches(imageLeft, leftKeyPoints, imageRight, rightKeyPoints, matches, drawMatches, new MCvScalar(0, 0, 255, 128), new MCvScalar(0, 255, 0, 128), Mask, Features2DToolbox.KeypointDrawType.DrawRichKeypoints);
            //drawMatches.Save(Path.Combine(path, "Pokus.jpg"));
        }

        public void StartSFM() {
            var list = fileManager.ListOfInputFile;

            foreach (var node in list)
            {
                File.Copy(node.fileInfo.FullName, Path.Combine(tempDirectory, node.fileInfo.Name), true);
            }

            ComputeSfM(new OrientedFastAndRotatedBrief(), new OrientedFastAndRotatedBrief(), new BruteForce(), list);
        }

        public void ComputeSfM(IFeatureDetector detector, IFeatureDescriptor descriptor, IFeatureMatcher matcher, List<InputFile> listOfInput)
        {
            foreach (var item in listOfInput)
            {
                FindKeypoint(item,detector);
            }

            foreach (var item in DetectedKeyPoints)
            {
                ComputeDescriptor(item,descriptor);
            }

            //for (int m = 0; m < ComputedDescriptors.Count; m++)
            //    for (int n = m + 1; n < ComputedDescriptors.Count; n++)
            //        FindMatches(matcher, ComputedDescriptors[m], ComputedDescriptors[n]);

            for (int m = 0; m < ComputedDescriptors.Count; m++)
                for (int n = m -2; n <m && n>=0 ; n++)
                    FindMatches(matcher, ComputedDescriptors[m], ComputedDescriptors[n]);

            WriteAllMatches(FoundedMatches);
            RunVisualSFM();
        }

        private void WriteAllMatches(List<DescriptorsMatch> findedMatches)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var node in findedMatches)
            {
                sb.AppendLine(node.FileFormatMatch);
                sb.AppendLine();
            }

            File.WriteAllText(Path.Combine(tempDirectory, matchFileName), sb.ToString());
        }

        private void FindMatches(IFeatureMatcher matcher, Descriptor leftDescriptor, Descriptor rightDescriptor, bool AddToList=true, bool FilterMatches=true,bool ComputeHomography=true, bool SaveInMatchNode=true)
        {
            var foundedMatch = new DescriptorsMatch() {
                FilteredMatch =FilterMatches,
                LeftDescriptor = leftDescriptor,
                RightDescriptor = rightDescriptor
            };

            var matches = new VectorOfVectorOfDMatch();
            matcher.Add(leftDescriptor.Descriptors);
            matcher.Match(rightDescriptor.Descriptors, matches);
            
            MDMatch[][] matchesArray = matches.ToArrayOfArray();
            foundedMatch.MatchesList = matchesArray.ToList();

            if (FilterMatches)
            {
                FindMinMaxDistInMatches(matchesArray, ref ms_MAX_DIST, ref ms_MIN_DIST);
                List<MDMatch[]> filteredMatchesList = FilterMatchesByMaxDist(matchesArray);
                foundedMatch.FilteredMatchesList = filteredMatchesList;
            }

            if (ComputeHomography)
            {
                var PerspectiveMatrix = new Mat();
                Mat Mask = new Mat();

                PerspectiveMatrix = FindHomography(leftDescriptor.KeyPoint.DetectedKeyPoints, rightDescriptor.KeyPoint.DetectedKeyPoints,FilterMatches? foundedMatch.FilteredMatchesList:foundedMatch.MatchesList, Mask);

                foundedMatch.Mask = Mask;
                foundedMatch.PerspectiveMatrix = PerspectiveMatrix;
            }

            // Save drawing image
            Mat output = new Mat();
            Directory.CreateDirectory($@"{tempDirectory}\DrawMatches");
            Features2DToolbox.DrawMatches(new Mat(foundedMatch.LeftDescriptor.KeyPoint.InputFile.fileInfo.FullName), foundedMatch.LeftDescriptor.KeyPoint.DetectedKeyPoints, new Mat(foundedMatch.RightDescriptor.KeyPoint.InputFile.fileInfo.FullName), foundedMatch.RightDescriptor.KeyPoint.DetectedKeyPoints, new VectorOfVectorOfDMatch(foundedMatch.FilteredMatchesList.ToArray()), output, new MCvScalar(0, 0, 255), new MCvScalar(0, 255, 0), foundedMatch.Mask);
            output.Save(Path.Combine($@"{tempDirectory}\DrawMatches", $"{Path.GetFileNameWithoutExtension(foundedMatch.LeftDescriptor.KeyPoint.InputFile.fileInfo.Name)}{Path.GetFileNameWithoutExtension(foundedMatch.RightDescriptor.KeyPoint.InputFile.fileInfo.Name)}.JPG"));

            if (SaveInMatchNode)
                SaveMatchString(foundedMatch,true);

            if (AddToList)
                FoundedMatches.Add(foundedMatch);
        }

        private void SaveMatchString(DescriptorsMatch descriptorsMatch, bool UseMask)
        {
            var leftImageName = descriptorsMatch.LeftDescriptor.KeyPoint.InputFile.fileInfo.Name;
            var rightImageName = descriptorsMatch.RightDescriptor.KeyPoint.InputFile.fileInfo.Name;

            var matchesList = descriptorsMatch.FilteredMatch ? descriptorsMatch.FilteredMatchesList : descriptorsMatch.MatchesList;


            int countMaskMatches = 0;
            if (UseMask)
            {
                for (int m = 0; m < matchesList.Count; m++)
                {
                    if (descriptorsMatch.Mask.GetValue(0, m) > 0)
                        countMaskMatches++;
                }
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Path.Combine(Path.GetFullPath(tempDirectory),leftImageName));
            sb.AppendLine(Path.Combine(Path.GetFullPath(tempDirectory), rightImageName));
            sb.AppendLine($"{(UseMask ? countMaskMatches : matchesList.Count)}");
            for(int m = 0; m < matchesList.Count; m++) { 
                if(!UseMask || descriptorsMatch.Mask.GetValue(0,m)>0)
                sb.Append($"{matchesList[m][0].TrainIdx} ");
            }
            sb.AppendLine();
            for (int m = 0; m < matchesList.Count; m++)
            {
                if (!UseMask || descriptorsMatch.Mask.GetValue(0, m) > 0)
                    sb.Append($"{matchesList[m][0].QueryIdx} ");
            }

            descriptorsMatch.FileFormatMatch = sb.ToString();
        }

        private List<MDMatch[]> FilterMatchesByMaxDist(MDMatch[][] matchesArray)
        {
            List<MDMatch[]> filteredMatchesList = new List<MDMatch[]>();

            for (int i = 0; i < matchesArray.Length; i++)
            {
                if(matchesArray[i].Length == 0) continue;

                if (matchesArray[i][0].Distance < GetMaxPossibleDist())
                {
                    filteredMatchesList.Add(matchesArray[i]);
                }
            }

            return filteredMatchesList;
        }

        private float GetMaxPossibleDist()
        {
            //return (ms_MIN_DIST + ((ms_MAX_DIST - ms_MIN_DIST)*0.5F));
            return ((ms_MAX_DIST)*0.95f);
        }
        
        private void FindMinMaxDistInMatches(MDMatch[][] matchesArray, ref float ms_MAX_DIST, ref float ms_MIN_DIST)
        {
            for (int i = 0; i < matchesArray.Length; i++)
            {
                if (matchesArray[i].Length == 0)
                    continue;

                MDMatch first = matchesArray[i][0];
                float dist1 = matchesArray[i][0].Distance;

                if (ms_MAX_DIST < dist1)
                    ms_MAX_DIST = dist1;

                if (ms_MIN_DIST > dist1)
                    ms_MIN_DIST = dist1;
            }
        }

        private void ComputeDescriptor(KeyPoint keypoint, IFeatureDescriptor descriptor, bool AddToList=true, bool SaveOnDisk=true)
        {
            var computedDescriptor = descriptor.ComputeDescriptor(keypoint);
            var descriptorNode = new Descriptor()
                {
                    Descriptors = computedDescriptor,
                    KeyPoint = keypoint
                };

            if (AddToList)
                ComputedDescriptors.Add(descriptorNode);

            if (SaveOnDisk)
                SaveSiftFile(descriptorNode);
        }

        private void SaveSiftFile(Descriptor Descriptor, bool SaveInTempDirectory= true, bool SaveInDescriptorNode = true )
        {
            var descriptor = Descriptor.Descriptors;
            var keyPoints = Descriptor.KeyPoint.DetectedKeyPoints;
            var fileName = $"{Path.GetFileNameWithoutExtension(Descriptor.KeyPoint.InputFile.fileInfo.Name)}.SIFT";

            var countKeypoint = keyPoints.Size;
            var countDescriptor = descriptor.Cols;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{countKeypoint} {countDescriptor}");

            for (int i = 0; i < descriptor.Rows; i++)
            {
                // X a Y su prehodene, teraz je to dobre
                sb.AppendLine($"{keyPoints[i].Point.Y} {keyPoints[i].Point.X} {keyPoints[i].Size} {keyPoints[i].Angle}");

                for (int j = 0; j < descriptor.Cols; j++)
                    sb.Append($"{descriptor.GetValue(i, j)} ");
                sb.AppendLine();
            }

            if(SaveInTempDirectory)
            File.WriteAllText(Path.Combine(tempDirectory, fileName), sb.ToString());

            if (SaveInDescriptorNode)
                Descriptor.FileFormatSIFT = sb.ToString();
        }

        private void FindKeypoint(InputFile inputFile, IFeatureDetector detector, bool AddToList=true)
        {
            var detectedKeyPoints = detector.DetectKeyPoints(new Mat(inputFile.fileInfo.FullName));
            
            if (AddToList)
                DetectedKeyPoints.Add(new KeyPoint()
                {
                    DetectedKeyPoints = new VectorOfKeyPoint(detectedKeyPoints),
                    InputFile=inputFile
                }
                );
        }

        public Mat FindHomography(VectorOfKeyPoint keypointsModel, VectorOfKeyPoint keypointsTest, List<MDMatch[]> matches, Mat Mask)
        {
            MKeyPoint[] kptsModel = keypointsModel.ToArray();
            MKeyPoint[] kptsTest = keypointsTest.ToArray();

            PointF[] srcPoints = new PointF[matches.Count];
            PointF[] destPoints = new PointF[matches.Count];

            for (int i = 0; i < matches.Count; i++)
            {
                srcPoints[i] = kptsModel[matches[i][0].TrainIdx].Point;
                destPoints[i] = kptsTest[matches[i][0].QueryIdx].Point;
            }

            Mat homography = CvInvoke.FindHomography(srcPoints, destPoints, Emgu.CV.CvEnum.HomographyMethod.Ransac, 10, Mask);
            
            return homography;
        }







        public void RunVisualSFM()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(Path.Combine(pathVisualSFM, "VisualSFM.exe"));
            startInfo.Arguments = $"sfm+import {tempDirectory} {Path.Combine(tempDirectory, "Result.nvm")} {Path.Combine(tempDirectory, "AllFoundedMatches.txt")}";
            Process process = Process.Start(startInfo);

            displayManager.LeftViewItem = Enumerate.EDisplayItem.PointCloud;
            displayManager.Display();
        }

        //
        //
        //
        // Funkcne riesenie pre ukazku
        //
        //
        //

        //public void FindAndMatch() {
        //    var orb = new ORBDetector(200000);

        //    // Third
        //    var imageThird = new Mat(Path.Combine(path, $"{rightImageName}.JPG"));
        //    var ThirdKeyPoints = new VectorOfKeyPoint();
        //    var ThirdDescriptor = new Mat();

        //    var imageLeft = new Mat(Path.Combine(path, $"{leftImageName}.JPG"));
        //    var imageRight = new Mat(Path.Combine(path, $"{centerImageName}.JPG"));

        //    var leftKeyPoints = new VectorOfKeyPoint();
        //    var rightKeyPoints = new VectorOfKeyPoint();
        //    var leftDescriptor = new Mat();
        //    var rightDescriptor = new Mat();

        //    orb.DetectAndCompute(imageLeft, null, leftKeyPoints, leftDescriptor, false);
        //    orb.DetectAndCompute(imageRight, null, rightKeyPoints, rightDescriptor, false);

        //    WriteSiftFile($"{leftImageName}.sift",leftKeyPoints,leftDescriptor,imageLeft);
        //    WriteSiftFile($"{centerImageName}.sift", rightKeyPoints, rightDescriptor,imageRight);



        //    orb.DetectAndCompute(imageThird, null, ThirdKeyPoints, ThirdDescriptor, false);
        //    WriteSiftFile($"{rightImageName}.sift", ThirdKeyPoints, ThirdDescriptor, imageThird);


        //    List<MDMatch[]> leftRight = ComputeMatches(leftDescriptor, rightDescriptor);
        //    List<MDMatch[]> leftThird = ComputeMatches(leftDescriptor, ThirdDescriptor);
        //    List<MDMatch[]> rightThird = ComputeMatches(rightDescriptor, ThirdDescriptor);


        //    WriteFilteredMatches(leftRight, $"{leftImageName}.JPG", $"{centerImageName}.JPG", "LeftRight",leftKeyPoints,rightKeyPoints);
        //    WriteFilteredMatches(leftThird, $"{leftImageName}.JPG", $"{rightImageName}.JPG", "LeftThird",leftKeyPoints,ThirdKeyPoints);
        //    WriteFilteredMatches(rightThird, $"{centerImageName}.JPG", $"{rightImageName}.JPG", "RightThird",rightKeyPoints,ThirdKeyPoints);

        //    var PerspectiveMatrix = new Mat();
        //    Mat Mask = new Mat();

        //    PerspectiveMatrix = FindHomography(leftKeyPoints, rightKeyPoints, leftRight, Mask);


        //    var drawMatches = new Mat();
        //    Features2DToolbox.DrawMatches(imageLeft, leftKeyPoints, imageRight, rightKeyPoints, new VectorOfVectorOfDMatch(leftRight.ToArray()), drawMatches, new MCvScalar(0, 0, 255, 128), new MCvScalar(0, 255, 0, 128), Mask, Features2DToolbox.KeypointDrawType.DrawRichKeypoints);
        //    drawMatches.Save(Path.Combine(path, "Pokus.jpg"));

        //}

        //private List<MDMatch[]> ComputeMatches(Mat leftDescriptor, Mat rightDescriptor)
        //{
        //    var matcher = new BFMatcher(DistanceType.Hamming2, true);
        //    var matches = new VectorOfVectorOfDMatch();
        //    matcher.Add(leftDescriptor);
        //    matcher.KnnMatch(rightDescriptor, matches, 1, null);



        //    MDMatch[][] matchesArray = matches.ToArrayOfArray();
        //    VectorOfVectorOfDMatch filteredMatches = new VectorOfVectorOfDMatch();
        //    List<MDMatch[]> filteredMatchesList = new List<MDMatch[]>();
        //    float ms_MIN_RATIO = 0.8F, ms_MAX_DIST = 0, ms_MIN_DIST = float.MaxValue;

        //    for (int i = 0; i < matchesArray.Length; i++)
        //    {
        //        if (matchesArray[i].Length == 0) continue;
        //        MDMatch first = matchesArray[i][0];
        //        float dist1 = matchesArray[i][0].Distance;

        //        if (ms_MAX_DIST < dist1) ms_MAX_DIST = dist1;
        //        if (ms_MIN_DIST > dist1) ms_MIN_DIST = dist1;
        //        filteredMatchesList.Add(matchesArray[i]);
        //    }


        //    //Filter by threshold
        //    MDMatch[][] defCopy = new MDMatch[filteredMatchesList.Count][];
        //    filteredMatchesList.CopyTo(defCopy);
        //    filteredMatchesList = new List<MDMatch[]>();

        //    foreach (var item in defCopy)
        //    {
        //        if (item[0].Distance < (((ms_MAX_DIST + ms_MIN_DIST) / 2) + (ms_MAX_DIST / 2)))
        //        {
        //            filteredMatchesList.Add(item);
        //        }
        //    }

        //    filteredMatches = new VectorOfVectorOfDMatch(filteredMatchesList.ToArray());
        //    return filteredMatchesList;
        //}

        //private void WriteFilteredMatches(List<MDMatch[]> matches, string leftName, string rightName, string matchesName, VectorOfKeyPoint leftKeyPoint, VectorOfKeyPoint rightKeypoint)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendLine($"{path}\\{leftName} {path}\\{rightName} {matches.Count}");
        //    foreach (var match in matches)
        //        sb.Append($"{match[0].TrainIdx} ");
        //    sb.AppendLine();
        //    foreach (var match in matches)
        //        sb.Append($"{match[0].QueryIdx} ");
        //    File.WriteAllText(Path.Combine(path, $"{matchesName}.txt"), sb.ToString());

        //    StringBuilder sx = new StringBuilder();
        //    sx.AppendLine($"0 {path}\\{leftName}\n0 {path}\\{rightName}\n{matches.Count}");
        //    foreach (var match in matches)
        //        sx.AppendLine($"{match[0].TrainIdx} {leftKeyPoint[match[0].TrainIdx].Point.Y} {leftKeyPoint[match[0].TrainIdx].Point.X} {match[0].QueryIdx} {leftKeyPoint[match[0].QueryIdx].Point.Y} {leftKeyPoint[match[0].QueryIdx].Point.Y} ");
        //    sx.AppendLine();
        //    File.WriteAllText(Path.Combine(path, $"{matchesName}_F-Matrix.txt"), sx.ToString());


        //}

        //private void WriteSiftFile(string v, VectorOfKeyPoint keyPoints, Mat descriptor, Mat Image)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendLine($"{keyPoints.Size} 32");
        //    for (int i = 0; i < descriptor.Rows; i++)
        //    {
        //        // X a Y su prehodene, teraz je to dobre
        //        sb.AppendLine($"{keyPoints[i].Point.Y} {keyPoints[i].Point.X} {keyPoints[i].Size} {keyPoints[i].Angle}");
        //        for (int j = 0; j < descriptor.Cols; j++)
        //            sb.Append($"{descriptor.GetValue(i, j)} ");
        //        sb.AppendLine();
        //    }
        //    File.WriteAllText(Path.Combine(path, v),sb.ToString());

        //    Mat OutputImage = new Mat();
        //    Features2DToolbox.DrawKeypoints(Image, keyPoints, OutputImage, new Bgr(0, 0, 255), Features2DToolbox.KeypointDrawType.DrawRichKeypoints);
        //    OutputImage.Save(Path.Combine(path, $"{v.Split('.')[0]}X.JPG"));
        //}
        

        
    }
}
