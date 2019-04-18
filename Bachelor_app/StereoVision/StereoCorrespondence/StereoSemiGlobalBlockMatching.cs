using Bachelor_app.StereoVision.Model;
using Bachelor_app.StereoVision.WindowsForm;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Bachelor_app.StereoVision.StereoCorrespondence
{
    /// <summary>
    /// StereoSGBM algorithm.
    /// </summary>
    public class StereoSemiGlobalBlockMatching : StereoBlockMatching, IStereoSolver
    {

        public StereoSemiGlobalBlockMatching() : base(new StereoSemiGlobalBlockMatchingModel())
        {
            WinForm = new StereoSGBMForm(this);
        }

        /// <summary>
        /// Compute depth map from images
        /// </summary>
        /// <param name="leftImage">Left image</param>
        /// <param name="rightImage">Right image</param>
        /// <returns>Depth map</returns>
        public override Mat ComputeDepthMap(Image<Bgr, byte> leftImage, Image<Bgr, byte> rightImage)
        {
            StereoSGBM _stereoSGBM = CreateInstance();
            ConvertImageToGray(leftImage, rightImage);

            Mat imageDisparity = new Mat();
            Mat imageToSave = new Mat();
            _stereoSGBM.Compute(LeftGrayImage, RightGrayImage, imageDisparity);
            imageDisparity.ConvertTo(imageToSave, DepthType.Cv8U);

            return imageDisparity;
        }


        public override dynamic CreateInstance()
        {
            return new StereoSGBM(
                Model.MinDispatiries,
                Model.Disparity,
                Model.BlockSize,
                Model.P1,
                Model.P2,
                Model.Disp12MaxDiff,
                Model.PreFilterCap,
                Model.UniquenessRatio,
                Model.SpeckleWindowsSize,
                Model.SpeckleRange,
                Model.Mode
                );
        }
    }
}
