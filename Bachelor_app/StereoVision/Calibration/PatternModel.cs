using System;
using System.Drawing;
using Emgu.CV.Structure;

namespace Bachelor_app.StereoVision.Calibration
{
    public class PatternModel
    {
        public int width { get; set; } = 9;
        public int height { get; set; } = 6;
        public Size patternSize { get { return new Size(width, height); }} //size of chess board to be detected
        public Bgr[] line_colour_array; // just for displaying coloured lines of detected chessboard
        public bool start_Flag = true; //start straight away
        public int count { get; set; } = 100;
        public float distance { get; set; } = 25.0f;
        public ECalibrationPattern pattern = ECalibrationPattern.Chessboard;

        public PatternModel()
        {
            line_colour_array = new Bgr[width * height];

            Random R = new Random();
            for (int i = 0; i < height; i++)
            {
                var color = new Bgr(R.Next(0, 255), R.Next(0, 255), R.Next(0, 255));
                for (int j=0; j<width;j++)
                line_colour_array[i*width + j] = color;
            }
        }
    }
}
