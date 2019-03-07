using Bakalárska_práca.Extension;
using Bakalárska_práca.StereoVision.Model;
using Emgu.CV;
using Emgu.CV.Cuda;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Bakalárska_práca.StereoVision.StereoCorrespondence
{
    public class CudaStereoBlockMatching : StereoBlockMatching, IStereoSolver
    {
        private CudaStereoBM _cudaStereoBM;
        public new CudaStereoBlockMatchingModel model = new CudaStereoBlockMatchingModel() { Disparity = 16, BlockSize = 15 };

        public CudaStereoBlockMatching()
        {
            _cudaStereoBM = new CudaStereoBM(model.Disparity, model.BlockSize);
        }

        public override Image<Bgr, byte> ComputeDepthMap(Image<Bgr, byte> leftImage, Image<Bgr, byte> rightImage)
        {
            ConvertImageToGray(leftImage, rightImage);

            LeftGrayImage.Save(@"D:\Downloads\LImage.png");
            RightGrayImage.Save(@"D:\Downloads\RImage.png");

            GpuMat imageDisparity = new GpuMat();
            _cudaStereoBM.FindStereoCorrespondence(LeftGrayImage.ImageToGpuMat(), RightGrayImage.ImageToGpuMat(), imageDisparity);

            imageDisparity.ConvertTo(imageDisparity, DepthType.Cv8U);
            imageDisparity.Save(@"D:\Downloads\Image.png");

            return new Image<Bgr, byte>(imageDisparity.Bitmap);
        }

        public override void UpdateModel<T>(T model)
        {
            this.model = model as CudaStereoBlockMatchingModel;
            _cudaStereoBM = new CudaStereoBM(this.model.Disparity, this.model.BlockSize);
        }
    }
}
