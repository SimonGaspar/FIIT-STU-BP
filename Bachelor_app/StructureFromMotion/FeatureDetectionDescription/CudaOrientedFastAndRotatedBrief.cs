using Bachelor_app.StructureFromMotion.Model;
using Bachelor_app.StructureFromMotion.WindowsForm;
using Bakalárska_práca.Model;
using Emgu.CV;
using Emgu.CV.Cuda;
using Emgu.CV.Structure;

namespace Bakalárska_práca.StructureFromMotion.FeatureDetectionDescription
{
    public class CudaOrientedFastAndRotatedBrief : IFeatureDetector, IFeatureDescriptor
    {
        CudaORBDetector _cudaORB;
        private CudaOrientedFastAndRotatedBriefForm _windowsForm;
        private CudaOrientedFastAndRotatedBriefModel model = new CudaOrientedFastAndRotatedBriefModel();

        public CudaOrientedFastAndRotatedBrief()
        {
            model.NumberOfFeatures = 200000;
            UpdateModel(model);
        }

        public Mat ComputeDescriptor(KeyPointModel keyPoints)
        {
            Mat result = new Mat();
            Mat image = new Mat(keyPoints.InputFile.fileInfo.FullName);
            _cudaORB.Compute(image, keyPoints.DetectedKeyPoints, result);
            return result;
        }

        public MKeyPoint[] DetectKeyPoints(IInputArray image)
        {
            MKeyPoint[] result;
            result = _cudaORB.Detect(image);

            return result;
        }

        public void ShowSettingForm()
        {
            _windowsForm = new CudaOrientedFastAndRotatedBriefForm(this);
            _windowsForm.Show();
        }

        public void UpdateModel<T>(T model)
        {
            this.model = model as CudaOrientedFastAndRotatedBriefModel;
            _cudaORB = new CudaORBDetector(
                this.model.NumberOfFeatures,
                this.model.ScaleFactor,
                this.model.NLevels,
                this.model.EdgeThreshold,
                this.model.firstLevel,
                this.model.WTK_A,
                this.model.ScoreType,
                this.model.PatchSize,
                this.model.FastThreshold,
                this.model.BlurForDescriptor
                );
        }
    }
}
