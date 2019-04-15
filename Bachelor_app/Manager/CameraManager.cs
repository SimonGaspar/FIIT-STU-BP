using System.Collections.Generic;
using Bachelor_app.Enumerate;
using Bachelor_app.Extension;
using Bachelor_app.Helper;
using Bachelor_app.Model;
using Bakalárska_práca;

namespace Bachelor_app.Manager
{
    /// <summary>
    /// Manager for camera
    /// </summary>
    public class CameraManager
    {
        public List<KeyValuePair<int, string>> ListCamerasData;
        private MainForm _winForm;
        public CameraModel LeftCamera = new CameraModel();
        public CameraModel RightCamera = new CameraModel();
        public ECameraResolution resolution;

        public CameraManager(MainForm WinForm)
        {
            this._winForm = WinForm;
            ListCamerasData = CameraHelper.GetListOfWebCam();
        }


        /// <summary>
        /// Create camera model and instance of camera
        /// </summary>
        /// <param name="cameraModel">Camera model</param>
        /// <param name="ID">Id of device</param>
        /// <param name="name">Name of device</param>
        public void SetCanera(CameraModel cameraModel, int ID, string name)
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
    }
}
