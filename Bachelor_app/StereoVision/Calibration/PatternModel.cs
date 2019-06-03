using System.Drawing;

namespace Bachelor_app.StereoVision.Calibration
{
    /// <summary>
    /// Pattern model, which is used in calibration.
    /// </summary>
    public class PatternModel
    {
        public Size PatternSize => new Size(width, height);

        public bool Start_Flag { get; set; } = true;

        public int Count { get; private set; } = 100;

        public float Distance { get; private set; } = 25.0f;

        public ECalibrationPattern Pattern { get; private set; } = ECalibrationPattern.Chessboard;

        private readonly int width = 9;
        private readonly int height = 6;

        public PatternModel(int width = 9, int height = 6, int count = 100, float distance = 25.0F, ECalibrationPattern pattern = ECalibrationPattern.Chessboard)
        {
            this.width = width;
            this.height = height;
            Count = count;
            Distance = distance;
            Pattern = pattern;
        }
    }
}
