using System;
using Emgu.CV;
using Emgu.CV.Cuda;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Bakalárska_práca.StereoVision.StereoCorrespondence
{
    public class CudaStereoBlockMatching : StereoBlockMatching, IStereoSolver
    {
        CudaStereoBM _cudaStereoBM;

        public CudaStereoBlockMatching()
        {
            _cudaStereoBM = new CudaStereoBM(model.Disparity, model.BlockSize);
        }

        public override Image<Bgr, byte> ComputeDepthMap(Image<Bgr, byte> leftImage, Image<Bgr, byte> rightImage)
        {
            ConvertImageToGray(leftImage, rightImage);

            LeftGrayImage.Save(@"D:\Downloads\LImage.png");
            RightGrayImage.Save(@"D:\Downloads\RImage.png");

            Mat imageDisparity = new Mat();
            _cudaStereoBM.FindStereoCorrespondence(LeftGrayImage, RightGrayImage, imageDisparity);
            imageDisparity.ConvertTo(imageDisparity, DepthType.Cv8U);
            imageDisparity.Save(@"D:\Downloads\Image.png");
            var image = new Image<Bgra, Int32>(leftImage.Size);

            CvInvoke.CvtColor(imageDisparity, DepthMap, ColorConversion.Gray2Bgr);
            return DepthMap;
        }

        public override void UpdateModel<T>(T model)
        {
            this.model = model as StereoBlockMatchingModel;
            _cudaStereoBM = new CudaStereoBM(this.model.Disparity, this.model.BlockSize);
        }
    }
}
