using Bachelor_app.Model;
using Bachelor_app.StructureFromMotion.Model;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.XFeatures2D;
using System;
using System.Threading;

namespace Bachelor_app.StructureFromMotion.FeatureDetectionDescription
{
    /// <summary>
    /// SIFT algorithm
    /// </summary>
    public class Sift : AbstractFeatureDetectorDescriptor, IFeatureDetector, IFeatureDescriptor
    {
        private static SemaphoreSlim semaphore = new SemaphoreSlim(2);
        public Sift() : base(new SiftModel())
        {
            WindowsForm = null;
        }

        public override Mat ComputeDescriptor(KeyPointModel keyPoints)
        {
            semaphore.Wait();
            Mat result = new Mat();
            try
            {
                using (Mat image = new Mat(keyPoints.InputFile.FullPath))
                using (var _sift = CreateInstance())
                    _sift.Compute(image, keyPoints.DetectedKeyPoints, result);
            }
            catch (Exception e)
            {
                throw e;
            }
            semaphore.Release();
            return result;
        }

        public override MKeyPoint[] DetectKeyPoints(IInputArray image)
        {
            semaphore.Wait();
            MKeyPoint[] result = null;

            try
            {
                using (var _sift = CreateInstance())
                    result = _sift.Detect(image);
            }
            catch (Exception e)
            {
                throw e;
            }
            semaphore.Release();
            return result;
        }

        public override dynamic CreateInstance()
        {
            return new SIFT(
                Model.NumberOfFeatures,
                Model.NOctaveLayers,
                Model.ContrastThreshold,
                Model.EdgeThreshold,
                Model.Sigma
                );
        }
    }
}
