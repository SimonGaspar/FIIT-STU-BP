using Bakalárska_práca.Extension;
using Bakalárska_práca.StereoVision.Model;
using Emgu.CV;
using Emgu.CV.Cuda;
using Emgu.CV.Structure;

namespace Bakalárska_práca.StereoVision.StereoCorrespondence
{
    /// <summary>
    /// CudaStereoBM algorithm
    /// </summary>
    public class CudaStereoBlockMatching : StereoBlockMatching, IStereoSolver
    {
        public new CudaStereoBlockMatchingModel model = new CudaStereoBlockMatchingModel();

        public CudaStereoBlockMatching()
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

            CudaStereoBM _cudaStereoBM = CreateCudaStereoBM();
            ConvertImageToGray(leftImage, rightImage);

            _cudaStereoBM.FindStereoCorrespondence(LeftGrayImage.ImageToGpuMat(), RightGrayImage.ImageToGpuMat(), imageDisparity);
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
            this.model = model as CudaStereoBlockMatchingModel;
        }

        /// <summary>
        /// Create new instance of using algorithm
        /// </summary>
        /// <returns>New instance</returns>
        private CudaStereoBM CreateCudaStereoBM()
        {
            return new CudaStereoBM(this.model.Disparity, this.model.BlockSize);
        }
    }
}
