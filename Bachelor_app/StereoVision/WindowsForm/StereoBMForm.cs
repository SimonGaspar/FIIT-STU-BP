using System;
using System.Windows.Forms;
using Bachelor_app.Extension;
using Bachelor_app.Helper;
using Bachelor_app.StereoVision.StereoCorrespondence;

namespace Bachelor_app.StereoVision.WindowsForm
{
    public partial class StereoBMForm : Form
    {
        private StereoBlockMatching _stereoBlockMatching;

        public StereoBMForm(StereoBlockMatching stereoBlockMatching)
        {
            this._stereoBlockMatching = stereoBlockMatching;

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
            var model = new StereoBlockMatchingModel()
            {
                Disparity = DisparityTrackBar.ValueMultiple16(),
                BlockSize = BlockSizeTrackBar.ValueOdd()
            };

            _stereoBlockMatching.UpdateModel(model);
        }
    }
}
