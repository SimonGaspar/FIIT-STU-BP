using System;
using System.Windows.Forms;
using Bachelor_app.StructureFromMotion.FeatureDetectionDescription;
using Bachelor_app.StructureFromMotion.Model;

namespace Bachelor_app.StructureFromMotion.WindowsForm
{
    public partial class FreakForm : Form
    {
        private FREAK _freak;
        private FreakModel defaultModel = new FreakModel();

        public FreakForm(FREAK freak)
        {
            this._freak = freak;
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

                _freak.UpdateModel(model);

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
            this.textBox1.Text = defaultModel.PatternScale.ToString();
            this.textBox2.Text = defaultModel.NOctaves.ToString();
            this.checkBox1.Checked = defaultModel.OrientationNormalized;
            this.checkBox2.Checked = defaultModel.ScaleNormalized;
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
