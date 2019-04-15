using System.Numerics;

namespace Bachelor_app.Model
{
    /// <summary>
    /// Model for points (.nvm file)
    /// </summary>
    public class nvmPointModel
    {
        public Vector3 position { get; set; } = new Vector3();
        public Vector3 color { get; set; } = new Vector3();

        //Dodatocne informacie -> teraz nepotrebne

        //public int numberOfMeasurement { get; set; }
        //public List<nvmMeasurement> listOfMeasurement { get; set; } = new List<nvmMeasurement>();
    }

    /// <summary>
    /// Now not used, but in future, we can use this information.
    /// DELETE these, when not using.
    /// </summary>
    public class nvmMeasurement
    {
        public int imageIndex { get; set; }
        public int featureIndex { get; set; }
        public Vector2 position { get; set; } = new Vector2();
    }
}
