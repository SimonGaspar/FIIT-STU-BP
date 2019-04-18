using System;
using Emgu.CV;
using Emgu.CV.Util;

namespace Bachelor_app.StructureFromMotion.FeatureMatcher
{
    public abstract class AbstractMatcher : IFeatureMatcher
    {
        protected dynamic Model { get; private set; }
        protected dynamic WinForm { get; set; }

        public AbstractMatcher(dynamic model)
        {
            Model = model;
        }

        public virtual void Match(IInputArray queryDescriptors, IInputArray trainDescriptors, VectorOfVectorOfDMatch matches)
        {
            throw new NotSupportedException();
        }

        public virtual void ShowSettingForm()
        {
            WinForm.Show();
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
