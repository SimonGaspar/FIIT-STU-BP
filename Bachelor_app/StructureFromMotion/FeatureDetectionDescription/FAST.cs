using Bachelor_app.StructureFromMotion.Model;
using Bachelor_app.StructureFromMotion.WindowsForm;
using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;

namespace Bachelor_app.StructureFromMotion.FeatureDetectionDescription
{
    /// <summary>
    /// FAST algorithm
    /// </summary>
    public class FAST : AbstractFeatureDetectorDescriptor, IFeatureDetector
    {
        private FastForm _windowsForm;
        private FastModel model = new FastModel();

        public FAST()
        {
            UpdateModel(model);
        }

        public override MKeyPoint[] DetectKeyPoints(IInputArray image)
        {
            MKeyPoint[] result;
            var _fast = CreatDetector();
            result = _fast.Detect(image);

            return result;
        }

        public override void ShowSettingForm()
        {
            _windowsForm = new FastForm(this);
            _windowsForm.Show();
        }

        public override void UpdateModel<T>(T model)
        {
            this.model = model as FastModel;
        }

        private FastDetector CreatDetector()
        {
            var _fast = new FastDetector(
                this.model.Threshold,
                this.model.NonMaxSupression,
                this.model.Type
                );
            return _fast;
        }
    }
}
