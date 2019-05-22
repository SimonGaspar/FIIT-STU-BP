namespace Bachelor_app.StereoVision.StereoCorrespondence
{
    /// <summary>
    /// CudaStereoBM algorithm
    /// </summary>
    public class CudaStereoBlockMatching : StereoBlockMatching, IStereoSolver
    {
        public CudaStereoBlockMatching()
            : base(new CudaStereoBlockMatchingModel())
        {
            WinForm = new StereoBMForm(this);
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
            using (CudaStereoBM _cudaStereoBM = CreateInstance())
            {
                ConvertImageToGray(leftImage, rightImage);

                using (GpuMat LeftGpuMat = LeftGrayImage.ToGpuMat(), RightGpuMat = RightGrayImage.ToGpuMat())
                    _cudaStereoBM.FindStereoCorrespondence(LeftGpuMat, RightGpuMat, imageDisparity);

                imageDisparity.Download(disparity);
            }
            return disparity;
        }

        public override dynamic CreateInstance()
        {
            return new CudaStereoBM(Model.Disparity, Model.BlockSize);
        }
    }
}
