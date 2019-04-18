using System;
using System.Windows.Forms;
using Bachelor_app.Extension;
using Bachelor_app.Helper;
using Bachelor_app.StereoVision.Model;
using Bachelor_app.StereoVision.StereoCorrespondence;
using Emgu.CV;

namespace Bachelor_app.StereoVision.WindowsForm
{
    public partial class StereoSGBMForm : Form
    {
        private StereoSemiGlobalBlockMatching _stereoSemiGlobalBlockMatching;

        public StereoSGBMForm(StereoSemiGlobalBlockMatching stereoSemiGlobalBlockMatching)
        {
            this._stereoSemiGlobalBlockMatching = stereoSemiGlobalBlockMatching;

            InitializeComponent();
        }

        #region TrackBar
        private void TrackBar_ValueChangedOdd(object sender, EventArgs e)
        {
            WindowsFormHelper.TrackBar_ValueChangedOdd(sender as TrackBar, toolTip1, GetPropertiesAndSetModel);
        }

        private void TrackBar_ValueChangedMultiple16(object sender, EventArgs e)
        {
            WindowsFormHelper.TrackBar_ValueChangedMultiple16(sender as TrackBar, toolTip1, GetPropertiesAndSetModel);
        }
        #endregion

        /// <summary>
        /// Create model from WinForm values and update.
        /// </summary>
        private void GetPropertiesAndSetModel()
        {
            var Disparity = DisparityTrackBar.ValueMultiple16();
            var BlockSize = BlockSizeTrackBar.ValueOdd();
            var MinDispatiries = MinDisparityTrackBar.Value;
            var P1 = P1TrackBar.Value;
            var P2 = P2TrackBar.Value;
            var Disp12MaxDiff = DispMax12DiffTrackBar.Value;
            var PreFilterCap = PreFilterCapTrackBar.Value;
            var UniquenessRatio = UniquenessRatioTrackBar.Value;
            var SpeckleRange = SpeckleRangeTrackBar.Value;
            var SpeckleWindowsSize = SpeckleWindowsSizeTrackBar.Value;
            var Mode = ModeSGBM.Checked ? StereoSGBM.Mode.SGBM : StereoSGBM.Mode.HH;

            var model = new StereoSemiGlobalBlockMatchingModel(
                Disparity,
                BlockSize,
                MinDispatiries,
                P1,
                P2,
                Disp12MaxDiff,
                PreFilterCap,
                UniquenessRatio,
                SpeckleWindowsSize,
                SpeckleRange,
                Mode);

            _stereoSemiGlobalBlockMatching.UpdateModel(model);
        }

        private void StereoSGBMForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
