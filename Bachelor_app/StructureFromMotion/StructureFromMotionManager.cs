using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        private SortedList<int, KeyPointModel> detectedKeyPoints;
        private SortedList<int, DescriptorModel> computedDescriptors;
        private List<MatchModel> foundedMatches;

        private FileManager fileManager;
        private CameraManager cameraManager;

        private float msMAXDIST;
        private float msMINDIST;
        private int countMatches = 0;
        private int countKeyPoint = 0;
        private int countDeskriptor = 0;

        // private static object locker = new object();
        public SfM(FileManager fileManager, CameraManager cameraManager)
        {
            detectedKeyPoints = new SortedList<int, KeyPointModel>();
            computedDescriptors = new SortedList<int, DescriptorModel>();
            foundedMatches = new List<MatchModel>();

            this.fileManager = fileManager;
            this.cameraManager = cameraManager;
        }

        public void StartSFM(bool continueSFM = false)
        {
            StopSFM = false;
            List<InputFileModel> listOfInput = new List<InputFileModel>();
            var countInputFile = detectedKeyPoints.Count;
            var startSFMFrom = 0;
            var iterMatches = 0;

            if (continueSFM)
            {
                startSFMFrom = countInputFile;
                iterMatches = foundedMatches.Count;
            }
            else
            {
                ClearList();
                countMatches = 0;
                countDeskriptor = 0;
                countKeyPoint = 0;
            }

            GC.Collect();

            switch (fileManager.InputType)
            {
                case EInput.ListViewBasicStack:
                    listOfInput = GetListFromListView(continueSFM);
                    ComputeSfM(startSFMFrom, listOfInput);
                    break;
                case EInput.ConnectedStereoCamera:
                    while (!StopSFM)
                    {
                        var cameraStereoOutput = cameraManager.GetInputFromStereoCamera(true, countInputFile++);
                        listOfInput.AddRange(cameraStereoOutput);

                        ComputeSfM(detectedKeyPoints.Count, cameraStereoOutput);
                    }

                    break;
                case EInput.ConnectedLeftCamera:
                    while (!StopSFM)
                    {
                        var cameraOutput = cameraManager.GetInputFromCamera(cameraManager.LeftCamera.Camera, countInputFile++);
                        listOfInput.AddRange(cameraOutput);

                        ComputeSfM(detectedKeyPoints.Count, cameraOutput);
                    }

                    break;
                case EInput.ConnectedRightCamera:
                    while (!StopSFM)
                    {
                        var cameraOutput = cameraManager.GetInputFromCamera(cameraManager.RightCamera.Camera, countInputFile++);
                        listOfInput.AddRange(cameraOutput);

                        ComputeSfM(detectedKeyPoints.Count, cameraOutput);
                    }

                    break;
            }

            GC.Collect();

            if (continueSFM)
                WriteAddedImages(listOfInput);

            WriteAllMatches(foundedMatches, iterMatches);
            ToolHelper.RunVisualSFM(continueSFM);
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

        private List<InputFileModel> GetListFromListView(bool continueSFM)
        {
            var list = continueSFM ? fileManager.ListViewModel.BasicStack.Where(x => x.UseInSFM == false).ToList() : fileManager.ListViewModel.BasicStack;
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
            detectedKeyPoints.Clear();
            computedDescriptors.Clear();
            foundedMatches.Clear();
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
            FindMatches(matcher, computedDescriptors[countOfExistedKeypoint - 2], computedDescriptors[countOfExistedKeypoint - 1]);
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
                    for (int m = countOfExistedKeypoint; m < computedDescriptors.Count; m++)
                    {
                        for (int n = m + 1; n < computedDescriptors.Count; n++)
                        {
                            FindMatches(matcher, computedDescriptors[m], computedDescriptors[n]);
                        }
                    }

                    break;
                case EMatchingType.AllWithAllBackward:
                    for (int m = countOfExistedKeypoint; m < computedDescriptors.Count; m++)
                    {
                        for (int n = 0; n < m; n++)
                        {
                            FindMatches(matcher, computedDescriptors[m], computedDescriptors[n]);
                        }
                    }

                    break;
                case EMatchingType.FullAllWithAll:
                    for (int m = countOfExistedKeypoint; m < computedDescriptors.Count; m++)
                    {
                        for (int n = 0; n < computedDescriptors.Count; n++)
                        {
                            if (m != n)
                                FindMatches(matcher, computedDescriptors[m], computedDescriptors[n]);
                        }
                    }

                    break;
            }
        }

        private void StartStereoMatchingParallel(int countOfExistedKeypoint, IFeatureMatcher matcher)
        {
            FindMatches(matcher, computedDescriptors[countOfExistedKeypoint - 2], computedDescriptors[countOfExistedKeypoint - 1]);
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
                    for (int index = countOfExistedKeypoint; index < computedDescriptors.Count; index++)
                    {
                        Parallel.For(index + 1, computedDescriptors.Count, i =>
                        {
                            FindMatches(matcher, computedDescriptors[index], computedDescriptors[i]);
                        });
                        GC.Collect();
                    }

                    break;
                case EMatchingType.AllWithAllBackward:
                    for (int index = countOfExistedKeypoint; index < computedDescriptors.Count; index++)
                    {
                        Parallel.For(0, index, i =>
                        {
                            FindMatches(matcher, computedDescriptors[index], computedDescriptors[i]);
                        });
                        GC.Collect();
                    }

                    break;
                case EMatchingType.FullAllWithAll:
                    for (int index = countOfExistedKeypoint; index < computedDescriptors.Count; index++)
                    {
                        Parallel.For(0, computedDescriptors.Count, i =>
                        {
                            if (index != i)
                            FindMatches(matcher, computedDescriptors[index], computedDescriptors[i]);
                        });
                        GC.Collect();
                    }

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
                    for (int m = countOfExistedKeypoint; m < computedDescriptors.Count; m++)
                    {
                        for (int n = m + 1; n < computedDescriptors.Count; n++)
                            FindMatches(matcher, computedDescriptors[m], computedDescriptors[n]);

                        GC.Collect();
                    }

                    break;
                case EMatchingType.AllWithAllBackward:
                    for (int m = countOfExistedKeypoint; m < computedDescriptors.Count; m++)
                    {
                        for (int n = 0; n < m; n++)
                            FindMatches(matcher, computedDescriptors[m], computedDescriptors[n]);

                        GC.Collect();
                    }

                    break;
                case EMatchingType.FullAllWithAll:
                    for (int m = countOfExistedKeypoint; m < computedDescriptors.Count; m++)
                    {
                        for (int n = 0; n < computedDescriptors.Count; n++)
                        {
                            if (m != n)
                                FindMatches(matcher, computedDescriptors[m], computedDescriptors[n]);
                        }

                        GC.Collect();
                    }

                    break;
            }
        }

        private void MatchingSequencePrevious(int startMatchingFrom, int startMatchingFromPrevious, int iteration, IFeatureMatcher matcher)
        {
            for (int m = startMatchingFrom; m < computedDescriptors.Count; m += iteration)
            {
                for (int n = m - startMatchingFromPrevious; n < m && n >= 0; n += iteration)
                    FindMatches(matcher, computedDescriptors[m], computedDescriptors[n]);

                GC.Collect();
            }
        }

        private void MatchingParallelPrevious(int startMatchingFrom, int startMatchingFromPrevious, IFeatureMatcher matcher)
        {
            Parallel.For(startMatchingFrom, computedDescriptors.Count, index =>
            {
                if (index >= startMatchingFromPrevious)
                {
                    Parallel.For(index - startMatchingFromPrevious, index, i =>
                    {
                        FindMatches(matcher, computedDescriptors[index], computedDescriptors[i]);
                    });

                    GC.Collect();
                }
            });
        }

        private void MatchingStereoParallelPrevious(int startMatchingFrom, int startMatchingFromPrevious, IFeatureMatcher matcher)
        {
            Parallel.For(startMatchingFrom, computedDescriptors.Count, index =>
            {
                if (index >= startMatchingFromPrevious * 2)
                {
                    Parallel.For(index - (startMatchingFromPrevious * 2), index, i =>
                    {
                        if ((index - i) % 2 == 0)
                            FindMatches(matcher, computedDescriptors[index], computedDescriptors[i]);
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
                    for (int index = countOfExistedKeypoint; index < computedDescriptors.Count; index++)
                    {
                        Parallel.For(index + 1, computedDescriptors.Count, i =>
                         {
                             FindMatches(matcher, computedDescriptors[index], computedDescriptors[i]);
                         });

                        GC.Collect();
                    }

                    break;
                case EMatchingType.AllWithAllBackward:
                    for (int index = countOfExistedKeypoint; index < computedDescriptors.Count; index++)
                    {
                        Parallel.For(0, index, i =>
                        {
                            FindMatches(matcher, computedDescriptors[index], computedDescriptors[i]);
                        });

                        GC.Collect();
                    }

                    break;
                case EMatchingType.FullAllWithAll:
                    for (int index = countOfExistedKeypoint; index < computedDescriptors.Count; index++)
                    {
                        Parallel.For(0, computedDescriptors.Count, i =>
                        {
                            if (i != index)
                                FindMatches(matcher, computedDescriptors[index], computedDescriptors[i]);
                        });

                        GC.Collect();
                    }

                    break;
            }
        }

        private void StartDetectingKeyPoint(int countOfInput, List<InputFileModel> listOfInput, IFeatureDetector detector)
        {
            if (UseParallel)
            {
                Parallel.For(0, listOfInput.Count, x =>
                {
                    FindKeypoint(countOfInput + x, listOfInput[x], detector);
                    GC.Collect();
                });
            }
            else
            {
                for (int i = 0; i < listOfInput.Count; i++)
                {
                    FindKeypoint(countOfInput + i, listOfInput[i], detector);
                    GC.Collect();
                }
            }
        }

        private void StartComputingDescriptor(int countOfExistedKeyPoint, IFeatureDescriptor descriptor)
        {
            if (UseParallel)
            {
                Parallel.For(countOfExistedKeyPoint, detectedKeyPoints.Count, x =>
                {
                    ComputeDescriptor(detectedKeyPoints[x], descriptor);
                    GC.Collect();
                });
            }
            else
            {
                for (int i = countOfExistedKeyPoint; i < detectedKeyPoints.Count; i++)
                {
                    ComputeDescriptor(detectedKeyPoints[i], descriptor);
                    GC.Collect();
                }
            }
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
                if (string.IsNullOrWhiteSpace(node.FileFormatMatch))
                    sb.AppendLine(node.SaveMatchString());
                else
                    sb.AppendLine(node.FileFormatMatch);

                sb.AppendLine();
            }

            File.WriteAllText(Configuration.MatchFilePath, sb.ToString());
        }

        private static SemaphoreSlim semaphore = new SemaphoreSlim(1);

        private int FindMatches(IFeatureMatcher matcher, DescriptorModel leftDescriptor, DescriptorModel rightDescriptor, bool addToList = true, bool filterMatches = true, bool computeHomography = true, bool saveInMatchNode = false, bool drawAndSave = Configuration.SaveImagesFromProcess)
        {
            MatchModel foundedMatch;

            // var path = Path.Combine(Configuration.TempComputedDescriptorDirectoryPath, $"{UsedDetector}{UsedDescriptor}_{leftDescriptor.KeyPoint.InputFile.FileNameWithoutExtension}_{rightDescriptor.KeyPoint.InputFile.FileNameWithoutExtension}.txt");
            // if (File.Exists(path))
            // {
            //    Interlocked.Increment(ref countMatches);
            //    foundedMatch = new MatchModel(null, null, null, null, null, File.ReadAllText(path), null, false);
            //    return 1;
            // }
            // else
            // {
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
                    WindowsFormHelper.AddLogToConsole($"ERROR with matching:\n {e.Message}\n");
                    return 1;

                    // throw new Exception($"Happend with {leftDescriptor.KeyPoint.InputFile.FileName}:{leftDescriptor.KeyPoint.DetectedKeyPoints.Size} and {rightDescriptor.KeyPoint.InputFile.FileName}:{rightDescriptor.KeyPoint.DetectedKeyPoints.Size}", e);
                }

                WindowsFormHelper.AddLogToConsole(
                    $"FINISH ({Interlocked.Increment(ref countMatches)}) computing matches for: \n" +
                    $"\t{leftDescriptor.KeyPoint.InputFile.FileName}\n" +
                    $"\t{rightDescriptor.KeyPoint.InputFile.FileName}\n"
                    );

                semaphore.Release();
                matchesArray = matches.ToArrayOfArray();

            // }
                matchesList = matchesArray.ToList();

                if (filterMatches)
                {
                    FindMinMaxDistInMatches(matchesArray, ref msMAXDIST, ref msMINDIST);
                    filteredMatchesList = FilterMatchesByMaxDist(matchesArray);
                }

                if (computeHomography)
                {
                    var matchesForHomography = filterMatches ? filteredMatchesList : matchesList;
                    if (matchesForHomography.Count > 0)
                    {
                        perspectiveMatrix = FindHomography(leftDescriptor.KeyPoint.DetectedKeyPoints, rightDescriptor.KeyPoint.DetectedKeyPoints, filterMatches ? filteredMatchesList : matchesList, mask);
                    }
                }

                foundedMatch = new MatchModel(
                    leftDescriptor,
                    rightDescriptor,
                    matchesList,
                    perspectiveMatrix,
                    mask,
                    null,
                    filteredMatchesList,
                    filterMatches
                    );

                if (drawAndSave)
                    Task.Run(async () => await foundedMatch.DrawAndSaveAsync(fileManager));

                if (saveInMatchNode)
                    foundedMatch.SaveMatchString(saveInNode: saveInMatchNode);

                // if (!string.IsNullOrWhiteSpace(path))
                //    File.WriteAllText(path, foundedMatch.SaveMatchString());
            }

        // File.WriteAllText(Path.Combine(Configuration.TempDrawMatches, $"{leftDescriptor.KeyPoint.InputFile.FileNameWithoutExtension}_{rightDescriptor.KeyPoint.InputFile.FileNameWithoutExtension}.txt"), foundedMatch.SaveMatchString(true, false));

        // File.WriteAllText(Path.Combine(Configuration.TempDrawKeypoint, $"{leftDescriptor.KeyPoint.InputFile.FileNameWithoutExtension}_{rightDescriptor.KeyPoint.InputFile.FileNameWithoutExtension}.txt"), foundedMatch.SaveMatchString(false, false));
            if (addToList)
            foundedMatches.Add(foundedMatch);

        //// Dispose
        // filteredMatchesList = new List<MDMatch[]>();
        // filteredMatchesList.Clear();
        // matchesList.Clear();
        // perspectiveMatrix.Dispose();
        // mask.Dispose();
        // if (foundedMatch.Mask != null)
        //    foundedMatch.Mask.Dispose();
        // if (foundedMatch.PerspectiveMatrix != null)
        //    foundedMatch.PerspectiveMatrix.Dispose();
        // foundedMatch = null;
            return 0;
        }

        private List<MDMatch[]> FilterMatchesByMaxDist(MDMatch[][] matchesArray)
        {
            List<MDMatch[]> filteredMatchesList = new List<MDMatch[]>();

            for (int i = 0; i < matchesArray.Length; i++)
            {
                if (matchesArray[i].Length == 0)
                    continue;

                if (matchesArray[i][0].Distance < GetMaxPossibleDist())
                {
                    filteredMatchesList.Add(matchesArray[i]);
                }
            }

            return filteredMatchesList;
        }

        private float GetMaxPossibleDist()
        {
            // return (ms_MIN_DIST + ((ms_MAX_DIST - ms_MIN_DIST)*0.5F));
            return msMAXDIST * 0.95f;
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

        private SemaphoreSlim semDescriptor = new SemaphoreSlim(8);

        private int ComputeDescriptor(KeyPointModel keypoint, IFeatureDescriptor descriptor, bool addToList = true, bool saveOnDisk = true)
        {
            semDescriptor.Wait();
            var fileName = keypoint.InputFile.FileName;

            // var path = Path.Combine(Configuration.TempComputedDescriptorDirectoryPath, $"{UsedDetector}{UsedDescriptor}_{keypoint.InputFile.FileNameWithoutExtension}.SIFT");
            // if (File.Exists(path))
            // {
            //    WindowsFormHelper.AddLogToConsole($"({Interlocked.Increment(ref countDeskriptor)}) Load descriptor for: {fileName}\n");
            //    File.Copy(path, Path.Combine(Configuration.TempDirectoryPath,$"{keypoint.InputFile.FileNameWithoutExtension}.SIFT"), true);

            // var descriptorNode = new DescriptorModel(keypoint, new Mat());
            //    descriptorNode.LoadSiftFile(true);

            // if (AddToList)
            //        ComputedDescriptors.Add(keypoint.ID, descriptorNode);
            //    semDescriptor.Release();
            //    return 0;
            // }
            try
            {
                WindowsFormHelper.AddLogToConsole($"Start computing descriptor for: {fileName}\n");

                var computedDescriptor = descriptor.ComputeDescriptor(keypoint);
                var descriptorNode = new DescriptorModel(keypoint, computedDescriptor);

                WindowsFormHelper.AddLogToConsole($"FINISH ({Interlocked.Increment(ref countDeskriptor)}) computing descriptor for: {fileName}\n");

                if (addToList)
                    computedDescriptors.Add(keypoint.ID, descriptorNode);

                if (saveOnDisk)
                    Task.Run(async () => await descriptorNode.SaveSiftFileAsync(true, false));
            }
            catch (Exception)
            {
                WindowsFormHelper.AddLogToConsole("Error\n");
                semDescriptor.Release();
                return 1;
            }

            semDescriptor.Release();
            return 0;
        }

        private SemaphoreSlim semKeypoint = new SemaphoreSlim(8);

        private int FindKeypoint(int id, InputFileModel inputFile, IFeatureDetector detector, bool addToList = true, bool drawAndSave = Configuration.SaveImagesFromProcess)
        {
            semKeypoint.Wait();
            try
            {
                var fileName = inputFile.FileName;

                WindowsFormHelper.AddLogToConsole($"Start finding key points for: {fileName}\n");

                var image = new Mat(inputFile.FullPath);
                var detectedKeyPoints = detector.DetectKeyPoints(image);
                image.Dispose();

                WindowsFormHelper.AddLogToConsole(
                    $"FINISH ({Interlocked.Increment(ref countKeyPoint)}) finding key points for: {fileName}\n" +
                    $"Count of key points: {detectedKeyPoints.Length}\n");

                var newItem = new KeyPointModel(
                    new VectorOfKeyPoint(detectedKeyPoints),
                    inputFile,
                    id
                    );

                if (addToList)
                    this.detectedKeyPoints.Add(id, newItem);

                if (drawAndSave)
                    Task.Run(async () => await newItem.DrawAndSaveAsync(fileManager));
            }
            catch (Exception)
            {
                WindowsFormHelper.AddLogToConsole("Error\n");
                semKeypoint.Release();
                return 1;
            }

            semKeypoint.Release();
            return 0;
        }

        public Mat FindHomography(VectorOfKeyPoint keypointsModel, VectorOfKeyPoint keypointsTest, List<MDMatch[]> matches, Mat mask)
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

            Mat homography = CvInvoke.FindHomography(srcPoints, destPoints, Emgu.CV.CvEnum.HomographyMethod.Ransac, 10, mask);

            return homography;
        }
    }
}
