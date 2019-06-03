namespace Bachelor_app.StereoVision
{
    /// <summary>
    /// Model for StereoBM
    /// </summary>
    public class StereoBlockMatchingModel
    {
        public int Disparity { get; private set; }

        public int BlockSize { get; private set; }

        public StereoBlockMatchingModel(int disparity = 16, int blockSize = 15)
        {
            Disparity = disparity;
            BlockSize = blockSize;
        }
    }
}
