using System;
using Emgu.CV;

namespace Bakalárska_práca.StereoVision.StereoCorrespondence
{
    public class StereoSemiGlobalBlockMatching : IStereoSolver
    {
        private StereoSGBM _stereoSGBM;

        public StereoSemiGlobalBlockMatching()
        {
        }

        public void ComputeDepthMap()
        {
            throw new NotImplementedException();
        }
    }
}
