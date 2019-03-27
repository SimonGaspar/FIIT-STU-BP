using Bachelor_app.StructureFromMotion;
using Bachelor_app.StructureFromMotion.WindowsForm;
using Emgu.CV;
using Emgu.CV.Cuda;
using Emgu.CV.Features2D;
using Emgu.CV.Util;

namespace Bakalárska_práca.StructureFromMotion.FeatureMatcher
{
    public class CudaBruteForce : IFeatureMatcher
    {
        //CudaBFMatcher _cudaBruteForceMatcher = new CudaBFMatcher(DistanceType.Hamming);
        private CudaBruteForceForm _windowsForm;
        private CudaBruteForceModel model = new CudaBruteForceModel();

        public CudaBruteForce()
        {
            UpdateModel(model);
        }

        public void Add(IInputArray Descriptor)
        {
        }

        public void Match(IInputArray queryDescriptors, IInputArray trainDescriptors, VectorOfVectorOfDMatch matches)
        {
            var _cudaBruteForceMatcher = new CudaBFMatcher(DistanceType.Hamming);
            _cudaBruteForceMatcher.KnnMatch(queryDescriptors, trainDescriptors, matches, 1);
        }

        public void ShowSettingForm()
        {
            //_windowsForm = new CudaBruteForceForm(this);
            //_windowsForm.Show();
        }

        public void UpdateModel<T>(T model)
        {
            //this.model = model as CudaBruteForceModel;
            //_cudaBruteForceMatcher = new CudaBFMatcher(
            //    this.model.Type
            //    );
        }
    }
}
