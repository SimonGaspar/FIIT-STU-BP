using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV.Structure;

namespace Bachelor_app.StereoVision.Calibration
{
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
        public void UpdateTitle(String Text)
        {
            if (this.InvokeRequired)
            {
                try
                {
                    // update title asynchronously
                    UpateTitleDelgate ut = new UpateTitleDelgate(UpdateTitle);
                    //if (this.IsHandleCreated && !this.IsDisposed)
                    this.BeginInvoke(ut, new object[] { Text });
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                this.Text = Text;
            }
        }


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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            toolStripButton1.Enabled = false;
            _calibrationManager.UpdatePatternModel();
            _calibrationManager.StartCalibration();
        }
    }
}