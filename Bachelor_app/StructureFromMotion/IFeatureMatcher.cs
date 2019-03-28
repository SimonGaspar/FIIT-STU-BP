using Emgu.CV;
using Emgu.CV.Util;

namespace Bakalárska_práca.StructureFromMotion
{
    public interface IFeatureMatcher
    {
        void Match(IInputArray queryDescriptors, IInputArray trainDescriptors, VectorOfVectorOfDMatch matches);

        void UpdateModel<T>(T model);

        void ShowSettingForm();
    }
}
