using Bachelor_app.StructureFromMotion.Model;
using Bachelor_app.StructureFromMotion.WindowsForm;
using Bakalárska_práca.Model;
using Bakalárska_práca.StructureFromMotion;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.XFeatures2D;

namespace Bachelor_app.StructureFromMotion.FeatureDetectionDescription
{
    public class FREAK : IFeatureDetector, IFeatureDescriptor
    {
        Freak _freak;
        private FreakForm _windowsForm;
        private FreakModel model = new FreakModel();

        public FREAK()
        {
            UpdateModel(model);
        }

        public Mat ComputeDescriptor(KeyPointModel keyPoints)
        {
            Mat result = new Mat();
            Mat image = new Mat(keyPoints.InputFile.fileInfo.FullName);
            _freak.Compute(image, keyPoints.DetectedKeyPoints, result);
            return result;
        }

        public MKeyPoint[] DetectKeyPoints(IInputArray image)
        {
            MKeyPoint[] result;
            result = _freak.Detect(image);

            return result;
        }

        public void ShowSettingForm()
        {
            _windowsForm = new FreakForm(this);
            _windowsForm.Show();
        }

        public void UpdateModel<T>(T model)
        {
            this.model = model as FreakModel;
            _freak = new Freak(
                this.model.OrientationNormalized,
                this.model.ScaleNormalized,
                this.model.PatternScale,
                this.model.NOctaves
                );
        }
    }
}
