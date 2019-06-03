using System;
using System.Threading;
using Bachelor_app.Extension;
using Bachelor_app.Model;
using Bachelor_app.StructureFromMotion.Model;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.XFeatures2D;

namespace Bachelor_app.StructureFromMotion.FeatureDetectionDescription
{
    /// <summary>
    /// SIFT algorithm
    /// </summary>
    public class Sift : AbstractFeatureDetectorDescriptor, IFeatureDetector, IFeatureDescriptor
    {
        private static SemaphoreSlim semaphore = new SemaphoreSlim(1);

        public Sift()
            : base(new SiftModel())
        {
            WindowsForm = null;
        }

        public override Mat ComputeDescriptor(KeyPointModel keyPoints)
        {
            semaphore.Wait();
            var convertedResult = new Mat();

            using (Mat result = new Mat())
            {
                try
                {
                    using (Mat image = new Mat(keyPoints.InputFile.FullPath))
                    using (var sift = CreateInstance())
                        sift.Compute(image, keyPoints.DetectedKeyPoints, result);
                }
                catch (Exception e)
                {
                    throw e;
                }

                convertedResult = result.ConvertMatForMatching();
                result.Dispose();
            }

            semaphore.Release();
            return convertedResult;
        }

        public override MKeyPoint[] DetectKeyPoints(IInputArray image)
        {
            semaphore.Wait();
            MKeyPoint[] result = null;

            try
            {
                using (var sift = CreateInstance())
                    result = sift.Detect(image);
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
