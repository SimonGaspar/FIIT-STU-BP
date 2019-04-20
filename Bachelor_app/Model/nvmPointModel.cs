using System.Numerics;

namespace Bachelor_app.Model
{
    /// <summary>
    /// Model for points (.nvm file)
    /// </summary>
    public class NvmPointModel
    {
        public Vector3 Position { get; private set; }
        public Vector3 Color { get; private set; }

        public NvmPointModel(Vector3 position, Vector3 color) {
            Position = position;
            Color = color;
        }
        
        // Additional information

        //public int numberOfMeasurement { get; set; }
        //public List<nvmMeasurement> listOfMeasurement { get; set; } = new List<nvmMeasurement>();

        private class NvmMeasurement
        {
            public int ImageIndex { get; set; }
            public int FeatureIndex { get; set; }
            public Vector2 Position { get; set; } = new Vector2();
        }
    }
}
