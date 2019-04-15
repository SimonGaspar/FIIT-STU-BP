using System.Drawing;
using Bachelor_app.Enumerate;
using Emgu.CV;
using Emgu.CV.CvEnum;

namespace Bachelor_app.Extension
{
    /// <summary>
    /// Extensions for camera
    /// </summary>
    public static class CameraExtension
    {
        /// <summary>
        /// Get frame from camera and save it in Mat.
        /// </summary>
        /// <param name="camera">Camera</param>
        /// <returns>Frame in Mat</returns>
        public static Mat GetImageInMat(this VideoCapture camera)
        {
            var input = new Mat();
            camera.Grab();
            camera.Retrieve(input);
            return input;
        }

        public static void UpdateResolution(this VideoCapture camera, Size resolution)
        {
            camera.SetCaptureProperty(CapProp.FrameWidth, resolution.Width);
            camera.SetCaptureProperty(CapProp.FrameHeight, resolution.Height);
        }
        public static void UpdateResolution(this VideoCapture camera, int Width, int Height)
        {
            var resolution = new Size(Width, Height);
            UpdateResolution(camera, resolution);
        }

        public static void UpdateResolution(this VideoCapture camera, ECameraResolution type)
        {
            var resolution = type.GetResolution();
            UpdateResolution(camera, resolution);
        }
    }
}
