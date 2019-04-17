using System;
using Emgu.CV;
using Emgu.CV.Util;

namespace Bachelor_app.StructureFromMotion.FeatureMatcher
{
    public abstract class AbstractMatcher : IFeatureMatcher
    {
        public virtual void Match(IInputArray queryDescriptors, IInputArray trainDescriptors, VectorOfVectorOfDMatch matches)
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
