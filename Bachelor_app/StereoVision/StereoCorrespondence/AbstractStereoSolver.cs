using System;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Bakalárska_práca.StereoVision.StereoCorrespondence
{
    public abstract class AbstractStereoSolver : IStereoSolver
    {
        protected Image<Bgr, byte> LeftImage;
        protected Image<Bgr, byte> RightImage;
        protected Image<Gray, byte> LeftGrayImage;
        protected Image<Gray, byte> RightGrayImage;
        public Image<Gray, byte> DepthMapGray { get; protected set; }
        public Image<Bgr, byte> DepthMap { get; protected set; }

        public virtual Image<Bgr, byte> ComputeDepthMap(Image<Bgr, byte> leftImage, Image<Bgr, byte> rightImage)
        {
            return null;
        }

        public Image ComputeDepthMap(Image leftImage, Image rightImage)
        {
            var depthMapImage = ComputeDepthMap(
                new Image<Bgr, byte>((Bitmap)leftImage),
                new Image<Bgr, byte>((Bitmap)rightImage)
                );

            return depthMapImage.ToBitmap();
        }

        public virtual void ShowSettingForm() { }

        public virtual void UpdateModel<T>(T model)
        {
            throw new NotImplementedException();
        }

        protected void ConvertImageToGray<T>(T leftImage, T rightImage) where T : Image<Bgr, byte>
        {
            LeftImage = leftImage;
            RightImage = rightImage;
            DepthMapGray = new Image<Gray, byte>(leftImage.Size);
            DepthMap = new Image<Bgr, byte>(leftImage.Size);
            LeftGrayImage = leftImage.Convert<Gray, byte>();
            RightGrayImage = rightImage.Convert<Gray, byte>();
        }

    }
}
