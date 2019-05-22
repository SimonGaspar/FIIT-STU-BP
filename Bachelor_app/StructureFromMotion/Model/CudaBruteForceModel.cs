namespace Bachelor_app.StructureFromMotion
{
    /// <summary>
    /// Model for CudaBFMatcher
    /// </summary>
    public class CudaBruteForceModel
    {
        public DistanceType Type { get; private set; }

        public CudaBruteForceModel(DistanceType type = DistanceType.Hamming)
        {
            Type = type;
        }
    }
}
