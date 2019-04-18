using Bachelor_app.StereoVision.Model;
using Bachelor_app.StereoVision.WindowsForm;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

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
            StereoBM _stereoBM = CreateInstance();
            ConvertImageToGray(leftImage, rightImage);

            Mat imageDisparity = new Mat();
            Mat imageToSave = new Mat();
            _stereoBM.Compute(LeftGrayImage, RightGrayImage, imageDisparity);
            imageDisparity.ConvertTo(imageToSave, DepthType.Cv8U);

            return imageDisparity;
        }

        protected override dynamic CreateInstance()
        {
            return new StereoBM(this.Model.Disparity, this.Model.BlockSize);
        }
    }
}
