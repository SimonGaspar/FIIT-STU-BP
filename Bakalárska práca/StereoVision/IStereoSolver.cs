using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Bakalárska_práca.StereoVision
{
    interface IStereoSolver
    {
        Image<Bgr, byte> ComputeDepthMap(Image<Bgr, byte> leftImage, Image<Bgr, byte> rightImage);
        Image ComputeDepthMap(Image leftImage, Image rightImage);

        void UpdateModel<T>(T model);
        void ShowSettingForm();
    }
}
