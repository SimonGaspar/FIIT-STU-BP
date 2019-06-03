using static Emgu.CV.Features2D.ORBDetector;

namespace Bachelor_app.StructureFromMotion.Model
{
    /// <summary>
    /// Model for CudaORB
    /// </summary>
    public class CudaOrientedFastAndRotatedBriefModel : OrientedFastAndRotatedBriefModel
    {
        public bool BlurForDescriptor { get; private set; }

        public CudaOrientedFastAndRotatedBriefModel(int numberOfFeatures = 30000, float scaleFactor = 1.2F, int nLevels = 8, int edgeThreshold = 31, int firstLevel = 0, int wTK_A = 2, ScoreType scoreType = ScoreType.Harris, int patchSize = 31, int fastThreshold = 20, bool blurForDescriptor = false)
            : base(numberOfFeatures, scaleFactor, nLevels, edgeThreshold, firstLevel, wTK_A, scoreType, patchSize, fastThreshold)
        {
            BlurForDescriptor = blurForDescriptor;
        }
    }
}
