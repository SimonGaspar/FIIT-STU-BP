using Emgu.CV;
using Emgu.CV.CvEnum;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakalárska_práca.Calibration
{
    public class CameraModel
    {
        //Intrinsic parameters
        public Mat CameraMatrix { get; set; }
        //Fundamental matrix
        public Mat F { get; set; }
        //Essential matrix
        public Mat E { get; set; }

        public CameraModel()
        {
        }
    }
}
