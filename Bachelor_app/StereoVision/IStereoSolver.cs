using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Bakalárska_práca.StereoVision
{
    interface IStereoSolver
    {
        Mat ComputeDepthMap(Image<Bgr, byte> leftImage, Image<Bgr, byte> rightImage);
        Mat ComputeDepthMap(Image leftImage, Image rightImage);
        
        void UpdateModel<T>(T model);
        void ShowSettingForm();
    }
}
