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
            var result = new GpuMat();

            using (var cudaORB = CreateInstance())
            using (var mat = new Mat(keyPoints.InputFile.FullPath))
            using (Image<Gray, byte> image = new Image<Gray, byte>(mat.Bitmap))
            using (GpuMat gpumat = new GpuMat(image))
                cudaORB.Compute(gpumat, keyPoints.DetectedKeyPoints, result);

            return result.ToMat();
        }

        public override MKeyPoint[] DetectKeyPoints(IInputArray input)
        {
            MKeyPoint[] result;

            using (var cudaORB = CreateInstance())
            using (Image<Gray, byte> image = new Image<Gray, byte>((input as Mat).Bitmap))
            using (GpuMat gpumat = new GpuMat(image))
                result = cudaORB.Detect(gpumat);

            return result;
        }

        public override dynamic CreateInstance()
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
