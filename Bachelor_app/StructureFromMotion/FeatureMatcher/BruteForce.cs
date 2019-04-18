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
            var _bruteForceMatcher = CreateInstance();
            _bruteForceMatcher.Add(queryDescriptors);
            _bruteForceMatcher.KnnMatch(trainDescriptors, matches, 1, null);
            _bruteForceMatcher.Clear();
        }

        protected override dynamic CreateInstance()
        {
            return new BFMatcher(
               Model.Type,
               Model.CrossCheck
               );
        }
    }
}
