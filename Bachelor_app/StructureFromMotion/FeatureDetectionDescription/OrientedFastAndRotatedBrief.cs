using Bachelor_app.Model;
using Bachelor_app.StructureFromMotion.Model;
using Bachelor_app.StructureFromMotion.WindowsForm;
using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;

namespace Bachelor_app.StructureFromMotion.FeatureDetectionDescription
{
    /// <summary>
    /// ORB algorithm
    /// </summary>
    public class OrientedFastAndRotatedBrief : AbstractFeatureDetectorDescriptor, IFeatureDetector, IFeatureDescriptor
    {
        public OrientedFastAndRotatedBrief() : base(new OrientedFastAndRotatedBriefModel(30000))
        {
            WindowsForm = new OrientedFastAndRotatedBriefForm(this);
        }

        public override Mat ComputeDescriptor(KeyPointModel keyPoints)
        {
            Mat result = new Mat();
            Mat image = new Mat(keyPoints.InputFile.FullPath);
            var _orb = CreateInstance();
            _orb.Compute(image, keyPoints.DetectedKeyPoints, result);
            return result;
        }

        public override MKeyPoint[] DetectKeyPoints(IInputArray image)
        {
            MKeyPoint[] result;
            var _orb = CreateInstance();
            result = _orb.Detect(image);

            return result;
        }

        protected override dynamic CreateInstance()
        {
            return new ORBDetector(
                Model.NumberOfFeatures,
                Model.ScaleFactor,
                Model.NLevels,
                Model.EdgeThreshold,
                Model.FirstLevel,
                Model.WTK_A,
                Model.ScoreType,
                Model.PatchSize,
                Model.FastThreshold
                );
        }
    }
}
