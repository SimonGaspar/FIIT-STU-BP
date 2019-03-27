using System.Threading;
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
        private static object locker = new object();

        public BruteForce()
        {
            UpdateModel(model);
        }

        public void Add(IInputArray Descriptor)
        {
            _bruteForceMatcher.Add(Descriptor);
        }

        public void Match(IInputArray queryDescriptors, IInputArray trainDescriptors, VectorOfVectorOfDMatch matches)
        {
            Monitor.Enter(locker);
            Add(trainDescriptors);
            _bruteForceMatcher.KnnMatch(queryDescriptors, matches, 1, null);
            _bruteForceMatcher.Clear();
            Monitor.Exit(locker);
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
