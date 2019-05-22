namespace Bachelor_app.StructureFromMotion
{
    /// <summary>
    /// Possible matching algorithm
    /// </summary>
    public enum EFeatureMatcher
    {
        BruteForce,
        CudaBruteForce
    }

    public static class FeatureMatcherExtension
    {

        /// <summary>
        /// Create instance of matcher type.
        /// </summary>
        /// <param name="type">matcher type.</param>
        /// <returns>Instance of matcher.</returns>
        public static IFeatureMatcher GetMatcherInstance(this EFeatureMatcher type)
        {
            IFeatureMatcher returnItem = null;

            switch (type)
            {
                case EFeatureMatcher.BruteForce: returnItem = new BruteForce(); break;
                case EFeatureMatcher.CudaBruteForce: returnItem = new CudaBruteForce(); break;
            }
            return returnItem;
        }
    }
}
