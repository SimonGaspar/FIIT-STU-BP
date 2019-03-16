using Bakalárska_práca.Model;
using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace Bakalárska_práca.StructureFromMotion.FeatureDetectionDescription
{
    public class OrientedFastAndRotatedBrief : IFeatureDetector, IFeatureDescriptor
    {
        ORBDetector _orb;

        public OrientedFastAndRotatedBrief()
        {
            _orb = new ORBDetector();
        }

        public Mat ComputeDescriptor(KeyPoint keyPoints)
        {
            Mat result = new Mat();
            Mat image = new Mat(keyPoints.InputFile.fileInfo.FullName);
            _orb.Compute(image, keyPoints.DetectedKeyPoints, result);
            return result;
        }

        public MKeyPoint[] DetectKeyPoints(IInputArray image)
        {
            MKeyPoint[] result;
            result = _orb.Detect(image);

            return result;
        }
    }
}
