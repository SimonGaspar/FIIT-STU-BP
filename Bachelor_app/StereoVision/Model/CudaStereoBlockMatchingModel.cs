namespace Bakalárska_práca.StereoVision.Model
{
    public class CudaStereoBlockMatchingModel : StereoBlockMatchingModel
    {

        public CudaStereoBlockMatchingModel()
        {
            Disparity = 64;
            BlockSize = 5;
        }
    }
}
