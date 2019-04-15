using System;
using System.Drawing;
using Emgu.CV.Structure;

namespace Bachelor_app.StereoVision.Calibration
{
    /// <summary>
    /// Pattern model, which is used in calibration.
    /// </summary>
    public class PatternModel
    {
        public int width { get; set; } = 9;
        public int height { get; set; } = 6;
        public Size patternSize { get { return new Size(width, height); } }

        // Needed only, when using own draw method
        public Bgr[] line_colour_array;
        public bool start_Flag = true;
        public int count { get; set; } = 100;
        public float distance { get; set; } = 25.0f;
        public ECalibrationPattern pattern = ECalibrationPattern.Chessboard;

        /// <summary>
        /// Colors for drawing pattern
        /// </summary>
        public PatternModel()
        {
            line_colour_array = new Bgr[width * height];

            Random R = new Random();
            for (int i = 0; i < height; i++)
            {
                var color = new Bgr(R.Next(0, 255), R.Next(0, 255), R.Next(0, 255));
                for (int j = 0; j < width; j++)
                    line_colour_array[i * width + j] = color;
            }
        }
    }
}
