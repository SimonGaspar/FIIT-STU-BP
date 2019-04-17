using Emgu.CV;
using Emgu.CV.Util;

namespace Bachelor_app.StructureFromMotion
{
    /// <summary>
    /// Interface for matching algorithm
    /// </summary>
    public interface IFeatureMatcher
    {
        void Match(IInputArray queryDescriptors, IInputArray trainDescriptors, VectorOfVectorOfDMatch matches);

        void UpdateModel<T>(T model);

        void ShowSettingForm();
    }
}
