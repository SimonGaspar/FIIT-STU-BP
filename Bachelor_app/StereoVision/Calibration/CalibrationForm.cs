using System;
using System.Windows.Forms;
using Bachelor_app.Helper;

namespace Bachelor_app.StereoVision.Calibration
{
    /// <summary>
    /// WinForm for calibration
    /// </summary>
    public partial class CalibrationForm : Form
    {
        private CalibrationManager calibrationManager;

        public CalibrationForm(CalibrationManager calibrationManager, PatternModel patternModel)
        {
            this.calibrationManager = calibrationManager;
            InitializeComponent();
            InitializeString(patternModel);
        }

        public delegate void UpateTitleDelgate(string text);

        /// <summary>
        /// Update title of winform
        /// </summary>
        /// <param name="text"></param>
        public void UpdateTitle(string text)
        {
            if (InvokeRequired)
            {
                try
                {
                    UpateTitleDelgate ut = new UpateTitleDelgate(UpdateTitle);
                    BeginInvoke(ut, new object[] { text });
                }
                catch (Exception ex)
                {
                    WindowsFormHelper.AddLogToConsole(ex.Message);
                }
            }
            else
            {
                Text = text;
            }
        }

        /// <summary>
        /// Start calibration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            toolStripButton1.Enabled = false;
            calibrationManager.UpdatePatternModel();
            calibrationManager.StartCalibration();
        }
    }
}