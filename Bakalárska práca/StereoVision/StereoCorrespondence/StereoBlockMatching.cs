using System;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Bakalárska_práca.StereoVision.StereoCorrespondence
{
    public class StereoBlockMatching : AbstractStereoSolver
    {
        private StereoBM _stereoBM = new StereoBM();

        public StereoBlockMatching()
        {
        }
        
        public override Image<Bgr, byte> ComputeDepthMap(Image<Bgr, byte> leftImage, Image<Bgr, byte> rightImage) 
        {
            ConvertImageToGray(leftImage, rightImage);

            _stereoBM.Compute(LeftGrayImage, RightGrayImage, DepthMapGray);
            CvInvoke.CvtColor(DepthMapGray, DepthMap, ColorConversion.gray);
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


    }
}
