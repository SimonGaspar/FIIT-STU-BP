namespace Bachelor_app.StereoVision.StereoCorrespondence
{
    /// <summary>
    /// StereoBM algorithm
    /// </summary>
    public class StereoBlockMatching : AbstractStereoSolver, IStereoSolver
    {
        public StereoBlockMatching() : base(new StereoBlockMatchingModel())
        {
            WinForm = new StereoBMForm(this);
        }

        public StereoBlockMatching(CudaStereoBlockMatchingModel Model) : base(Model)
        {
        }
        public StereoBlockMatching(StereoSemiGlobalBlockMatchingModel Model) : base(Model)
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
            Mat imageDisparity = new Mat();
            using (StereoBM _stereoBM = CreateInstance())
            {
                ConvertImageToGray(leftImage, rightImage);
                _stereoBM.Compute(LeftGrayImage, RightGrayImage, imageDisparity);
            }
            return imageDisparity;
        }

        public override dynamic CreateInstance()
        {
            return new StereoBM(this.Model.Disparity, this.Model.BlockSize);
        }
    }
}
