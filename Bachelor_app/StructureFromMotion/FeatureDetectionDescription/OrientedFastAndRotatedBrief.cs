using Bachelor_app.StructureFromMotion.Model;
using Bachelor_app.StructureFromMotion.WindowsForm;
using Bakalárska_práca.Model;
using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;

namespace Bakalárska_práca.StructureFromMotion.FeatureDetectionDescription
{
    public class OrientedFastAndRotatedBrief : IFeatureDetector, IFeatureDescriptor
    {
        ORBDetector _orb;
        private OrientedFastAndRotatedBriefForm _windowsForm;
        private OrientedFastAndRotatedBriefModel model;

        public OrientedFastAndRotatedBrief()
        {
            //_orb = new ORBDetector(200000);
        }

        public Mat ComputeDescriptor(KeyPointModel keyPoints)
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

        public void ShowSettingForm()
        {
            _windowsForm = new OrientedFastAndRotatedBriefForm(this);
            _windowsForm.Show();
        }

        public void UpdateModel<T>(T model)
        {
            this.model = model as OrientedFastAndRotatedBriefModel;
            _orb = new ORBDetector(
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
        }
    }
}
