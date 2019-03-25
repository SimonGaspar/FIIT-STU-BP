using Bachelor_app.StructureFromMotion.Model;
using Bachelor_app.StructureFromMotion.WindowsForm;
using Bakalárska_práca.Model;
using Bakalárska_práca.StructureFromMotion;
using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;

namespace Bachelor_app.StructureFromMotion.FeatureDetectionDescription
{
    public class FAST : IFeatureDetector, IFeatureDescriptor
    {
        FastDetector _fast;
        private FastForm _windowsForm;
        private FastModel model = new FastModel();

        public FAST()
        {
            UpdateModel(model);
        }

        public Mat ComputeDescriptor(KeyPointModel keyPoints)
        {
            Mat result = new Mat();
            Mat image = new Mat(keyPoints.InputFile.fileInfo.FullName);
            _fast.Compute(image, keyPoints.DetectedKeyPoints, result);
            return result;
        }

        public MKeyPoint[] DetectKeyPoints(IInputArray image)
        {
            MKeyPoint[] result;
            result = _fast.Detect(image);

            return result;
        }

        public void ShowSettingForm()
        {
            _windowsForm = new FastForm(this);
            _windowsForm.Show();
        }

        public void UpdateModel<T>(T model)
        {
            this.model = model as FastModel;
            _fast = new FastDetector(
                this.model.Threshold,
                this.model.NonMaxSupression,
                this.model.Type
                );
        }
    }
}
