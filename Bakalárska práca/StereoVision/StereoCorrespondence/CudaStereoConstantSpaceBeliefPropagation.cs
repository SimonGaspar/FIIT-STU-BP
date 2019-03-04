using System;
using Emgu.CV;
using Emgu.CV.Cuda;
using Emgu.CV.Structure;

namespace Bakalárska_práca.StereoVision.StereoCorrespondence
{
    public class CudaStereoConstantSpaceBeliefPropagation //: IStereoSolver
    {
        CudaStereoConstantSpaceBP _cudaStereoConstantSpaceBP;

        public CudaStereoConstantSpaceBeliefPropagation()
        {
        }

        public void ComputeDepthMap<T>(T leftImage, T rightImage) where T : Image<Bgr, byte>
        {
            throw new NotImplementedException();
        }
    }
}
