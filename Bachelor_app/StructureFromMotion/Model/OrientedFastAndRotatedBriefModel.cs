using static Emgu.CV.Features2D.ORBDetector;

namespace Bachelor_app.StructureFromMotion.Model
{
    public class OrientedFastAndRotatedBriefModel
    {
        public int NumberOfFeatures { get; set; }
        public float ScaleFactor { get; set; }
        public int NLevels { get; set; }
        public int EdgeThreshold { get; set; }
        public int firstLevel { get; set; }
        public int WTK_A { get; set; }
        public ScoreType ScoreType { get; set; }
        public int PatchSize { get; set; }
        public int FastThreshold { get; set; }
    }
}
