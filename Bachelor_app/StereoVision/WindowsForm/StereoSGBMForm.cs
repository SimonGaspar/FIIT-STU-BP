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
            var model = new StereoSemiGlobalBlockMatchingModel()
            {
                Disparity = DisparityTrackBar.ValueMultiple16(),
                BlockSize = BlockSizeTrackBar.ValueOdd(),
                MinDispatiries = MinDisparityTrackBar.Value,
                P1 = P1TrackBar.Value,
                P2 = P2TrackBar.Value,
                Disp12MaxDiff = DispMax12DiffTrackBar.Value,
                PreFilterCap = PreFilterCapTrackBar.Value,
                UniquenessRatio = UniquenessRatioTrackBar.Value,
                SpeckleRange = SpeckleRangeTrackBar.Value,
                SpeckleWindowsSize = SpeckleWindowsSizeTrackBar.Value,
                Mode = ModeSGBM.Checked ? StereoSGBM.Mode.SGBM : StereoSGBM.Mode.HH

            };

            _stereoSemiGlobalBlockMatching.UpdateModel(model);
        }
    }
}
