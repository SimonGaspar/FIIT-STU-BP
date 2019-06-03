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
        public CudaStereoConstantSpaceBeliefPropagation()
            : base(new CudaStereoConstantSpaceBPModel())
        {
            WinForm = new CudaStereoConstantSpaceBPForm(this);
        }

        /// <summary>
        /// Compute depth map from images
        /// </summary>
        /// <param name="leftImage">Left image</param>
        /// <param name="rightImage">Right image</param>
        /// <returns>Depth map</returns>
        public override Mat ComputeDepthMap(Image<Bgr, byte> leftImage, Image<Bgr, byte> rightImage)
        {
            Mat disparity = new Mat();
            using (GpuMat imageDisparity = new GpuMat())
            using (CudaStereoConstantSpaceBP cudaStereoBM = CreateInstance())
            {
                ConvertImageToGray(leftImage, rightImage);

                using (GpuMat leftGpuMat = LeftGrayImage.ToGpuMat(), rightGpuMat = RightGrayImage.ToGpuMat())
                    cudaStereoBM.FindStereoCorrespondence(leftGpuMat, rightGpuMat, imageDisparity);

                imageDisparity.Download(disparity);
            }

            return disparity;
        }

        public override dynamic CreateInstance()
        {
            return new CudaStereoConstantSpaceBP(Model.Disparity, Model.Iteration, Model.Level, Model.Plane);
        }
    }
}
