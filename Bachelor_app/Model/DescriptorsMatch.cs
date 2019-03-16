using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakalárska_práca.Model
{
    public class DescriptorsMatch
    {
        public Descriptor LeftDescriptor;
        public Descriptor RightDescriptor;
        public List<MDMatch[]> MatchesList;
        public Mat PerspectiveMatrix;
        public Mat Mask;
        public string FileFormatMatch;
        public List<MDMatch[]> FilteredMatchesList;
        public bool FilteredMatch;
    }
}
