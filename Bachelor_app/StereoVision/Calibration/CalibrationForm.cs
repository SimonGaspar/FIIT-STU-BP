using System;
using System.Windows.Forms;
using Bakalárska_práca.Helper;

namespace Bachelor_app.StereoVision.Calibration
{
    /// <summary>
    /// WinForm for calibration
    /// </summary>
    public partial class CalibrationForm : Form
    {
        CalibrationManager _calibrationManager;
        public CalibrationForm(CalibrationManager calibrationManager)
        {
            this._calibrationManager = calibrationManager;
            InitializeComponent();
            InitializeString();
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
        /// Kill calibration and close window
        /// </summary>
        public void ExitWindows()
        {
            if (this.InvokeRequired)
                this.Invoke((Action)delegate { ExitWindows(); });
            else
            {
                _calibrationManager.CalibrationProcess.Abort();
                this.Close();
            }
        }

        /// <summary>
        /// Start calibration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            toolStripButton1.Enabled = false;
            _calibrationManager.UpdatePatternModel();
            _calibrationManager.StartCalibration();
        }
    }
}