using System;
using System.Windows.Forms;
using Bachelor_app.Helper;
using Bachelor_app.StereoVision.StereoCorrespondence;

namespace Bachelor_app.StereoVision.WindowsForm
{
    public partial class StereoBMForm : Form
    {
        private StereoBlockMatching _stereoBlockMatching;
        private StereoBlockMatchingModel defaultModel = new StereoBlockMatchingModel();

        public StereoBMForm(StereoBlockMatching stereoBlockMatching)
        {
            this._stereoBlockMatching = stereoBlockMatching;

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
                var Disparity = int.Parse(textBox1.Text);
                var BlockSize = int.Parse(textBox2.Text);

                var model = new StereoBlockMatchingModel(Disparity, BlockSize);

                _stereoBlockMatching.UpdateModel(model);

                this.Hide();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to set these parameters.");
            }
        }

        private void StereoBMForm_FormClosing(object sender, FormClosingEventArgs e)
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
        }
    }
}
