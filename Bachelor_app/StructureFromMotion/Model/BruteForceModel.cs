namespace Bachelor_app.StructureFromMotion
{
    /// <summary>
    /// Model for BFMatcher
    /// </summary>
    public class BruteForceModel : CudaBruteForceModel
    {
        public bool CrossCheck { get; private set; }

        public BruteForceModel(DistanceType type = DistanceType.Hamming2, bool crossCheck = true)
            : base(type)
        {
            CrossCheck = crossCheck;
        }
    }
}
