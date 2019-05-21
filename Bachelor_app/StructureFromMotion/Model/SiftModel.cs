using Emgu.CV.XFeatures2D;

namespace Bachelor_app.StructureFromMotion.Model
{
    /// <summary>
    /// Model for ORB
    /// </summary>
    public class SiftModel
    {
        public int NumberOfFeatures { get; private set; }
        public int NOctaveLayers { get; private set; }
        public double ContrastThreshold { get; private set; }
        public double EdgeThreshold { get; private set; }
        public double Sigma { get; private set; }

        public SiftModel(int nFeatures = 0, int nOctaveLayers = 6, double contrastThreshold = 0.04, double edgeThreshold = 10, double sigma = 3.2)
        {
            NumberOfFeatures = nFeatures;
            NOctaveLayers = nOctaveLayers;
            ContrastThreshold = contrastThreshold;
            EdgeThreshold = edgeThreshold;
            Sigma = sigma;
            new SIFT();
        }
    }
}
