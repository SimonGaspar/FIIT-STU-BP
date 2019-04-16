using System.Collections.Generic;
using System.IO;
using Bachelor_app.Enumerate;
using Bachelor_app.Extension;
using Bachelor_app.Helper;
using Bachelor_app.Model;
using Bakalárska_práca;
using Bakalárska_práca.Enumerate;
using Bakalárska_práca.Manager;
using Bakalárska_práca.Model;
using Emgu.CV;

namespace Bachelor_app.Manager
{
    /// <summary>
    /// Manager for camera
    /// </summary>
    public class CameraManager
    {
        public List<KeyValuePair<int, string>> ListCamerasData;
        private MainForm _winForm;
        private FileManager _fileManager;
        public CameraModel LeftCamera = new CameraModel();
        public CameraModel RightCamera = new CameraModel();
        public ECameraResolution resolution;

        public CameraManager(MainForm WinForm, FileManager fileManager)
        {
            this._winForm = WinForm;
            this._fileManager = fileManager;
            ListCamerasData = CameraHelper.GetListOfWebCam();
        }


        /// <summary>
        /// Create camera model and instance of camera
        /// </summary>
        /// <param name="cameraModel">Camera model</param>
        /// <param name="ID">Id of device</param>
        /// <param name="name">Name of device</param>
        public void SetCamera(CameraModel cameraModel, int ID, string name)
        {
            cameraModel.ID = ID;
            cameraModel.Name = name;
            cameraModel.CreateCameraInstance();
            cameraModel.camera.UpdateResolution(resolution);
        }

        /// <summary>
        /// Update camera resolution
        /// </summary>
        public void UpdateResolution()
        {
            LeftCamera.camera.UpdateResolution(resolution);
            RightCamera.camera.UpdateResolution(resolution);
        }

        public List<InputFileModel> GetInputFromStereoCamera(int countInputFile = 0)
        {
            Mat LeftImage = new Mat();
            Mat RightImage = new Mat();

            string LeftImagePath = Path.Combine($@"{Configuration.TempLeftStackDirectoryPath}", $"Left_{countInputFile}.JPG");
            string RightImagePath = Path.Combine($@"{Configuration.TempRightStackDirectoryPath}", $"Right_{countInputFile}.JPG");

            CameraHelper.GetStereoImage(LeftCamera.camera, RightCamera.camera, ref LeftImage, ref RightImage);

            LeftImage.Save(LeftImagePath);
            RightImage.Save(RightImagePath);

            _fileManager.AddInputFileToList(LeftImagePath, EListViewGroup.LeftCameraStack);
            _fileManager.AddInputFileToList(RightImagePath, EListViewGroup.RightCameraStack);

            var inputFileLeft = new InputFileModel(LeftImagePath);
            var inputFileRight = new InputFileModel(RightImagePath);

            var returnList = new List<InputFileModel>();
            returnList.Add(inputFileLeft);
            returnList.Add(inputFileRight);

            return returnList;
        }
    }
}
