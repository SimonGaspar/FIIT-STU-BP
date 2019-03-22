﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bakalárska_práca.Extension;
using Bakalárska_práca.Manager;
using Bakalárska_práca.Model;
using Bakalárska_práca.StructureFromMotion;
using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace Bakalárska_práca
{
    public class SfM
    {
        const string path = @"C:\Users\Notebook\Desktop\VisualSFM_windows_cuda_64bit\Example\images";
        const string pathVisualSFM = @"C:\Users\Notebook\Desktop\VisualSFM_windows_cuda_64bit";

        string tempDirectory = Path.GetFullPath($"..\\..\\..\\Temp");
        string matchFileName = $"AllFoundedMatches.txt";

        public IFeatureDetector _detector;
        public IFeatureDescriptor _descriptor;
        public IFeatureMatcher _matcher;
        float ms_MAX_DIST = 0, ms_MIN_DIST = float.MaxValue;

        private List<KeyPointModel> DetectedKeyPoints;
        private List<DescriptorModel> ComputedDescriptors;
        private List<DescriptorsMatchModel> FoundedMatches;

        private FileManager fileManager;
        private DisplayManager displayManager;

        private static object locker = new object();

        public SfM(FileManager fileManager, DisplayManager displayManager)
        {

            Directory.CreateDirectory(tempDirectory);
            DetectedKeyPoints = new List<KeyPointModel>();
            ComputedDescriptors = new List<DescriptorModel>();
            FoundedMatches = new List<DescriptorsMatchModel>();

            this.fileManager = fileManager;
            this.displayManager = displayManager;
        }

        //public void StartSFM()
        //{
        //    var list = fileManager.listViewerModel.BasicStack;

        //    foreach (var node in list)
        //    {
        //        File.Copy(node.fileInfo.FullName, Path.Combine(tempDirectory, node.fileInfo.Name), true);
        //    }

        //    ComputeSfM(new OrientedFastAndRotatedBrief(), new OrientedFastAndRotatedBrief(), new BruteForce(), list);
        //}

        public void StartSFM()
        {
            var list = fileManager.listViewerModel.BasicStack;

            foreach (var node in list)
            {
                File.Copy(node.fileInfo.FullName, Path.Combine(tempDirectory, node.fileInfo.Name), true);
            }

            ComputeSfM(_detector, _descriptor, _matcher, list);
        }

        public void ComputeSfM(IFeatureDetector detector, IFeatureDescriptor descriptor, IFeatureMatcher matcher, List<InputFileModel> listOfInput)
        {
            //foreach (var item in listOfInput)
            //{
            //    FindKeypoint(item, detector);
            //}

            //foreach (var item in DetectedKeyPoints)
            //{
            //    ComputeDescriptor(item, descriptor);
            //}

            //for (int m = 0; m < ComputedDescriptors.Count; m++)
            //    for (int n = m + 1; n < ComputedDescriptors.Count; n++)
            //        FindMatches(matcher, ComputedDescriptors[m], ComputedDescriptors[n]);

            //for (int m = 2; m < ComputedDescriptors.Count; m++)
            //    for (int n = m - 2; n < m && n >= 0; n++)
            //        FindMatches(matcher, ComputedDescriptors[m], ComputedDescriptors[n]);


            Parallel.ForEach(listOfInput, x => FindKeypoint(x, detector));
            Parallel.ForEach(DetectedKeyPoints, x => ComputeDescriptor(x, descriptor));

            for (int m = 2; m < ComputedDescriptors.Count; m++)
                Parallel.For(m - 2, m, index =>
                {
                    FindMatches(matcher, ComputedDescriptors[m], ComputedDescriptors[index]);
                });

            WriteAllMatches(FoundedMatches);
            RunVisualSFM();
        }

        private void WriteAllMatches(List<DescriptorsMatchModel> findedMatches)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var node in findedMatches)
            {
                sb.AppendLine(node.FileFormatMatch);
                sb.AppendLine();
            }

            File.WriteAllText(Path.Combine(tempDirectory, matchFileName), sb.ToString());
        }

        private void FindMatches(IFeatureMatcher matcher, DescriptorModel leftDescriptor, DescriptorModel rightDescriptor, bool AddToList = true, bool FilterMatches = true, bool ComputeHomography = true, bool SaveInMatchNode = true)
        {
            var foundedMatch = new DescriptorsMatchModel()
            {
                FilteredMatch = FilterMatches,
                LeftDescriptor = leftDescriptor,
                RightDescriptor = rightDescriptor
            };

            var matches = new VectorOfVectorOfDMatch();
            lock (locker)
            {
                matcher.Add(leftDescriptor.Descriptors);
                matcher.Match(rightDescriptor.Descriptors, matches);
            }
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

                lock (locker)
                {
                    PerspectiveMatrix = FindHomography(leftDescriptor.KeyPoint.DetectedKeyPoints, rightDescriptor.KeyPoint.DetectedKeyPoints, FilterMatches ? foundedMatch.FilteredMatchesList : foundedMatch.MatchesList, Mask);
                }

                foundedMatch.Mask = Mask;
                foundedMatch.PerspectiveMatrix = PerspectiveMatrix;
            }

            // Save drawing image
            lock (locker)
            {
                Mat output = new Mat();
                Directory.CreateDirectory($@"{tempDirectory}\DrawMatches");
                Features2DToolbox.DrawMatches(new Mat(foundedMatch.LeftDescriptor.KeyPoint.InputFile.fileInfo.FullName), foundedMatch.LeftDescriptor.KeyPoint.DetectedKeyPoints, new Mat(foundedMatch.RightDescriptor.KeyPoint.InputFile.fileInfo.FullName), foundedMatch.RightDescriptor.KeyPoint.DetectedKeyPoints, new VectorOfVectorOfDMatch(foundedMatch.FilteredMatchesList.ToArray()), output, new MCvScalar(0, 0, 255), new MCvScalar(0, 255, 0), foundedMatch.Mask);
                output.Save(Path.Combine($@"{tempDirectory}\DrawMatches", $"{Path.GetFileNameWithoutExtension(foundedMatch.LeftDescriptor.KeyPoint.InputFile.fileInfo.Name)}{Path.GetFileNameWithoutExtension(foundedMatch.RightDescriptor.KeyPoint.InputFile.fileInfo.Name)}.JPG"));
            }

            if (SaveInMatchNode)
                SaveMatchString(foundedMatch, true);

            if (AddToList)
                FoundedMatches.Add(foundedMatch);
        }

        private void SaveMatchString(DescriptorsMatchModel descriptorsMatch, bool UseMask)
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
            sb.AppendLine(Path.Combine(Path.GetFullPath(tempDirectory), leftImageName));
            sb.AppendLine(Path.Combine(Path.GetFullPath(tempDirectory), rightImageName));
            sb.AppendLine($"{(UseMask ? countMaskMatches : matchesList.Count)}");
            for (int m = 0; m < matchesList.Count; m++)
            {
                if (!UseMask || descriptorsMatch.Mask.GetValue(0, m) > 0)
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
                if (matchesArray[i].Length == 0) continue;

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
            return ((ms_MAX_DIST) * 0.95f);
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

        private void ComputeDescriptor(KeyPointModel keypoint, IFeatureDescriptor descriptor, bool AddToList = true, bool SaveOnDisk = true)
        {
            var computedDescriptor = descriptor.ComputeDescriptor(keypoint);
            var descriptorNode = new DescriptorModel()
            {
                Descriptors = computedDescriptor,
                KeyPoint = keypoint
            };

            if (AddToList)
                ComputedDescriptors.Add(descriptorNode);

            if (SaveOnDisk)
                SaveSiftFile(descriptorNode);
        }

        private void SaveSiftFile(DescriptorModel Descriptor, bool SaveInTempDirectory = true, bool SaveInDescriptorNode = true)
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

            if (SaveInTempDirectory)
                File.WriteAllText(Path.Combine(tempDirectory, fileName), sb.ToString());

            if (SaveInDescriptorNode)
                Descriptor.FileFormatSIFT = sb.ToString();
        }

        private void FindKeypoint(InputFileModel inputFile, IFeatureDetector detector, bool AddToList = true)
        {
            var detectedKeyPoints = detector.DetectKeyPoints(new Mat(inputFile.fileInfo.FullName));

            if (AddToList)
                DetectedKeyPoints.Add(new KeyPointModel()
                {
                    DetectedKeyPoints = new VectorOfKeyPoint(detectedKeyPoints),
                    InputFile = inputFile
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

            displayManager.LeftViewWindowItem = Enumerate.EDisplayItem.PointCloud;
            displayManager.Display();
        }

        public void ContinueVisualStudio()
        {
            // Poriesit, ako prepisat SIFT subory, pridat novy match file a pridat fiel Result.nvm.txt, ktory bude obsahovat nove obrazky
            ProcessStartInfo startInfo = new ProcessStartInfo(Path.Combine(pathVisualSFM, "VisualSFM.exe"));
            startInfo.Arguments = $"sfm+import+resume {Path.Combine(tempDirectory, "Result.nvm")} {Path.Combine(tempDirectory, "Result.nvm")} {Path.Combine(tempDirectory, "AllFoundedMatches.txt")}";
            Process process = Process.Start(startInfo);
            // Poriesit ako citat point cloud, ked sa prepise
            displayManager.LeftViewWindowItem = Enumerate.EDisplayItem.PointCloud;
            displayManager.Display();
        }
    }
}
