using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Bachelor_app;
using Bachelor_app.Enumerate;
using Bachelor_app.Manager;
using Bachelor_app.StereoVision;
using Bakalárska_práca.Enumerate;
using Bakalárska_práca.Extension;
using Bakalárska_práca.Manager;
using Bakalárska_práca.StereoVision.StereoCorrespondence;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Bakalárska_práca.StereoVision
{
    public class StereoVisionManager
    {
        private IStereoSolver StereoSolver;
        private FileManager _fileManager;
        private DisplayManager _displayManager;
        private CameraManager _cameraManager;
        public CalibrationManager _calibrationManager;
        private MainForm _winForm;

        public bool stopStereoCorrespondence = false;
        public bool _useParallel = false;

        public StereoVisionManager(FileManager fileManager, DisplayManager displayManager, CameraManager cameraManager, MainForm mainForm)
        {
            this._fileManager = fileManager;
            this._displayManager = displayManager;
            this._cameraManager = cameraManager;
            this._winForm = mainForm;
        }

        /// <summary>
        /// Setting algorithm for stereo correspondence
        /// </summary>
        /// <param name="item">Algorithm</param>
        public void SetStereoCorrespondenceAlgorithm(EStereoCorrespondenceAlgorithm item)
        {
            switch (item)
            {
                case EStereoCorrespondenceAlgorithm.CudaStereoBM: StereoSolver = new CudaStereoBlockMatching(); break;
                case EStereoCorrespondenceAlgorithm.StereoBM: StereoSolver = new StereoBlockMatching(); break;
                case EStereoCorrespondenceAlgorithm.StereoSGBM: StereoSolver = new StereoSemiGlobalBlockMatching(); break;
                case EStereoCorrespondenceAlgorithm.CudaStereoConstantSpaceBP: StereoSolver = new CudaStereoConstantSpaceBeliefPropagation(); break;
                default: throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Start computing depth map
        /// </summary>
        public void ComputeStereoCorrespondence()
        {
            stopStereoCorrespondence = false;

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
            while (!stopStereoCorrespondence)
            {
                var listOfInput = _cameraManager.GetInputFromStereoCamera(false,_fileManager.listViewerModel.ListOfListInputFolder[(int)EListViewGroup.LeftCameraStack].Count);

                var leftImage = new Mat(listOfInput[0].fileInfo.FullName).Image2ImageBGR();
                var rightImage = new Mat(listOfInput[1].fileInfo.FullName).Image2ImageBGR();

                if (_calibrationManager != null && _calibrationManager.calibrationModel != null)
                {
                    CvInvoke.Remap(leftImage, leftImage, _calibrationManager.calibrationModel.UndistortCam1.MapX, _calibrationManager.calibrationModel.UndistortCam1.MapY, Inter.Linear);
                    CvInvoke.Remap(rightImage, rightImage, _calibrationManager.calibrationModel.UndistortCam2.MapX, _calibrationManager.calibrationModel.UndistortCam2.MapY, Inter.Linear);
                }

                var DepthMap = StereoSolver.ComputeDepthMap(leftImage, rightImage);
                var DepthMapToSave = new Mat();
                DepthMap.ConvertTo(DepthMapToSave, DepthType.Cv8U);

                _fileManager.listViewerModel._lastDepthMapImage = new Image<Bgr, byte>(DepthMapToSave.Bitmap);

                SaveAndAddDepthMapToListView(new Image<Bgr, byte>(DepthMapToSave.Bitmap));
                Computer3DPointsFromStereoPair(DepthMap);

                //Vymazat ak sa nic nenakresli
                //DELETE these, when not using.
                leftImage.Draw(_calibrationManager.calibrationModel.Rec1, new Bgr(Color.LimeGreen), 20);
                rightImage.Draw(_calibrationManager.calibrationModel.Rec2, new Bgr(Color.LimeGreen), 20);
                leftImage.Save(Path.Combine(Configuration.TempDirectoryPath, "LeftRecImager.JPG"));
                rightImage.Save(Path.Combine(Configuration.TempDirectoryPath, "RightRecImager.JPG"));
            }
        }

        /// <summary>
        /// Save generated depth map
        /// </summary>
        /// <param name="disparityMap">Disparity map</param>
        private void SaveAndAddDepthMapToListView(Image<Bgr, byte> disparityMap)
        {
            var path = Path.Combine(Configuration.TempDepthMapDirectoryPath, $"DepthMap_{_fileManager.listViewerModel.LeftCameraStack.Count}.JPG");

            disparityMap.Save(path);
            _fileManager.AddInputFileToList(path, EListViewGroup.DepthMap);
        }

        /// <summary>
        /// Start computing depth map from stack
        /// </summary>
        public void ComputeStereoCorrespondenceFromStack()
        {
            if (_useParallel)
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
            for (int i = 0; i < _fileManager.listViewerModel.LeftCameraStack.Count; i++)
            {
                var leftImage = new Image<Bgr, byte>((Bitmap)_fileManager.listViewerModel.LeftCameraStack[i].image);
                var rightImage = new Image<Bgr, byte>((Bitmap)_fileManager.listViewerModel.RightCameraStack[i].image);

                if (_calibrationManager != null && _calibrationManager.calibrationModel != null)
                {
                    CvInvoke.Remap(leftImage, leftImage, _calibrationManager.calibrationModel.UndistortCam1.MapX, _calibrationManager.calibrationModel.UndistortCam1.MapY, Inter.Linear);
                    CvInvoke.Remap(rightImage, rightImage, _calibrationManager.calibrationModel.UndistortCam2.MapX, _calibrationManager.calibrationModel.UndistortCam2.MapY, Inter.Linear);
                }

                var DepthMap = StereoSolver.ComputeDepthMap(leftImage, rightImage);

                var DepthMapToSave = new Mat();
                DepthMap.ConvertTo(DepthMapToSave, DepthType.Cv8U);

                _fileManager.listViewerModel._lastDepthMapImage = DepthMapToSave.Image2ImageBGR();

                SaveAndAddDepthMapToListView(DepthMapToSave.Image2ImageBGR());
                Computer3DPointsFromStereoPair(DepthMap);
            }
        }

        /// <summary>
        /// Computing depth map from stack (PARALLEL)
        /// </summary>
        private void ComputeStereoCorrespondenceFromStackParallel()
        {
            Parallel.For(0, _fileManager.listViewerModel.LeftCameraStack.Count, i =>
            {
                var leftImage = _fileManager.listViewerModel.LeftCameraStack[i];
                var rightImage = _fileManager.listViewerModel.RightCameraStack[i];
                var DepthMap = StereoSolver.ComputeDepthMap(leftImage.image, rightImage.image);

                var DepthMapToSave = new Mat();
                DepthMap.ConvertTo(DepthMapToSave, DepthType.Cv8U);

                _fileManager.listViewerModel._lastDepthMapImage = new Image<Bgr, byte>(DepthMapToSave.Bitmap);

                SaveAndAddDepthMapToListView(new Image<Bgr, byte>(DepthMapToSave.Bitmap));
                Computer3DPointsFromStereoPair(DepthMap);
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
        private MCvPoint3D32f[] Computer3DPointsFromStereoPair(Mat disparityMap)
        {
            if (_calibrationManager != null && _calibrationManager.calibrationModel != null)
            {
                MCvPoint3D32f[] points = PointCollection.ReprojectImageTo3D(disparityMap, _calibrationManager.calibrationModel.Q);
            }
            return null;
        }

        /// <summary>
        /// Show calibration WinForm
        /// </summary>
        public void ShowCalibration()
        {
            _calibrationManager = new CalibrationManager(_cameraManager);
        }
    }
}

