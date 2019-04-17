using System.Collections.Generic;
using DirectShowLib;
using Emgu.CV;

namespace Bachelor_app.Helper
{
    /// <summary>
    /// Helper for camera.
    /// </summary>
    public static class CameraHelper
    {
        /// <summary>
        /// Get list of connected video devices.
        /// </summary>
        /// <returns>List of connected video devices</returns>
        public static List<KeyValuePair<int, string>> GetListOfWebCam()
        {
            var ListCamerasData = new List<KeyValuePair<int, string>>();

            DsDevice[] _SystemCamereas = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

            int _DeviceIndex = 0;
            foreach (DsDevice _Camera in _SystemCamereas)
            {
                // Vyriesit ID camery a vytvorenie VideoCapture s ID
                // Prepojenie DirectShow.NET s EmguCV
                ListCamerasData.Add(new KeyValuePair<int, string>(_DeviceIndex++, _Camera.Name));
            }

            return ListCamerasData;
        }

        /// <summary>
        /// Get stereo image with synchornization.
        /// </summary>
        /// <param name="LeftCamera"></param>
        /// <param name="RightCamera"></param>
        /// <param name="LeftImage"></param>
        /// <param name="RightImage"></param>
        public static void GetStereoImageSync(VideoCapture LeftCamera, VideoCapture RightCamera, ref Mat LeftImage, ref Mat RightImage)
        {
            LeftCamera.Grab();
            RightCamera.Grab();
            LeftCamera.Retrieve(LeftImage);
            RightCamera.Retrieve(RightImage);
        }

        /// <summary>
        /// Get stereo image without synchornization. For better result for stereo image use GetStereoImageSync.
        /// </summary>
        /// <param name="LeftCamera"></param>
        /// <param name="RightCamera"></param>
        /// <param name="LeftImage"></param>
        /// <param name="RightImage"></param>
        public static void GetStereoImage(VideoCapture LeftCamera, VideoCapture RightCamera, ref Mat LeftImage, ref Mat RightImage)
        {
            GetImage(LeftCamera, ref LeftImage);
            GetImage(RightCamera, ref RightImage);
        }

        /// <summary>
        /// Get stereo image with synchornization.
        /// </summary>
        /// <param name="Camera"></param>
        /// <param name="Image"></param>
        public static void GetImage(VideoCapture Camera, ref Mat Image)
        {
            Camera.Grab();
            Camera.Retrieve(Image);
        }

    }
}
