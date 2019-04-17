using System;
using Bachelor_app.Model;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Bachelor_app.StructureFromMotion.FeatureDetectionDescription
{
    public abstract class AbstractFeatureDetectorDescriptor : IFeatureDetector, IFeatureDescriptor
    {
        public virtual Mat ComputeDescriptor(KeyPointModel keyPoints)
        {
            throw new NotSupportedException();
        }

        public virtual MKeyPoint[] DetectKeyPoints(IInputArray image)
        {
            throw new NotSupportedException();
        }

        public virtual void ShowSettingForm()
        {
            throw new NotSupportedException();
        }

        public virtual void UpdateModel<T>(T model)
        {
            throw new NotSupportedException();
        }
    }
}
