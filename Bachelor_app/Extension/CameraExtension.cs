namespace Bachelor_app.Extension
{
    /// <summary>
    /// Extension method for camera (class VideoCapture).
    /// </summary>
    public static class CameraExtension
    {
        /// <summary>
        /// Get frame from camera and save it in Mat.
        /// </summary>
        /// <param name="camera"></param>
        /// <returns>Frame in Mat</returns>
        public static Mat GetImageInMat(this VideoCapture camera)
        {
            if (camera == null)
                return null;

            var input = new Mat();
            camera.Grab();
            camera.Retrieve(input);
            return input;
        }

        /// <summary>
        /// Set camera resolution.
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="resolution"></param>
        private static void UpdateResolution(this VideoCapture camera, Size resolution)
        {
            if (camera != null)
            {
                camera.SetCaptureProperty(CapProp.FrameWidth, resolution.Width);
                camera.SetCaptureProperty(CapProp.FrameHeight, resolution.Height);
            }
        }

        /// <summary>
        /// Set camera resolution.
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="type">Type of resolution.</param>
        public static void UpdateResolution(this VideoCapture camera, ECameraResolution type)
        {
            var resolution = type.GetResolution();
            UpdateResolution(camera, resolution);
        }
    }
}
