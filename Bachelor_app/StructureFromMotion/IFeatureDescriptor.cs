using Bachelor_app.Model;
using Emgu.CV;

namespace Bachelor_app.StructureFromMotion
{
    /// <summary>
    /// Interface for descriptor algorithm
    /// </summary>
    public interface IFeatureDescriptor
    {
        Mat ComputeDescriptor(KeyPointModel keyPoints);

        void UpdateModel<T>(T model);

        void ShowSettingForm();

        dynamic CreateInstance();
    }
}
