namespace Bachelor_app.StructureFromMotion.Model
{
    /// <summary>
    /// Model for CudaORB
    /// </summary>
    public class CudaSurfModel
    {
        public float HessianThreshold { get; private set; }

        private int nOctaves;

        public int GetNOctaves()
        {
            return nOctaves;
        }

        private void SetNOctaves(int value)
        {
            nOctaves = value;
        }

        public int NOctaveLayers { get; private set; }

        public bool Extended { get; private set; }

        public float FeaturesRatio { get; private set; }

        public bool Upright { get; private set; }

        public CudaSurfModel(float hessianThreshold = 100, int nOctaves = 4, int nOctaveLayers = 2, bool extended = true, float featuresRatio = 0.01F, bool upright = false)
        {
            HessianThreshold = hessianThreshold;
            SetNOctaves(nOctaves);
            NOctaveLayers = nOctaveLayers;
            Extended = extended;
            FeaturesRatio = featuresRatio;
            Upright = upright;
        }
    }
}
