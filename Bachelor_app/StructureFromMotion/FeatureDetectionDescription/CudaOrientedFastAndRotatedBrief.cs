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
        public CudaOrientedFastAndRotatedBrief() : base(new CudaOrientedFastAndRotatedBriefModel(30000))
        {
            WindowsForm = new CudaOrientedFastAndRotatedBriefForm(this);
        }

        public override Mat ComputeDescriptor(KeyPointModel keyPoints)
        {
            var cudaORB = CreateInstance();
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
            var cudaORB = CreateInstance();
            var mat = input as Mat;
            Image<Gray, byte> image = new Image<Gray, byte>(mat.Bitmap);
            MKeyPoint[] result;
            GpuMat gpumat = new GpuMat(image);

            result = cudaORB.Detect(gpumat);

            return result;
        }

        protected override dynamic CreateInstance()
        {
            return new CudaORBDetector(
                Model.NumberOfFeatures,
                Model.ScaleFactor,
                Model.NLevels,
                Model.EdgeThreshold,
                Model.FirstLevel,
                Model.WTK_A,
                Model.ScoreType,
                Model.PatchSize,
                Model.FastThreshold,
                Model.BlurForDescriptor
                );
        }
    }
}
