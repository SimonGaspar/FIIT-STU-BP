namespace Bachelor_app.StereoVision.Model
{
    /// <summary>
    /// Model for CudaStereoConstantSpaceBP
    /// </summary>
    public class CudaStereoConstantSpaceBPModel
    {
        public int Disparity { get; private set; }
        public int Iteration { get; private set; }
        public int Level { get; private set; }
        public int Plane { get; private set; }

        public CudaStereoConstantSpaceBPModel(int disparity = 128, int iteration = 8, int level = 4, int plane = 4)
        {
            Disparity = disparity;
            Iteration = iteration;
            Level = level;
            Plane = plane;
        }
    }
}
