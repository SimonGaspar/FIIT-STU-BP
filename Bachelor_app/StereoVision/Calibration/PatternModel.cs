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
        public int Width { get; set; } = 9;
        public int Height { get; set; } = 6;
        public Size PatternSize => new Size(Width, Height);

        // Needed only, when using own draw method
        public Bgr[] line_colour_array;
        public bool start_Flag = true;
        public int Count { get; set; } = 100;
        public float Distance { get; set; } = 25.0f;
        public ECalibrationPattern pattern = ECalibrationPattern.Chessboard;

        /// <summary>
        /// Colors for drawing pattern
        /// </summary>
        public PatternModel()
        {
            line_colour_array = new Bgr[Width * Height];

            Random R = new Random();
            for (int i = 0; i < Height; i++)
            {
                var color = new Bgr(R.Next(0, 255), R.Next(0, 255), R.Next(0, 255));
                for (int j = 0; j < Width; j++)
                    line_colour_array[i * Width + j] = color;
            }
        }
    }
}
