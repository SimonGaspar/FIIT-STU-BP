using Emgu.CV;
using Emgu.CV.Util;

namespace Bakalárska_práca.StructureFromMotion
{
    public interface IFeatureMatcher
    {
        void Add(IInputArray Descriptor);
        //void Match(Mat Descriptor, VectorOfVectorOfDMatch matches);
        void Match(IInputArray queryDescriptors, IInputArray trainDescriptors, VectorOfVectorOfDMatch matches);

        void UpdateModel<T>(T model);
        void ShowSettingForm();
    }
}
