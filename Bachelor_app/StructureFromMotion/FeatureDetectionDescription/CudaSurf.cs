using Bachelor_app.Model;
using Bachelor_app.StructureFromMotion.Model;
using Bachelor_app.StructureFromMotion.WindowsForm;
using Emgu.CV;
using Emgu.CV.Cuda;
using Emgu.CV.Structure;
using Emgu.CV.XFeatures2D;

namespace Bachelor_app.StructureFromMotion.FeatureDetectionDescription
{
    /// <summary>
    /// CudaORB algorithm
    /// </summary>
    public class CudaSurf : AbstractFeatureDetectorDescriptor, IFeatureDetector, IFeatureDescriptor
    {
        public CudaSurf() : base(new CudaSurfModel())
        {
            WindowsForm = null;
        }

        public override Mat ComputeDescriptor(KeyPointModel keyPoints)
        {
            var result = new GpuMat();
            using (var cudaSurf = CreateInstance())
            using (var mat = new Mat(keyPoints.InputFile.FullPath))
            using (Image<Gray, byte> image = new Image<Gray, byte>(mat.Bitmap))
            using (GpuMat gpumat = new GpuMat(image), keypoint = new GpuMat())
            {
                cudaSurf.UploadKeypoints(keyPoints.DetectedKeyPoints, keypoint);
                result = cudaSurf.ComputeDescriptorsRaw(gpumat, null, keypoint);
            }

            return result.ToMat();
        }

        public override MKeyPoint[] DetectKeyPoints(IInputArray input)
        {
            MKeyPoint[] result;

            using (var cudaSurf = CreateInstance())
            using (Image<Gray, byte> image = new Image<Gray, byte>((input as Mat).Bitmap))
            using (GpuMat gpumat = new GpuMat(image))
                result = cudaSurf.DetectKeyPoints(gpumat, null);

            return result;
        }

        public override dynamic CreateInstance()
        {
            return new Emgu.CV.XFeatures2D.CudaSURF(
                Model.HessianThreshold,
                Model.NOctaves,
                Model.NOctaveLayers,
                Model.Extended,
                Model.FeaturesRatio,
                Model.Upright
                );
        }
    }
}
