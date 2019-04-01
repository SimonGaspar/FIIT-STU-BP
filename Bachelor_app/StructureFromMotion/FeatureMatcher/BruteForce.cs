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
        private BruteForceForm _windowsForm;
        private BruteForceModel model = new BruteForceModel();
        private static object locker = new object();

        public BruteForce()
        {
            UpdateModel(model);
        }
        
        public void Match(IInputArray queryDescriptors, IInputArray trainDescriptors, VectorOfVectorOfDMatch matches)
        {
            var _bruteForceMatcher = CreateMatcher();
            _bruteForceMatcher.Add(queryDescriptors);
            _bruteForceMatcher.KnnMatch(trainDescriptors, matches, 1, null);
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
