using Emgu.CV;
using Emgu.CV.Structure;

namespace Bachelor_app.StructureFromMotion
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
