using Bachelor_app.StructureFromMotion;
using Bachelor_app.StructureFromMotion.WindowsForm;
using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Util;

namespace Bakalárska_práca.StructureFromMotion.FeatureMatcher
{
    public class BruteForce : IFeatureMatcher
    {
        BFMatcher _bruteForceMatcher;
        private BruteForceForm _windowsForm;
        private BruteForceModel model = new BruteForceModel();

        public BruteForce()
        {
            UpdateModel(model);
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

        public void ShowSettingForm()
        {
            _windowsForm = new BruteForceForm(this);
            _windowsForm.Show();
        }

        public void UpdateModel<T>(T model)
        {
            this.model = model as BruteForceModel;
            _bruteForceMatcher = new BFMatcher(
                this.model.Type,
                this.model.CrossCheck
                );
        }
    }
}
