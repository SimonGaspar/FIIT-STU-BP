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
        public Size PatternSize => new Size(Width, Height);

        // Needed only, when using own draw method
        public Bgr[] Line_colour_array { get { return CreateOwnColor(); } }
        public bool Start_Flag { get; set; } = true;
        public int Count { get; private set; } = 100;
        public float Distance { get; private set; } = 25.0f;
        public ECalibrationPattern Pattern { get; private set; } = ECalibrationPattern.Chessboard;

        private readonly int Width = 9;
        private readonly int Height = 6;

        public PatternModel(int width = 9, int height = 6, int count = 100, float distance = 25.0F, ECalibrationPattern pattern = ECalibrationPattern.Chessboard)
        {
            Width = width;
            Height = height;
            Count = count;
            Distance = distance;
            Pattern = pattern;
        }

        private Bgr[] CreateOwnColor()
        {
            var colorArray = new Bgr[Width * Height];

            Random R = new Random();
            for (int i = 0; i < Height; i++)
            {
                var color = new Bgr(R.Next(0, 255), R.Next(0, 255), R.Next(0, 255));
                for (int j = 0; j < Width; j++)
                    colorArray[i * Width + j] = color;
            }
            return colorArray;
        }
    }
}
