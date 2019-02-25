using System;
using Emgu.CV.Cuda;

namespace Bakalárska_práca.StereoVision.StereoCorrespondence
{
    public class CudaStereoConstantSpaceBeliefPropagation : IStereoSolver
    {
        CudaStereoConstantSpaceBP _cudaStereoConstantSpaceBP;

        public CudaStereoConstantSpaceBeliefPropagation()
        {
        }

        public void ComputeDepthMap()
        {
            throw new NotImplementedException();
        }
    }
}
