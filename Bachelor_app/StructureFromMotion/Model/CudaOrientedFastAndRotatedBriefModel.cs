namespace Bachelor_app.StructureFromMotion.Model
{
    /// <summary>
    /// Model for CudaORB
    /// </summary>
    public class CudaOrientedFastAndRotatedBriefModel : OrientedFastAndRotatedBriefModel
    {
        public bool BlurForDescriptor { get; private set; }

        public CudaOrientedFastAndRotatedBriefModel(int NumberOfFeatures = 500, float ScaleFactor = 1.2F, int NLevels = 8, int EdgeThreshold = 31, int FirstLevel = 0, int WTK_A = 2, ScoreType ScoreType = ScoreType.Harris, int PatchSize = 31, int FastThreshold = 20, bool blurForDescriptor = false)
            : base(NumberOfFeatures, ScaleFactor, NLevels, EdgeThreshold, FirstLevel, WTK_A, ScoreType, PatchSize, FastThreshold)
        {
            BlurForDescriptor = blurForDescriptor;
        }
    }
}
