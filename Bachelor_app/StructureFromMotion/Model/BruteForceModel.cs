﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV.Features2D;

namespace Bachelor_app.StructureFromMotion
{
    public class BruteForceModel : CudaBruteForceModel
    {
        public bool CrossCheck { get; set; } = true;

        public BruteForceModel()
        {
            Type = DistanceType.Hamming2;
        }
    }
}
