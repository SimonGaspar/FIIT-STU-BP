using System;
using System.Windows.Forms;
using Bachelor_app.StereoVision.Model;
using Bachelor_app.StereoVision.StereoCorrespondence;
using Emgu.CV;

namespace Bachelor_app.StereoVision.WindowsForm
{
    public partial class StereoSGBMForm : Form
    {
        private StereoSemiGlobalBlockMatching _stereoSemiGlobalBlockMatching;
        private StereoSemiGlobalBlockMatchingModel defaultModel = new StereoSemiGlobalBlockMatchingModel();

        public StereoSGBMForm(StereoSemiGlobalBlockMatching stereoSemiGlobalBlockMatching)
        {
            this._stereoSemiGlobalBlockMatching = stereoSemiGlobalBlockMatching;

            InitializeComponent();
            ShowDefaultModelSetting();
        }

        /// <summary>
        /// Create model from WinForm values and update.
        /// </summary>
        private void GetPropertiesAndSetModel()
        {
            try
            {
                var model = new StereoSemiGlobalBlockMatchingModel(
                    int.Parse(textBox1.Text),
                    int.Parse(textBox2.Text),
                    int.Parse(textBox10.Text),
                    int.Parse(textBox3.Text),
                    int.Parse(textBox8.Text),
                    int.Parse(textBox9.Text),
                    int.Parse(textBox4.Text),
                    int.Parse(textBox7.Text),
                    int.Parse(textBox6.Text),
                    int.Parse(textBox5.Text),
                    ModeSGBM.Checked ? StereoSGBM.Mode.SGBM : StereoSGBM.Mode.HH
                );

                _stereoSemiGlobalBlockMatching.UpdateModel(model);

                // Try create instance with new value in model.
                using (_stereoSemiGlobalBlockMatching.CreateInstance()) { };

                this.Hide();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to set these parameters.");
            }
        }

        private void StereoSGBMForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            GetPropertiesAndSetModel();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            ShowDefaultModelSetting();
        }

        private void ShowDefaultModelSetting()
        {
            this.textBox1.Text = defaultModel.Disparity.ToString();
            this.textBox2.Text = defaultModel.BlockSize.ToString();
            this.textBox3.Text = defaultModel.P1.ToString();
            this.textBox4.Text = defaultModel.PreFilterCap.ToString();
            this.textBox5.Text = defaultModel.SpeckleWindowsSize.ToString();
            this.textBox6.Text = defaultModel.SpeckleRange.ToString();
            this.textBox7.Text = defaultModel.UniquenessRatio.ToString();
            this.textBox8.Text = defaultModel.P2.ToString();
            this.textBox9.Text = defaultModel.Disp12MaxDiff.ToString();
            this.textBox10.Text = defaultModel.MinDispatiries.ToString();

            ModeSGBM.Checked = defaultModel.Mode == StereoSGBM.Mode.SGBM ? true : false;
        }
    }
}
