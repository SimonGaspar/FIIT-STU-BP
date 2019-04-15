using System.Threading;
using Bachelor_app.StructureFromMotion;
using Bachelor_app.StructureFromMotion.WindowsForm;
using Bakalárska_práca.Helper;
using Emgu.CV;
using Emgu.CV.Cuda;
using Emgu.CV.Util;

namespace Bakalárska_práca.StructureFromMotion.FeatureMatcher
{
    /// <summary>
    /// CudaBFMatcher algorithm
    /// </summary>
    public class CudaBruteForce : IFeatureMatcher
    {
        //CudaBFMatcher _cudaBruteForceMatcher = new CudaBFMatcher(DistanceType.Hamming);
        private CudaBruteForceForm _windowsForm;
        private CudaBruteForceModel model = new CudaBruteForceModel();
        private static object locker = new object();

        public CudaBruteForce()
        {
            UpdateModel(model);
        }

        public void Match(IInputArray queryDescriptors, IInputArray trainDescriptors, VectorOfVectorOfDMatch matches)
        {
            Monitor.Enter(locker);
            WindowsFormHelper.AddLogToConsole("In monitor");

            var _cudaBruteForceMatcher = CreateMatcher();
            var left = new GpuMat(queryDescriptors as Mat);
            var right = new GpuMat(trainDescriptors as Mat);
            _cudaBruteForceMatcher.KnnMatch(right, left, matches, 1);
            left.Dispose();
            right.Dispose();
            _cudaBruteForceMatcher.Dispose();
            WindowsFormHelper.AddLogToConsole("Exit monitor");
            Monitor.Exit(locker);
        }

        public void ShowSettingForm()
        {
            //_windowsForm = new CudaBruteForceForm(this);
            //_windowsForm.Show();
        }

        public void UpdateModel<T>(T model)
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
