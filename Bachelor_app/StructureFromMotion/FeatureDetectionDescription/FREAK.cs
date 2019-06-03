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
        public FREAK()
            : base(new FreakModel())
        {
            WindowsForm = new FreakForm(this);
        }

        public override Mat ComputeDescriptor(KeyPointModel keyPoints)
        {
            Mat result = new Mat();

            using (Mat image = new Mat(keyPoints.InputFile.FullPath))
            using (var freak = CreateInstance())
                freak.Compute(image, keyPoints.DetectedKeyPoints, result);

            return result;
        }

        public override dynamic CreateInstance()
        {
            return new Freak(
                Model.OrientationNormalized,
                Model.ScaleNormalized,
                Model.PatternScale,
                Model.NOctaves
                );
        }
    }
}
