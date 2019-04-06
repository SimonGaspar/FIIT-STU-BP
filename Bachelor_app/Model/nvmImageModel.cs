using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Bachelor_app.Model
{
    public class nvmImageModel
    {
        public string fileName { get; set; }
        public float focalLength { get; set; }
        public Quaternion quaternion { get; set; }
        public Vector3 cameraCenter { get; set; }
        public float radialDistortion { get; set; }

    }
}
