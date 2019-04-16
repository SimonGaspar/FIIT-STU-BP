using Bakalárska_práca.StructureFromMotion.FeatureMatcher;

namespace Bakalárska_práca.StructureFromMotion
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
