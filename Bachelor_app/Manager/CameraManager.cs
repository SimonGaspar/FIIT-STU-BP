using System.Collections.Generic;
using System.Drawing;
using Bachelor_app.Enumerate;
using Bachelor_app.Model;
using Bakalárska_práca;
using DirectShowLib;

namespace Bachelor_app.Manager
{
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
            GetListOfWebCam();
        }

        public void GetListOfWebCam()
        {
            ListCamerasData = new List<KeyValuePair<int, string>>();

            DsDevice[] _SystemCamereas = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

            int _DeviceIndex = 0;
            foreach (DsDevice _Camera in _SystemCamereas)
            {
                // Vyriesit ID camery
                ListCamerasData.Add(new KeyValuePair<int, string>(_DeviceIndex, _Camera.Name));
                _DeviceIndex++;
            }
            
        }

        public void SetCameraID(CameraModel cameraModel, int ID, string name)
        {
            cameraModel.ID = ID;
            cameraModel.Name = name;
            cameraModel.CreateCameraInstance();
            //cameraModel.camera.QueryFrame().Save($@"D:\Downloads\{ID}.JPG");
        }

        public void UpdateResolution()
        {
            Size newResolution = new Size();
            switch (resolution)
            {
                case ECameraResolution.VGA: newResolution = new Size(640, 360); break;
                case ECameraResolution.HD: newResolution = new Size(1280, 720); break;
                case ECameraResolution.FullHD: newResolution = new Size(1920, 1080); break;
            }

            LeftCamera.UpdateResolution(newResolution);
            RightCamera.UpdateResolution(newResolution);
        }
    }
}
