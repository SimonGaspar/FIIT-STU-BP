using System.Collections.Generic;
using System.Linq;
using System.Management;
using Emgu.CV;

namespace Bachelor_app.Helper
{
    /// <summary>
    /// Helper for camera.
    /// </summary>
    public static class CameraHelper
    {
        private static List<KeyValuePair<int, string>> listOfCamera = null;

        public static List<KeyValuePair<int, string>> ListOfCamera => listOfCamera ?? GetListOfWebCam();

        public static void UpdateCameraList() => listOfCamera = GetListOfWebCam();

        /// <summary>
        /// Get list of connected video devices.
        /// </summary>
        /// <returns>List of connected video devices</returns>
        private static List<KeyValuePair<int, string>> GetListOfWebCam()
        {
            var listCamerasData = new List<KeyValuePair<int, string>>();
            var listUSBDevice = GetUSBDevices();

            foreach (var camera in listUSBDevice)
            {
                listCamerasData.Add(new KeyValuePair<int, string>(listUSBDevice.IndexOf(camera), camera.Name));
            }

            listCamerasData.Add(new KeyValuePair<int, string>(-1, "Empty"));

            return listCamerasData;
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
                {
                    devices.Add(new USBDeviceInfo(
                        (string)device.GetPropertyValue("Service"),
                        (string)device.GetPropertyValue("ClassGUID"),
                        (string)device.GetPropertyValue("Description"),
                        (string)device.GetPropertyValue("PNPDeviceID"),
                        (string)device.GetPropertyValue("DeviceID"),
                        (string)device.GetPropertyValue("Name")));
                }
            }

            var videoDevice = devices.Where(x => x.Description == "USB Video Device").OrderBy(x => x.DeviceID).ToList();

            collection.Dispose();
            return videoDevice;
        }

        /// <summary>
        /// Class defined PNP devices
        /// </summary>
        private class USBDeviceInfo
        {
            public USBDeviceInfo(
                string service,
                string classGUID,
                string description,
                string pnpDeviceID,
                string deviceID,
                string name)
            {
                Service = service;
                ClassGUID = classGUID;
                Description = description;
                PNPDeviceID = pnpDeviceID;
                DeviceID = deviceID;
                Name = name;
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
        /// <param name="leftCamera"></param>
        /// <param name="rightCamera"></param>
        /// <param name="leftImage"></param>
        /// <param name="rightImage"></param>
        public static void GetStereoImageSync(VideoCapture leftCamera, VideoCapture rightCamera, Mat leftImage, Mat rightImage)
        {
            if (leftCamera != null && rightCamera != null)
            {
                leftCamera.Grab();
                rightCamera.Grab();
                leftCamera.Retrieve(leftImage);
                rightCamera.Retrieve(rightImage);
            }
            else
                leftImage = rightImage = null;
        }

        /// <summary>
        /// Get stereo image without synchornization. For better result for stereo image use GetStereoImageSync.
        /// </summary>
        /// <param name="leftCamera"></param>
        /// <param name="rightCamera"></param>
        /// <param name="leftImage"></param>
        /// <param name="rightImage"></param>
        public static void GetStereoImage(VideoCapture leftCamera, VideoCapture rightCamera, Mat leftImage, Mat rightImage)
        {
            GetImage(leftCamera, leftImage);
            GetImage(rightCamera, rightImage);
        }

        /// <summary>
        /// Get stereo image with synchornization.
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="image"></param>
        public static void GetImage(VideoCapture camera, Mat image)
        {
            if (camera != null)
            {
                camera.Grab();
                camera.Retrieve(image);
            }
            else
                image = null;
        }
    }
}
