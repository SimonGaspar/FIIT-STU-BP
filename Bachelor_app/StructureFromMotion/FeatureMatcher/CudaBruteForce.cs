using System.Threading;
using Bachelor_app.StructureFromMotion.WindowsForm;
using Emgu.CV;
using Emgu.CV.Cuda;
using Emgu.CV.Util;

namespace Bachelor_app.StructureFromMotion.FeatureMatcher
{
    /// <summary>
    /// CudaBFMatcher algorithm
    /// </summary>
    public class CudaBruteForce : AbstractMatcher, IFeatureMatcher
    {
        private CudaBruteForceForm _windowsForm;
        private CudaBruteForceModel model = new CudaBruteForceModel();
        private static object locker = new object();

        public CudaBruteForce()
        {
            UpdateModel(model);
        }

        public override void Match(IInputArray queryDescriptors, IInputArray trainDescriptors, VectorOfVectorOfDMatch matches)
        {
            Monitor.Enter(locker);
            var _cudaBruteForceMatcher = CreateMatcher();
            var left = new GpuMat(queryDescriptors as Mat);
            var right = new GpuMat(trainDescriptors as Mat);
            _cudaBruteForceMatcher.KnnMatch(right, left, matches, 1);
            Monitor.Exit(locker);
        }

        public override void ShowSettingForm()
        {
            _windowsForm = new CudaBruteForceForm(this);
            _windowsForm.Show();
        }

        public override void UpdateModel<T>(T model)
        {
            this.model = model as CudaBruteForceModel;
        }

        public CudaBFMatcher CreateMatcher()
        {
            var _cudaBruteForceMatcher = new CudaBFMatcher(
               this.model.Type
               );
            return _cudaBruteForceMatcher;
        }
    }
}
