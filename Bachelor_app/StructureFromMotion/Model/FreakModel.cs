namespace Bachelor_app.StructureFromMotion.Model
{
    /// <summary>
    /// Model for FREAK
    /// </summary>
    public class FreakModel
    {
        public bool OrientationNormalized { get; set; } = true;
        public bool ScaleNormalized { get; set; } = true;
        public float PatternScale { get; set; } = 22;
        public int NOctaves { get; set; } = 4;
    }
}
