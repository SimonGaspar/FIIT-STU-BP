using Emgu.CV;
using Emgu.CV.Structure;

namespace Bakalárska_práca.StructureFromMotion
{
    /// <summary>
    /// Interface for detector algorithm
    /// </summary>
    public interface IFeatureDetector
    {
        MKeyPoint[] DetectKeyPoints(IInputArray image);

        void UpdateModel<T>(T model);
        void ShowSettingForm();
    }
}
