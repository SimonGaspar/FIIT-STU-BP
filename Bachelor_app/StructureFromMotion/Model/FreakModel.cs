namespace Bachelor_app.StructureFromMotion.Model
{
    public class FreakModel
    {
        public bool OrientationNormalized { get; set; } = true;
        public bool ScaleNormalized { get; set; } = true;
        public float PatternScale { get; set; } = 22;
        public int NOctaves { get; set; } = 4;
    }
}
