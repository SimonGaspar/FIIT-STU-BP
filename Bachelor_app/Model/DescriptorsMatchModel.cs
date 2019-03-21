using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakalárska_práca.Model
{
    public class DescriptorsMatchModel
    {
        public DescriptorModel LeftDescriptor;
        public DescriptorModel RightDescriptor;
        public List<MDMatch[]> MatchesList;
        public Mat PerspectiveMatrix;
        public Mat Mask;
        public string FileFormatMatch;
        public List<MDMatch[]> FilteredMatchesList;
        public bool FilteredMatch;
    }
}
