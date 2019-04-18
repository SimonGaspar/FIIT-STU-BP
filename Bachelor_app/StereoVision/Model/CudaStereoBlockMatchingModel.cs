namespace Bachelor_app.StereoVision.Model
{
    /// <summary>
    /// Model for CudaStereoBM
    /// </summary>
    public class CudaStereoBlockMatchingModel : StereoBlockMatchingModel
    {
        public CudaStereoBlockMatchingModel(int disparity = 64, int blockSize = 5)
            :base(disparity,blockSize)
        {
        }
    }
}
