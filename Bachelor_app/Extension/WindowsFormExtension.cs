using System.Windows.Forms;

namespace Bakalárska_práca.Extension
{
    /// <summary>
    /// Extensions for WinForm elements
    /// </summary>
    public static class WindowsFormExtension
    {
        /// <summary>
        /// Convert value from TrackBar
        /// </summary>
        /// <param name="trackBar">TrackBar from WinForm</param>
        /// <returns>Int value of TrackBar</returns>
        #region TrackBar
        public static int ValueMultiple16(this TrackBar trackBar)
        {
            return trackBar.Value * 16;
        }

        /// <summary>
        /// Convert value from TrackBar
        /// </summary>
        /// <param name="trackBar">TrackBar from WinForm</param>
        /// <returns>Int value of TrackBar</returns>
        public static int ValueOdd(this TrackBar trackBar)
        {
            return trackBar.Value * 2 + 1;
        }

        /// <summary>
        /// Convert value from TrackBar
        /// </summary>
        /// <param name="trackBar">TrackBar from WinForm</param>
        /// <returns>Int value of TrackBar</returns>
        public static int ValueEven(this TrackBar trackBar)
        {
            return trackBar.Value * 2;
        }

        /// <summary>
        /// Convert value from TrackBar
        /// </summary>
        /// <param name="trackBar">TrackBar from WinForm</param>
        /// <returns>Int value of TrackBar</returns>
        public static int ValueDivide16(this TrackBar trackBar)
        {
            return trackBar.Value / 16;
        }

        /// <summary>
        /// Convert value from TrackBar
        /// </summary>
        /// <param name="trackBar">TrackBar from WinForm</param>
        /// <returns>Int value of TrackBar</returns>
        public static int ValueDivideEvenOdd(this TrackBar trackBar)
        {
            return trackBar.Value / 2;
        }
        #endregion
    }
}
