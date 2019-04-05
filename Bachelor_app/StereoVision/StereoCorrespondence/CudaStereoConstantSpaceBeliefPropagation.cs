using Bakalárska_práca.Extension;
using Bakalárska_práca.StereoVision.Model;
using Bakalárska_práca.StereoVision.WindowsForm;
using Emgu.CV;
using Emgu.CV.Cuda;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Bakalárska_práca.StereoVision.StereoCorrespondence
{
    public class CudaStereoConstantSpaceBeliefPropagation : AbstractStereoSolver, IStereoSolver
    {
        private CudaStereoConstantSpaceBPForm _windowsForm;
        public CudaStereoConstantSpaceBPModel model = new CudaStereoConstantSpaceBPModel();

        public CudaStereoConstantSpaceBeliefPropagation()
        {
        }

        public override Image<Bgr, byte> ComputeDepthMap(Image<Bgr, byte> leftImage, Image<Bgr, byte> rightImage)
        {
            CudaStereoConstantSpaceBP _cudaStereoConstantSpaceBP = CreateCudaStereoConstantSpaceBP();
            ConvertImageToGray(leftImage, rightImage);

            LeftGrayImage.Save(@"D:\Downloads\LImage.png");
            RightGrayImage.Save(@"D:\Downloads\RImage.png");

            GpuMat imageDisparity = new GpuMat();
            Image<Bgr, byte> disparity = new Image<Bgr, byte>(leftImage.Size);
                        
            _cudaStereoConstantSpaceBP.FindStereoCorrespondence(LeftGrayImage.ImageToGpuMat(), RightGrayImage.ImageToGpuMat(), imageDisparity);

            imageDisparity.ConvertTo(imageDisparity, DepthType.Cv8U);
            imageDisparity.Download(disparity);
            disparity.Save(@"D:\Downloads\Image.png");

            return new Image<Bgr, byte>(imageDisparity.Bitmap);
        }

        public override void UpdateModel<T>(T model)
        {
            this.model = model as CudaStereoConstantSpaceBPModel;
        }

        public CudaStereoConstantSpaceBP CreateCudaStereoConstantSpaceBP() {
            return new CudaStereoConstantSpaceBP(this.model.Disparity, this.model.Iteration, this.model.Level, this.model.Plane);
        }

        public override void ShowSettingForm()
        {
            _windowsForm = new CudaStereoConstantSpaceBPForm(this);
            _windowsForm.Show();
        }
    }
}
