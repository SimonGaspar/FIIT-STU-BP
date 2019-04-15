using Bachelor_app.StructureFromMotion.FeatureDetectionDescription;
using Bachelor_app.StructureFromMotion.Model;
using Bachelor_app.StructureFromMotion.WindowsForm;
using Bakalárska_práca.Model;
using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;

namespace Bakalárska_práca.StructureFromMotion.FeatureDetectionDescription
{
    /// <summary>
    /// ORB algorithm
    /// </summary>
    public class OrientedFastAndRotatedBrief : AbstractFeatureDetectorDescriptor, IFeatureDetector, IFeatureDescriptor
    {
        private OrientedFastAndRotatedBriefForm _windowsForm;
        private OrientedFastAndRotatedBriefModel model = new OrientedFastAndRotatedBriefModel();

        public OrientedFastAndRotatedBrief()
        {
            model.NumberOfFeatures = 30000;
            UpdateModel(model);
        }

        public override Mat ComputeDescriptor(KeyPointModel keyPoints)
        {
            Mat result = new Mat();
            Mat image = new Mat(keyPoints.InputFile.fileInfo.FullName);
            var _orb = CreateDetectorExtractor();
            _orb.Compute(image, keyPoints.DetectedKeyPoints, result);
            return result;
        }

        public override MKeyPoint[] DetectKeyPoints(IInputArray image)
        {
            MKeyPoint[] result;
            var _orb = CreateDetectorExtractor();
            result = _orb.Detect(image);

            return result;
        }

        public override void ShowSettingForm()
        {
            _windowsForm = new OrientedFastAndRotatedBriefForm(this);
            _windowsForm.Show();
        }

        public override void UpdateModel<T>(T model)
        {
            this.model = model as OrientedFastAndRotatedBriefModel;
        }

        public ORBDetector CreateDetectorExtractor()
        {
            var _orb = new ORBDetector(
                this.model.NumberOfFeatures,
                this.model.ScaleFactor,
                this.model.NLevels,
                this.model.EdgeThreshold,
                this.model.firstLevel,
                this.model.WTK_A,
                this.model.ScoreType,
                this.model.PatchSize,
                this.model.FastThreshold
                );
            return _orb;
        }
    }
}
