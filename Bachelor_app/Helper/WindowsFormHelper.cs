using System;
using System.Linq;

namespace Bachelor_app.Helper
{
    /// <summary>
    /// Helper for WinForm
    /// </summary>
    public static class WindowsFormHelper
    {
        private static MainForm winForm;

        public static void SetWinForm(MainForm winForm)
        {
            WindowsFormHelper.winForm = winForm;
        }

        /// <summary>
        /// Method to adding text to console in MainForm.
        /// </summary>
        /// <param name="text">Text with format</param>
        public static void AddLogToConsole(string text)
        {
            var element = winForm.richTextBox1;

            if (element.InvokeRequired)
            {
                element.Invoke((Action)delegate
                {
                    var allText = element.Text + $"[{DateTime.Now}]\n{text}\n";
                    var allLines = allText.Split('\n');
                    var count = allLines.Length - 1;
                    var result = allLines.Skip(count - 100);
                    winForm.richTextBox1.Text = string.Join("\n", result);
                });
            }
            else
            {
                string allText = element.Text + $"[{DateTime.Now}]\n{text}\n";
                var allLines = allText.Split('\n');
                var count = allLines.Length - 1;
                var result = allLines.Skip(count - 100);
                element.Text = string.Join("\n", result);
            }
        }

        /// <summary>
        /// Method to adding text to console in MainForm.
        /// </summary>
        /// <param name="text">Text with format</param>
        public static void ClearConsole()
        {
            var element = winForm.richTextBox1;

            if (element.InvokeRequired)
            {
                element.Invoke((Action)delegate
                {
                    winForm.richTextBox1.Text = $"";
                });
            }
            else
            {
                element.Text = $"";
            }
        }
    }
}
