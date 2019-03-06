using System;
using System.Drawing;
using Bakalárska_práca.StereoVision.WindowsForm;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Bakalárska_práca.StereoVision.StereoCorrespondence
{
    public class StereoBlockMatching : AbstractStereoSolver
    {
        public StereoBlockMatchingModel model = new StereoBlockMatchingModel() { Disparity = 16, BlockSize = 15 };
        private StereoBM _stereoBM;
        private StereoBMForm _stereoBMForm;

        public StereoBlockMatching()
        {
            _stereoBM = new StereoBM(model.Disparity, model.BlockSize);
        }

        public override Image<Bgr, byte> ComputeDepthMap(Image<Bgr, byte> leftImage, Image<Bgr, byte> rightImage)
        {
            ConvertImageToGray(leftImage, rightImage);

            LeftGrayImage.Save(@"D:\Downloads\LImage.png");
            RightGrayImage.Save(@"D:\Downloads\RImage.png");

            _stereoBM = new StereoBM(16, 15);
            Mat imageDisparity = new Mat();
            _stereoBM.Compute(LeftGrayImage, RightGrayImage, imageDisparity);
            imageDisparity.ConvertTo(imageDisparity, DepthType.Cv8U);
            imageDisparity.Save(@"D:\Downloads\Image.png");
            var image = new Image<Bgra, Int32>(leftImage.Size);

            CvInvoke.CvtColor(imageDisparity, DepthMap, ColorConversion.Gray2Bgr);
            return DepthMap;
        }

        public override Image ComputeDepthMap(Image leftImage, Image rightImage)
        {
            var depthMapImage = ComputeDepthMap(
                new Image<Bgr, byte>((Bitmap)leftImage),
                new Image<Bgr, byte>((Bitmap)rightImage)
                );

            return depthMapImage.ToBitmap();
        }

        public void UpdateStereoBM(StereoBlockMatchingModel model)
        {
            this.model = model;
            _stereoBM = new StereoBM(model.Disparity, model.BlockSize);
        }

        public void UpdateStereoBM(int Disparity, int BlockSize)
        {
            UpdateStereoBM(
                new StereoBlockMatchingModel()
                {
                    Disparity = Disparity,
                    BlockSize = BlockSize
                }
            );
        }

        public override void ShowSettingForm()
        {
            _stereoBMForm = new StereoBMForm(this);
            _stereoBMForm.Show();
        }


    }
}
