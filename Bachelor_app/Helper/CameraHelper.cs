using System.Collections.Generic;
using System.Linq;

namespace Bachelor_app.Helper
{
    /// <summary>
    /// Helper for camera.
    /// </summary>
    public static class CameraHelper
    {
        private static List<KeyValuePair<int, string>> _listOfCamera = null;
        public static List<KeyValuePair<int, string>> ListOfCamera
        {
            get
            {
                if (_listOfCamera == null)
                    return _listOfCamera = GetListOfWebCam();

                return _listOfCamera;
            }
        }

        /// <summary>
        /// Get list of connected video devices.
        /// </summary>
        /// <returns>List of connected video devices</returns>
        private static List<KeyValuePair<int, string>> GetListOfWebCam()
        {
            var ListCamerasData = new List<KeyValuePair<int, string>>();
            var ListUSBDevice = GetUSBDevices();

            foreach (var camera in ListUSBDevice)
            {
                ListCamerasData.Add(new KeyValuePair<int, string>(ListUSBDevice.IndexOf(camera), camera.Name));
            }

            ListCamerasData.Add(new KeyValuePair<int, string>(-1, "Empty"));

            return ListCamerasData;
        }

        /// <summary>
        /// Update list of connected cameras
        /// </summary>
        public static void UpdateCameraList()
        {
            _listOfCamera = GetListOfWebCam();
        }

        /// <summary>
        /// Get list of camera devices from all PNP (Plug&Play) entity.
        /// </summary>
        /// <returns></returns>
        private static List<USBDeviceInfo> GetUSBDevices()
        {
            List<USBDeviceInfo> devices = new List<USBDeviceInfo>();

            ManagementObjectCollection collection;
            using (var searcher = new ManagementObjectSearcher(@"SELECT * FROM Win32_PnPEntity"))
                collection = searcher.Get();

            foreach (var device in collection)
            {
                if ((string)device.GetPropertyValue("PNPClass") == "Camera" && (string)device.GetPropertyValue("Status") == "OK")
                    devices.Add(new USBDeviceInfo(
                        (string)device.GetPropertyValue("Service"),
                        (string)device.GetPropertyValue("ClassGUID"),
                        (string)device.GetPropertyValue("Description"),
                        (string)device.GetPropertyValue("PNPDeviceID"),
                        (string)device.GetPropertyValue("DeviceID"),
                        (string)device.GetPropertyValue("Name")));
            }

            var VideoDevice = devices.Where(x => x.Description == "USB Video Device").OrderBy(x => x.DeviceID).ToList();

            collection.Dispose();
            return VideoDevice;
        }

        /// <summary>
        /// Class defined PNP devices
        /// </summary>
        private class USBDeviceInfo
        {
            public USBDeviceInfo(
                string Service,
                string ClassGUID,
                string Description,
                string PNPDeviceID,
                string DeviceID,
                string Name)
            {
                this.Service = Service;
                this.ClassGUID = ClassGUID;
                this.Description = Description;
                this.PNPDeviceID = PNPDeviceID;
                this.DeviceID = DeviceID;
                this.Name = Name;
            }
            public string Service { get; set; }
            public string ClassGUID { get; set; }
            public string Description { get; set; }
            public string PNPDeviceID { get; set; }
            public string DeviceID { get; set; }
            public string Name { get; set; }
        }

        /// <summary>
        /// Get stereo image with synchornization.
        /// </summary>
        /// <param name="LeftCamera"></param>
        /// <param name="RightCamera"></param>
        /// <param name="LeftImage"></param>
        /// <param name="RightImage"></param>
        public static void GetStereoImageSync(VideoCapture LeftCamera, VideoCapture RightCamera, Mat LeftImage, Mat RightImage)
        {
            if (LeftCamera != null && RightCamera != null)
            {
                LeftCamera.Grab();
                RightCamera.Grab();
                LeftCamera.Retrieve(LeftImage);
                RightCamera.Retrieve(RightImage);
            }
            else
                LeftImage = RightImage = null;
        }

        /// <summary>
        /// Get stereo image without synchornization. For better result for stereo image use GetStereoImageSync.
        /// </summary>
        /// <param name="LeftCamera"></param>
        /// <param name="RightCamera"></param>
        /// <param name="LeftImage"></param>
        /// <param name="RightImage"></param>
        public static void GetStereoImage(VideoCapture LeftCamera, VideoCapture RightCamera, Mat LeftImage, Mat RightImage)
        {
            GetImage(LeftCamera, LeftImage);
            GetImage(RightCamera, RightImage);
        }

        /// <summary>
        /// Get stereo image with synchornization.
        /// </summary>
        /// <param name="Camera"></param>
        /// <param name="Image"></param>
        public static void GetImage(VideoCapture Camera, Mat Image)
        {
            if (Camera != null)
            {
                Camera.Grab();
                Camera.Retrieve(Image);
            }
            else
                Image = null;
        }

    }
}
