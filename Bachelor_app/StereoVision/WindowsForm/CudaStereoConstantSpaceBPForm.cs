using System;
using System.Windows.Forms;
using Bachelor_app.StereoVision.Model;
using Bachelor_app.StereoVision.StereoCorrespondence;

namespace Bachelor_app.StereoVision.WindowsForm
{
    /// <summary>
    /// Now not used, but in future, we can use video for SfM and stereo correspondence.
    /// DELETE these, when not using.
    /// </summary>
    public partial class CudaStereoConstantSpaceBPForm : Form
    {
        private CudaStereoConstantSpaceBeliefPropagation _cudaStereoConstantSpaceBeliefPropagation;
        private CudaStereoConstantSpaceBPModel defaultModel = new CudaStereoConstantSpaceBPModel();

        public CudaStereoConstantSpaceBPForm(CudaStereoConstantSpaceBeliefPropagation cudaStereoConstantSpaceBeliefPropagation)
        {
            this._cudaStereoConstantSpaceBeliefPropagation = cudaStereoConstantSpaceBeliefPropagation;

            InitializeComponent();
            ShowDefaultModelSetting();
        }

        private void CudaStereoConstantSpaceBPForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
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

                var model = new CudaStereoConstantSpaceBPModel(
                    int.Parse(textBox1.Text),
                    int.Parse(textBox2.Text),
                    int.Parse(textBox4.Text),
                    int.Parse(textBox3.Text)
                    );

                _cudaStereoConstantSpaceBeliefPropagation.UpdateModel(model);

                // Try create instance with new value in model.
                using (_cudaStereoConstantSpaceBeliefPropagation.CreateInstance()) { };

                this.Hide();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to set these parameters.");
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
            this.textBox2.Text = defaultModel.Iteration.ToString();
            this.textBox4.Text = defaultModel.Level.ToString();
            this.textBox3.Text = defaultModel.Plane.ToString();
        }
    }
}

