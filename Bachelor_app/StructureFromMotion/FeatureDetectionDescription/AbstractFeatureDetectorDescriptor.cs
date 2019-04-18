using System;
using Bachelor_app.Model;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Bachelor_app.StructureFromMotion.FeatureDetectionDescription
{
    public abstract class AbstractFeatureDetectorDescriptor : IFeatureDetector, IFeatureDescriptor
    {
        protected dynamic WindowsForm { get; set; }
        protected dynamic Model { get; private set; }

        public AbstractFeatureDetectorDescriptor(dynamic model)
        {
            Model = model;
        }

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
            WindowsForm.Show();
        }

        public virtual void UpdateModel<T>(T model)
        {
            Model = model;
        }

        protected virtual dynamic CreateInstance() { return null; }
    }
}
