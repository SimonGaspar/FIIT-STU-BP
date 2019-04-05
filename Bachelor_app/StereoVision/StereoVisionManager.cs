﻿using System;
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
        public CalibrationManager _calibrationManager;
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
                var listOfInput = GetInputFromStereoCamera(_cameraManager.LeftCamera.camera, _cameraManager.RightCamera.camera, _fileManager.listViewerModel.ListOfListInputFolder[(int)EListViewGroup.LeftCameraStack].Count);
                
                var DepthMap = StereoSolver.ComputeDepthMap(listOfInput[0].image, listOfInput[1].image);
                _fileManager.listViewerModel._lastDepthMapImage = DepthMap;

                AddDepthMapToListView(DepthMap);
                Computer3DPointsFromStereoPair(DepthMap);

                new Image<Bgr, byte>((Bitmap)listOfInput[0].image).Draw(_calibrationManager.calibrationModel.Rec1, new Bgr(Color.LimeGreen), 1);
                new Image<Bgr, byte>((Bitmap)listOfInput[1].image).Draw(_calibrationManager.calibrationModel.Rec2, new Bgr(Color.LimeGreen), 1);
                listOfInput[0].image.Save(Path.Combine(tempDirectory, "LeftRecImager.JPG"));
                listOfInput[1].image.Save(Path.Combine(tempDirectory, "RightRecImager.JPG"));
            }
        }

        private void AddDepthMapToListView(Image<Bgr, byte> disparityMap)
        {
            disparityMap.Save(Path.Combine($@"{tempDirectory}", $"DepthMap_{_fileManager.listViewerModel.LeftCameraStack.Count}.JPG"));


            var inputFileLeft = new InputFileModel(Path.Combine($@"{tempDirectory}", $"DepthMap_{_fileManager.listViewerModel.LeftCameraStack.Count}.JPG"));
            var imageList = _winForm.ImageList[(int)EListViewGroup.DepthMap];
            var listViewer = _winForm.ListViews[(int)EListViewGroup.DepthMap];
            _fileManager.AddInputFileToList(inputFileLeft, _fileManager.listViewerModel.ListOfListInputFolder[(int)EListViewGroup.DepthMap], imageList, listViewer);
        }

        public void ComputeStereoCorrespondenceFromStack()
        {
            for (int i = 0; i < _fileManager.listViewerModel.LeftCameraStack.Count; i++)
            {
                var leftImage = _fileManager.listViewerModel.LeftCameraStack[i];
                var rightImage = _fileManager.listViewerModel.RightCameraStack[i];
                var DepthMap = StereoSolver.ComputeDepthMap(leftImage.image, rightImage.image);
                _fileManager.listViewerModel._lastDepthMapImage = DepthMap;

                AddDepthMapToListView(DepthMap);
                Computer3DPointsFromStereoPair(DepthMap);
            }
        }

        public void ShowSettingForStereoSolver()
        {
            StereoSolver.ShowSettingForm();
        }

        private MCvPoint3D32f[] Computer3DPointsFromStereoPair(Image<Bgr, byte> disparityMap)
        {
            MCvPoint3D32f[] points = PointCollection.ReprojectImageTo3D(disparityMap, _calibrationManager.calibrationModel.Q);
            return points;
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

