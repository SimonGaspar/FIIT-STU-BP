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
            IFeatureDescriptor returnType = null;

            switch (type)
            {
                case EFeatureDescriptor.ORB: returnType = new OrientedFastAndRotatedBrief(); break;
                case EFeatureDescriptor.FAST: returnType = new FAST(); break;
                case EFeatureDescriptor.FREAK: returnType = new FREAK(); break;
                case EFeatureDescriptor.BRIEF: returnType = new BRIEF(); break;
                case EFeatureDescriptor.CudaORB: returnType = new CudaOrientedFastAndRotatedBrief(); break;
            }

            return returnType;
        }
    }
}
