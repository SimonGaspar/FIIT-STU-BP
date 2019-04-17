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
        private BruteForceForm _windowsForm;
        private BruteForceModel model = new BruteForceModel();

        public BruteForce()
        {
            UpdateModel(model);
        }

        public override void Match(IInputArray queryDescriptors, IInputArray trainDescriptors, VectorOfVectorOfDMatch matches)
        {
            var _bruteForceMatcher = CreateMatcher();
            _bruteForceMatcher.Add(queryDescriptors);
            _bruteForceMatcher.KnnMatch(trainDescriptors, matches, 1, null);
            _bruteForceMatcher.Clear();
        }

        public override void ShowSettingForm()
        {
            _windowsForm = new BruteForceForm(this);
            _windowsForm.Show();
        }

        public override void UpdateModel<T>(T model)
        {
            this.model = model as BruteForceModel;

        }

        public BFMatcher CreateMatcher()
        {
            var _bruteForceMatcher = new BFMatcher(
               this.model.Type,
               this.model.CrossCheck
               );
            return _bruteForceMatcher;
        }
    }
}
