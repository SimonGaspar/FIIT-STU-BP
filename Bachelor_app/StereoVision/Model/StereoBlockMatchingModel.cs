namespace Bakalárska_práca.StereoVision
{
    /// <summary>
    /// Model for StereoBM
    /// </summary>
    public class StereoBlockMatchingModel
    {
        public int Disparity { get; set; } = 16;
        public int BlockSize { get; set; } = 15;
    }
}
