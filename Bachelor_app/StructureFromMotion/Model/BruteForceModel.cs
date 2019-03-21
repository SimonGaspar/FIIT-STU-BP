using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV.Features2D;

namespace Bachelor_app.StructureFromMotion
{
    public class BruteForceModel
    {
        public DistanceType Type { get; set; }
        public bool CrossCheck { get; set; }
    }
}
