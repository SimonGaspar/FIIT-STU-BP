using Bakalárska_práca.StereoVision.WindowsForm;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Bakalárska_práca.StereoVision.StereoCorrespondence
{
    public class StereoBlockMatching : AbstractStereoSolver, IStereoSolver
    {
        public StereoBlockMatchingModel model = new StereoBlockMatchingModel() { Disparity = 16, BlockSize = 15 };
        private StereoBM _stereoBM;
        protected StereoBMForm _windowsForm;

        public StereoBlockMatching()
        {
            //_stereoBM = new StereoBM(model.Disparity, model.BlockSize);
        }

        public override Image<Bgr, byte> ComputeDepthMap(Image<Bgr, byte> leftImage, Image<Bgr, byte> rightImage)
        {
            ConvertImageToGray(leftImage, rightImage);

            LeftGrayImage.Save(@"D:\Downloads\LImage.png");
            RightGrayImage.Save(@"D:\Downloads\RImage.png");

            Mat imageDisparity = new Mat();
            _stereoBM.Compute(LeftGrayImage, RightGrayImage, imageDisparity);
            imageDisparity.ConvertTo(imageDisparity, DepthType.Cv8U);
            imageDisparity.Save(@"D:\Downloads\Image.png");

            CvInvoke.CvtColor(imageDisparity, DepthMap, ColorConversion.Gray2Bgr);
            return DepthMap;
        }

        public override void UpdateModel<T>(T model)
        {
            this.model = model as StereoBlockMatchingModel;
            _stereoBM = new StereoBM(
                this.model.Disparity,
                this.model.BlockSize
                );
        }

        public override void ShowSettingForm()
        {
            _windowsForm = new StereoBMForm(this);
            _windowsForm.Show();
        }

    }
}
