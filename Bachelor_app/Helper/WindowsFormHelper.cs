using System;
using System.Threading;
using System.Windows.Forms;

namespace Bakalárska_práca.Helper
{
    public static class WindowsFormHelper
    {
        private static MainForm _winForm;
        public static void trackBar_ValueChanged(TrackBar trackBar, ToolTip toolTip, Action GetPropertiesAndSetModel)
        {
            toolTip.SetToolTip(trackBar, trackBar.Value.ToString());
            GetPropertiesAndSetModel();
        }

        public static void trackBar_ValueChangedMultiple16(TrackBar trackBar, ToolTip toolTip, Action GetPropertiesAndSetModel)
        {
            toolTip.SetToolTip(trackBar, (trackBar.Value * 16).ToString());
            GetPropertiesAndSetModel();
        }

        public static void trackBar_ValueChangedOdd(TrackBar trackBar, ToolTip toolTip, Action GetPropertiesAndSetModel)
        {
            toolTip.SetToolTip(trackBar, ((trackBar.Value * 2) + 1).ToString());
            GetPropertiesAndSetModel();
        }

        public static void trackBar_ValueChangedEven(TrackBar trackBar, ToolTip toolTip, Action GetPropertiesAndSetModel)
        {
            toolTip.SetToolTip(trackBar, (trackBar.Value * 2).ToString());
            GetPropertiesAndSetModel();
        }

        public static void SetWinForm(MainForm winForm) {
            _winForm = winForm;
        }

        public static void AddLogToConsole(string text)
        {
            _winForm.richTextBox1.Invoke((Action)delegate
            {
                _winForm.richTextBox1.Text += $"[{DateTime.Now}]\n"+text;
            });
        }
    }
}
