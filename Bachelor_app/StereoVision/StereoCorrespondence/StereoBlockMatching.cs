using Bakalárska_práca.StereoVision.WindowsForm;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;

namespace Bakalárska_práca.StereoVision.StereoCorrespondence
{
    public class StereoBlockMatching : AbstractStereoSolver, IStereoSolver
    {
        public StereoBlockMatchingModel model = new StereoBlockMatchingModel();
        protected StereoBMForm _windowsForm;

        public StereoBlockMatching()
        {
        }

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

        public override void UpdateModel<T>(T model)
        {
            this.model = model as StereoBlockMatchingModel;
        }
        public StereoBM CreateStereoBM()
        {
            return new StereoBM(this.model.Disparity, this.model.BlockSize);
        }
        public override void ShowSettingForm()
        {
            _windowsForm = new StereoBMForm(this);
            _windowsForm.Show();
        }

    }
}
