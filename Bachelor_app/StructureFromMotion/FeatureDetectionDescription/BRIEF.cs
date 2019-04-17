using Bachelor_app.Model;
using Bachelor_app.StructureFromMotion.Model;
using Bachelor_app.StructureFromMotion.WindowsForm;
using Emgu.CV;
using Emgu.CV.XFeatures2D;

namespace Bachelor_app.StructureFromMotion.FeatureDetectionDescription
{
    /// <summary>
    /// BRIEF algorithm
    /// </summary>
    public class BRIEF : AbstractFeatureDetectorDescriptor, IFeatureDescriptor
    {
        private BriefForm _windowsForm;
        private BriefModel model = new BriefModel();

        public BRIEF()
        {
            UpdateModel(model);
        }

        public override Mat ComputeDescriptor(KeyPointModel keyPoints)
        {
            Mat result = new Mat();
            Mat image = new Mat(keyPoints.InputFile.FullPath);

            var _brief = CreatExtractor();
            _brief.Compute(image, keyPoints.DetectedKeyPoints, result);
            return result;
        }

        public override void ShowSettingForm()
        {
            _windowsForm = new BriefForm(this);
            _windowsForm.Show();
        }

        public override void UpdateModel<T>(T model)
        {
            this.model = model as BriefModel;
        }

        private BriefDescriptorExtractor CreatExtractor()
        {
            var _brief = new BriefDescriptorExtractor(this.model.DescriptorSize);
            return _brief;
        }
    }
}
