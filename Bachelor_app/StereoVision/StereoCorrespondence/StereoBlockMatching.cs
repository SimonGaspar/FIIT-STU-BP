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
        public StereoBlockMatchingModel model = new StereoBlockMatchingModel();
        protected StereoBMForm _windowsForm;

        public StereoBlockMatching()
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
            StereoBM _stereoBM = CreateStereoBM();
            ConvertImageToGray(leftImage, rightImage);

            Mat imageDisparity = new Mat();
            Mat imageToSave = new Mat();
            _stereoBM.Compute(LeftGrayImage, RightGrayImage, imageDisparity);
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
            this.model = model as StereoBlockMatchingModel;
        }

        /// <summary>
        /// Create new instance of using algorithm.
        /// </summary>
        /// <returns>New instance</returns>
        public StereoBM CreateStereoBM()
        {
            return new StereoBM(this.model.Disparity, this.model.BlockSize);
        }

        /// <summary>
        /// Show WinForm(settings)
        /// </summary>
        public override void ShowSettingForm()
        {
            _windowsForm = new StereoBMForm(this);
            _windowsForm.Show();
        }

    }
}
