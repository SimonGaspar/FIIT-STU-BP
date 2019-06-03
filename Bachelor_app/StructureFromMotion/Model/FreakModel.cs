namespace Bachelor_app.StructureFromMotion.Model
{
    /// <summary>
    /// Model for FREAK
    /// </summary>
    public class FreakModel
    {
        public bool OrientationNormalized { get; private set; }

        public bool ScaleNormalized { get; private set; }

        public float PatternScale { get; private set; }

        public int NOctaves { get; private set; }

        public FreakModel(bool orientationNormalized = true, bool scaleNormalized = true, float patternScale = 22, int nOctaves = 4)
        {
            OrientationNormalized = orientationNormalized;
            ScaleNormalized = scaleNormalized;
            PatternScale = patternScale;
            NOctaves = nOctaves;
        }
    }
}
