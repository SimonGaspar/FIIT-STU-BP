using System;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Bachelor_app.StereoVision.StereoCorrespondence
{
    /// <summary>
    /// Abstract class for stereo correspondence
    /// </summary>
    public abstract class AbstractStereoSolver : IStereoSolver
    {
        protected Image<Gray, byte> LeftGrayImage { get; private set; }
        protected Image<Gray, byte> RightGrayImage { get; private set; }
        protected dynamic Model { get; private set; }
        protected dynamic WinForm { get; set; }


        public AbstractStereoSolver(dynamic model)
        {
            Model = model;
        }

        /// <summary>
        /// Compute depth map from images
        /// </summary>
        /// <param name="leftImage">Left image</param>
        /// <param name="rightImage">Right image</param>
        /// <returns>Depth map</returns>
        public virtual Mat ComputeDepthMap(Image<Bgr, byte> leftImage, Image<Bgr, byte> rightImage)
        {
            return null;
        }

        /// <summary>
        /// Compute depth map from images
        /// </summary>
        /// <param name="leftImage">Left image</param>
        /// <param name="rightImage">Right image</param>
        /// <returns>Depth map</returns>
        public Mat ComputeDepthMap(Image leftImage, Image rightImage)
        {
            var depthMapImage = ComputeDepthMap(
                new Image<Bgr, byte>((Bitmap)leftImage),
                new Image<Bgr, byte>((Bitmap)rightImage)
                );

            return depthMapImage;
        }

        /// <summary>
        /// Show WinForm(settings)
        /// </summary>
        public virtual void ShowSettingForm()
        {
            WinForm.Show();
        }

        /// <summary>
        /// Update model with WinForm value
        /// </summary>
        /// <typeparam name="T">Type of model</typeparam>
        /// <param name="model">New model</param>
        public virtual void UpdateModel<T>(T model)
        {
            Model = model;
            TryCreateInstance();
        }

        private void TryCreateInstance()
        {
            var instance = CreateInstance();

            if (instance == null)
                throw new NullReferenceException("Instance is null.");

            if (instance is IDisposable)
                instance.Dispose();
        }

        /// <summary>
        /// Convert image to gray
        /// </summary>
        /// <typeparam name="T">Type of image</typeparam>
        /// <param name="leftImage">Left image</param>
        /// <param name="rightImage">Right image</param>
        protected void ConvertImageToGray<T>(T leftImage, T rightImage) where T : Image<Bgr, byte>
        {
            LeftGrayImage = leftImage.Convert<Gray, byte>();
            RightGrayImage = rightImage.Convert<Gray, byte>();
        }

        public virtual dynamic CreateInstance()
        {
            return null;
        }

    }
}
