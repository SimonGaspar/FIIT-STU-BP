using Emgu.CV.Features2D;

namespace Bachelor_app.StructureFromMotion
{
    /// <summary>
    /// Model for BFMatcher
    /// </summary>
    public class BruteForceModel : CudaBruteForceModel
    {
        public bool CrossCheck { get; set; } = true;

        public BruteForceModel()
        {
            Type = DistanceType.Hamming2;
        }
    }
}
