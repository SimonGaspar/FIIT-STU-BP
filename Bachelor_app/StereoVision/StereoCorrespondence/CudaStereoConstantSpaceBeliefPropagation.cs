using Bachelor_app.Extension;
using Bachelor_app.StereoVision.Model;
using Bachelor_app.StereoVision.WindowsForm;
using Emgu.CV;
using Emgu.CV.Cuda;
using Emgu.CV.Structure;

namespace Bachelor_app.StereoVision.StereoCorrespondence
{
    /// <summary>
    /// CudaStereoConstantSpaceBP algorithm
    /// </summary>
    public class CudaStereoConstantSpaceBeliefPropagation : AbstractStereoSolver, IStereoSolver
    {
        private CudaStereoConstantSpaceBPForm _windowsForm;
        public CudaStereoConstantSpaceBPModel model = new CudaStereoConstantSpaceBPModel();

        public CudaStereoConstantSpaceBeliefPropagation()
        {
        }

        /// <summary>
        /// Compute depth map from images
        /// </summary>
        /// <param name="leftImage">Left image</param>
        /// <param name="rightImage">Right image</param>
        /// <returns>Depth map</returns>
        public override Mat ComputeDepthMap(Image<Bgr, byte> leftImage, Image<Bgr, byte> rightImage)
        {
            GpuMat imageDisparity = new GpuMat();
            Mat disparity = new Mat();

            CudaStereoConstantSpaceBP _cudaStereoConstantSpaceBP = CreateCudaStereoConstantSpaceBP();
            ConvertImageToGray(leftImage, rightImage);

            _cudaStereoConstantSpaceBP.FindStereoCorrespondence(LeftGrayImage.ToGpuMat(), RightGrayImage.ToGpuMat(), imageDisparity);
            imageDisparity.Download(disparity);

            return disparity;
        }

        /// <summary>
        /// Update model with WinForm value
        /// </summary>
        /// <typeparam name="T">Type of model</typeparam>
        /// <param name="model">New model</param>
        public override void UpdateModel<T>(T model)
        {
            this.model = model as CudaStereoConstantSpaceBPModel;
        }

        /// <summary>
        /// Create new instance of using algorithm.
        /// </summary>
        /// <returns>New instance</returns>
        public CudaStereoConstantSpaceBP CreateCudaStereoConstantSpaceBP()
        {
            return new CudaStereoConstantSpaceBP(this.model.Disparity, this.model.Iteration, this.model.Level, this.model.Plane);
        }

        /// <summary>
        /// Show WinForm(settings)
        /// </summary>
        public override void ShowSettingForm()
        {
            _windowsForm = new CudaStereoConstantSpaceBPForm(this);
            _windowsForm.Show();
        }
    }
}
