using Bachelor_app.Model;
using Emgu.CV;
using Emgu.CV.Cuda;
using Emgu.CV.Structure;
using Bachelor_app.StructureFromMotion.Model;
using System;
using Bachelor_app.Helper;

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
            Mat returnValue;
            using (var cudaSurf = CreateInstance())
            using (var mat = new Mat(keyPoints.InputFile.FullPath))
            using (Image<Gray, byte> image = new Image<Gray, byte>(mat.Bitmap))
            using (GpuMat gpumat = new GpuMat(image), keypoint = new GpuMat())
            {
                try
                {
                    cudaSurf.UploadKeypoints(keyPoints.DetectedKeyPoints, keypoint);
                    result = cudaSurf.ComputeDescriptorsRaw(gpumat, null, keypoint);
                    
                    returnValue = result.ToMat();
                    result.Dispose();
                }
                catch (Exception e)
                {
                    WindowsFormHelper.AddLogToConsole($"Error in computing descriptors:\n{e.Message}\nStart computing descriptors with CPU version of algorithm.\n");
                    returnValue = new Surf().ComputeDescriptor(keyPoints);
                }
            }
            return returnValue;
        }

        public override MKeyPoint[] DetectKeyPoints(IInputArray input)
        {
            MKeyPoint[] result;

            using (var cudaSurf = CreateInstance())
            using (Image<Gray, byte> image = new Image<Gray, byte>((input as Mat).Bitmap))
            using (GpuMat gpumat = new GpuMat(image))
            {
                try
                {
                    result = cudaSurf.DetectKeyPoints(gpumat, null);
                }
                catch (Exception e)
                {
                    WindowsFormHelper.AddLogToConsole($"Error in finding key points:\n{e.Message}\n Start finding key points with CPU version of algorithm.\n");
                    result = new Surf().DetectKeyPoints(input);
                }
            }

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
