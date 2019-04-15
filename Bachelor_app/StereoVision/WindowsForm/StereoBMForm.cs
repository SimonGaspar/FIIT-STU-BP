using System;
using System.Windows.Forms;
using Bakalárska_práca.Extension;
using Bakalárska_práca.Helper;
using Bakalárska_práca.StereoVision.StereoCorrespondence;

namespace Bakalárska_práca.StereoVision.WindowsForm
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
            var model = new StereoBlockMatchingModel()
            {
                Disparity = DisparityTrackBar.ValueMultiple16(),
                BlockSize = BlockSizeTrackBar.ValueOdd()
            };

            _stereoBlockMatching.UpdateModel(model);
        }
    }
}
