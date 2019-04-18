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
        private CalibrationManager _calibrationManager;

        public CalibrationForm(CalibrationManager calibrationManager, PatternModel patternModel)
        {
            this._calibrationManager = calibrationManager;
            InitializeComponent();
            InitializeString(patternModel);
        }

        public delegate void UpateTitleDelgate(String Text);
        /// <summary>
        /// Update title of winform
        /// </summary>
        /// <param name="Text"></param>
        public void UpdateTitle(String Text)
        {
            if (this.InvokeRequired)
            {
                try
                {
                    UpateTitleDelgate ut = new UpateTitleDelgate(UpdateTitle);
                    this.BeginInvoke(ut, new object[] { Text });
                }
                catch (Exception ex)
                {
                    WindowsFormHelper.AddLogToConsole(ex.Message);
                }
            }
            else
            {
                this.Text = Text;
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
            _calibrationManager.UpdatePatternModel();
            _calibrationManager.StartCalibration();
        }
    }
}