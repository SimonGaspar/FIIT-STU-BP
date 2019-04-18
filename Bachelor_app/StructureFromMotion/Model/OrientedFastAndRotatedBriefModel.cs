using static Emgu.CV.Features2D.ORBDetector;

namespace Bachelor_app.StructureFromMotion.Model
{
    /// <summary>
    /// Model for ORB
    /// </summary>
    public class OrientedFastAndRotatedBriefModel
    {
        public int NumberOfFeatures { get; private set; }
        public float ScaleFactor { get; private set; }
        public int NLevels { get; private set; }
        public int EdgeThreshold { get; private set; }
        public int FirstLevel { get; private set; }
        public int WTK_A { get; private set; }
        public ScoreType ScoreType { get; private set; }
        public int PatchSize { get; private set; }
        public int FastThreshold { get; private set; }

        public OrientedFastAndRotatedBriefModel(int numberOfFeatures = 500, float scaleFactor = 1.2F, int nLevels = 8, int edgeThreshold = 31, int firstLevel = 0, int wTK_A = 2, ScoreType scoreType = ScoreType.Harris, int patchSize = 31, int fastThreshold = 20)
        {
            NumberOfFeatures = numberOfFeatures;
            ScaleFactor = scaleFactor;
            NLevels = NLevels;
            EdgeThreshold = edgeThreshold;
            FirstLevel = firstLevel;
            WTK_A = wTK_A;
            ScoreType = scoreType;
            PatchSize = patchSize;
            FastThreshold = fastThreshold;
        }
    }
}
