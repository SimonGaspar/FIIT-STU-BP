using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Bachelor_app.Enumerate;
using Bachelor_app.Manager;
using Bachelor_app.StereoVision;
using Bakalárska_práca.Enumerate;
using Bakalárska_práca.Manager;
using Bakalárska_práca.Model;
using Bakalárska_práca.StereoVision.StereoCorrespondence;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Bakalárska_práca.StereoVision
{
    public class StereoVisionManager
    {
        private IStereoSolver StereoSolver = new StereoBlockMatching();
        private FileManager _fileManager;
        private DisplayManager _displayManager;
        private CameraManager _cameraManager;
        private CalibrationManager _calibrationManager;
        private MainForm _winForm;



        public bool stopStereoCorrespondence = false;

        string tempDirectory = Path.GetFullPath($"..\\..\\..\\Temp");

        public StereoVisionManager(FileManager fileManager, DisplayManager displayManager, CameraManager cameraManager, MainForm mainForm)
        {
            this._fileManager = fileManager;
            this._displayManager = displayManager;
            this._cameraManager = cameraManager;
            this._winForm = mainForm;
        }

        public void SetStereoCorrespondenceAlgorithm(EStereoCorrespondenceAlgorithm item)
        {
            switch (item)
            {
                case EStereoCorrespondenceAlgorithm.CudaStereoBM: StereoSolver = new CudaStereoBlockMatching(); break;
                case EStereoCorrespondenceAlgorithm.StereoBM: StereoSolver = new StereoBlockMatching(); break;
                case EStereoCorrespondenceAlgorithm.StereoSGBM: StereoSolver = new StereoSemiGlobalBlockMatching(); break;
                case EStereoCorrespondenceAlgorithm.CudaStereoConstantSpaceBP: StereoSolver = new CudaStereoConstantSpaceBeliefPropagation(); break;

            }
        }

        public void ComputeStereoCorrespondence()
        {
            stopStereoCorrespondence = false;

            switch (_fileManager._inputType)
            {
                case EInput.ConnectedStereoCamera: ComputeStereoCorrespondenceFromConnectedStereoCamera(); break;
                case EInput.ListView: ComputeStereoCorrespondenceFromStack(); break;
                default: throw new NotImplementedException();
            }
        }

        private void ComputeStereoCorrespondenceFromConnectedStereoCamera()
        {
            while (!stopStereoCorrespondence)
            {
                var listOfInput = GetInputFromStereoCamera(_cameraManager.LeftCamera.camera, _cameraManager.RightCamera.camera);
                var DepthMap = new Image<Gray, byte>((Bitmap)StereoSolver.ComputeDepthMap(listOfInput[0].image, listOfInput[1].image));
                _fileManager.listViewerModel._lastDepthMapImage = DepthMap.Convert<Bgr, byte>();
                Computer3DPointsFromStereoPair(DepthMap);

            }
        }

        public void ComputeStereoCorrespondenceFromStack()
        {
            for (int i = 0; i < _fileManager.listViewerModel.LeftCameraStack.Count; i++)
            {
                var leftImage = _fileManager.listViewerModel.LeftCameraStack[i];
                var rightImage = _fileManager.listViewerModel.RightCameraStack[i];
                _fileManager.listViewerModel._lastDepthMapImage = new Image<Bgr, byte>((Bitmap)StereoSolver.ComputeDepthMap(leftImage.image, rightImage.image));
            }
        }

        public void ShowSettingForStereoSolver()
        {
            StereoSolver.ShowSettingForm();
        }

        // default was Image<Gray,short> <---- check it
        private void Computer3DPointsFromStereoPair(Image<Gray, byte> disparityMap)
        {
            MCvPoint3D32f[] points = PointCollection.ReprojectImageTo3D(disparityMap, _calibrationManager.calibrationModel.Q);
        }

        private List<InputFileModel> GetInputFromStereoCamera(VideoCapture LeftCamera, VideoCapture RightCamera, int countInputFile = 0)
        {
            LeftCamera.Grab();
            RightCamera.Grab();
            Mat LeftImage = new Mat();
            Mat RightImage = new Mat();
            LeftCamera.Retrieve(LeftImage);
            RightCamera.Retrieve(RightImage);
            LeftImage.Save(Path.Combine($@"{tempDirectory}", $"Left_{countInputFile}.JPG"));
            RightImage.Save(Path.Combine($@"{tempDirectory}", $"Right_{countInputFile}.JPG"));


            var inputFileLeft = new InputFileModel(Path.Combine($@"{tempDirectory}", $"Left_{countInputFile}.JPG"));
            var imageList = _winForm.ImageList[(int)EListViewGroup.LeftCameraStack];
            var listViewer = _winForm.ListViews[(int)EListViewGroup.LeftCameraStack];
            _fileManager.AddInputFileToList(inputFileLeft, _fileManager.listViewerModel.ListOfListInputFolder[(int)EListViewGroup.LeftCameraStack], imageList, listViewer);

            var inputFileRight = new InputFileModel(Path.Combine($@"{tempDirectory}", $"Right_{countInputFile}.JPG"));
            imageList = _winForm.ImageList[(int)EListViewGroup.RightCameraStack];
            listViewer = _winForm.ListViews[(int)EListViewGroup.RightCameraStack];
            _fileManager.AddInputFileToList(inputFileLeft, _fileManager.listViewerModel.ListOfListInputFolder[(int)EListViewGroup.RightCameraStack], imageList, listViewer);

            var returnList = new List<InputFileModel>();
            returnList.Add(inputFileLeft);
            returnList.Add(inputFileRight);

            return returnList;
        }

        public void ShowCalibration()
        {
           _calibrationManager = new CalibrationManager(_cameraManager);
        }
    }
}

