using System;
using System.Windows.Forms;
using Bachelor_app.StructureFromMotion.FeatureDetectionDescription;
using Bachelor_app.StructureFromMotion.Model;

namespace Bachelor_app.StructureFromMotion.WindowsForm
{
    public partial class BriefForm : Form
    {
        BRIEF _brief;
        BriefModel defaultModel = new BriefModel();

        public BriefForm(BRIEF brief)
        {
            this._brief = brief;
            InitializeComponent();
            InitializeStringForComponents();
        }

        private void GetPropertiesAndSetModel()
        {
            try
            {
                var model = new BriefModel()
                {
                    DescriptorSize = int.Parse(textBox1.Text)
                };

                _brief.UpdateModel(model);
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
            this.textBox1.Text = defaultModel.DescriptorSize.ToString();

        }
    }
}
