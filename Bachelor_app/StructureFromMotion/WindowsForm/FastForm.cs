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
        private FAST fast;
        private FastModel defaultModel = new FastModel();

        public FastForm(FAST fast)
        {
            this.fast = fast;
            InitializeComponent();
            InitializeStringForComponents();
            ShowDefaultModelSetting();
        }

        private void GetPropertiesAndSetModel()
        {
            try
            {
                var type = Enum.GetValues(typeof(DetectorType)).Cast<DetectorType>().First(x => x.ToString() == comboBox1.SelectedItem.ToString());
                var model = new FastModel(int.Parse(textBox1.Text), checkBox1.Checked, type);

                fast.UpdateModel(model);

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
            comboBox1.SelectedIndex = (int)defaultModel.Type;
            textBox1.Text = defaultModel.Threshold.ToString();
            checkBox1.Checked = defaultModel.NonMaxSupression;
        }

        private void FastForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
