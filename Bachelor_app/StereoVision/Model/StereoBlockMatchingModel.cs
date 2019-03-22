using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakalárska_práca.StereoVision
{
    public class StereoBlockMatchingModel
    {
        public int Disparity { get; set; } = 16;
        public int BlockSize { get; set; } = 15;
    }
}
