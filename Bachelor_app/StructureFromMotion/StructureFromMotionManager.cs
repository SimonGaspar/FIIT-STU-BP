using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bachelor_app.Enumerate;
using Bachelor_app.Extension;
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

        private SortedList<int, KeyPointModel> DetectedKeyPoints;
        private SortedList<int, DescriptorModel> ComputedDescriptors;
        private List<MatchModel> FoundedMatches;

        private FileManager fileManager;
        private CameraManager cameraManager;

        private float ms_MAX_DIST;
        private float ms_MIN_DIST;
        private int countMatches = 0;
        //private static object locker = new object();


        //private DataTable KeyPointTable = new DataTable();
        //private DataTable DescriptorTable = new DataTable();
        //private DataTable MatchTable = new DataTable();
        //private DataTable NVMModelTable = new DataTable();
        //private DataTable ProcessTable = new DataTable();
        private Stopwatch Stopwatch = new Stopwatch();

        public void SetDataTable()
        {
            //KeyPointTable = new DataTable();
            //DescriptorTable = new DataTable();
            //MatchTable = new DataTable();
            //NVMModelTable = new DataTable();
            //ProcessTable = new DataTable();
            //Stopwatch = new Stopwatch();

            //KeyPointTable.Columns.Add("DateTime");
            //KeyPointTable.Columns.Add("Image name");
            //KeyPointTable.Columns.Add("Resolution width");
            //KeyPointTable.Columns.Add("Resolution height");
            //KeyPointTable.Columns.Add("Count of keypoints");
            //KeyPointTable.Columns.Add("Algorithm");
            //KeyPointTable.Columns.Add("Time to generate");
            //KeyPointTable.Columns.Add("Parallel");


            //DescriptorTable.Columns.Add("DateTime");
            //DescriptorTable.Columns.Add("Image name");
            //DescriptorTable.Columns.Add("Count of keypoints");
            //DescriptorTable.Columns.Add("Count of descriptors width");
            //DescriptorTable.Columns.Add("Count of descriptors hegiht");
            //DescriptorTable.Columns.Add("Algorithm");
            //DescriptorTable.Columns.Add("Time to generate");
            //DescriptorTable.Columns.Add("Parallel");

            //MatchTable.Columns.Add("DateTime");
            //MatchTable.Columns.Add("Matching type");
            //MatchTable.Columns.Add("Left image");
            //MatchTable.Columns.Add("Right image");
            //MatchTable.Columns.Add("Count of left descriptors width");
            //MatchTable.Columns.Add("Count of left descriptors height");
            //MatchTable.Columns.Add("Count of right descriptors width");
            //MatchTable.Columns.Add("Count of right descriptors height");
            //MatchTable.Columns.Add("Founded matches");
            //MatchTable.Columns.Add("Filtered matches");
            //MatchTable.Columns.Add("Algorithm");
            //MatchTable.Columns.Add("Time to generate");
            //MatchTable.Columns.Add("Time to filtered");
            //MatchTable.Columns.Add("Filtered");
            //MatchTable.Columns.Add("Parallel");

            //NVMModelTable.Columns.Add("DateTime");
            //NVMModelTable.Columns.Add("Time to generate model");
            //NVMModelTable.Columns.Add("Time to load model");
            //NVMModelTable.Columns.Add("Count of camera");
            //NVMModelTable.Columns.Add("Count of point");

            //ProcessTable.Columns.Add("DateTime");
            //ProcessTable.Columns.Add("KeypointAlgorithm");
            //ProcessTable.Columns.Add("KeypointCount");
            //ProcessTable.Columns.Add("KeyPointTime");
            //ProcessTable.Columns.Add("DescriptorAlgorithm");
            //ProcessTable.Columns.Add("DescriptorCount");
            //ProcessTable.Columns.Add("DescriptorTime");
            //ProcessTable.Columns.Add("MatcherAlgorithm");
            //ProcessTable.Columns.Add("MatcherCount");
            //ProcessTable.Columns.Add("MetchingType");
            //ProcessTable.Columns.Add("MatcherTime");
            //ProcessTable.Columns.Add("ModelCameraCount");
            //ProcessTable.Columns.Add("ModelPointCount");
            //ProcessTable.Columns.Add("ModelTime");
            //ProcessTable.Columns.Add("Parallel");
            //ProcessTable.Columns.Add("TimeToCompute");

        }


        public SfM(FileManager fileManager, CameraManager cameraManager)
        {
            SetDataTable();
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
                countMatches = 0;
                Configuration.DeleteTempFolder();
                Configuration.GenerateFolders();
                ClearList();
            }

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

            if (ContinueSFM)
                WriteAddedImages(listOfInput);

            WriteAllMatches(FoundedMatches, iterMatches);
            ToolHelper.RunVisualSFM(ContinueSFM);
        }

        //DataTable a = new DataTable();
        //DataTable b = new DataTable();
        //DataTable c = new DataTable();
        //DataTable d = new DataTable();
        //DataTable e = new DataTable();

        public void ComputeSfM(int startIndex, List<InputFileModel> inputImages)
        {
            //CopyColumnDataTable(KeyPointTable, a);
            //CopyColumnDataTable(DescriptorTable, b);
            //CopyColumnDataTable(MatchTable, c);
            //CopyColumnDataTable(NVMModelTable, d);
            //CopyColumnDataTable(ProcessTable, e);

            var SpecificStopWatch = new Stopwatch();
            UseParallel = false;
            //foreach (var detectoris in Enum.GetValues(typeof(EFeatureDetector)).Cast<EFeatureDetector>().ToList())
            {
                var detectoris = EFeatureDetector.SIFT;
                //continue;
                //foreach (var descriptoris in Enum.GetValues(typeof(EFeatureDescriptor)).Cast<EFeatureDescriptor>())
                {
                    var descriptoris = EFeatureDescriptor.SIFT;
                    //continue;
                    //foreach (var matcheris in Enum.GetValues(typeof(EFeatureMatcher)).Cast<EFeatureMatcher>())
                    {
                        var matcheris = EFeatureMatcher.BruteForce;
                        var matcheristype = EMatchingType.AllWithAll;
                        //continue;
                        //foreach (var matcheristype in Enum.GetValues(typeof(EMatchingType)).Cast<EMatchingType>())
                        {
                            //continue;
                            {
                                //if (detectoris == EFeatureDetector.FAST && descriptoris == EFeatureDescriptor.CudaORB)
                                //continue;
                                //SetDataTable();
                                ClearList();
                                Configuration.DeleteTempFolder();
                                Configuration.GenerateFolders();
                                inputImages = GetListFromListView(false);
                                long time1 = 0, time2 = 0, time3 = 0, time4 = 0;
                                countMatches = 0;
                                MatchingType = matcheristype;
                                Detector = detectoris.GetDetectorInstance();
                                Descriptor = descriptoris.GetDescriptorInstance();
                                Matcher = matcheris.GetMatcherInstance();
                                var nvmFile = new List<NvmModel>();
                                try
                                {
                                    SpecificStopWatch.Restart();
                                    StartDetectingKeyPoint(startIndex, inputImages, Detector);

                                    Console.WriteLine("Memory used before collection:{0:N0}",GC.GetTotalMemory(false));
                                    GC.Collect();
                                    Console.WriteLine("Memory used after full collection:{0:N0}",GC.GetTotalMemory(true));

                                    SpecificStopWatch.Stop();
                                    time1 = SpecificStopWatch.ElapsedMilliseconds;
                                    SpecificStopWatch.Start();
                                    StartComputingDescriptor(startIndex, Descriptor);

                                    Console.WriteLine("Memory used before collection:{0:N0}", GC.GetTotalMemory(false));
                                    GC.Collect();
                                    Console.WriteLine("Memory used after full collection:{0:N0}", GC.GetTotalMemory(true));

                                    SpecificStopWatch.Stop();
                                    time2 = SpecificStopWatch.ElapsedMilliseconds;
                                    WindowsFormHelper.ClearConsole();
                                    SpecificStopWatch.Start();
                                    StartMatching(startIndex, Matcher);
                                    SpecificStopWatch.Stop();
                                    time3 = SpecificStopWatch.ElapsedMilliseconds;
                                    SpecificStopWatch.Start();

                                    WriteAllMatches(FoundedMatches, 0);
                                    Stopwatch.Reset();
                                    Stopwatch.Start();
                                    ToolHelper.RunVisualSFM(false);
                                    Stopwatch.Stop();
                                    SpecificStopWatch.Stop();
                                    time4 = SpecificStopWatch.ElapsedMilliseconds;
                                    var time = Stopwatch.ElapsedMilliseconds;
                                    Stopwatch.Reset();
                                    Stopwatch.Start();
                                    nvmFile = SfMHelper.LoadPointCloud();
                                    Stopwatch.Stop();
                                    //NVMModelTable.Columns.Add("DateTime");
                                    //NVMModelTable.Columns.Add("Time to generate model");
                                    //NVMModelTable.Columns.Add("Time to load model");
                                    //NVMModelTable.Columns.Add("Count of camera");
                                    //NVMModelTable.Columns.Add("Count of point");
                                    //NVMModelTable.Rows.Add($"{DateTime.Now}", $"{time}", $"{Stopwatch.ElapsedMilliseconds}", $"{nvmFile.Sum(x => x.ImageCount)}", $"{nvmFile.Sum(x => x.PointCount)}");


                                    //ProcessTable.Columns.Add("DateTime");
                                    //ProcessTable.Columns.Add("KeypointAlgorithm");
                                    //ProcessTable.Columns.Add("KeypointCount");
                                    //ProcessTable.Columns.Add("KeyPointTime");
                                    //ProcessTable.Columns.Add("DescriptorAlgorithm");
                                    //ProcessTable.Columns.Add("DescriptorCount");
                                    //ProcessTable.Columns.Add("DescriptorTime");
                                    //ProcessTable.Columns.Add("MatcherAlgorithm");
                                    //ProcessTable.Columns.Add("MatcherCount");
                                    //ProcessTable.Columns.Add("MetchingType");
                                    //ProcessTable.Columns.Add("MatcherTime");
                                    //ProcessTable.Columns.Add("ModelCameraCount");
                                    //ProcessTable.Columns.Add("ModelPointCount");
                                    //ProcessTable.Columns.Add("ModelTime");
                                    //ProcessTable.Columns.Add("Parallel");
                                    //ProcessTable.Columns.Add("TimeToCompute");

                                    //private SortedList<int, KeyPointModel> DetectedKeyPoints;
                                    //private SortedList<int, DescriptorModel> ComputedDescriptors;
                                    //private List<MatchModel> FoundedMatches;

                                    //ProcessTable.Rows.Add($"{DateTime.Now}", $"{detectoris}", $"{DetectedKeyPoints.Count}", $"{time1}", $"{descriptoris}", $"{ComputedDescriptors.Count}", $"{time2}",
                                    //    $"{matcheris}", $"{FoundedMatches.Count}", $"{matcheristype}", $"{time3}", $"{nvmFile.Sum(x => x.ImageCount)}", $"{nvmFile.Sum(x => x.PointCount)}",
                                    //    $"{time4}", $"{UseParallel}", $"{SpecificStopWatch.ElapsedMilliseconds}");

                                    WriteDatatabletoJson();
                                }
                                catch (Exception e)
                                {
                                    //ProcessTable.Rows.Add($"{DateTime.Now}", $"{detectoris}", $"FAILED", $"FAILED", $"{descriptoris}", $"FAILED", $"FAILED",
                                    //    $"{matcheris}", $"FAILED", $"{matcheristype}", $"FAILED", $"FAILED", $"FAILED",
                                    //    $"FAILED", $"{UseParallel}", $"FAILED");
                                }

                                WindowsFormHelper.ClearConsole();
                            }
                        }
                    }
                }
            }

            //a.ExportToCSV($"KEYPOINT.csv", @"C:\Users\Notebook\Desktop\FIIT-STU-BC\FIIT-STU-BP");
            //b.ExportToCSV($"DESCRIPTOR.csv", @"C:\Users\Notebook\Desktop\FIIT-STU-BC\FIIT-STU-BP");
            //c.ExportToCSV($"MATCH.csv", @"C:\Users\Notebook\Desktop\FIIT-STU-BC\FIIT-STU-BP");
            //d.ExportToCSV($"MODEL.csv", @"C:\Users\Notebook\Desktop\FIIT-STU-BC\FIIT-STU-BP");
            //e.ExportToCSV($"VISUALSFM.csv", @"C:\Users\Notebook\Desktop\FIIT-STU-BC\FIIT-STU-BP");
        }

        private void WriteDatatabletoJson()
        {
            var name = $"{Detector.GetType().Name}_{Descriptor.GetType().Name}_{Matcher.GetType().Name}_{(UseParallel ? "Parallel" : "Sequel")}_{MatchingType}";
            var path = @"C:\Users\Notebook\Desktop\FIIT-STU-BC\FIIT-STU-BP";
            //KeyPointTable.ExportToCSV($"KEYPOINT_{name}.csv", path);
            //DescriptorTable.ExportToCSV($"DESCRIPTOR_{name}.csv", path);
            //MatchTable.ExportToCSV($"MATCH_{name}.csv", path);
            //NVMModelTable.ExportToCSV($"MODEL_{name}.csv", path);
            //ProcessTable.ExportToCSV($"VISUALSFM_{name}.csv", path);

            File.Copy(Configuration.VisualSFMResultPath, Path.Combine(path, name + ".nvm"), true);

            //JsonHelper.SaveJson(KeyPointTable, name, path);
            //JsonHelper.SaveJson(DescriptorTable, name, path);
            //JsonHelper.SaveJson(MatchTable, name, path);
            //JsonHelper.SaveJson(NVMModelTable, name, path);
            //JsonHelper.SaveJson(ProcessTable, name, path);
            try
            {
                //CopyDataTable(KeyPointTable, a);
                //CopyDataTable(DescriptorTable, b);
                //CopyDataTable(MatchTable, c);
                //CopyDataTable(NVMModelTable, d);
                //CopyDataTable(ProcessTable, e);
            }
            catch (Exception e)
            {
                Console.WriteLine();
            }
        }

        private void CopyDataTable(DataTable a, DataTable b)
        {
            foreach (DataRow dr in a.Rows)
            {
                b.Rows.Add(dr.ItemArray);
            }
        }

        private void CopyColumnDataTable(DataTable a, DataTable b)
        {
            foreach (DataColumn dr in a.Columns)
            {
                b.Columns.Add(dr.ColumnName);
            }
        }

        private List<InputFileModel> GetListFromListView(bool ContinueSFM)
        {
            var list = ContinueSFM ? fileManager.ListViewModel.BasicStack.Where(x => x.UseInSFM == false).ToList() : fileManager.ListViewModel.BasicStack;
            foreach (var node in list)
            {
                var savePath = Path.Combine(Configuration.TempDirectoryPath, node.FileName);
                File.Copy(node.FullPath, savePath, true);
                //node.SetFileInfo(new FileInfo(savePath));
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
                case EMatchingType.AllWithAll:
                    for (int m = countOfExistedKeypoint; m < ComputedDescriptors.Count; m++)
                        for (int n = m + 1; n < ComputedDescriptors.Count; n++)
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
                case EMatchingType.AllWithAll:
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
                case EMatchingType.AllWithAll:
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
                case EMatchingType.AllWithAll:
                    for (int index = countOfExistedKeypoint; index < ComputedDescriptors.Count; index++)
                    //Parallel.For(countOfExistedKeypoint, ComputedDescriptors.Count, index =>
                    {
                        Parallel.For(index + 1, ComputedDescriptors.Count, i =>
                         {
                             FindMatches(matcher, ComputedDescriptors[index], ComputedDescriptors[i]);
                         });

                        ComputedDescriptors[index].Descriptor.Dispose();
                        ComputedDescriptors[index].KeyPoint.DetectedKeyPoints.Dispose();

                        //WindowsFormHelper.AddLogToConsole(string.Format("Memory used before collection:{0:N0}", GC.GetTotalMemory(false)));
                        GC.Collect();
                        //WindowsFormHelper.AddLogToConsole(string.Format("Memory used after full collection:{0:N0}", GC.GetTotalMemory(true)));
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

            //foreach (var node in findedMatches.Skip(index))
            //{
            //    sb.AppendLine(node.FileFormatMatch);
            //    sb.AppendLine();
            //}
            int i = 0;
            int m = 0;
            var directory = new DirectoryInfo(Configuration.TempDrawMatches);
            foreach (var item in directory.GetFiles()) {
                m++;
                var lines = File.ReadAllLines(item.FullName);
                if (int.Parse(lines[2].ToString().Split(' ')[0]) < 400)
                    continue;
                sb.AppendLine(File.ReadAllText(item.FullName));
                sb.AppendLine();
                i++;
            }


            File.WriteAllText(Configuration.MatchFilePath, sb.ToString());
        }

        public static SemaphoreSlim semaphore = new SemaphoreSlim(1);
        public static StringBuilder sb = new StringBuilder();

        //zmenit draw na true
        private int FindMatches(IFeatureMatcher matcher, DescriptorModel leftDescriptor, DescriptorModel rightDescriptor, bool AddToList = true, bool FilterMatches = true, bool ComputeHomography = true, bool SaveInMatchNode = true, bool DrawAndSave = false)
        {
            semaphore.Wait();
            if (File.Exists(Path.Combine(Configuration.TempDrawMatches, $"{leftDescriptor.KeyPoint.InputFile.FileNameWithoutExtension}_{rightDescriptor.KeyPoint.InputFile.FileNameWithoutExtension}.txt")))
            {
                Interlocked.Increment(ref countMatches);
                if (countMatches % 120 == 0)
                    //WindowsFormHelper.AddLogToConsole(
                    //    $"FINISH ({countMatches}) computing matches for: \n" +
                    //    $"\t{leftDescriptor.KeyPoint.InputFile.FileName}\n" +
                    //    $"\t{rightDescriptor.KeyPoint.InputFile.FileName}\n"
                    //    );
                semaphore.Release();
                return 0;
            }

            long matchestime = 0;
            bool filtered = false;
            //var Stopwatch = new Stopwatch();
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
                        //Stopwatch.Reset();
                        //WindowsFormHelper.AddLogToConsole($"Start computing matches for: \n" +
                        // $"\t{leftDescriptor.KeyPoint.InputFile.FileName}\n" +
                        // $"\t{rightDescriptor.KeyPoint.InputFile.FileName}\n");

                        var leftDesc = new Mat(leftDescriptor.Descriptor.Rows > 30000 ? 30000 : leftDescriptor.Descriptor.Rows, leftDescriptor.Descriptor.Cols, Emgu.CV.CvEnum.DepthType.Cv8U, 1);
                        var rightDesc = new Mat(rightDescriptor.Descriptor.Rows > 30000 ? 30000 : rightDescriptor.Descriptor.Rows, rightDescriptor.Descriptor.Cols, Emgu.CV.CvEnum.DepthType.Cv8U, 1);

                        for (int i = 0; i < leftDesc.Rows; i++)
                            for (int j = 0; j < leftDesc.Cols; j++)
                                leftDesc.SetValue(i, j, (byte)leftDescriptor.Descriptor.GetValue(i, j));

                        for (int i = 0; i < rightDesc.Rows; i++)
                            for (int j = 0; j < leftDesc.Cols; j++)
                                rightDesc.SetValue(i, j, (byte)rightDescriptor.Descriptor.GetValue(i, j));

                        //var leftFile = File.ReadAllLines(leftDescriptor.DescriptorFilePath);
                        //var count = int.Parse(leftFile[0].Split(' ')[0]);

                        //var leftDesc = new Mat(count, 32, Emgu.CV.CvEnum.DepthType.Cv8U, 1);
                        //for (int i = 1; i <= count; i++)
                        //    for (int x = 0; x < 32; x++)
                        //        leftDesc.SetValue(i-1,x, byte.Parse(leftFile[i * 2].Split(' ')[x]));

                        //var rightFile = File.ReadAllLines(rightDescriptor.DescriptorFilePath);
                        //count = int.Parse(rightFile[0].Split(' ')[0]);
                        //var rightDesc = new Mat(count, 32, Emgu.CV.CvEnum.DepthType.Cv8U, 1);
                        //for (int i = 1; i <= count; i++)
                        //    for (int x = 0; x < 32; x++)
                        //        rightDesc.SetValue(i - 1, x, byte.Parse(rightFile[i * 2].Split(' ')[x]));

                        //Stopwatch.Start();
                        matcher.Match(leftDesc, rightDesc, matches);
                        //Stopwatch.Stop();
                        //matchestime = Stopwatch.ElapsedMilliseconds;

                        countMatches++;

                        if (countMatches % 120 == 0)
                            WindowsFormHelper.AddLogToConsole(
                                $"FINISH ({countMatches}) computing matches for: \n" +
                                $"\t{leftDescriptor.KeyPoint.InputFile.FileName}\n" +
                                $"\t{rightDescriptor.KeyPoint.InputFile.FileName}\n"
                                );
                        leftDesc.Dispose();
                        rightDesc.Dispose();
                        semaphore.Release();
                    }
                    else
                    {
                        //Stopwatch.Reset();
                        WindowsFormHelper.AddLogToConsole($"Start computing matches for: \n" +
                         $"\t{leftDescriptor.KeyPoint.InputFile.FileName}\n" +
                         $"\t{rightDescriptor.KeyPoint.InputFile.FileName}\n");
                        var leftDesc = new Mat(leftDescriptor.Descriptor.Rows > 30000 ? 30000 : leftDescriptor.Descriptor.Rows, leftDescriptor.Descriptor.Cols, leftDescriptor.Descriptor.Depth, leftDescriptor.Descriptor.NumberOfChannels);
                        var rightDesc = new Mat(rightDescriptor.Descriptor.Rows > 30000 ? 30000 : rightDescriptor.Descriptor.Rows, rightDescriptor.Descriptor.Cols, rightDescriptor.Descriptor.Depth, rightDescriptor.Descriptor.NumberOfChannels);

                        for (int i = 0; i < leftDesc.Rows; i++)
                            leftDescriptor.Descriptor.Row(i).CopyTo(leftDesc.Row(i));
                        for (int i = 0; i < rightDesc.Rows; i++)
                            rightDescriptor.Descriptor.Row(i).CopyTo(rightDesc.Row(i));
                        //Stopwatch.Start();
                        matcher.Match(leftDesc, rightDesc, matches);
                        //Stopwatch.Stop();
                        //matchestime = Stopwatch.ElapsedMilliseconds;

                        leftDesc.Dispose();
                        rightDesc.Dispose();
                        countMatches++;
                    }
                }
                catch (Exception e)
                {
                    sb.AppendLine(Path.Combine(Configuration.TempDrawMatches, $"{leftDescriptor.KeyPoint.InputFile.FileNameWithoutExtension}_{rightDescriptor.KeyPoint.InputFile.FileNameWithoutExtension}.txt"));
                    WindowsFormHelper.AddLogToConsole($"ERROR:\n" +
                        $"{leftDescriptor.KeyPoint.InputFile.FileNameWithoutExtension}_{rightDescriptor.KeyPoint.InputFile.FileNameWithoutExtension}.txt");
                    semaphore.Release();
                    return 0;
                    //throw new Exception($"Happend with {leftDescriptor.KeyPoint.InputFile.FileName}:{leftDescriptor.KeyPoint.DetectedKeyPoints.Size} and {rightDescriptor.KeyPoint.InputFile.FileName}:{rightDescriptor.KeyPoint.DetectedKeyPoints.Size}", e);
                }
                matchesArray = matches.ToArrayOfArray();
            }
            matchesList = matchesArray.ToList();

            //WindowsFormHelper.AddLogToConsole(
            //    $"FINISH ({countMatches}) computing matches for: \n" +
            //    $"\t{leftDescriptor.KeyPoint.InputFile.FileName}\n" +
            //    $"\t{rightDescriptor.KeyPoint.InputFile.FileName}\n"
            //    );

            // Stopwatch.Reset();
            //Stopwatch.Start();
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
                    filtered = true;
                    perspectiveMatrix = FindHomography(leftDescriptor.KeyPoint.DetectedKeyPoints, rightDescriptor.KeyPoint.DetectedKeyPoints, FilterMatches ? filteredMatchesList : matchesList, mask);
                }
            }
            //Stopwatch.Stop();


            //MatchTable.Columns.Add("DateTime");
            //MatchTable.Columns.Add("Left image");
            //MatchTable.Columns.Add("Right image");
            //MatchTable.Columns.Add("Count of left descriptors");
            //MatchTable.Columns.Add("Count of right descriptors");
            //MatchTable.Columns.Add("Founded matches");
            //MatchTable.Columns.Add("Filtered matches");
            //MatchTable.Columns.Add("Algorithm");
            //MatchTable.Columns.Add("Time to generate");
            //MatchTable.Columns.Add("Time to filtered");
            //MatchTable.Columns.Add("Filtered");
            //MatchTable.Rows.Add($"{DateTime.Now }",$"{MatchingType}", $"{leftDescriptor.KeyPoint.InputFile.FileName }", $"{rightDescriptor.KeyPoint.InputFile.FileName }", $"{leftDescriptor.Descriptor.Size.Width }", $"{leftDescriptor.Descriptor.Size.Height }", $"{rightDescriptor.Descriptor.Size.Width }", $"{rightDescriptor.Descriptor.Size.Height }", $"{matchesList.Count }", $"{filteredMatchesList.Count }", $"{matcher.GetType().Name }", $"{matchestime }", $"{Stopwatch.ElapsedMilliseconds }", $"{filtered}");

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

            //if (DrawAndSave)
            //    foundedMatch.DrawAndSave(fileManager);

            if (SaveInMatchNode)
                foundedMatch.SaveMatchString();

            File.WriteAllText(Path.Combine(Configuration.TempDrawMatches, $"{leftDescriptor.KeyPoint.InputFile.FileNameWithoutExtension}_{rightDescriptor.KeyPoint.InputFile.FileNameWithoutExtension}.txt"), foundedMatch.FileFormatMatch);
            //if (AddToList)
            //    FoundedMatches.Add(foundedMatch);

            // Dispose

            filteredMatchesList = new List<MDMatch[]>();
            filteredMatchesList.Clear();
            matchesList.Clear();
            perspectiveMatrix.Dispose();
            mask.Dispose();
            if (foundedMatch.Mask != null)
                foundedMatch.Mask.Dispose();
            if (foundedMatch.PerspectiveMatrix != null)
                foundedMatch.PerspectiveMatrix.Dispose();
            foundedMatch = null;

            //Console.WriteLine("Memory used before collection:{0:N0}", GC.GetTotalMemory(false));
            //GC.Collect();
            //Console.WriteLine("Memory used after full collection:{0:N0}", GC.GetTotalMemory(true));

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

        private void ComputeDescriptor(KeyPointModel keypoint, IFeatureDescriptor descriptor, bool AddToList = true, bool SaveOnDisk = true)
        {
            try
            {
                var fileName = keypoint.InputFile.FileName;

                var Stopwatch = new Stopwatch();

                WindowsFormHelper.AddLogToConsole($"Start computing descriptor for: {fileName}\n");
                Stopwatch.Reset();
                Stopwatch.Start();
                var computedDescriptor = descriptor.ComputeDescriptor(keypoint);
                Stopwatch.Stop();

                //DescriptorTable.Columns.Add("DateTime");
                //DescriptorTable.Columns.Add("Image name");
                //DescriptorTable.Columns.Add("Count of keypoints");
                //DescriptorTable.Columns.Add("Count of descriptors width");
                //DescriptorTable.Columns.Add("Count of descriptors hegiht");
                //DescriptorTable.Columns.Add("Algorithm");
                //DescriptorTable.Columns.Add("Time to generate");
                //DescriptorTable.Rows.Add($"{DateTime.Now}", $"{keypoint.InputFile.FileName}", $"{keypoint.DetectedKeyPoints.Size}", $"{computedDescriptor.Size.Width}", $"{computedDescriptor.Size.Height}", $"{descriptor.GetType().Name}", $"{Stopwatch.ElapsedMilliseconds}");

                var descriptorNode = new DescriptorModel(keypoint, computedDescriptor);

                WindowsFormHelper.AddLogToConsole($"FINISH computing descriptor for: {fileName}\n");

                if (AddToList)
                    ComputedDescriptors.Add(keypoint.ID, descriptorNode);

                if (SaveOnDisk)
                 descriptorNode.SaveSiftFile(true,false);

                var fileNameDes = $"{descriptorNode.KeyPoint.InputFile.FileNameWithoutExtension}.SIFT";
                var descriptorSavePath = Path.Combine(Configuration.TempDirectoryPath, fileNameDes);
                descriptorNode.DescriptorFilePath = descriptorSavePath;

                //computedDescriptor.Dispose();
            }
            catch (Exception e)
            {
                WindowsFormHelper.AddLogToConsole("Error\n");
            }
        }

        //zmenit draw na true
        private void FindKeypoint(int ID, InputFileModel inputFile, IFeatureDetector detector, bool AddToList = true, bool DrawAndSave = false)
        {
            try
            {
                var fileName = inputFile.FileName;

                var Stopwatch = new Stopwatch();
                WindowsFormHelper.AddLogToConsole($"Start finding key points for: {fileName}\n");
                Stopwatch.Reset();
                Stopwatch.Start();
                var image = new Mat(inputFile.FullPath);
                var detectedKeyPoints = detector.DetectKeyPoints(image);
                Stopwatch.Stop();
                //KeyPointTable.Columns.Add("DateTime");
                //KeyPointTable.Columns.Add("Image name");
                //KeyPointTable.Columns.Add("Resolution width");
                //KeyPointTable.Columns.Add("Resolution height");
                //KeyPointTable.Columns.Add("Count of keypoints");
                //KeyPointTable.Columns.Add("Algorithm");
                //KeyPointTable.Columns.Add("Time to generate");
                //KeyPointTable.Rows.Add($"{DateTime.Now}", $"{inputFile.FileName}", $"{inputFile.Image.Size.Width}", $"{inputFile.Image.Size.Height}", $"{detectedKeyPoints.Length}", $"{detector.GetType().Name}", $"{Stopwatch.ElapsedMilliseconds}");
                WindowsFormHelper.AddLogToConsole(
                    $"FINISH finding key points for: {fileName}\n" +
                    $"Count of key points: {detectedKeyPoints.Length}\n"
                    );

                var newItem = new KeyPointModel(
                    new VectorOfKeyPoint(detectedKeyPoints),
                    inputFile,
                    ID
                    );

                if (AddToList)
                    DetectedKeyPoints.Add(ID, newItem);

                //if (DrawAndSave)
                //    newItem.DrawAndSave(fileManager);

                image.Dispose();
            }
            catch (Exception e)
            {
                WindowsFormHelper.AddLogToConsole("Error\n");
            }
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
