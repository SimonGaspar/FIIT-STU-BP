using Bachelor_app.Enumerate;
using Bachelor_app.Extension;
using Bachelor_app.Manager;
using Bachelor_app.StereoVision.Calibration;
using Bachelor_app.StereoVision.StereoCorrespondence;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace Bachelor_app.StereoVision
{
    public class StereoVisionManager
    {
        private IStereoSolver StereoSolver;
        private FileManager _fileManager;
        private CameraManager _cameraManager;

        public bool StopStereoCorrespondence { get; set; }
        public bool UseParallel { get; set; }

        public StereoVisionManager(FileManager fileManager, CameraManager cameraManager)
        {
            this._fileManager = fileManager;
            this._cameraManager = cameraManager;
        }

        /// <summary>
        /// Setting algorithm for stereo correspondence
        /// </summary>
        /// <param name="item">Algorithm</param>
        public void SetStereoCorrespondenceAlgorithm(EStereoCorrespondenceAlgorithm item)
        {
            IStereoSolver resultItem = null;
            switch (item)
            {
                case EStereoCorrespondenceAlgorithm.CudaStereoBM: resultItem = new CudaStereoBlockMatching(); break;
                case EStereoCorrespondenceAlgorithm.StereoBM: resultItem = new StereoBlockMatching(); break;
                case EStereoCorrespondenceAlgorithm.StereoSGBM: resultItem = new StereoSemiGlobalBlockMatching(); break;
                case EStereoCorrespondenceAlgorithm.CudaStereoConstantSpaceBP: resultItem = new CudaStereoConstantSpaceBeliefPropagation(); break;
                default: throw new NotImplementedException();
            }

            StereoSolver = resultItem;
        }

        /// <summary>
        /// Start computing depth map
        /// </summary>
        public void ComputeStereoCorrespondence()
        {
            StopStereoCorrespondence = false;

            switch (_fileManager._inputType)
            {
                case EInput.ConnectedStereoCamera: ComputeStereoCorrespondenceFromConnectedStereoCamera(); break;
                case EInput.ListViewBasicStack: ComputeStereoCorrespondenceFromStack(); break;
                default: throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Start computing depth map with image from stereo camera.
        /// </summary>
        private void ComputeStereoCorrespondenceFromConnectedStereoCamera()
        {
            while (!StopStereoCorrespondence)
            {
                var listOfInput = _cameraManager.GetInputFromStereoCamera(false, _fileManager.ListViewModel.ListOfListInputFolder[(int)EListViewGroup.LeftCameraStack].Count);

                using (Image<Bgr, byte> leftImage = new Mat(listOfInput[0].FullPath).ToImageBGR(), rightImage = new Mat(listOfInput[1].FullPath).ToImageBGR())
                {
                    if (CalibrationModel.IsCalibrated)
                    {
                        CvInvoke.Remap(leftImage, leftImage, CalibrationModel.UndistortCam1.MapX, CalibrationModel.UndistortCam1.MapY, Inter.Linear);
                        CvInvoke.Remap(rightImage, rightImage, CalibrationModel.UndistortCam2.MapX, CalibrationModel.UndistortCam2.MapY, Inter.Linear);
                    }
                    using (Mat DepthMap = StereoSolver.ComputeDepthMap(leftImage, rightImage), DepthMapToSave = new Mat())
                    {
                        DepthMap.ConvertTo(DepthMapToSave, DepthType.Cv8U);

                        _fileManager.ListViewModel._lastDepthMapImage = new Image<Bgr, byte>(DepthMapToSave.Bitmap);
                        var index = _fileManager.ListViewModel.LeftCameraStack.Count;

                        SaveAndAddDepthMapToListView(new Image<Bgr, byte>(DepthMapToSave.Bitmap), index);
                        Computer3DPointsFromStereoPair(DepthMap, index);
                    }
                }
            }
        }

        /// <summary>
        /// Save generated depth map
        /// </summary>
        /// <param name="disparityMap">Disparity map</param>
        private void SaveAndAddDepthMapToListView(Image<Bgr, byte> disparityMap, int index)
        {
            var path = Path.Combine(Configuration.TempDepthMapDirectoryPath, $"DepthMap_{index}.JPG");

            disparityMap.Save(path);
            disparityMap.Dispose();

            _fileManager.AddInputFileToList(path, EListViewGroup.DepthMap);
        }

        /// <summary>
        /// Start computing depth map from stack
        /// </summary>
        public void ComputeStereoCorrespondenceFromStack()
        {
            if (UseParallel)
            {
                ComputeStereoCorrespondenceFromStackParallel();
            }
            else
            {
                ComputeStereoCorrespondenceFromStackSequel();
            }
        }

        /// <summary>
        /// Computing depth map from stack (SEQUEL)
        /// </summary>
        private void ComputeStereoCorrespondenceFromStackSequel()
        {
            for (int i = 0; i < _fileManager.ListViewModel.LeftCameraStack.Count; i++)
            {
                using (Image<Bgr, byte> leftImage = new Image<Bgr, byte>((Bitmap)_fileManager.ListViewModel.LeftCameraStack[i].Image), rightImage = new Image<Bgr, byte>((Bitmap)_fileManager.ListViewModel.RightCameraStack[i].Image))
                {
                    if (CalibrationModel.IsCalibrated)
                    {
                        CvInvoke.Remap(leftImage, leftImage, CalibrationModel.UndistortCam1.MapX, CalibrationModel.UndistortCam1.MapY, Inter.Linear);
                        CvInvoke.Remap(rightImage, rightImage, CalibrationModel.UndistortCam2.MapX, CalibrationModel.UndistortCam2.MapY, Inter.Linear);
                    }

                    using (Mat DepthMap = StereoSolver.ComputeDepthMap(leftImage, rightImage), DepthMapToSave = new Mat())
                    {
                        DepthMap.ConvertTo(DepthMapToSave, DepthType.Cv8U);
                        _fileManager.ListViewModel._lastDepthMapImage = DepthMapToSave.ToImageBGR();

                        SaveAndAddDepthMapToListView(DepthMapToSave.ToImageBGR(), i);
                        Computer3DPointsFromStereoPair(DepthMap, i);
                    }
                }
            }
        }

        /// <summary>
        /// Computing depth map from stack (PARALLEL)
        /// </summary>
        private void ComputeStereoCorrespondenceFromStackParallel()
        {
            Parallel.For(0, _fileManager.ListViewModel.LeftCameraStack.Count, i =>
            {
                var leftImage = new Image<Bgr, byte>((Bitmap)_fileManager.ListViewModel.LeftCameraStack[i].Image);
                var rightImage = new Image<Bgr, byte>((Bitmap)_fileManager.ListViewModel.RightCameraStack[i].Image);

                if (CalibrationModel.IsCalibrated)
                {
                    CvInvoke.Remap(leftImage, leftImage, CalibrationModel.UndistortCam1.MapX, CalibrationModel.UndistortCam1.MapY, Inter.Linear);
                    CvInvoke.Remap(rightImage, rightImage, CalibrationModel.UndistortCam2.MapX, CalibrationModel.UndistortCam2.MapY, Inter.Linear);
                }

                var DepthMap = StereoSolver.ComputeDepthMap(leftImage, rightImage);

                var DepthMapToSave = new Mat();
                DepthMap.ConvertTo(DepthMapToSave, DepthType.Cv8U);

                _fileManager.ListViewModel._lastDepthMapImage = new Image<Bgr, byte>(DepthMapToSave.Bitmap);

                SaveAndAddDepthMapToListView(new Image<Bgr, byte>(DepthMapToSave.Bitmap), i);
                Computer3DPointsFromStereoPair(DepthMap, i);
            }
            );
        }

        /// <summary>
        /// Show setting form
        /// </summary>
        public void ShowSettingForStereoSolver()
        {
            StereoSolver.ShowSettingForm();
        }

        /// <summary>
        /// Compute 3D points from disparity map
        /// </summary>
        /// <param name="disparityMap">Disparity map</param>
        /// <returns></returns>
        private async Task Computer3DPointsFromStereoPair(Mat disparityMap, int index)
        {
            if (CalibrationModel.IsCalibrated)
            {
                var path = Path.Combine(Configuration.TempDepthMapDirectoryPath, $"DepthMap_{index}.json");

                MCvPoint3D32f[] points = PointCollection.ReprojectImageTo3D(disparityMap, CalibrationModel.Q);
                var json = JsonConvert.SerializeObject(points);
                File.WriteAllText(path, json);
            }
        }

        /// <summary>
        /// Show calibration WinForm
        /// </summary>
        public void ShowCalibration()
        {
            new CalibrationManager(_cameraManager);
        }
    }
}

