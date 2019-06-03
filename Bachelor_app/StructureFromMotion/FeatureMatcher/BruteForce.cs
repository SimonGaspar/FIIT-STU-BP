using Bachelor_app.StructureFromMotion.WindowsForm;
using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Util;

namespace Bachelor_app.StructureFromMotion.FeatureMatcher
{
    /// <summary>
    /// BFMatcher algorithm
    /// </summary>
    public class BruteForce : AbstractMatcher, IFeatureMatcher
    {
        public BruteForce()
            : base(new BruteForceModel())
        {
            WinForm = new BruteForceForm(this);
        }

        public override void Match(IInputArray queryDescriptors, IInputArray trainDescriptors, VectorOfVectorOfDMatch matches)
        {
            using (var bruteForceMatcher = CreateInstance())
            {
                bruteForceMatcher.Add(queryDescriptors);
                bruteForceMatcher.KnnMatch(trainDescriptors, matches, 1, null);
            }
        }

        public override dynamic CreateInstance()
        {
            return new BFMatcher(
               Model.Type,
               Model.CrossCheck
               );
        }
    }
}
