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
        private FileManager fileManager;

        public CameraModel LeftCamera { get; set; } = new CameraModel();

        public CameraModel RightCamera { get; set; } = new CameraModel();

        public ECameraResolution Resolution { get; set; }

        public CameraManager(FileManager fileManager)
        {
            this.fileManager = fileManager;
        }

        /// <summary>
        /// Create camera model and instance of camera.
        /// </summary>
        /// <param name="cameraModel"></param>
        /// <param name="deviceId"></param>
        /// <param name="deviceName"></param>
        public void SetCamera(CameraModel cameraModel, int deviceId, string deviceName)
        {
            if (deviceId >= 0)
            {
                cameraModel.CreateCameraInstance(deviceId, deviceName);
                cameraModel.Camera.UpdateResolution(Resolution);
            }
            else
                if (cameraModel.Camera != null)
                cameraModel.Camera.Dispose();
        }

        /// <summary>
        /// Update camera resolution
        /// </summary>
        public void UpdateResolution()
        {
            LeftCamera.Camera.UpdateResolution(Resolution);
            RightCamera.Camera.UpdateResolution(Resolution);
        }

        /// <summary>
        /// Get stereo image for our application and save it in temp folder.
        /// </summary>
        /// <param name="isSFM">Using SfM or StereoVision.</param>
        /// <param name="countInputFile">Count of saved images.</param>
        /// <returns>List of images, which create stereo image.</returns>
        public List<InputFileModel> GetInputFromStereoCamera(bool isSFM, int countInputFile = 0)
        {
            string leftImagePath = Path.Combine($@"{(isSFM ? Configuration.TempDirectoryPath : Configuration.TempLeftStackDirectoryPath)}", $"Left_{countInputFile}.JPG");
            string rightImagePath = Path.Combine($@"{(isSFM ? Configuration.TempDirectoryPath : Configuration.TempRightStackDirectoryPath)}", $"Right_{countInputFile}.JPG");

            using (Mat leftImage = new Mat(), rightImage = new Mat())
            {
                CameraHelper.GetStereoImageSync(LeftCamera.Camera, RightCamera.Camera, leftImage, rightImage);

                if (leftImage == null || rightImage == null)
                    throw new EmptyFrameException("Empty frame was captured from camera.");

                leftImage.Save(leftImagePath);
                rightImage.Save(rightImagePath);
            }

            fileManager.AddInputFileToList(leftImagePath, EListViewGroup.LeftCameraStack);
            fileManager.AddInputFileToList(rightImagePath, EListViewGroup.RightCameraStack);

            var returnList = new List<InputFileModel>
            {
                new InputFileModel(leftImagePath),
                new InputFileModel(rightImagePath)
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
            string imagePath = Path.Combine($@"{Configuration.TempDirectoryPath}", $"Image_{countInputFile}.JPG");

            using (Mat image = new Mat())
            {
                CameraHelper.GetImage(camera, image);

                if (image == null)
                    throw new EmptyFrameException("Empty frame was captured from camera.");

                image.Save(imagePath);
            }

            fileManager.AddInputFileToList(imagePath, EListViewGroup.BasicStack);

            var returnList = new List<InputFileModel> { new InputFileModel(imagePath) };

            return returnList;
        }
    }
}
