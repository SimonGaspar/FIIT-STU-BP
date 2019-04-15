using Emgu.CV;

namespace Bakalárska_práca.Model
{
    /// <summary>
    /// Model for descriptor
    /// </summary>
    public class DescriptorModel
    {
        public KeyPointModel KeyPoint;
        public Mat Descriptors;
        public string FileFormatSIFT;
    }
}
