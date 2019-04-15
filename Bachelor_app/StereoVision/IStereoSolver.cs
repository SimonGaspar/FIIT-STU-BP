using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Bakalárska_práca.StereoVision
{
    /// <summary>
    /// Interface for stereo correspondence
    /// </summary>
    interface IStereoSolver
    {
        /// <summary>
        /// Compute depth map from images
        /// </summary>
        /// <param name="leftImage">Left image</param>
        /// <param name="rightImage">Right image</param>
        /// <returns>Depth map</returns>
        Mat ComputeDepthMap(Image<Bgr, byte> leftImage, Image<Bgr, byte> rightImage);

        /// <summary>
        /// Compute depth map from images
        /// </summary>
        /// <param name="leftImage">Left image</param>
        /// <param name="rightImage">Right image</param>
        /// <returns>Depth map</returns>
        Mat ComputeDepthMap(Image leftImage, Image rightImage);

        /// <summary>
        /// Update model with WinForm value
        /// </summary>
        /// <typeparam name="T">Type of model</typeparam>
        /// <param name="model">New model</param>
        void UpdateModel<T>(T model);

        /// <summary>
        /// Show WinForm(settings)
        /// </summary>
        void ShowSettingForm();
    }
}
