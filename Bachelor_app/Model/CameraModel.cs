using Emgu.CV;

namespace Bachelor_app.Model
{
    public class CameraModel
    {
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public VideoCapture Camera { get; private set; }

        public void CreateCameraInstance()
        {
            Camera = new VideoCapture(DeviceId);
        }
    }
}
