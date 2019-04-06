using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_app.Model
{
    public class nvmPointModel
    {
        public Vector3 position { get; set; } = new Vector3();
        public Vector3 color { get; set; } = new Vector3();

        //Dodatocne informacie -> teraz nepotrebne

        //public int numberOfMeasurement { get; set; }
        //public List<nvmMeasurement> listOfMeasurement { get; set; } = new List<nvmMeasurement>();
    }

    public class nvmMeasurement {
        public int imageIndex { get; set; }
        public int featureIndex { get; set; }
        public Vector2 position { get; set; } = new Vector2();
    }
}
