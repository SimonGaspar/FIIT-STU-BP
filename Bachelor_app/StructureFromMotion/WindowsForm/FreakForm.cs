using System;
using System.Windows.Forms;
using Bachelor_app.StructureFromMotion.FeatureDetectionDescription;
using Bachelor_app.StructureFromMotion.Model;

namespace Bachelor_app.StructureFromMotion.WindowsForm
{
    public partial class FreakForm : Form
    {
        FREAK _freak;
        FreakModel defaultModel = new FreakModel();

        public FreakForm(FREAK freak)
        {
            this._freak = freak;
            InitializeComponent();
            InitializeStringForComponents();
        }

        private void GetPropertiesAndSetModel()
        {
            try
            {
                var model = new FreakModel()
                {
                    PatternScale = float.Parse(textBox1.Text),
                    NOctaves = int.Parse(textBox2.Text),
                    OrientationNormalized = checkBox1.Checked,
                    ScaleNormalized = checkBox2.Checked
                };

                _freak.UpdateModel(model);
                this.Close();
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
    }
}
