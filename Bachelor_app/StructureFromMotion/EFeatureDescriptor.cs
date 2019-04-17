using Bachelor_app.StructureFromMotion.FeatureDetectionDescription;

namespace Bachelor_app.StructureFromMotion
{
    /// <summary>
    /// Possible descriptor algorithm
    /// </summary>
    public enum EFeatureDescriptor
    {
        ORB,
        FAST,
        FREAK,
        BRIEF,
        CudaORB
    }

    public static class FeatureDescriptorExtension
    {
        public static IFeatureDescriptor GetDescriptorInstance(this EFeatureDescriptor type)
        {
            IFeatureDescriptor returnItem = null;

            switch (type)
            {
                case EFeatureDescriptor.ORB: returnItem = new OrientedFastAndRotatedBrief(); break;
                case EFeatureDescriptor.FAST: returnItem = new FAST(); break;
                case EFeatureDescriptor.FREAK: returnItem = new FREAK(); break;
                case EFeatureDescriptor.BRIEF: returnItem = new BRIEF(); break;
                case EFeatureDescriptor.CudaORB: returnItem = new CudaOrientedFastAndRotatedBrief(); break;
            }

            return returnItem;
        }
    }
}
