using Bakalárska_práca.Model;
using Emgu.CV;

namespace Bakalárska_práca.StructureFromMotion
{
    /// <summary>
    /// Interface for descriptor algorithm
    /// </summary>
    public interface IFeatureDescriptor
    {
        Mat ComputeDescriptor(KeyPointModel keyPoints);

        void UpdateModel<T>(T model);
        void ShowSettingForm();
    }
}
