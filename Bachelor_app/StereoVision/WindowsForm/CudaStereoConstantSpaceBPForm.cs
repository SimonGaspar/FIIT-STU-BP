using System;
using System.Windows.Forms;
using Bachelor_app.StereoVision.Model;
using Bachelor_app.StereoVision.StereoCorrespondence;

namespace Bachelor_app.StereoVision.WindowsForm
{
    public partial class CudaStereoConstantSpaceBPForm : Form
    {
        private CudaStereoConstantSpaceBeliefPropagation cudaStereoConstantSpaceBeliefPropagation;
        private CudaStereoConstantSpaceBPModel defaultModel = new CudaStereoConstantSpaceBPModel();

        public CudaStereoConstantSpaceBPForm(CudaStereoConstantSpaceBeliefPropagation cudaStereoConstantSpaceBeliefPropagation)
        {
            this.cudaStereoConstantSpaceBeliefPropagation = cudaStereoConstantSpaceBeliefPropagation;

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
                var disparity = int.Parse(textBox1.Text);
                var blockSize = int.Parse(textBox2.Text);

                var model = new CudaStereoConstantSpaceBPModel(
                    int.Parse(textBox1.Text),
                    int.Parse(textBox2.Text),
                    int.Parse(textBox4.Text),
                    int.Parse(textBox3.Text)
                    );

                cudaStereoConstantSpaceBeliefPropagation.UpdateModel(model);

                Hide();
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
            textBox1.Text = defaultModel.Disparity.ToString();
            textBox2.Text = defaultModel.Iteration.ToString();
            textBox4.Text = defaultModel.Level.ToString();
            textBox3.Text = defaultModel.Plane.ToString();
        }
    }
}
