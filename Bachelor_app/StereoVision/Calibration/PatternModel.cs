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
    }
}
