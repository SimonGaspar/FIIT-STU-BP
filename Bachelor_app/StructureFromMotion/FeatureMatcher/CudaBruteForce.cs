﻿using Bachelor_app.StructureFromMotion;
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
        
        public void Match(IInputArray queryDescriptors, IInputArray trainDescriptors, VectorOfVectorOfDMatch matches)
        {
            var _cudaBruteForceMatcher = CreateMatcher();
            var left = new GpuMat(queryDescriptors as Mat);
            var right = new GpuMat(trainDescriptors as Mat);
            _cudaBruteForceMatcher.KnnMatch(right, left, matches, 1);
            left.Dispose();
            right.Dispose();
            _cudaBruteForceMatcher.Dispose();
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
