using System;
using System.Windows.Forms;
using Bachelor_app.StructureFromMotion.FeatureDetectionDescription;
using Bachelor_app.StructureFromMotion.Model;

namespace Bachelor_app.StructureFromMotion.WindowsForm
{
    public partial class BriefForm : Form
    {
        private BRIEF _brief;
        private BriefModel defaultModel = new BriefModel();

        public BriefForm(BRIEF brief)
        {
            this._brief = brief;
            InitializeComponent();
            ShowDefaultModelSetting();
        }

        private void GetPropertiesAndSetModel()
        {
            try
            {
                var model = new BriefModel(int.Parse(textBox1.Text));

                _brief.UpdateModel(model);

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
            this.textBox1.Text = defaultModel.DescriptorSize.ToString();

        }

        private void BriefForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
