using System;
using System.Windows.Forms;
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
        
        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            var trackBar = sender as TrackBar;
            toolTip1.SetToolTip(trackBar, trackBar.Value.ToString());
            GetPropertiesAndSetModel();
        }

        private void trackBar_ValueChangedMultiple16(object sender, EventArgs e)
        {
            var trackBar = sender as TrackBar;
            toolTip1.SetToolTip(trackBar, (trackBar.Value * 16).ToString());
            GetPropertiesAndSetModel();
        }

        private void GetPropertiesAndSetModel()
        {
            var model = new StereoBlockMatchingModel()
            {
                Disparity = DisparityTrackBar.Value * 16,
                BlockSize = BlockSizeTrackBar.Value
            };

            _stereoBlockMatching.UpdateStereoBM(model);
        }
    }
}
