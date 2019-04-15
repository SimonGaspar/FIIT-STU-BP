using Emgu.CV.Features2D;

namespace Bachelor_app.StructureFromMotion
{
    /// <summary>
    /// Model for CudaBFMatcher
    /// </summary>
    public class CudaBruteForceModel
    {
        public DistanceType Type { get; set; } = DistanceType.Hamming;
    }
}
