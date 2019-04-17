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
        FREAK,
        BRIEF,
        CudaORB
    }

    public static class FeatureDetectorExtension
    {
        public static IFeatureDetector GetDetectorInstance(this EFeatureDetector type)
        {
            IFeatureDetector returnItem = null;

            switch (type)
            {
                case EFeatureDetector.ORB: returnItem = new OrientedFastAndRotatedBrief(); break;
                case EFeatureDetector.FAST: returnItem = new FAST(); break;
                case EFeatureDetector.FREAK: returnItem = new FREAK(); break;
                case EFeatureDetector.BRIEF: returnItem = new BRIEF(); break;
                case EFeatureDetector.CudaORB: returnItem = new CudaOrientedFastAndRotatedBrief(); break;
                default: throw new NotImplementedException();
            }

            return returnItem;
        }
    }
}
