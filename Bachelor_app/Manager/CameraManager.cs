using System.Collections.Generic;
using System.IO;
using Bachelor_app.Enumerate;
using Bachelor_app.Extension;
using Bachelor_app.Helper;
using Bachelor_app.Model;
using Emgu.CV;

namespace Bachelor_app.Manager
{
    /// <summary>
    /// Manager for camera.
    /// </summary>
    public class CameraManager
    {
        private FileManager _fileManager;
        public CameraModel LeftCamera = new CameraModel();
        public CameraModel RightCamera = new CameraModel();
        public ECameraResolution resolution;

        public CameraManager(FileManager fileManager)
        {
            this._fileManager = fileManager;
        }


        /// <summary>
        /// Create camera model and instance of camera.
        /// </summary>
        /// <param name="cameraModel"></param>
        /// <param name="deviceId"></param>
        /// <param name="deviceName"></param>
        public void SetCamera(CameraModel cameraModel, int deviceId, string deviceName)
        {
            cameraModel.DeviceId = deviceId;
            cameraModel.DeviceName = deviceName;
            cameraModel.CreateCameraInstance();
            cameraModel.Camera.UpdateResolution(resolution);
        }

        /// <summary>
        /// Update camera resolution
        /// </summary>
        public void UpdateResolution()
        {
            LeftCamera.Camera.UpdateResolution(resolution);
            RightCamera.Camera.UpdateResolution(resolution);
        }

        /// <summary>
        /// Get stereo image for our application and save it in temp folder.
        /// </summary>
        /// <param name="IsSFM">Using SfM or StereoVision.</param>
        /// <param name="countInputFile">Count of saved images.</param>
        /// <returns>List of images, which create stereo image.</returns>
        public List<InputFileModel> GetInputFromStereoCamera(bool IsSFM, int countInputFile = 0)
        {
            Mat LeftImage = new Mat();
            Mat RightImage = new Mat();

            CameraHelper.GetStereoImageSync(LeftCamera.Camera, RightCamera.Camera, ref LeftImage, ref RightImage);

            string LeftImagePath = Path.Combine($@"{(IsSFM ? Configuration.TempDirectoryPath : Configuration.TempLeftStackDirectoryPath)}", $"Left_{countInputFile}.JPG");
            string RightImagePath = Path.Combine($@"{(IsSFM ? Configuration.TempDirectoryPath : Configuration.TempRightStackDirectoryPath)}", $"Right_{countInputFile}.JPG");

            LeftImage.Save(LeftImagePath);
            RightImage.Save(RightImagePath);

            _fileManager.AddInputFileToList(LeftImagePath, EListViewGroup.LeftCameraStack);
            _fileManager.AddInputFileToList(RightImagePath, EListViewGroup.RightCameraStack);

            var returnList = new List<InputFileModel>
            {
                new InputFileModel(LeftImagePath),
                new InputFileModel(RightImagePath)
            };

            return returnList;
        }

        /// <summary>
        /// Get image for our application and save it in temp folder. Only for SfM.
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="countInputFile">Count of saved images</param>
        /// <returns>Saved image in list.</returns>
        public List<InputFileModel> GetInputFromCamera(VideoCapture camera, int countInputFile = 0)
        {
            string ImagePath = Path.Combine($@"{Configuration.TempDirectoryPath}", $"Image_{countInputFile}.JPG");

            Mat Image = camera.GetImageInMat();
            Image.Save(ImagePath);

            _fileManager.AddInputFileToList(ImagePath, EListViewGroup.BasicStack);

            var returnList = new List<InputFileModel>
            {
                new InputFileModel(ImagePath)
            };

            return returnList;
        }
    }
}
