using Bachelor_app.Enumerate;
using Bachelor_app.Helper;
using Bachelor_app.Manager;
using Bachelor_app.Model;
using Bachelor_app.StructureFromMotion;
using Bachelor_app.StructureFromMotion.FeatureMatcher;
using Bachelor_app.Tools;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bachelor_app
{
    public class SfM
    {
        public IFeatureDetector Detector { get; set; }
        public IFeatureDescriptor Descriptor { get; set; }
        public IFeatureMatcher Matcher { get; set; }
        public EMatchingType MatchingType { get; set; }
        public bool UseParallel { get; set; } = false;
        public bool StopSFM { get; set; } = false;

        private SortedList<int, KeyPointModel> DetectedKeyPoints;
        private SortedList<int, DescriptorModel> ComputedDescriptors;
        private List<MatchModel> FoundedMatches;

        private FileManager fileManager;
        private CameraManager cameraManager;

        private float ms_MAX_DIST;
        private float ms_MIN_DIST;
        private int countMatches = 0;
        //private static object locker = new object();
        
        public SfM(FileManager fileManager, CameraManager cameraManager)
        {
            DetectedKeyPoints = new SortedList<int, KeyPointModel>();
            ComputedDescriptors = new SortedList<int, DescriptorModel>();
            FoundedMatches = new List<MatchModel>();

            this.fileManager = fileManager;
            this.cameraManager = cameraManager;
        }

        public void StartSFM(bool ContinueSFM = false)
        {
            StopSFM = false;
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
                ClearList();
                countMatches = 0;
            }

            GC.Collect();

            switch (fileManager._inputType)
            {
                case EInput.ListViewBasicStack:
                    listOfInput = GetListFromListView(ContinueSFM);
                    ComputeSfM(startSFMFrom, listOfInput);
                    break;
                case EInput.ConnectedStereoCamera:
                    while (!StopSFM)
                    {
                        var cameraStereoOutput = cameraManager.GetInputFromStereoCamera(true, countInputFile++);
                        listOfInput.AddRange(cameraStereoOutput);

                        ComputeSfM(DetectedKeyPoints.Count, cameraStereoOutput);
                    }
                    break;
                case EInput.ConnectedRightCamera:
                    while (!StopSFM)
                    {
                        var cameraOutput = cameraManager.GetInputFromCamera(cameraManager.LeftCamera.Camera, countInputFile++);
                        listOfInput.AddRange(cameraOutput);

                        ComputeSfM(DetectedKeyPoints.Count, cameraOutput);
                    }
                    break;
                case EInput.ConnectedLeftCamera:
                    while (!StopSFM)
                    {
                        var cameraOutput = cameraManager.GetInputFromCamera(cameraManager.RightCamera.Camera, countInputFile++);
                        listOfInput.AddRange(cameraOutput);

                        ComputeSfM(DetectedKeyPoints.Count, cameraOutput);
                    }
                    break;
            }
            GC.Collect();

            if (ContinueSFM)
                WriteAddedImages(listOfInput);

            WriteAllMatches(FoundedMatches, iterMatches);
            ToolHelper.RunVisualSFM(ContinueSFM);
        }

        public void ComputeSfM(int startIndex, List<InputFileModel> inputImages)
        {
            GC.Collect();
            StartDetectingKeyPoint(startIndex, inputImages, Detector);
            GC.Collect();
            StartComputingDescriptor(startIndex, Descriptor);
            GC.Collect();
            StartMatching(startIndex, Matcher);
            GC.Collect();
        }

        private List<InputFileModel> GetListFromListView(bool ContinueSFM)
        {
            var list = ContinueSFM ? fileManager.ListViewModel.BasicStack.Where(x => x.UseInSFM == false).ToList() : fileManager.ListViewModel.BasicStack;
            foreach (var node in list)
            {
                var savePath = Path.Combine(Configuration.TempDirectoryPath, node.FileName);
                File.Copy(node.FullPath, savePath, true);
                node.SetFileInfo(new FileInfo(savePath));
                node.UseInSFM = true;
            }
            GC.Collect();
            return list;
        }

        private void ClearList()
        {
            DetectedKeyPoints.Clear();
            ComputedDescriptors.Clear();
            FoundedMatches.Clear();
            GC.Collect();
        }

        private void StartStereoMatching(int countOfExistedKeypoint, IFeatureMatcher matcher)
        {
            if (UseParallel)
                StartStereoMatchingParallel(countOfExistedKeypoint, matcher);
            else
                StartStereoMatchingSequence(countOfExistedKeypoint, matcher);
        }

        private void StartStereoMatchingSequence(int countOfExistedKeypoint, IFeatureMatcher matcher)
        {
            FindMatches(matcher, ComputedDescriptors[countOfExistedKeypoint - 2], ComputedDescriptors[countOfExistedKeypoint - 1]);
            int startMatchingFromPrevious;

            switch (MatchingType)
            {
                case EMatchingType.OnePrevious:
                    startMatchingFromPrevious = 1;
                    MatchingSequencePrevious(countOfExistedKeypoint, startMatchingFromPrevious, 2, matcher);
                    break;
                case EMatchingType.TwoPrevious:
                    startMatchingFromPrevious = 2;
                    MatchingSequencePrevious(countOfExistedKeypoint, startMatchingFromPrevious, 2, matcher);
                    break;
                case EMatchingType.AllWithAllForward:
                    for (int m = countOfExistedKeypoint; m < ComputedDescriptors.Count; m++)
                        for (int n = m + 1; n < ComputedDescriptors.Count; n++)
                            FindMatches(matcher, ComputedDescriptors[m], ComputedDescriptors[n]);
                    break;
                case EMatchingType.AllWithAllBackward:
                    for (int m = countOfExistedKeypoint; m < ComputedDescriptors.Count; m++)
                        for (int n = 0; n < m; n++)
                            FindMatches(matcher, ComputedDescriptors[m], ComputedDescriptors[n]);
                    break;
                case EMatchingType.FullAllWithAll:
                    for (int m = countOfExistedKeypoint; m < ComputedDescriptors.Count; m++)
                        for (int n = 0; n < ComputedDescriptors.Count; n++)
                            if(m!=n)
                                FindMatches(matcher, ComputedDescriptors[m], ComputedDescriptors[n]);
                    break;
            }
        }

        private void StartStereoMatchingParallel(int countOfExistedKeypoint, IFeatureMatcher matcher)
        {
            FindMatches(matcher, ComputedDescriptors[countOfExistedKeypoint - 2], ComputedDescriptors[countOfExistedKeypoint - 1]);
            int startMatchingFromPrevious = 0;

            switch (MatchingType)
            {
                case EMatchingType.OnePrevious:
                    startMatchingFromPrevious = 1;
                    MatchingStereoParallelPrevious(countOfExistedKeypoint, startMatchingFromPrevious, matcher);
                    break;
                case EMatchingType.TwoPrevious:
                    startMatchingFromPrevious = 2;
                    MatchingStereoParallelPrevious(countOfExistedKeypoint, startMatchingFromPrevious, matcher);
                    break;
                case EMatchingType.AllWithAllForward:
                    for (int index = countOfExistedKeypoint; index < ComputedDescriptors.Count; index++)
                    //Parallel.For(countOfExistedKeypoint, ComputedDescriptors.Count, index =>
                    {
                        Parallel.For(index + 1, ComputedDescriptors.Count, i =>
                        {
                            FindMatches(matcher, ComputedDescriptors[index], ComputedDescriptors[i]);
                        });
                        GC.Collect();
                    };//);
                    break;
                case EMatchingType.AllWithAllBackward:
                    for (int index = countOfExistedKeypoint; index < ComputedDescriptors.Count; index++)
                    //Parallel.For(countOfExistedKeypoint, ComputedDescriptors.Count, index =>
                    {
                        Parallel.For(0, index, i =>
                        {
                            FindMatches(matcher, ComputedDescriptors[index], ComputedDescriptors[i]);
                        });
                        GC.Collect();
                    };//);
                    break;
                case EMatchingType.FullAllWithAll:
                    for (int index = countOfExistedKeypoint; index < ComputedDescriptors.Count; index++)
                    //Parallel.For(countOfExistedKeypoint, ComputedDescriptors.Count, index =>
                    {
                        Parallel.For(0, ComputedDescriptors.Count, i =>
                        {
                            if(index!=i)
                            FindMatches(matcher, ComputedDescriptors[index], ComputedDescriptors[i]);
                        });
                        GC.Collect();
                    };//);
                    break;
            }
        }

        private void StartMatching(int countOfExistedKeypoint, IFeatureMatcher matcher)
        {
            if (UseParallel)
                StartParallelMatching(countOfExistedKeypoint, matcher);
            else
                StartMatchingSequence(countOfExistedKeypoint, matcher);
        }

        private void StartMatchingSequence(int countOfExistedKeypoint, IFeatureMatcher matcher)
        {
            int startMatchingFromPrevious = 0;

            switch (MatchingType)
            {
                case EMatchingType.OnePrevious:
                    startMatchingFromPrevious = 1;
                    MatchingSequencePrevious(countOfExistedKeypoint, startMatchingFromPrevious, 1, matcher);
                    break;
                case EMatchingType.TwoPrevious:
                    startMatchingFromPrevious = 2;
                    MatchingSequencePrevious(countOfExistedKeypoint, startMatchingFromPrevious, 1, matcher);
                    break;
                case EMatchingType.AllWithAllForward:
                    for (int m = countOfExistedKeypoint; m < ComputedDescriptors.Count; m++)
                    {
                        for (int n = m + 1; n < ComputedDescriptors.Count; n++)
                            FindMatches(matcher, ComputedDescriptors[m], ComputedDescriptors[n]);

                        GC.Collect();
                    }
                    break;
                case EMatchingType.AllWithAllBackward:
                    for (int m = countOfExistedKeypoint; m < ComputedDescriptors.Count; m++)
                    {
                        for (int n = 0; n < m; n++)
                            FindMatches(matcher, ComputedDescriptors[m], ComputedDescriptors[n]);

                        GC.Collect();
                    }
                    break;
                case EMatchingType.FullAllWithAll:
                    for (int m = countOfExistedKeypoint; m < ComputedDescriptors.Count; m++)
                    {
                        for (int n = 0; n < ComputedDescriptors.Count; n++)
                            if(m!=n)
                                FindMatches(matcher, ComputedDescriptors[m], ComputedDescriptors[n]);

                        GC.Collect();
                    }
                    break;
            }
        }

        private void MatchingSequencePrevious(int startMatchingFrom, int startMatchingFromPrevious, int iteration, IFeatureMatcher matcher)
        {
            for (int m = startMatchingFrom; m < ComputedDescriptors.Count; m += iteration)
            {
                for (int n = m - startMatchingFromPrevious; n < m && n >= 0; n += iteration)
                    FindMatches(matcher, ComputedDescriptors[m], ComputedDescriptors[n]);

                GC.Collect();
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

                    GC.Collect();
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

                    GC.Collect();
                }
            });
        }

        private void StartParallelMatching(int countOfExistedKeypoint, IFeatureMatcher matcher)
        {
            int startMatchingFromPrevious = 0;
            switch (MatchingType)
            {
                case EMatchingType.OnePrevious:
                    startMatchingFromPrevious = 1;
                    MatchingParallelPrevious(countOfExistedKeypoint, startMatchingFromPrevious, matcher);
                    break;
                case EMatchingType.TwoPrevious:
                    startMatchingFromPrevious = 2;
                    MatchingParallelPrevious(countOfExistedKeypoint, startMatchingFromPrevious, matcher);
                    break;
                case EMatchingType.AllWithAllForward:
                    for (int index = countOfExistedKeypoint; index < ComputedDescriptors.Count; index++)
                    //Parallel.For(countOfExistedKeypoint, ComputedDescriptors.Count, index =>
                    {
                        Parallel.For(index + 1, ComputedDescriptors.Count, i =>
                         {
                             FindMatches(matcher, ComputedDescriptors[index], ComputedDescriptors[i]);
                         });

                        GC.Collect();
                    };//);
                    break;
                case EMatchingType.AllWithAllBackward:
                    for (int index = countOfExistedKeypoint; index < ComputedDescriptors.Count; index++)
                    //Parallel.For(countOfExistedKeypoint, ComputedDescriptors.Count, index =>
                    {
                        Parallel.For(0, index, i =>
                        {
                            FindMatches(matcher, ComputedDescriptors[index], ComputedDescriptors[i]);
                        });

                        GC.Collect();
                    };//);
                    break;
                case EMatchingType.FullAllWithAll:
                    for (int index = countOfExistedKeypoint; index < ComputedDescriptors.Count; index++)
                    //Parallel.For(countOfExistedKeypoint, ComputedDescriptors.Count, index =>
                    {
                        Parallel.For(0, ComputedDescriptors.Count, i =>
                        {
                            if(i!=index)
                                FindMatches(matcher, ComputedDescriptors[index], ComputedDescriptors[i]);
                        });

                        GC.Collect();
                    };//);
                    break;
            }
        }

        private void StartDetectingKeyPoint(int countOfInput, List<InputFileModel> listOfInput, IFeatureDetector detector)
        {
            if (UseParallel)
                Parallel.For(0, listOfInput.Count, x => { FindKeypoint(countOfInput + x, listOfInput[x], detector); });
            else
                for (int i = 0; i < listOfInput.Count; i++)
                    FindKeypoint(countOfInput + i, listOfInput[i], detector);
        }

        private void StartComputingDescriptor(int countOfExistedKeyPoint, IFeatureDescriptor descriptor)
        {
            if (UseParallel)
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
                sb.AppendLine(Path.Combine(Configuration.TempDirectoryPath, node.FileName));
            }

            File.WriteAllText(Path.Combine(Configuration.TempDirectoryPath, $"{Configuration.VisualSFMResult}.txt"), sb.ToString());
        }

        private void WriteAllMatches(List<MatchModel> findedMatches, int index = 0)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var node in findedMatches.Skip(index))
            {
                sb.AppendLine(node.SaveMatchString());
                sb.AppendLine();
            }

            File.WriteAllText(Configuration.MatchFilePath, sb.ToString());
        }

        static SemaphoreSlim semaphore = new SemaphoreSlim(1);

        private int FindMatches(IFeatureMatcher matcher, DescriptorModel leftDescriptor, DescriptorModel rightDescriptor, bool AddToList = true, bool FilterMatches = true, bool ComputeHomography = true, bool SaveInMatchNode = false, bool DrawAndSave = Configuration.SaveImagesFromProcess)
        {
            semaphore.Wait();

            var filteredMatchesList = new List<MDMatch[]>();
            var matchesList = new List<MDMatch[]>();
            MDMatch[][] matchesArray;
            var perspectiveMatrix = new Mat();
            var mask = new Mat();
            using (var matches = new VectorOfVectorOfDMatch())
            {
                try
                {
                    if (matcher.GetType().Name == typeof(CudaBruteForce).Name)
                    {
                        // Only 30000 points per descriptors can compute our GeForce GTX 1060 6GB 
                        var leftDesc = new Mat(leftDescriptor.Descriptor.Rows > 30000 ? 30000 : leftDescriptor.Descriptor.Rows, leftDescriptor.Descriptor.Cols, leftDescriptor.Descriptor.Depth, leftDescriptor.Descriptor.NumberOfChannels);
                        var rightDesc = new Mat(rightDescriptor.Descriptor.Rows > 30000 ? 30000 : rightDescriptor.Descriptor.Rows, rightDescriptor.Descriptor.Cols, rightDescriptor.Descriptor.Depth, rightDescriptor.Descriptor.NumberOfChannels);

                        for (int i = 0; i < leftDesc.Rows; i++)
                            leftDescriptor.Descriptor.Row(i).CopyTo(leftDesc.Row(i));
                        for (int i = 0; i < rightDesc.Rows; i++)
                            rightDescriptor.Descriptor.Row(i).CopyTo(rightDesc.Row(i));

                        WindowsFormHelper.AddLogToConsole($"Start CUDA computing matches for: \n" +
                                $"\t{leftDescriptor.KeyPoint.InputFile.FileName}\n" +
                                $"\t{rightDescriptor.KeyPoint.InputFile.FileName}\n");

                        matcher.Match(leftDesc, rightDesc, matches);

                        leftDesc.Dispose();
                        rightDesc.Dispose();
                    }
                    else
                    {
                        WindowsFormHelper.AddLogToConsole($"Start computing matches for: \n" +
                                $"\t{leftDescriptor.KeyPoint.InputFile.FileName}\n" +
                                $"\t{rightDescriptor.KeyPoint.InputFile.FileName}\n");

                        matcher.Match(leftDescriptor.Descriptor, rightDescriptor.Descriptor, matches);
                    }
                }
                catch (Exception e)
                {
                    semaphore.Release();
                    return 1;
                    //throw new Exception($"Happend with {leftDescriptor.KeyPoint.InputFile.FileName}:{leftDescriptor.KeyPoint.DetectedKeyPoints.Size} and {rightDescriptor.KeyPoint.InputFile.FileName}:{rightDescriptor.KeyPoint.DetectedKeyPoints.Size}", e);
                }

                WindowsFormHelper.AddLogToConsole(
                    $"FINISH ({Interlocked.Increment(ref countMatches)}) computing matches for: \n" +
                    $"\t{leftDescriptor.KeyPoint.InputFile.FileName}\n" +
                    $"\t{rightDescriptor.KeyPoint.InputFile.FileName}\n"
                    );

                semaphore.Release();
                matchesArray = matches.ToArrayOfArray();
            }
            matchesList = matchesArray.ToList();

            if (FilterMatches)
            {
                FindMinMaxDistInMatches(matchesArray, ref ms_MAX_DIST, ref ms_MIN_DIST);
                filteredMatchesList = FilterMatchesByMaxDist(matchesArray);
            }

            if (ComputeHomography)
            {
                var matchesForHomography = FilterMatches ? filteredMatchesList : matchesList;
                if (matchesForHomography.Count > 0)
                {
                    perspectiveMatrix = FindHomography(leftDescriptor.KeyPoint.DetectedKeyPoints, rightDescriptor.KeyPoint.DetectedKeyPoints, FilterMatches ? filteredMatchesList : matchesList, mask);
                }
            }

            var foundedMatch = new MatchModel(
                leftDescriptor,
                rightDescriptor,
                matchesList,
                perspectiveMatrix,
                mask,
                null,
                filteredMatchesList,
                FilterMatches
                );

            if (DrawAndSave)
                foundedMatch.DrawAndSave(fileManager);

            if (SaveInMatchNode)
                foundedMatch.SaveMatchString(true);

            //File.WriteAllText(Path.Combine(Configuration.TempDrawMatches, $"{leftDescriptor.KeyPoint.InputFile.FileNameWithoutExtension}_{rightDescriptor.KeyPoint.InputFile.FileNameWithoutExtension}.txt"), foundedMatch.SaveMatchString(true, false));

            //File.WriteAllText(Path.Combine(Configuration.TempDrawKeypoint, $"{leftDescriptor.KeyPoint.InputFile.FileNameWithoutExtension}_{rightDescriptor.KeyPoint.InputFile.FileNameWithoutExtension}.txt"), foundedMatch.SaveMatchString(false, false));

            if (AddToList)
                FoundedMatches.Add(foundedMatch);

            ////Dispose
            //filteredMatchesList = new List<MDMatch[]>();
            //filteredMatchesList.Clear();
            //matchesList.Clear();
            //perspectiveMatrix.Dispose();
            //mask.Dispose();
            //if (foundedMatch.Mask != null)
            //    foundedMatch.Mask.Dispose();
            //if (foundedMatch.PerspectiveMatrix != null)
            //    foundedMatch.PerspectiveMatrix.Dispose();
            //foundedMatch = null;

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
            ms_MAX_DIST = 0;
            ms_MIN_DIST = float.MaxValue;

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

        private int ComputeDescriptor(KeyPointModel keypoint, IFeatureDescriptor descriptor, bool AddToList = true, bool SaveOnDisk = true)
        {
            var fileName = keypoint.InputFile.FileName;

            if (File.Exists(Path.Combine(Configuration.TempDirectoryPath, keypoint.InputFile.FileNameWithoutExtension + ".SIFT")))
            {
                WindowsFormHelper.AddLogToConsole($"Load descriptor for: {fileName}\n");

                var descriptorNode = new DescriptorModel(keypoint, new Mat());
                descriptorNode.LoadSiftFile(true);

                if (AddToList)
                    ComputedDescriptors.Add(keypoint.ID, descriptorNode);

                return 0;
            }

            try
            {
                WindowsFormHelper.AddLogToConsole($"Start computing descriptor for: {fileName}\n");

                var computedDescriptor = descriptor.ComputeDescriptor(keypoint);
                var descriptorNode = new DescriptorModel(keypoint, computedDescriptor);

                WindowsFormHelper.AddLogToConsole($"FINISH computing descriptor for: {fileName}\n");

                if (AddToList)
                    ComputedDescriptors.Add(keypoint.ID, descriptorNode);

                if (SaveOnDisk)
                    descriptorNode.SaveSiftFile(true, false);
            }
            catch (Exception e)
            {
                WindowsFormHelper.AddLogToConsole("Error\n");
                return 1;
            }
            return 0;
        }

        private int FindKeypoint(int ID, InputFileModel inputFile, IFeatureDetector detector, bool AddToList = true, bool DrawAndSave = Configuration.SaveImagesFromProcess)
        {
            try
            {
                var fileName = inputFile.FileName;

                WindowsFormHelper.AddLogToConsole($"Start finding key points for: {fileName}\n");

                var image = new Mat(inputFile.FullPath);
                var detectedKeyPoints = detector.DetectKeyPoints(image);
                image.Dispose();

                WindowsFormHelper.AddLogToConsole(
                    $"FINISH finding key points for: {fileName}\n" +
                    $"Count of key points: {detectedKeyPoints.Length}\n");

                var newItem = new KeyPointModel(
                    new VectorOfKeyPoint(detectedKeyPoints),
                    inputFile,
                    ID
                    );

                if (AddToList)
                    DetectedKeyPoints.Add(ID, newItem);

                if (DrawAndSave)
                    newItem.DrawAndSave(fileManager);
            }
            catch (Exception e)
            {
                WindowsFormHelper.AddLogToConsole("Error\n");
                return 1;
            }
            return 0;
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
