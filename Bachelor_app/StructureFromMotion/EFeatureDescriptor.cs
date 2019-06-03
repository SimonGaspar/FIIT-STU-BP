using Bachelor_app.StructureFromMotion.FeatureDetectionDescription;

namespace Bachelor_app.StructureFromMotion
{
    /// <summary>
    /// Possible descriptor algorithm
    /// </summary>
    public enum EFeatureDescriptor
    {
        ORB,
        FREAK,
        BRIEF,
        CudaORB,
        SIFT
        //CudaSURF,
        //SURF
    }

    public static class FeatureDescriptorExtension
    {
        /// <summary>
        /// Create instance of descriptor type.
        /// </summary>
        /// <param name="type">Descriptor type.</param>
        /// <returns>Instance of descriptor.</returns>
        public static IFeatureDescriptor GetDescriptorInstance(this EFeatureDescriptor type)
        {
            IFeatureDescriptor returnItem = null;

            switch (type)
            {
                case EFeatureDescriptor.ORB: returnItem = new OrientedFastAndRotatedBrief(); break;
                case EFeatureDescriptor.FREAK: returnItem = new FREAK(); break;
                case EFeatureDescriptor.BRIEF: returnItem = new BRIEF(); break;
                case EFeatureDescriptor.CudaORB: returnItem = new CudaOrientedFastAndRotatedBrief(); break;
                case EFeatureDescriptor.SIFT: returnItem = new Sift(); break;
                //case EFeatureDescriptor.CudaSURF: returnItem = new CudaSurf(); break;
                //case EFeatureDescriptor.SURF: returnItem = new Surf(); break;
            }

            return returnItem;
        }
    }
}
