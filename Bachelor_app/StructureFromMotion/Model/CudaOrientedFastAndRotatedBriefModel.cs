namespace Bachelor_app.StructureFromMotion.Model
{
    /// <summary>
    /// Model for CudaORB
    /// </summary>
    public class CudaOrientedFastAndRotatedBriefModel : OrientedFastAndRotatedBriefModel
    {
        public bool BlurForDescriptor { get; set; } = false;
    }
}
