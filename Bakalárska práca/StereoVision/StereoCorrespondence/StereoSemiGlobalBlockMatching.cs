using System;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Bakalárska_práca.StereoVision.StereoCorrespondence
{
    public class StereoSemiGlobalBlockMatching //: IStereoSolver
    {
        private StereoSGBM _stereoSGBM;

        public StereoSemiGlobalBlockMatching()
        {
        }
        
        public void ComputeDepthMap<T>(T leftImage, T rightImage) where T : Image<Bgr, byte>
        {
            throw new NotImplementedException();
        }
    }
}
