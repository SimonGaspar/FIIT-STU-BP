using Emgu.CV;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakalárska_práca.StructureFromMotion
{
    public interface IFeatureMatcher
    {
        void Add(Mat Descriptor);
        void Match(Mat Descriptor, VectorOfVectorOfDMatch matches);
    }
}
