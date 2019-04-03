using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;

namespace Bachelor_app.Model
{
    public class CameraModel
    {
        public Size Resolution { get; set; } = new Size(1920, 1080);
        public int ID { get; set; }
        public string Name { get; set; }

        public VideoCapture camera { get; set; } = null;

        public void CreateCameraInstance()
        {
            camera = new VideoCapture(ID);
            UpdateResolution(Resolution);
        }

        public void UpdateResolution(Size resolution)
        {
            Resolution = resolution;

            if (camera != null && Resolution.Height >= 360 && Resolution.Width >= 640)
            {
                camera.SetCaptureProperty(CapProp.FrameWidth, Resolution.Width);
                camera.SetCaptureProperty(CapProp.FrameHeight, Resolution.Height);
            }
        }
        public void UpdateResolution(int Width, int Height)
        {
            Resolution = new Size(Width, Height);
            UpdateResolution(Resolution);
        }

    }
}
