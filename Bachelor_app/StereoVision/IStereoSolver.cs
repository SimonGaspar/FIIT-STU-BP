using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace Bachelor_app.StereoVision
{
    /// <summary>
    /// Interface for stereo correspondence.
    /// </summary>
    interface IStereoSolver
    {
        /// <summary>
        /// </summary>
        /// <param name="leftImage"></param>
        /// <param name="rightImage"></param>
        /// <returns>Depth map</returns>
        Mat ComputeDepthMap(Image<Bgr, byte> leftImage, Image<Bgr, byte> rightImage);

        /// <summary>
        /// </summary>
        /// <param name="leftImage"></param>
        /// <param name="rightImage"></param>
        /// <returns>Depth map</returns>
        Mat ComputeDepthMap(Image leftImage, Image rightImage);

        /// <summary>
        /// Update model with WinForm value.
        /// </summary>
        /// <typeparam name="T">Type of model</typeparam>
        /// <param name="model">New model</param>
        void UpdateModel<T>(T model);

        /// <summary>
        /// Show WinForm(settings)
        /// </summary>
        void ShowSettingForm();

        /// <summary>
        /// Create instance of algorithm
        /// </summary>
        /// <returns></returns>
        dynamic CreateInstance();
    }
}
