using Emgu.CV;

namespace Bachelor_app.StereoVision.Model
{
    /// <summary>
    /// Model for StereoSGBM
    /// </summary>
    public class StereoSemiGlobalBlockMatchingModel : StereoBlockMatchingModel
    {
        public int MinDispatiries { get; set; } = 16;
        public int P1 { get; set; } = 0;
        public int P2 { get; set; } = 0;
        public int Disp12MaxDiff { get; set; } = 0;
        public int PreFilterCap { get; set; } = 0;
        public int UniquenessRatio { get; set; } = 0;
        public int SpeckleWindowsSize { get; set; } = 0;
        public int SpeckleRange { get; set; } = 0;
        public StereoSGBM.Mode Mode { get; set; } = StereoSGBM.Mode.SGBM;
    }
}
