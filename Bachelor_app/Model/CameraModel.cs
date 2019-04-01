using Emgu.CV;
using Emgu.CV.CvEnum;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_app.Model
{
    public class CameraModel
    {
        public Size Resolution { get; set; } = new Size(1920, 1080);
        public int ID { get; set; }
        public string Name { get; set; }

        public VideoCapture camera { get; set; }

        public void CreateCameraInstance() {
            camera = new VideoCapture(ID);
            camera.SetCaptureProperty(CapProp.FrameWidth, Resolution.Width);
            camera.SetCaptureProperty(CapProp.FrameHeight, Resolution.Height);
        }

    }
}
