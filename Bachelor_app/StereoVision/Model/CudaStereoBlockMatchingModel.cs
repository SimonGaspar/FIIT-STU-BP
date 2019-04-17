namespace Bachelor_app.StereoVision.Model
{
    /// <summary>
    /// Model for CudaStereoBM
    /// </summary>
    public class CudaStereoBlockMatchingModel : StereoBlockMatchingModel
    {
        public CudaStereoBlockMatchingModel()
        {
            Disparity = 64;
            BlockSize = 5;
        }
    }
}
