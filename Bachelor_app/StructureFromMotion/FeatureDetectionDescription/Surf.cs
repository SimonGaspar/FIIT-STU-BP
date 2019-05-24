using Bachelor_app.Extension;
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
    /// SURF algorithm
    /// </summary>
    public class Surf : AbstractFeatureDetectorDescriptor, IFeatureDetector, IFeatureDescriptor
    {
        public Surf() : base(new SurfModel())
        {
            WindowsForm = null;
        }

        public override Mat ComputeDescriptor(KeyPointModel keyPoints)
        {
            var convertedResult = new Mat();

            using (Mat result = new Mat())
            {
                try
                {
                    using (Mat image = new Mat(keyPoints.InputFile.FullPath))
                    using (var _Surf = CreateInstance())
                        _Surf.Compute(image, keyPoints.DetectedKeyPoints, result);
                }
                catch (Exception e)
                {
                    throw e;
                }

                convertedResult = result.ConvertMatForMatching();
                result.Dispose();
            }
            return convertedResult;
        }

        public override MKeyPoint[] DetectKeyPoints(IInputArray image)
        {
            MKeyPoint[] result = null;

            try
            {
                using (var _Surf = CreateInstance())
                    result = _Surf.Detect(image);
            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }

        public override dynamic CreateInstance()
        {
            return new SURF(
                Model.HessianThreshold,
                Model.NOctaves,
                Model.NOctaveLayers,
                Model.Extended,
                Model.Upright
                );
        }
    }
}
