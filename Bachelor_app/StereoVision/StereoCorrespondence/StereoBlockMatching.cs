using Bachelor_app.StereoVision.Model;
using Bachelor_app.StereoVision.WindowsForm;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Bachelor_app.StereoVision.StereoCorrespondence
{
    /// <summary>
    /// StereoBM algorithm
    /// </summary>
    public class StereoBlockMatching : AbstractStereoSolver, IStereoSolver
    {
        public StereoBlockMatching()
            : base(new StereoBlockMatchingModel())
        {
            WinForm = new StereoBMForm(this);
        }

        public StereoBlockMatching(CudaStereoBlockMatchingModel model)
            : base(model)
        {
        }

        public StereoBlockMatching(StereoSemiGlobalBlockMatchingModel model)
            : base(model)
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
            using (StereoBM stereoBM = CreateInstance())
            {
                ConvertImageToGray(leftImage, rightImage);
                stereoBM.Compute(LeftGrayImage, RightGrayImage, imageDisparity);
            }

            return imageDisparity;
        }

        public override dynamic CreateInstance()
        {
            return new StereoBM(Model.Disparity, Model.BlockSize);
        }
    }
}
