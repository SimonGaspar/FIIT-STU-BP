using System;
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
        private static readonly object locker = new object();

        public CudaBruteForce()
            : base(new CudaBruteForceModel())
        {
            WinForm = new CudaBruteForceForm(this);
        }

        public override void Match(IInputArray queryDescriptors, IInputArray trainDescriptors, VectorOfVectorOfDMatch matches)
        {
            try
            {
                using (var _cudaBruteForceMatcher = CreateInstance())
                using (GpuMat left = new GpuMat(queryDescriptors as Mat), right = new GpuMat(trainDescriptors as Mat))
                    _cudaBruteForceMatcher.KnnMatch(right, left, matches, 1);
            }
            catch (Exception e)
            {
                Console.WriteLine("a");
            }
        }

        public override dynamic CreateInstance()
        {
            return new CudaBFMatcher(Model.Type);
        }
    }
}
