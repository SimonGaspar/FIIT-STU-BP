using System;
using Bachelor_app.StructureFromMotion.FeatureDetectionDescription;

namespace Bachelor_app.StructureFromMotion
{
    /// <summary>
    /// Possible detector algorithm
    /// </summary>
    public enum EFeatureDetector
    {
        ORB,
        FAST,
        CudaORB
    }

    public static class FeatureDetectorExtension
    {
        /// <summary>
        /// Create instance of detector type.
        /// </summary>
        /// <param name="type">Detector type.</param>
        /// <returns>Instance of detector.</returns>
        public static IFeatureDetector GetDetectorInstance(this EFeatureDetector type)
        {
            IFeatureDetector returnItem = null;

            switch (type)
            {
                case EFeatureDetector.ORB: returnItem = new OrientedFastAndRotatedBrief(); break;
                case EFeatureDetector.FAST: returnItem = new FAST(); break;
                case EFeatureDetector.CudaORB: returnItem = new CudaOrientedFastAndRotatedBrief(); break;
                default: throw new NotImplementedException();
            }

            return returnItem;
        }
    }
}
