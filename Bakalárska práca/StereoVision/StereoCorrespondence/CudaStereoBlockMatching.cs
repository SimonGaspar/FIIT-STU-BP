using System;
using Emgu.CV.Cuda;

namespace Bakalárska_práca.StereoVision.StereoCorrespondence
{
    public class CudaStereoBlockMatching : IStereoSolver
    {
        CudaStereoBM _cudaStereoBM;

        public CudaStereoBlockMatching()
        {
        }

        public void ComputeDepthMap()
        {
            throw new NotImplementedException();
        }
    }
}
