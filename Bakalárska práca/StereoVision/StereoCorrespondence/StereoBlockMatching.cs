using System;
using Emgu.CV;

namespace Bakalárska_práca.StereoVision.StereoCorrespondence
{
    public class StereoBlockMatching : IStereoSolver
    {
        private StereoBM _stereoBM;

        public StereoBlockMatching()
        {
        }

        public void ComputeDepthMap()
        {
            throw new NotImplementedException();
        }
    }
}
