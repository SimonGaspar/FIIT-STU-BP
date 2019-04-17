using System.Numerics;

namespace Bachelor_app.Model
{
    /// <summary>
    /// Model for points (.nvm file)
    /// </summary>
    public class NvmPointModel
    {
        public Vector3 Position { get; set; } = new Vector3();
        public Vector3 Color { get; set; } = new Vector3();

        //Dodatocne informacie -> teraz nepotrebne

        //public int numberOfMeasurement { get; set; }
        //public List<nvmMeasurement> listOfMeasurement { get; set; } = new List<nvmMeasurement>();
    }

    /// <summary>
    /// Now not used, but in future, we can use this information.
    /// DELETE these, when not using.
    /// </summary>
    public class NvmMeasurement
    {
        public int ImageIndex { get; set; }
        public int FeatureIndex { get; set; }
        public Vector2 Position { get; set; } = new Vector2();
    }
}
