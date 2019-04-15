using System.Collections.Generic;
using DirectShowLib;

namespace Bachelor_app.Helper
{
    /// <summary>
    /// Helper for camera.
    /// </summary>
    public static class CameraHelper
    {
        /// <summary>
        /// Get list of connected video devices
        /// </summary>
        /// <returns>List of connected video devices</returns>
        public static List<KeyValuePair<int, string>> GetListOfWebCam()
        {
            var ListCamerasData = new List<KeyValuePair<int, string>>();

            DsDevice[] _SystemCamereas = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

            int _DeviceIndex = 0;
            foreach (DsDevice _Camera in _SystemCamereas)
            {
                // Vyriesit ID camery
                ListCamerasData.Add(new KeyValuePair<int, string>(_DeviceIndex, _Camera.Name));
                _DeviceIndex++;
            }
            return ListCamerasData;
        }
    }
}
