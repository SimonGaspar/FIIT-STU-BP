﻿using Bakalárska_práca.StereoVision.Model;
using Bakalárska_práca.StereoVision.WindowsForm;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Bakalárska_práca.StereoVision.StereoCorrespondence
{
    public class StereoSemiGlobalBlockMatching : StereoBlockMatching, IStereoSolver
    {
        private StereoSGBM _stereoSGBM;
        public new StereoSemiGlobalBlockMatchingModel model = new StereoSemiGlobalBlockMatchingModel() { MinDispatiries = 0, Disparity = 16, BlockSize = 15 };
        protected new StereoSGBMForm _windowsForm;

        public StereoSemiGlobalBlockMatching()
        {
            _stereoSGBM = new StereoSGBM(model.MinDispatiries, model.Disparity, model.BlockSize);
        }

        public override Image<Bgr, byte> ComputeDepthMap(Image<Bgr, byte> leftImage, Image<Bgr, byte> rightImage)
        {
            ConvertImageToGray(leftImage, rightImage);

            LeftGrayImage.Save(@"D:\Downloads\LImage.png");
            RightGrayImage.Save(@"D:\Downloads\RImage.png");

            Mat imageDisparity = new Mat();
            _stereoSGBM.Compute(LeftGrayImage, RightGrayImage, imageDisparity);
            imageDisparity.ConvertTo(imageDisparity, DepthType.Cv8U);
            imageDisparity.Save(@"D:\Downloads\Image.png");

            CvInvoke.CvtColor(imageDisparity, DepthMap, ColorConversion.Gray2Bgr);
            return DepthMap;
        }

        public override void UpdateModel<T>(T model)
        {
            this.model = model as StereoSemiGlobalBlockMatchingModel;
            _stereoSGBM = new StereoSGBM(
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