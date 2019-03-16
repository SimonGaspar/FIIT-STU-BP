using Emgu.CV;
using Emgu.CV.Structure;

namespace Bakalárska_práca.StructureFromMotion
{
    public interface IFeatureDetector
    {
        MKeyPoint[] DetectKeyPoints(IInputArray image);
    }
}
