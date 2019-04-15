using System;
using System.Windows.Forms;
using Bakalárska_práca.Extension;
using Bakalárska_práca.Helper;
using Bakalárska_práca.StereoVision.Model;
using Bakalárska_práca.StereoVision.StereoCorrespondence;
using Emgu.CV;

namespace Bakalárska_práca.StereoVision.WindowsForm
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
        private void trackBar_ValueChangedOdd(object sender, EventArgs e)
        {
            WindowsFormHelper.trackBar_ValueChangedOdd(sender as TrackBar, toolTip1, GetPropertiesAndSetModel);
        }

        private void trackBar_ValueChangedMultiple16(object sender, EventArgs e)
        {
            WindowsFormHelper.trackBar_ValueChangedMultiple16(sender as TrackBar, toolTip1, GetPropertiesAndSetModel);
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
