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
        public new StereoSemiGlobalBlockMatchingModel model = new StereoSemiGlobalBlockMatchingModel();
        protected new StereoSGBMForm _windowsForm;

        public StereoSemiGlobalBlockMatching()
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
            StereoSGBM _stereoSGBM = CreateStereoSGBM();
            ConvertImageToGray(leftImage, rightImage);

            Mat imageDisparity = new Mat();
            Mat imageToSave = new Mat();
            _stereoSGBM.Compute(LeftGrayImage, RightGrayImage, imageDisparity);
            imageDisparity.ConvertTo(imageToSave, DepthType.Cv8U);

            return imageDisparity;
        }

        /// <summary>
        /// Update model with WinForm value
        /// </summary>
        /// <typeparam name="T">Type of model</typeparam>
        /// <param name="model">New model</param>
        public override void UpdateModel<T>(T model)
        {
            this.model = model as StereoSemiGlobalBlockMatchingModel;
        }

        /// <summary>
        /// Create new instance of using algorithm.
        /// </summary>
        /// <returns>New instance</returns>
        public StereoSGBM CreateStereoSGBM()
        {
            return new StereoSGBM(
                this.model.MinDispatiries,
                this.model.Disparity,
                this.model.BlockSize,
                this.model.P1,
                this.model.P2,
                this.model.Disp12MaxDiff,
                this.model.PreFilterCap,
                this.model.UniquenessRatio,
                this.model.SpeckleWindowsSize,
                this.model.SpeckleRange,
                this.model.Mode
                );
        }

        /// <summary>
        /// Show WinForm(settings)
        /// </summary>
        public override void ShowSettingForm()
        {
            _windowsForm = new StereoSGBMForm(this);
            _windowsForm.Show();
        }
    }
}
