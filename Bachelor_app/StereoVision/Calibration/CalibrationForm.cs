using System;
using System.ComponentModel;
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

            //We will only use 1 frame ready event this is not really safe but it fits the purpose
            _calibrationManager._leftCamera.ImageGrabbed += _calibrationManager.ProcessFrame;
            _calibrationManager._rightCamera.Start(); //We make sure we start Capture device 2 first
            _calibrationManager._leftCamera.Start();
        }


        #region Window/Form Control
        /// <summary>
        /// Thread safe method to get a slider value from form
        /// </summary>
        /// <param name="Control"></param>
        /// <returns></returns>
        public delegate int GetSlideValueDelgate(TrackBar Control);
        public int GetSliderValue(TrackBar Control)
        {
            if (Control.InvokeRequired)
            {
                try
                {
                    return (int)Control.Invoke(new Func<int>(() => GetSliderValue(Control)));
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
            else
            {
                return Control.Value;
            }
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

        /// <summary>
        /// The matched block size. Must be an odd number >=1 . Normally, it should be somewhere in 3..11 range
        /// Each time the slider moves the value is checked and made odd if even
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SAD_Window_Scroll(object sender, EventArgs e)
        {
            /*The matched block size. Must be an odd number >=1 . Normally, it should be somewhere in 3..11 range*/
            //This ensures only odd numbers are allowed from slider value
            if (SAD_Window.Value % 2 == 0)
            {
                if (SAD_Window.Value == SAD_Window.Maximum) SAD_Window.Value = SAD_Window.Maximum - 2;
                else SAD_Window.Value++;
            }
        }

        /// <summary>
        /// This is maximum disparity minus minimum disparity. Always greater than 0. In the current implementation this parameter must be divisible by 16.
        /// Each time the slider moves the value is checked and made a factor of 16
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Num_Disparities_Scroll(object sender, EventArgs e)
        {

            if (Num_Disparities.Value % 16 != 0)
            {
                //value must be divisable by 16
                if (Num_Disparities.Value >= 152) Num_Disparities.Value = 160;
                else if (Num_Disparities.Value >= 136) Num_Disparities.Value = 144;
                else if (Num_Disparities.Value >= 120) Num_Disparities.Value = 128;
                else if (Num_Disparities.Value >= 104) Num_Disparities.Value = 112;
                else if (Num_Disparities.Value >= 88) Num_Disparities.Value = 96;
                else if (Num_Disparities.Value >= 72) Num_Disparities.Value = 80;
                else if (Num_Disparities.Value >= 56) Num_Disparities.Value = 64;
                else if (Num_Disparities.Value >= 40) Num_Disparities.Value = 48;
                else if (Num_Disparities.Value >= 24) Num_Disparities.Value = 32;
                else Num_Disparities.Value = 16;
            }
        }

        /// <summary>
        /// Maximum disparity variation within each connected component. If you do speckle filtering, set it to some positive value, multiple of 16. Normally, 16 or 32 is good enough.
        /// Each time the slider moves the value is checked and made a factor of 16
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void specklerange_Scroll(object sender, EventArgs e)
        {
            if (specklerange.Value % 16 != 0)
            {
                //value must be divisable by 16
                //TODO: we can do this in a loop
                if (specklerange.Value >= 152) specklerange.Value = 160;
                else if (specklerange.Value >= 136) specklerange.Value = 144;
                else if (specklerange.Value >= 120) specklerange.Value = 128;
                else if (specklerange.Value >= 104) specklerange.Value = 112;
                else if (specklerange.Value >= 88) specklerange.Value = 96;
                else if (specklerange.Value >= 72) specklerange.Value = 80;
                else if (specklerange.Value >= 56) specklerange.Value = 64;
                else if (specklerange.Value >= 40) specklerange.Value = 48;
                else if (specklerange.Value >= 24) specklerange.Value = 32;
                else if (specklerange.Value >= 8) specklerange.Value = 16;
                else specklerange.Value = 0;
            }
        }

        /// <summary>
        /// Sets the state of fulldp in the StereoSGBM algorithm allowing full-scale 2-pass dynamic programming algorithm. 
        /// It will consume O(W*H*numDisparities) bytes, which is large for 640x480 stereo and huge for HD-size pictures. By default this is false
        /// </summary>
        bool fullDP = false;
        public void fullDP_State_Click(object sender, EventArgs e)
        {
            if (fullDP_State.Text == "True")
            {
                fullDP = false;
                fullDP_State.Text = "False";
            }
            else
            {
                fullDP = true;
                fullDP_State.Text = "True";
            }
        }

        /// <summary>
        /// Overide form closing event to release cameras
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (_calibrationManager._leftCamera != null) _calibrationManager._leftCamera.Dispose();
            if (_calibrationManager._rightCamera != null) _calibrationManager._rightCamera.Dispose();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            OnClosing(null);
        }
        #endregion
    }
}