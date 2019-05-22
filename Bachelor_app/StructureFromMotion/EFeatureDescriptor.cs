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
            }

            return returnItem;
        }
    }
}
