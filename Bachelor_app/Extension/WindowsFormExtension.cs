using System.Windows.Forms;

namespace Bachelor_app.Extension
{
    /// <summary>
    /// Extensions for WinForm elements.
    /// </summary>
    public static class WindowsFormExtension
    {
        /// <summary>
        /// Convert value from TrackBar.
        /// </summary>
        /// <param name="trackBar"></param>
        /// <returns>Int value of TrackBar*16</returns>
        #region TrackBar
        public static int ValueMultiple16(this TrackBar trackBar)
        {
            return trackBar.Value * 16;
        }

        /// <summary>
        /// Convert value from TrackBar.
        /// </summary>
        /// <param name="trackBar"></param>
        /// <returns>Int value of TrackBar*2+1</returns>
        public static int ValueOdd(this TrackBar trackBar)
        {
            return (trackBar.Value * 2) + 1;
        }

        /// <summary>
        /// Convert value from TrackBar.
        /// </summary>
        /// <param name="trackBar"></param>
        /// <returns>Int value of TrackBar*2</returns>
        public static int ValueEven(this TrackBar trackBar)
        {
            return trackBar.Value * 2;
        }

        /// <summary>
        /// Convert value from TrackBar.
        /// </summary>
        /// <param name="trackBar"></param>
        /// <returns>Int value of TrackBar/16</returns>
        public static int ValueDivide16(this TrackBar trackBar)
        {
            return trackBar.Value / 16;
        }

        /// <summary>
        /// Convert value from TrackBar.
        /// </summary>
        /// <param name="trackBar"></param>
        /// <returns>Int value of TrackBar/2</returns>
        public static int ValueDivideEvenOdd(this TrackBar trackBar)
        {
            return trackBar.Value / 2;
        }
        #endregion
    }
}
