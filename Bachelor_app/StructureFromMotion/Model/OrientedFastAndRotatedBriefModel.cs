using static Emgu.CV.Features2D.ORBDetector;

namespace Bachelor_app.StructureFromMotion.Model
{
    /// <summary>
    /// Model for ORB
    /// </summary>
    public class OrientedFastAndRotatedBriefModel
    {
        public int NumberOfFeatures { get; set; } = 500;
        public float ScaleFactor { get; set; } = 1.2F;
        public int NLevels { get; set; } = 8;
        public int EdgeThreshold { get; set; } = 31;
        public int firstLevel { get; set; } = 0;
        public int WTK_A { get; set; } = 2;
        public ScoreType ScoreType { get; set; } = ScoreType.Harris;
        public int PatchSize { get; set; } = 31;
        public int FastThreshold { get; set; } = 20;
    }
}
