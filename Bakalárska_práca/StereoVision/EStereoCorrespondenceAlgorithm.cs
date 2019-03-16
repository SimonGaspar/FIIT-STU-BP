using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakalárska_práca.StereoVision
{
    public enum EStereoCorrespondenceAlgorithm
    {
        StereoBM,
        StereoSGBM,
        CudaStereoBM,
        CudaStereoConstantSpaceBP
    }
}
