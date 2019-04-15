using Bakalárska_práca.StructureFromMotion;
using Emgu.CV;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_app.StructureFromMotion.FeatureMatcher
{
    public abstract class AbstractMatcher : IFeatureMatcher
    {
        public virtual void Match(IInputArray queryDescriptors, IInputArray trainDescriptors, VectorOfVectorOfDMatch matches)
        {
            throw new NotImplementedException();
        }

        public virtual void ShowSettingForm()
        {
            throw new NotImplementedException();
        }

        public virtual void UpdateModel<T>(T model)
        {
            throw new NotImplementedException();
        }
    }
}
