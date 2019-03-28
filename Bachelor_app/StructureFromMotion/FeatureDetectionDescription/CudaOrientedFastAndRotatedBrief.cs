using Bachelor_app.StructureFromMotion.Model;
using Bachelor_app.StructureFromMotion.WindowsForm;
using Bakalárska_práca.Model;
using Emgu.CV;
using Emgu.CV.Cuda;
using Emgu.CV.Structure;
using System;

namespace Bakalárska_práca.StructureFromMotion.FeatureDetectionDescription
{
    public class CudaOrientedFastAndRotatedBrief : IFeatureDetector, IFeatureDescriptor
    {
        private CudaOrientedFastAndRotatedBriefForm _windowsForm;
        private CudaOrientedFastAndRotatedBriefModel model = new CudaOrientedFastAndRotatedBriefModel();

        public CudaOrientedFastAndRotatedBrief()
        {
            model.NumberOfFeatures = 30000;
        }

        public Mat ComputeDescriptor(KeyPointModel keyPoints)
        {
            var cudaORB = CreateDetector();
            var mat = new Mat(keyPoints.InputFile.fileInfo.FullName);
            Image<Gray, byte> image = new Image<Gray, byte>(mat.Bitmap);
            GpuMat gpumat = new GpuMat(image);

            GpuMat result = new GpuMat();
            cudaORB.Compute(gpumat, keyPoints.DetectedKeyPoints, result);
            var returnResult = result.ToMat();
            gpumat.Dispose();
            result.Dispose();
            cudaORB.Dispose();
            return returnResult;
        }

        public MKeyPoint[] DetectKeyPoints(IInputArray input)
        {
            var cudaORB = CreateDetector();
            var mat = input as Mat;
            Image<Gray, byte> image = new Image<Gray, byte>(mat.Bitmap);
            MKeyPoint[] result;
            GpuMat gpumat = new GpuMat(image);


            result = cudaORB.Detect(gpumat);
            gpumat.Dispose();
            cudaORB.Dispose();
            return result;
        }

        public void ShowSettingForm()
        {
            _windowsForm = new CudaOrientedFastAndRotatedBriefForm(this);
            _windowsForm.Show();
        }

        public void UpdateModel<T>(T model)
        {
            this.model = model as CudaOrientedFastAndRotatedBriefModel;
            
        }

        public CudaORBDetector CreateDetector() {
            var _cudaORB = new CudaORBDetector(
                this.model.NumberOfFeatures,
                this.model.ScaleFactor,
                this.model.NLevels,
                this.model.EdgeThreshold,
                this.model.firstLevel,
                this.model.WTK_A,
                this.model.ScoreType,
                this.model.PatchSize,
                this.model.FastThreshold,
                this.model.BlurForDescriptor
                );
            return _cudaORB;
        }
    }
}
