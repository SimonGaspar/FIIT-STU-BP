using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Util;

namespace Bakalárska_práca.StructureFromMotion.FeatureMatcher
{
    public class BruteForce : IFeatureMatcher
    {
        BFMatcher _bruteForceMatcher;

        public BruteForce()
        {
            _bruteForceMatcher = new BFMatcher(DistanceType.Hamming, true);
        }

        public void Add(Mat Descriptor)
        {
            _bruteForceMatcher.Add(Descriptor);
        }

        public void Match(Mat Descriptor, VectorOfVectorOfDMatch matches)
        {
            _bruteForceMatcher.KnnMatch(Descriptor, matches, 1, null);
            _bruteForceMatcher.Clear();
        }
    }
}
