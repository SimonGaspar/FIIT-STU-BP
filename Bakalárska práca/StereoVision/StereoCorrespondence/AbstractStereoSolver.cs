using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public virtual Image ComputeDepthMap(Image leftImage, Image rightImage)
        {
            return null;
        }

        protected void ConvertImageToGray<T>(T leftImage, T rightImage) where T: Image<Bgr,byte>
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
