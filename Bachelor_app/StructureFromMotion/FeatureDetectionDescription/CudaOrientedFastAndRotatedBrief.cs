using System;
using Bachelor_app.Helper;
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
        public CudaOrientedFastAndRotatedBrief()
            : base(new CudaOrientedFastAndRotatedBriefModel())
        {
            WindowsForm = new CudaOrientedFastAndRotatedBriefForm(this);
        }

        public override Mat ComputeDescriptor(KeyPointModel keyPoints)
        {
            var result = new GpuMat();
            Mat returnValue;
            using (var cudaORB = CreateInstance())
            using (var mat = new Mat(keyPoints.InputFile.FullPath))
            using (Image<Gray, byte> image = new Image<Gray, byte>(mat.Bitmap))
            using (GpuMat gpumat = new GpuMat(image))
            {
                try
                {
                    cudaORB.Compute(gpumat, keyPoints.DetectedKeyPoints, result);

                    returnValue = result.ToMat();
                    result.Dispose();
                }
                catch (Exception e)
                {
                    WindowsFormHelper.AddLogToConsole($"Error in computing descriptors:\n{e.Message}\n Start computing descriptors with CPU version of algorithm.\n");
                    returnValue = new OrientedFastAndRotatedBrief().ComputeDescriptor(keyPoints);
                }
            }

            return returnValue;
        }

        public override MKeyPoint[] DetectKeyPoints(IInputArray input)
        {
            MKeyPoint[] result;

            using (var cudaORB = CreateInstance())
            using (Image<Gray, byte> image = new Image<Gray, byte>((input as Mat).Bitmap))
            using (GpuMat gpumat = new GpuMat(image))
            {
                try
                {
                    result = cudaORB.Detect(gpumat);
                }
                catch (Exception e)
                {
                    WindowsFormHelper.AddLogToConsole($"Error in finding key points:\n{e.Message}\n Start finding key points with CPU version of algorithm.\n");
                    result = new OrientedFastAndRotatedBrief().DetectKeyPoints(input);
                }
            }

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
