using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Bachelor_app.StructureFromMotion.FeatureDetectionDescription;
using Bachelor_app.StructureFromMotion.Model;
using static Emgu.CV.Features2D.FastDetector;

namespace Bachelor_app.StructureFromMotion.WindowsForm
{
    public partial class FastForm : Form
    {
        FAST _fast;
        FastModel defaultModel = new FastModel();

        public FastForm(FAST fast)
        {
            this._fast = fast;
            InitializeComponent();
            InitializeStringForComponents();
        }

        private void GetPropertiesAndSetModel()
        {
            try
            {
                var type = Enum.GetValues(typeof(DetectorType)).Cast<DetectorType>().First(x => x.ToString() == comboBox1.SelectedItem.ToString());

                var model = new FastModel()
                {
                    Type = type,
                    Threshold = int.Parse(textBox1.Text),
                    NonMaxSupression = checkBox1.Checked
                };

                _fast.UpdateModel(model);
                this.Close();
            }
            catch (Exception e) { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetPropertiesAndSetModel();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShowDefaultModelSetting();
        }

        private void ShowDefaultModelSetting()
        {
            this.comboBox1.SelectedIndex = (int)defaultModel.Type;
            this.textBox1.Text = defaultModel.Threshold.ToString();
            this.checkBox1.Checked = defaultModel.NonMaxSupression;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
