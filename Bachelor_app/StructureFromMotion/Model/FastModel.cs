using static Emgu.CV.Features2D.FastDetector;

namespace Bachelor_app.StructureFromMotion.Model
{
    /// <summary>
    /// Model for FAST
    /// </summary>
    public class FastModel
    {
        public int Threshold { get; set; } = 10;
        public bool NonMaxSupression { get; set; } = true;
        public DetectorType Type { get; set; } = DetectorType.Type9_16;
    }
}
