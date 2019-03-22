namespace Bakalárska_práca.StereoVision.Model
{
    public class CudaStereoConstantSpaceBPModel
    {
        public int Disparity { get; set; } = 128;
        public int Iteration { get; set; } = 8;
        public int Level { get; set; } = 4;
        public int Plane { get; set; } = 4;
    }
}
