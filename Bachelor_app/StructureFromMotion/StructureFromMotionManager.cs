using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bachelor_app.Enumerate;
using Bachelor_app.Manager;
using Bakalárska_práca.Enumerate;
using Bakalárska_práca.Extension;
using Bakalárska_práca.Helper;
using Bakalárska_práca.Manager;
using Bakalárska_práca.Model;
using Bakalárska_práca.StructureFromMotion;
using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using static Emgu.CV.Features2D.Features2DToolbox;

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
        public EMatchingType _matchingType;
        public bool _useParallel = false;
        float ms_MAX_DIST = 0, ms_MIN_DIST = float.MaxValue;
        int countInputFile = 0;
        int countCameraInput = 0;

        private SortedList<int, KeyPointModel> DetectedKeyPoints;
        private SortedList<int, DescriptorModel> ComputedDescriptors;
        private List<MatchModel> FoundedMatches;

        private FileManager fileManager;
        private DisplayManager displayManager;
        private MainForm _winForm;
        private CameraManager cameraManager;
        public bool stopSFM = false;
        private static readonly object locker = new object();

        public SfM(FileManager fileManager, DisplayManager displayManager, MainForm winForm,CameraManager cameraManager)
        {

            Directory.CreateDirectory(tempDirectory);
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
            var list = ContinueSFM ? fileManager.listViewerModel.BasicStack.Where(x => x.UseInSFM == false).ToList() : fileManager.listViewerModel.BasicStack;

            if (!ContinueSFM)
                DeleteAllInPath(tempDirectory);

            foreach (var node in list)
            {
                File.Copy(node.fileInfo.FullName, Path.Combine(tempDirectory, node.fileInfo.Name), true);
                node.UseInSFM = true;
            }

            if (ContinueSFM)
                ContinueInComputingSFM(_detector, _descriptor, _matcher, list);
            else
                ComputeSfM(_detector, _descriptor, _matcher, list);
        }

        private void DeleteAllInPath(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }

        public void ComputeSfM(IFeatureDetector detector, IFeatureDescriptor descriptor, IFeatureMatcher matcher, List<InputFileModel> listOfInput)
        {
            countInputFile = 0;
            DetectedKeyPoints.Clear();
            ComputedDescriptors.Clear();
            FoundedMatches.Clear();

            switch (fileManager._inputType) {
                case EInput.ListView:
                    StartDetectingKeyPoint(0, listOfInput, detector);
                    StartComputingDescriptor(0, descriptor);
                    StartMatching(0, matcher);
                    break;
                case EInput.ConnectedStereoCamera:

                    countInputFile = DetectedKeyPoints.Count;
                    listOfInput = GetInputFromStereoCamera(countInputFile);


                    StartDetectingKeyPoint(countInputFile, listOfInput, detector);
                    StartComputingDescriptor(countInputFile, descriptor);
                    while (!stopSFM)
                    {
                        
                        countInputFile = DetectedKeyPoints.Count;
                        listOfInput = GetInputFromStereoCamera(countInputFile);


                        StartDetectingKeyPoint(countInputFile, listOfInput, detector);
                        StartComputingDescriptor(countInputFile, descriptor);
                        StartStereoMatching(countInputFile, matcher);
                    }
                    break;
            }

            WriteAllMatches(FoundedMatches);
            RunVisualSFM();
        }

        private void StartStereoMatching(int countOfExistedKeypoint, IFeatureMatcher matcher)
        {
            if (_useParallel)
                StartStereoParallelMatching(countOfExistedKeypoint, matcher);
            else
                StartStereoSequenceMatching(countOfExistedKeypoint, matcher);
        }

        private void StartStereoSequenceMatching(int countOfExistedKeypoint, IFeatureMatcher matcher)
        {
            FindMatches(matcher, ComputedDescriptors[countOfExistedKeypoint-2], ComputedDescriptors[countOfExistedKeypoint-1]);

            switch (_matchingType)
            {
                case EMatchingType.OnePrevious:
                    for (int m = countOfExistedKeypoint; m < ComputedDescriptors.Count; m++)
                        for (int n = m - (1*2); n < m; n+=2)
                        {
                            if(n >= 0)
                            FindMatches(matcher, ComputedDescriptors[m], ComputedDescriptors[n]);
                        }
                    break;
                case EMatchingType.TwoPrevious:
                    for (int m = countOfExistedKeypoint; m < ComputedDescriptors.Count; m++)
                        for (int n = m - (2*2); n < m; n+=2)
                        {
                            if(n >= 0)
                            FindMatches(matcher, ComputedDescriptors[m], ComputedDescriptors[n]);
                        }
                    break;
                case EMatchingType.AllWithAll:
                    throw new NotImplementedException();
                    for (int m = countOfExistedKeypoint; m < ComputedDescriptors.Count; m++)
                        for (int n = m + 1; n < ComputedDescriptors.Count; n++)
                            FindMatches(matcher, ComputedDescriptors[m], ComputedDescriptors[n]);
                    break;
            }
        }

        private void StartStereoParallelMatching(int countOfExistedKeypoint, IFeatureMatcher matcher)
        {
            FindMatches(matcher, ComputedDescriptors[countOfExistedKeypoint - 2], ComputedDescriptors[countOfExistedKeypoint - 1]);

            switch (_matchingType)
            {
                case EMatchingType.OnePrevious:
                    Parallel.For(countOfExistedKeypoint, ComputedDescriptors.Count, index =>
                    {
                        if (index >= 1*2)
                        {
                            Parallel.For(index - (1*2), index, i =>
                            {
                                if((index-i) %2 ==0)
                                FindMatches(matcher, ComputedDescriptors[index], ComputedDescriptors[i]);
                            });
                        }
                    });
                    break;
                case EMatchingType.TwoPrevious:
                    Parallel.For(countOfExistedKeypoint, ComputedDescriptors.Count, index =>
                    {
                        if (index >= 2*2)
                        {
                            Parallel.For(index - (2*2), index, i =>
                            {
                                if ((index - i) % 2 == 0)
                                    FindMatches(matcher, ComputedDescriptors[index], ComputedDescriptors[i]);
                            });
                        }
                    });
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

        private List<InputFileModel> GetInputFromStereoCamera(int countInputFile)
        {
            cameraManager.LeftCamera.camera.Grab();
            cameraManager.RightCamera.camera.Grab();
            Mat LeftImage = new Mat();
            Mat RightImage = new Mat();
            cameraManager.LeftCamera.camera.Retrieve(LeftImage);
            cameraManager.LeftCamera.camera.Retrieve(RightImage);
            LeftImage.Save(Path.Combine($@"{tempDirectory}", $"Left_{countInputFile}.JPG"));
            RightImage.Save(Path.Combine($@"{tempDirectory}", $"Right_{countInputFile}.JPG"));


            var inputFileLeft = new InputFileModel(Path.Combine($@"{tempDirectory}", $"Left_{countInputFile}.JPG"));
            var imageList = _winForm.ImageList[(int)EListViewGroup.LeftCameraStack];
            var listViewer = _winForm.ListViews[(int)EListViewGroup.LeftCameraStack];
            fileManager.AddInputFileToList(inputFileLeft, fileManager.listViewerModel.ListOfListInputFolder[(int)EListViewGroup.LeftCameraStack], imageList, listViewer);

            var inputFileRight = new InputFileModel(Path.Combine($@"{tempDirectory}", $"Right_{countInputFile}.JPG"));
            imageList = _winForm.ImageList[(int)EListViewGroup.RightCameraStack];
            listViewer = _winForm.ListViews[(int)EListViewGroup.RightCameraStack];
            fileManager.AddInputFileToList(inputFileLeft, fileManager.listViewerModel.ListOfListInputFolder[(int)EListViewGroup.RightCameraStack], imageList, listViewer);

            var returnList = new List<InputFileModel>();
            returnList.Add(inputFileLeft);
            returnList.Add(inputFileRight);

            return returnList;
        }

        private void StartMatching(int countOfExistedKeypoint, IFeatureMatcher matcher)
        {
            if(_useParallel)
            StartParallelMatching(countOfExistedKeypoint, matcher);
            else
            StartSequenceMatching(countOfExistedKeypoint, matcher);
        }

        private void StartSequenceMatching(int countOfExistedKeypoint, IFeatureMatcher matcher)
        {
            switch (_matchingType)
            {
                case EMatchingType.OnePrevious:
                    for (int m = countOfExistedKeypoint; m < ComputedDescriptors.Count; m++)
                        for (int n = m - 1; n < m && n >= 0; n++)
                        {
                            FindMatches(matcher, ComputedDescriptors[m], ComputedDescriptors[n]);
                        }
                    break;
                case EMatchingType.TwoPrevious:
                    for (int m = countOfExistedKeypoint; m < ComputedDescriptors.Count; m++)
                        for (int n = m - 2; n < m && n >= 0; n++)
                        {
                            FindMatches(matcher, ComputedDescriptors[m], ComputedDescriptors[n]);
                        }
                    break;
                case EMatchingType.AllWithAll:
                    throw new NotImplementedException();
                    for (int m = countOfExistedKeypoint; m < ComputedDescriptors.Count; m++)
                        for (int n = m + 1; n < ComputedDescriptors.Count; n++)
                            FindMatches(matcher, ComputedDescriptors[m], ComputedDescriptors[n]);
                    break;
            }
        }

        private void StartParallelMatching(int countOfExistedKeypoint, IFeatureMatcher matcher)
        {
            switch (_matchingType) {
                case EMatchingType.OnePrevious:
                    Parallel.For(countOfExistedKeypoint, ComputedDescriptors.Count, index =>
                    {
                        if (index >= 1)
                        {
                            Parallel.For(index - 1, index, i =>
                            {
                                FindMatches(matcher, ComputedDescriptors[index], ComputedDescriptors[i]);
                            });
                        }
                    });
                    break;
                case EMatchingType.TwoPrevious:
                    Parallel.For(countOfExistedKeypoint, ComputedDescriptors.Count, index =>
                    {
                        if (index >= 2)
                        {
                            Parallel.For(index - 2, index, i =>
                            {
                                FindMatches(matcher, ComputedDescriptors[index], ComputedDescriptors[i]);
                            });
                        }
                    });
                    break;
                case EMatchingType.AllWithAll:
                    throw new NotImplementedException();
                    Parallel.For(countOfExistedKeypoint, ComputedDescriptors.Count, index =>
                    {
                        Parallel.For(index +1, ComputedDescriptors.Count, i =>
                        {
                            FindMatches(matcher, ComputedDescriptors[index], ComputedDescriptors[i]);
                        });
                    });
                    break;
            }
        }

        private void StartDetectingKeyPoint(int countOfInput, List<InputFileModel> listOfInput, IFeatureDetector detector)
        {
            _winForm.SetMaximumProgressBar("Detecting keypoints",listOfInput.Count - countInputFile);

            if(_useParallel)
            Parallel.For(0, listOfInput.Count, x => { FindKeypoint(countOfInput + x, listOfInput[x], detector); });
            else
                for(int i=0;i< listOfInput.Count;i++)
                    FindKeypoint(countOfInput + i, listOfInput[i], detector);

            _winForm.IncrementValueProgressBar();
        }

        public void ContinueInComputingSFM(IFeatureDetector detector, IFeatureDescriptor descriptor, IFeatureMatcher matcher, List<InputFileModel> listOfInput)
        {
            var iterMatches = FoundedMatches.Count;
            countInputFile = DetectedKeyPoints.Count;

            StartDetectingKeyPoint(countInputFile, listOfInput, detector);
            StartComputingDescriptor(countInputFile, descriptor);
            StartMatching(countInputFile, matcher);

            WriteAddedImages(listOfInput);
            AppendMatches(FoundedMatches, iterMatches);
            ContinueVisualSFM();
        }

        private void StartComputingDescriptor(int countOfExistedKeyPoint, IFeatureDescriptor descriptor)
        {
            _winForm.SetMaximumProgressBar("Computing descriptors",DetectedKeyPoints.Count - countInputFile);

            if (_useParallel)
                Parallel.For(countOfExistedKeyPoint, DetectedKeyPoints.Count, x => ComputeDescriptor(DetectedKeyPoints[x], descriptor));
            else
                for (int i = countOfExistedKeyPoint; i < DetectedKeyPoints.Count; i++)
                    ComputeDescriptor(DetectedKeyPoints[i], descriptor);
            _winForm.IncrementValueProgressBar();
        }

        private void WriteAddedImages(List<InputFileModel> listOfInput)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var node in listOfInput)
            {
                sb.AppendLine(Path.Combine(tempDirectory, node.fileInfo.Name));
            }

            File.WriteAllText(Path.Combine(tempDirectory, "Result.nvm.txt"), sb.ToString());
        }

        private void AppendMatches(List<MatchModel> findedMatches, int index)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var node in findedMatches.Skip(index))
            {
                sb.AppendLine(node.FileFormatMatch);
                sb.AppendLine();
            }

            File.WriteAllText(Path.Combine(tempDirectory, matchFileName), sb.ToString());
        }

        private void WriteAllMatches(List<MatchModel> findedMatches)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var node in findedMatches)
            {
                if (string.IsNullOrEmpty(node.FileFormatMatch)) continue;
                sb.AppendLine(node.FileFormatMatch);
                sb.AppendLine();
            }

            File.WriteAllText(Path.Combine(tempDirectory, matchFileName), sb.ToString());
        }

        private void FindMatches(IFeatureMatcher matcher, DescriptorModel leftDescriptor, DescriptorModel rightDescriptor, bool AddToList = true, bool FilterMatches = true, bool ComputeHomography = true, bool SaveInMatchNode = true)
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

            // Save drawing image
            Mat output = new Mat();
            Directory.CreateDirectory($@"{tempDirectory}\DrawMatches");
            Features2DToolbox.DrawMatches(new Mat(foundedMatch.LeftDescriptor.KeyPoint.InputFile.fileInfo.FullName), foundedMatch.LeftDescriptor.KeyPoint.DetectedKeyPoints, new Mat(foundedMatch.RightDescriptor.KeyPoint.InputFile.fileInfo.FullName), foundedMatch.RightDescriptor.KeyPoint.DetectedKeyPoints, new VectorOfVectorOfDMatch(foundedMatch.FilteredMatchesList.ToArray()), output, new MCvScalar(0, 0, 255), new MCvScalar(0, 255, 0), foundedMatch.Mask);
            output.Save(Path.Combine($@"{tempDirectory}\DrawMatches", $"{Path.GetFileNameWithoutExtension(foundedMatch.RightDescriptor.KeyPoint.InputFile.fileInfo.Name)}-{Path.GetFileNameWithoutExtension(foundedMatch.LeftDescriptor.KeyPoint.InputFile.fileInfo.Name)}.JPG"));
            fileManager.listViewerModel._lastDrawnMatches = new Image<Bgr, byte>(output.Bitmap);

            var inputFile = new InputFileModel(Path.Combine($@"{tempDirectory}\DrawMatches", $"{Path.GetFileNameWithoutExtension(foundedMatch.RightDescriptor.KeyPoint.InputFile.fileInfo.Name)}-{Path.GetFileNameWithoutExtension(foundedMatch.LeftDescriptor.KeyPoint.InputFile.fileInfo.Name)}.JPG"));
            var imageList = _winForm.ImageList[(int)EListViewGroup.DrawnMatches];
            var listViewer = _winForm.ListViews[(int)EListViewGroup.DrawnMatches];

            fileManager.AddInputFileToList(inputFile, fileManager.listViewerModel.ListOfListInputFolder[(int)EListViewGroup.DrawnMatches], imageList, listViewer);

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
                File.WriteAllText(Path.Combine(tempDirectory, fileName), sb.ToString());

            if (SaveInDescriptorNode)
                Descriptor.FileFormatSIFT = sb.ToString();
        }

        private void FindKeypoint(int ID, InputFileModel inputFile, IFeatureDetector detector, bool AddToList = true)
        {
            WindowsFormHelper.AddLogToConsole($"Start finding key points for: {inputFile.fileInfo.Name.ToString()}\n");

            var detectedKeyPoints = detector.DetectKeyPoints(new Mat(inputFile.fileInfo.FullName));

            if (AddToList)
                DetectedKeyPoints.Add(ID, new KeyPointModel()
                {
                    DetectedKeyPoints = new VectorOfKeyPoint(detectedKeyPoints),
                    InputFile = inputFile,
                    ID = ID
                }
                );

            WindowsFormHelper.AddLogToConsole($"FINISH finding key points for: {inputFile.fileInfo.Name.ToString()}\n");


            // Save drawing image
            Mat output = new Mat();
            Directory.CreateDirectory($@"{tempDirectory}\DrawKeypoint");
            Features2DToolbox.DrawKeypoints(new Mat(inputFile.fileInfo.FullName), new VectorOfKeyPoint(detectedKeyPoints), output, new Bgr(0, 0, 255), KeypointDrawType.DrawRichKeypoints);
            output.Save(Path.Combine($@"{tempDirectory}\DrawKeypoint", $"{Path.GetFileNameWithoutExtension(inputFile.fileInfo.Name)}.JPG"));
            fileManager.listViewerModel._lastDrawnKeypoint = new Image<Bgr, byte>(output.Bitmap);

            var file = new InputFileModel(Path.Combine($@"{tempDirectory}\DrawKeypoint", $"{Path.GetFileNameWithoutExtension(inputFile.fileInfo.Name)}.JPG"));
            var imageList = _winForm.ImageList[(int)EListViewGroup.DrawnKeyPoint];
            var listViewer = _winForm.ListViews[(int)EListViewGroup.DrawnKeyPoint];

            fileManager.AddInputFileToList(file, fileManager.listViewerModel.ListOfListInputFolder[(int)EListViewGroup.DrawnKeyPoint], imageList, listViewer);
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
            ProcessStartInfo startInfo = new ProcessStartInfo(Path.Combine(pathVisualSFM, "VisualSFM.exe"))
            {
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                UseShellExecute = false
            };
            startInfo.Arguments = $"sfm+import {tempDirectory} {Path.Combine(tempDirectory, "Result.nvm")} {Path.Combine(tempDirectory, "AllFoundedMatches.txt")}";
            Process process = Process.Start(startInfo);

            while (!process.StandardOutput.EndOfStream)
            {
                var line = process.StandardOutput.ReadLine();
                if (string.IsNullOrEmpty(line)) continue;
                _winForm.richTextBox1.Invoke((Action)delegate
                {
                    _winForm.richTextBox1.Text += line + "\n";
                });
            }

            process.WaitForExit();

            //displayManager.LeftViewWindowItem = Enumerate.EDisplayItem.PointCloud;
            //displayManager.Display();
        }

        public void ContinueVisualSFM()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(Path.Combine(pathVisualSFM, "VisualSFM.exe"))
            {
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                UseShellExecute = false
            };
            startInfo.Arguments = $"sfm+import+resume {Path.Combine(tempDirectory, "Result.nvm")} {Path.Combine(tempDirectory, "Result.nvm")} {Path.Combine(tempDirectory, "AllFoundedMatches.txt")}";
            Process process = Process.Start(startInfo);
            while (!process.StandardOutput.EndOfStream)
            {
                var line = process.StandardOutput.ReadLine();
                _winForm.richTextBox1.Invoke((Action)delegate
                {
                    _winForm.richTextBox1.Text += line + "\n";
                });
            }
            process.WaitForExit();
            // Poriesit ako citat point cloud, ked sa prepise
            //displayManager.LeftViewWindowItem = Enumerate.EDisplayItem.PointCloud;
            //displayManager.Display();
        }
    }
}
