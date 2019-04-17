using Bachelor_app.Model;
using Bachelor_app.StructureFromMotion.Model;
using Bachelor_app.StructureFromMotion.WindowsForm;
using Emgu.CV;
using Emgu.CV.Cuda;
using Emgu.CV.Structure;

namespace Bachelor_app.StructureFromMotion.FeatureDetectionDescription
{
    /// <summary>
    /// CudaORB algorithm
    /// </summary>
    public class CudaOrientedFastAndRotatedBrief : AbstractFeatureDetectorDescriptor, IFeatureDetector, IFeatureDescriptor
    {
        private CudaOrientedFastAndRotatedBriefForm _windowsForm;
        private CudaOrientedFastAndRotatedBriefModel model = new CudaOrientedFastAndRotatedBriefModel();

        public CudaOrientedFastAndRotatedBrief()
        {
            model.NumberOfFeatures = 30000;
        }

        public override Mat ComputeDescriptor(KeyPointModel keyPoints)
        {
            var cudaORB = CreateDetectorExtractor();
            var mat = new Mat(keyPoints.InputFile.FullPath);
            Image<Gray, byte> image = new Image<Gray, byte>(mat.Bitmap);
            GpuMat gpumat = new GpuMat(image);

            GpuMat result = new GpuMat();
            cudaORB.Compute(gpumat, keyPoints.DetectedKeyPoints, result);
            var returnResult = result.ToMat();

            return returnResult;
        }

        public override MKeyPoint[] DetectKeyPoints(IInputArray input)
        {
            var cudaORB = CreateDetectorExtractor();
            var mat = input as Mat;
            Image<Gray, byte> image = new Image<Gray, byte>(mat.Bitmap);
            MKeyPoint[] result;
            GpuMat gpumat = new GpuMat(image);

            result = cudaORB.Detect(gpumat);

            return result;
        }

        public override void ShowSettingForm()
        {
            _windowsForm = new CudaOrientedFastAndRotatedBriefForm(this);
            _windowsForm.Show();
        }

        public override void UpdateModel<T>(T model)
        {
            this.model = model as CudaOrientedFastAndRotatedBriefModel;
        }

        private CudaORBDetector CreateDetectorExtractor()
        {
            var _cudaORB = new CudaORBDetector(
                this.model.NumberOfFeatures,
                this.model.ScaleFactor,
                this.model.NLevels,
                this.model.EdgeThreshold,
                this.model.FirstLevel,
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
