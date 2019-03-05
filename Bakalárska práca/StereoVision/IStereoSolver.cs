using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakalárska_práca.StereoVision
{
    interface IStereoSolver
    {
        Image<Bgr, byte> ComputeDepthMap(Image<Bgr, byte> leftImage, Image<Bgr, byte> rightImage);
        Image ComputeDepthMap(Image leftImage, Image rightImage);
        void ShowSettingForm();
    }
}
