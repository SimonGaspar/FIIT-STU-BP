using Emgu.CV;

namespace Bachelor_app.StereoVision.Model
{
    /// <summary>
    /// Model for StereoSGBM
    /// </summary>
    public class StereoSemiGlobalBlockMatchingModel : StereoBlockMatchingModel
    {
        public int MinDispatiries { get; private set; }

        public int P1 { get; private set; }

        public int P2 { get; private set; }

        public int Disp12MaxDiff { get; private set; }

        public int PreFilterCap { get; private set; }

        public int UniquenessRatio { get; private set; }

        public int SpeckleWindowsSize { get; private set; }

        public int SpeckleRange { get; private set; }

        public StereoSGBM.Mode Mode { get; private set; }

        public StereoSemiGlobalBlockMatchingModel(int disparity = 16, int blockSize = 15, int minDispatiries = 16, int p1 = 0, int p2 = 0, int disp12MaxDiff = 0, int preFilterCap = 0, int uniquenessRatio = 0, int speckleWindowsSize = 0, int speckleRange = 0, StereoSGBM.Mode mode = StereoSGBM.Mode.SGBM)
            : base(disparity, blockSize)
        {
            MinDispatiries = minDispatiries;
            P1 = p1;
            P2 = p2;
            Disp12MaxDiff = disp12MaxDiff;
            PreFilterCap = PreFilterCap;
            UniquenessRatio = uniquenessRatio;
            SpeckleWindowsSize = speckleWindowsSize;
            SpeckleRange = speckleRange;
            Mode = mode;
        }
    }
}
