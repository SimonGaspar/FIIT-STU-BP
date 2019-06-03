using static Emgu.CV.Features2D.FastDetector;

namespace Bachelor_app.StructureFromMotion.Model
{
    /// <summary>
    /// Model for FAST
    /// </summary>
    public class FastModel
    {
        public int Threshold { get; private set; }

        public bool NonMaxSupression { get; private set; }

        public DetectorType Type { get; private set; }

        public FastModel(int threshold = 10, bool nonMaxSepression = true, DetectorType type = DetectorType.Type9_16)
        {
            Threshold = threshold;
            NonMaxSupression = nonMaxSepression;
            Type = type;
        }
    }
}
