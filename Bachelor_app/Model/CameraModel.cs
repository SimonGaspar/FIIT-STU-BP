using Emgu.CV;

namespace Bachelor_app.Model
{
    public class CameraModel
    {
        public int DeviceId { get; private set; }
        public string DeviceName { get; private set; }
        public VideoCapture Camera { get; private set; }

        /// <summary>
        /// Create instance of choosen camera.
        /// </summary>
        /// <param name="deviceId">ID of camera.</param>
        /// <param name="deviceName">Name of camera.</param>
        public void CreateCameraInstance(int deviceId, string deviceName)
        {
            if (Camera != null)
                Camera.Dispose();

            DeviceId = deviceId;
            DeviceName = deviceName;
            Camera = new VideoCapture(DeviceId);
        }
    }
}
