using System;
using System.Windows.Forms;

namespace Bachelor_app.Helper
{
    /// <summary>
    /// Helper for WinForm
    /// </summary>
    public static class WindowsFormHelper
    {
        private static MainForm _winForm;

        public static void SetWinForm(MainForm winForm)
        {
            _winForm = winForm;
        }

        /// <summary>
        /// Method to adding text to console in MainForm.
        /// </summary>
        /// <param name="text">Text with format</param>
        public static void AddLogToConsole(string text)
        {
            var element = _winForm.richTextBox1;

            if (element.InvokeRequired)
            {
                element.Invoke((Action)delegate
                {
                    _winForm.richTextBox1.Text += $"[{DateTime.Now}]\n{text}\n";
                });
            }
            else
            {
                element.Text += $"[{DateTime.Now}]\n{text}\n";
            }
        }
        /// <summary>
        /// Method to adding text to console in MainForm.
        /// </summary>
        /// <param name="text">Text with format</param>
        public static void ClearConsole()
        {
            var element = _winForm.richTextBox1;

            if (element.InvokeRequired)
            {
                element.Invoke((Action)delegate
                {
                    _winForm.richTextBox1.Text = $"";
                });
            }
            else
            {
                element.Text = $"";
            }
        }
    }
}
