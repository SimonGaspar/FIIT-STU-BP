using Bakalárska_práca.StereoVision.Model;
using Bakalárska_práca.StereoVision.WindowsForm;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Bakalárska_práca.StereoVision.StereoCorrespondence
{
    public class StereoSemiGlobalBlockMatching : StereoBlockMatching, IStereoSolver
    {
        public new StereoSemiGlobalBlockMatchingModel model = new StereoSemiGlobalBlockMatchingModel();
        protected new StereoSGBMForm _windowsForm;

        public StereoSemiGlobalBlockMatching()
        {
        }

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

        public override void UpdateModel<T>(T model)
        {
            this.model = model as StereoSemiGlobalBlockMatchingModel;
        }

        public StereoSGBM CreateStereoSGBM() {
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

        public override void ShowSettingForm()
        {
            _windowsForm = new StereoSGBMForm(this);
            _windowsForm.Show();
        }
    }
}
