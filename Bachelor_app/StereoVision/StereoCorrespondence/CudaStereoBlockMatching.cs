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
        public new CudaStereoBlockMatchingModel model= new CudaStereoBlockMatchingModel();

        public CudaStereoBlockMatching()
        {
        }

        public override Mat ComputeDepthMap(Image<Bgr, byte> leftImage, Image<Bgr, byte> rightImage)
        {
            CudaStereoBM _cudaStereoBM = CreateCudaStereoBM();
            ConvertImageToGray(leftImage, rightImage);

            GpuMat imageDisparity = new GpuMat();
            GpuMat imageToSave = new GpuMat();
            Image<Bgr, byte> disparityToSave = new Image<Bgr, byte>(leftImage.Size);
            Mat disparity = new Mat();
            _cudaStereoBM.FindStereoCorrespondence(LeftGrayImage.ImageToGpuMat(), RightGrayImage.ImageToGpuMat(), imageDisparity);

            imageDisparity.ConvertTo(imageToSave, DepthType.Cv8U);
            imageToSave.Download(disparityToSave);
            imageDisparity.Download(disparity);

            return disparity;
        }

        public override void UpdateModel<T>(T model)
        {
            this.model = model as CudaStereoBlockMatchingModel;
        }

        public CudaStereoBM CreateCudaStereoBM() {
            return new CudaStereoBM(this.model.Disparity, this.model.BlockSize);
        }
    }
}
