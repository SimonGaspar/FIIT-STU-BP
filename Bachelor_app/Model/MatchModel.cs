using System.Collections.Generic;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Bakalárska_práca.Model
{
    /// <summary>
    /// Descriptor matches model
    /// </summary>
    public class MatchModel
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
