using System;

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
            TryCreateInstance();
        }

        private void TryCreateInstance()
        {
            var instance = CreateInstance();

            if (instance == null)
                throw new NullReferenceException("Instance is null.");
        }

        public virtual dynamic CreateInstance() { return null; }
    }
}
