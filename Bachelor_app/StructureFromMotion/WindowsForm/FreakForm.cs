using System;
using System.Windows.Forms;
using Bachelor_app.StructureFromMotion.FeatureDetectionDescription;
using Bachelor_app.StructureFromMotion.Model;

namespace Bachelor_app.StructureFromMotion.WindowsForm
{
    public partial class FreakForm : Form
    {
        private FREAK freak;
        private FreakModel defaultModel = new FreakModel();

        public FreakForm(FREAK freak)
        {
            this.freak = freak;
            InitializeComponent();
            ShowDefaultModelSetting();
        }

        private void GetPropertiesAndSetModel()
        {
            try
            {
                var model = new FreakModel(
                    checkBox1.Checked,
                    checkBox2.Checked,
                    float.Parse(textBox1.Text),
                    int.Parse(textBox2.Text));

                freak.UpdateModel(model);

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
            textBox1.Text = defaultModel.PatternScale.ToString();
            textBox2.Text = defaultModel.NOctaves.ToString();
            checkBox1.Checked = defaultModel.OrientationNormalized;
            checkBox2.Checked = defaultModel.ScaleNormalized;
        }

        private void FreakForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
