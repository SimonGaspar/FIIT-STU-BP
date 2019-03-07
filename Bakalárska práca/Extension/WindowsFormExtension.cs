using System.Windows.Forms;

namespace Bakalárska_práca.Extension
{
    public static class WindowsFormExtension
    {

        public static int ValueMultiple16(this TrackBar trackBar)
        {
            return trackBar.Value * 16;
        }

        public static int ValueOdd(this TrackBar trackBar)
        {
            return trackBar.Value * 2 + 1;
        }

        public static int ValueEven(this TrackBar trackBar)
        {
            return trackBar.Value * 2;
        }

        public static int ValueDivide16(this TrackBar trackBar)
        {
            return trackBar.Value / 16;
        }

        public static int ValueDivideEvenOdd(this TrackBar trackBar)
        {
            return trackBar.Value / 2;
        }

    }
}
