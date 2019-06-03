namespace Bachelor_app.StructureFromMotion.Model
{
    /// <summary>
    /// Model for SURF
    /// </summary>
    public class SurfModel
    {
        public float HessianThreshold { get; private set; }

        public int NOctaves { get; private set; }

        public int NOctaveLayers { get; private set; }

        public bool Extended { get; private set; }

        public bool Upright { get; private set; }

        public SurfModel(float hessianThreshold = 100, int nOctaves = 4, int nOctaveLayers = 2, bool extended = true, bool upright = false)
        {
            HessianThreshold = hessianThreshold;
            NOctaves = nOctaves;
            NOctaveLayers = nOctaveLayers;
            Extended = extended;
            Upright = upright;
        }
    }
}
