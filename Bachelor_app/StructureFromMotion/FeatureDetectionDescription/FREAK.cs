using Bachelor_app.Model;
using Bachelor_app.StructureFromMotion.Model;
using Bachelor_app.StructureFromMotion.WindowsForm;
using Emgu.CV;
using Emgu.CV.XFeatures2D;

namespace Bachelor_app.StructureFromMotion.FeatureDetectionDescription
{
    /// <summary>
    /// FREAK algorithm
    /// </summary>
    public class FREAK : AbstractFeatureDetectorDescriptor, IFeatureDescriptor
    {
        private FreakForm _windowsForm;
        private FreakModel model = new FreakModel();

        public FREAK()
        {
            UpdateModel(model);
        }

        public override Mat ComputeDescriptor(KeyPointModel keyPoints)
        {
            var _freak = CreateExtractor();
            Mat result = new Mat();
            Mat image = new Mat(keyPoints.InputFile.FullPath);
            _freak.Compute(image, keyPoints.DetectedKeyPoints, result);
            return result;
        }

        public override void ShowSettingForm()
        {
            _windowsForm = new FreakForm(this);
            _windowsForm.Show();
        }

        public override void UpdateModel<T>(T model)
        {
            this.model = model as FreakModel;
        }

        private Freak CreateExtractor()
        {
            var _freak = new Freak(
                this.model.OrientationNormalized,
                this.model.ScaleNormalized,
                this.model.PatternScale,
                this.model.NOctaves
                );
            return _freak;
        }
    }
}
