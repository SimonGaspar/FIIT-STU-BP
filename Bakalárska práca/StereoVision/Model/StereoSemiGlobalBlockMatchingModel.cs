using Emgu.CV;

namespace Bakalárska_práca.StereoVision.Model
{
    public class StereoSemiGlobalBlockMatchingModel : StereoBlockMatchingModel
    {
        public int MinDispatiries { get; set; }
        public int P1 { get; set; }
        public int P2 { get; set; }
        public int Disp12MaxDiff { get; set; }
        public int PreFilterCap { get; set; }
        public int UniquenessRatio { get; set; }
        public int SpeckleWindowsSize { get; set; }
        public int SpeckleRange { get; set; }
        public StereoSGBM.Mode Mode { get; set; }
    }
}
