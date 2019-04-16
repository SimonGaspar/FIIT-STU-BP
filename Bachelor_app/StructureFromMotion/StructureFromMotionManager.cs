﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bachelor_app;
using Bachelor_app.Enumerate;
using Bachelor_app.Manager;
using Bachelor_app.Tools;
using Bakalárska_práca.Extension;
using Bakalárska_práca.Helper;
using Bakalárska_práca.Manager;
using Bakalárska_práca.Model;
using Bakalárska_práca.StructureFromMotion;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace Bakalárska_práca
{
    public class SfM
    {
        const string pathVisualSFM = @"C:\Users\Notebook\Desktop\VisualSFM_windows_cuda_64bit";

        public IFeatureDetector _detector;
        public IFeatureDescriptor _descriptor;
        public IFeatureMatcher _matcher;
        public EMatchingType _matchingType;
        public bool _useParallel = false;
        float ms_MAX_DIST = 0;
        float ms_MIN_DIST = float.MaxValue;

        private SortedList<int, KeyPointModel> DetectedKeyPoints;
        private SortedList<int, DescriptorModel> ComputedDescriptors;
        private List<MatchModel> FoundedMatches;

        private FileManager fileManager;
        private DisplayManager displayManager;
        private MainForm _winForm;
        private CameraManager cameraManager;
        public bool stopSFM = false;
        private static object locker = new object();

        public SfM(FileManager fileManager, DisplayManager displayManager, MainForm winForm, CameraManager cameraManager)
        {
            DetectedKeyPoints = new SortedList<int, KeyPointModel>();
            ComputedDescriptors = new SortedList<int, DescriptorModel>();
            FoundedMatches = new List<MatchModel>();

            this.fileManager = fileManager;
            this.displayManager = displayManager;
            this._winForm = winForm;
            this.cameraManager = cameraManager;
        }

        public void StartSFM(bool ContinueSFM = false)
        {
            stopSFM = false;
            List<InputFileModel> listOfInput = new List<InputFileModel>();
            var countInputFile = DetectedKeyPoints.Count;
            var startSFMFrom = 0;
            var iterMatches = 0;

            if (ContinueSFM)
            {
                startSFMFrom = countInputFile;
                iterMatches = FoundedMatches.Count;
            }
            else
            {
                Configuration.DeleteTempFolder();
                ClearList();
            }

            switch (fileManager._inputType)
            {
                case EInput.ListView:
                    listOfInput = GetListFromListView(ContinueSFM);
                    ComputeSfM(startSFMFrom, listOfInput);
                    break;
                case EInput.ConnectedStereoCamera:
                    while (!stopSFM)
                    {
                        var cameraStereoOutput = cameraManager.GetInputFromStereoCamera(countInputFile);
                        listOfInput.AddRange(cameraStereoOutput);

                        ComputeSfM(countInputFile++, cameraStereoOutput);
                    }
                    break;
            }

            if (ContinueSFM)
                WriteAddedImages(listOfInput);

            WriteAllMatches(FoundedMatches, iterMatches);
            ToolHelper.RunVisualSFM(ContinueSFM);
        }

        public void ComputeSfM(int startIndex, List<InputFileModel> inputImages)
        {
            StartDetectingKeyPoint(startIndex, inputImages, _detector);
            StartComputingDescriptor(startIndex, _descriptor);
            StartMatching(startIndex, _matcher);
        }

        private List<InputFileModel> GetListFromListView(bool ContinueSFM)
        {
            var list = ContinueSFM ? fileManager.listViewerModel.BasicStack.Where(x => x.UseInSFM == false).ToList() : fileManager.listViewerModel.BasicStack;
            foreach (var node in list)
            {
                File.Copy(node.fileInfo.FullName, Path.Combine(Configuration.TempDirectoryPath, node.fileInfo.Name), true);
                node.UseInSFM = true;
            }
            return list;
        }

        private void ClearList()
        {
            DetectedKeyPoints.Clear();
            ComputedDescriptors.Clear();
            FoundedMatches.Clear();
        }

        private void StartStereoMatching(int countOfExistedKeypoint, IFeatureMatcher matcher)
        {
            if (_useParallel)
                StartStereoMatchingParallel(countOfExistedKeypoint, matcher);
            else
                StartStereoMatchingSequence(countOfExistedKeypoint, matcher);
        }

        private void StartStereoMatchingSequence(int countOfExistedKeypoint, IFeatureMatcher matcher)
        {
            //FindMatches(matcher, ComputedDescriptors[countOfExistedKeypoint - 2], ComputedDescriptors[countOfExistedKeypoint - 1]);
            int startMatchingFromPrevious;

            switch (_matchingType)
            {
                case EMatchingType.OnePrevious:
                    startMatchingFromPrevious = 1;
                    MatchingSequencePrevious(countOfExistedKeypoint, startMatchingFromPrevious, 2, matcher);
                    break;
                case EMatchingType.TwoPrevious:
                    startMatchingFromPrevious = 2;
                    MatchingSequencePrevious(countOfExistedKeypoint, startMatchingFromPrevious, 2, matcher);
                    break;
                case EMatchingType.AllWithAll:
                    throw new NotImplementedException();
                    for (int m = countOfExistedKeypoint; m < ComputedDescriptors.Count; m++)
                        for (int n = m + 1; n < ComputedDescriptors.Count; n++)
                            FindMatches(matcher, ComputedDescriptors[m], ComputedDescriptors[n]);
                    break;
            }
        }

        private void StartStereoMatchingParallel(int countOfExistedKeypoint, IFeatureMatcher matcher)
        {
            //FindMatches(matcher, ComputedDescriptors[countOfExistedKeypoint - 2], ComputedDescriptors[countOfExistedKeypoint - 1]);
            int startMatchingFromPrevious = 0;

            switch (_matchingType)
            {
                case EMatchingType.OnePrevious:
                    startMatchingFromPrevious = 1;
                    MatchingStereoParallelPrevious(countOfExistedKeypoint, startMatchingFromPrevious, matcher);
                    break;
                case EMatchingType.TwoPrevious:
                    startMatchingFromPrevious = 2;
                    MatchingStereoParallelPrevious(countOfExistedKeypoint, startMatchingFromPrevious, matcher);
                    break;
                case EMatchingType.AllWithAll:
                    throw new NotImplementedException();
                    Parallel.For(countOfExistedKeypoint, ComputedDescriptors.Count, index =>
                    {
                        Parallel.For(index + 1, ComputedDescriptors.Count, i =>
                        {
                            FindMatches(matcher, ComputedDescriptors[index], ComputedDescriptors[i]);
                        });
                    });
                    break;
            }
        }

        private void StartMatching(int countOfExistedKeypoint, IFeatureMatcher matcher)
        {
            if (_useParallel)
                StartParallelMatching(countOfExistedKeypoint, matcher);
            else
                StartMatchingSequence(countOfExistedKeypoint, matcher);
        }

        private void StartMatchingSequence(int countOfExistedKeypoint, IFeatureMatcher matcher)
        {
            int startMatchingFromPrevious = 0;

            switch (_matchingType)
            {
                case EMatchingType.OnePrevious:
                    startMatchingFromPrevious = 1;
                    MatchingSequencePrevious(countOfExistedKeypoint, startMatchingFromPrevious, 1, matcher);
                    break;
                case EMatchingType.TwoPrevious:
                    startMatchingFromPrevious = 2;
                    MatchingSequencePrevious(countOfExistedKeypoint, startMatchingFromPrevious, 1, matcher);
                    break;
                case EMatchingType.AllWithAll:
                    throw new NotImplementedException();
                    for (int m = countOfExistedKeypoint; m < ComputedDescriptors.Count; m++)
                        for (int n = m + 1; n < ComputedDescriptors.Count; n++)
                            FindMatches(matcher, ComputedDescriptors[m], ComputedDescriptors[n]);
                    break;
            }
        }

        private void MatchingSequencePrevious(int startMatchingFrom, int startMatchingFromPrevious, int iteration, IFeatureMatcher matcher)
        {
            for (int m = startMatchingFrom; m < ComputedDescriptors.Count; m += iteration)
                for (int n = m - startMatchingFromPrevious; n < m && n >= 0; n += iteration)
                {
                    FindMatches(matcher, ComputedDescriptors[m], ComputedDescriptors[n]);
                }
        }

        private void MatchingParallelPrevious(int startMatchingFrom, int startMatchingFromPrevious, IFeatureMatcher matcher)
        {
            Parallel.For(startMatchingFrom, ComputedDescriptors.Count, index =>
            {
                if (index >= startMatchingFromPrevious)
                {
                    Parallel.For(index - startMatchingFromPrevious, index, i =>
                    {
                        FindMatches(matcher, ComputedDescriptors[index], ComputedDescriptors[i]);
                    });
                }
            });
        }

        private void MatchingStereoParallelPrevious(int startMatchingFrom, int startMatchingFromPrevious, IFeatureMatcher matcher)
        {
            Parallel.For(startMatchingFrom, ComputedDescriptors.Count, index =>
            {
                if (index >= startMatchingFromPrevious * 2)
                {
                    Parallel.For(index - (startMatchingFromPrevious * 2), index, i =>
                    {
                        if ((index - i) % 2 == 0)
                            FindMatches(matcher, ComputedDescriptors[index], ComputedDescriptors[i]);
                    });
                }
            });
        }

        private void StartParallelMatching(int countOfExistedKeypoint, IFeatureMatcher matcher)
        {
            int startMatchingFromPrevious = 0;
            switch (_matchingType)
            {
                case EMatchingType.OnePrevious:
                    startMatchingFromPrevious = 1;
                    MatchingParallelPrevious(countOfExistedKeypoint, startMatchingFromPrevious, matcher);
                    break;
                case EMatchingType.TwoPrevious:
                    startMatchingFromPrevious = 2;
                    MatchingParallelPrevious(countOfExistedKeypoint, startMatchingFromPrevious, matcher);
                    break;
                case EMatchingType.AllWithAll:
                    throw new NotImplementedException();
                    Parallel.For(countOfExistedKeypoint, ComputedDescriptors.Count, index =>
                    {
                        Parallel.For(index + 1, ComputedDescriptors.Count, i =>
                         {
                             FindMatches(matcher, ComputedDescriptors[index], ComputedDescriptors[i]);
                         });
                    });
                    break;
            }
        }

        private void StartDetectingKeyPoint(int countOfInput, List<InputFileModel> listOfInput, IFeatureDetector detector)
        {
            if (_useParallel)
                Parallel.For(0, listOfInput.Count, x => { FindKeypoint(countOfInput + x, listOfInput[x], detector); });
            else
                for (int i = 0; i < listOfInput.Count; i++)
                    FindKeypoint(countOfInput + i, listOfInput[i], detector);
        }

        private void StartComputingDescriptor(int countOfExistedKeyPoint, IFeatureDescriptor descriptor)
        {
            if (_useParallel)
                Parallel.For(countOfExistedKeyPoint, DetectedKeyPoints.Count, x => ComputeDescriptor(DetectedKeyPoints[x], descriptor));
            else
                for (int i = countOfExistedKeyPoint; i < DetectedKeyPoints.Count; i++)
                    ComputeDescriptor(DetectedKeyPoints[i], descriptor);
        }

        private void WriteAddedImages(List<InputFileModel> listOfInput)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var node in listOfInput)
            {
                sb.AppendLine(Path.Combine(Configuration.TempDirectoryPath, node.fileInfo.Name));
            }

            File.WriteAllText(Path.Combine(Configuration.TempDirectoryPath, "Result.nvm.txt"), sb.ToString());
        }

        private void WriteAllMatches(List<MatchModel> findedMatches, int index = 0)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var node in findedMatches.Skip(index))
            {
                sb.AppendLine(node.FileFormatMatch);
                sb.AppendLine();
            }

            File.WriteAllText(Configuration.MatchFilePath, sb.ToString());
        }

        private void FindMatches(IFeatureMatcher matcher, DescriptorModel leftDescriptor, DescriptorModel rightDescriptor, bool AddToList = true, bool FilterMatches = true, bool ComputeHomography = true, bool SaveInMatchNode = true, bool DrawAndSave = true)
        {
            WindowsFormHelper.AddLogToConsole($"Start computing matches for: \n" +
                    $"\t{leftDescriptor.KeyPoint.InputFile.fileInfo.Name.ToString()}\n" +
                    $"\t{rightDescriptor.KeyPoint.InputFile.fileInfo.Name.ToString()}\n");


            var foundedMatch = new MatchModel()
            {
                FilteredMatch = FilterMatches,
                LeftDescriptor = leftDescriptor,
                RightDescriptor = rightDescriptor
            };

            var matches = new VectorOfVectorOfDMatch();

            matcher.Match(leftDescriptor.Descriptors, rightDescriptor.Descriptors, matches);

            WindowsFormHelper.AddLogToConsole(
                $"FINISH computing matches for: \n" +
                $"\t{leftDescriptor.KeyPoint.InputFile.fileInfo.Name.ToString()}\n" +
                $"\t{rightDescriptor.KeyPoint.InputFile.fileInfo.Name.ToString()}\n"
                );


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
                    var matchesForHomography = FilterMatches ? foundedMatch.FilteredMatchesList : foundedMatch.MatchesList;
                    if (matchesForHomography.Count > 0)
                    {
                        PerspectiveMatrix = FindHomography(leftDescriptor.KeyPoint.DetectedKeyPoints, rightDescriptor.KeyPoint.DetectedKeyPoints, FilterMatches ? foundedMatch.FilteredMatchesList : foundedMatch.MatchesList, Mask);
                        foundedMatch.Mask = Mask;
                        foundedMatch.PerspectiveMatrix = PerspectiveMatrix;
                    }
                }


            }

            if (DrawAndSave)
                foundedMatch.DrawAndSave(fileManager);

            if (SaveInMatchNode)
                SaveMatchString(foundedMatch, true);

            if (AddToList)
                FoundedMatches.Add(foundedMatch);
        }

        private int SaveMatchString(MatchModel descriptorsMatch, bool UseMask)
        {
            var leftImageName = descriptorsMatch.LeftDescriptor.KeyPoint.InputFile.fileInfo.Name;
            var rightImageName = descriptorsMatch.RightDescriptor.KeyPoint.InputFile.fileInfo.Name;

            var matchesList = descriptorsMatch.FilteredMatch ? descriptorsMatch.FilteredMatchesList : descriptorsMatch.MatchesList;

            if (descriptorsMatch.Mask == null || descriptorsMatch.FilteredMatchesList.Count == 0)
            {
                descriptorsMatch.FileFormatMatch = null;
                return 0;
            }

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
            sb.AppendLine(Path.Combine(Path.GetFullPath(Configuration.TempDirectoryPath), leftImageName));
            sb.AppendLine(Path.Combine(Path.GetFullPath(Configuration.TempDirectoryPath), rightImageName));
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
            return 0;
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
            return (ms_MAX_DIST * 0.95f);
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
            WindowsFormHelper.AddLogToConsole($"Start computing descriptor for: {keypoint.InputFile.fileInfo.Name.ToString()}\n");

            var computedDescriptor = descriptor.ComputeDescriptor(keypoint);
            var descriptorNode = new DescriptorModel()
            {
                Descriptors = computedDescriptor,
                KeyPoint = keypoint
            };
            WindowsFormHelper.AddLogToConsole($"FINISH computing descriptor for: {keypoint.InputFile.fileInfo.Name.ToString()}\n");


            if (AddToList)
                ComputedDescriptors.Add(keypoint.ID, descriptorNode);

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
                Descriptor.FileFormatSIFT = sb.ToString();
        }

        private void FindKeypoint(int ID, InputFileModel inputFile, IFeatureDetector detector, bool AddToList = true, bool DrawAndSave = true)
        {
            WindowsFormHelper.AddLogToConsole($"Start finding key points for: {inputFile.fileInfo.Name.ToString()}\n");

            var detectedKeyPoints = detector.DetectKeyPoints(new Mat(inputFile.fileInfo.FullName));

            WindowsFormHelper.AddLogToConsole(
                $"FINISH finding key points for: {inputFile.fileInfo.Name.ToString()}\n" +
                $"Count of key points: {detectedKeyPoints.Length}\n");

            var newItem = new KeyPointModel()
            {
                DetectedKeyPoints = new VectorOfKeyPoint(detectedKeyPoints),
                InputFile = inputFile,
                ID = ID
            };

            if (AddToList)
                DetectedKeyPoints.Add(ID, newItem);

            if (DrawAndSave)
                newItem.DrawAndSave(fileManager);
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
    }
}
