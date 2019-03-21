using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Bachelor_app.StructureFromMotion.Model;
using Bakalárska_práca.StructureFromMotion.FeatureDetectionDescription;
using static Emgu.CV.Features2D.ORBDetector;

namespace Bachelor_app.StructureFromMotion.WindowsForm
{
    public partial class OrientedFastAndRotatedBriefForm : Form
    {
        OrientedFastAndRotatedBrief orb;
        public OrientedFastAndRotatedBriefModel defaultModel = new OrientedFastAndRotatedBriefModel();

        public OrientedFastAndRotatedBriefForm(OrientedFastAndRotatedBrief orientedFastAndRotatedBrief)
        {
            orb = orientedFastAndRotatedBrief;
            InitializeComponent();
            InitializeStringForComponents();
        }

        private void GetPropertiesAndSetModel()
        {
            bool ConversionOK = false;
            OrientedFastAndRotatedBriefModel model = null;
            try
            {
                var type = Enum.GetValues(typeof(ScoreType)).Cast<ScoreType>().First(x => x.ToString() == comboBox1.SelectedItem.ToString());

                model = new OrientedFastAndRotatedBriefModel()
                {
                    NumberOfFeatures = int.Parse(textBox1.Text),
                    ScaleFactor = float.Parse(textBox2.Text),
                    NLevels = int.Parse(textBox3.Text),
                    EdgeThreshold = int.Parse(textBox4.Text),
                    firstLevel = int.Parse(textBox5.Text),
                    WTK_A = int.Parse(textBox6.Text),
                    PatchSize = int.Parse(textBox7.Text),
                    FastThreshold = int.Parse(textBox8.Text),
                    ScoreType = type
                };
                ConversionOK = true;

                if (ConversionOK)
                    orb.UpdateModel(model);

                this.Close();

            }
            catch (Exception e)
            {

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShowDefaultModelSetting();
        }

        private void ShowDefaultModelSetting()
        {
            this.comboBox1.SelectedIndex = (int)defaultModel.ScoreType;
            this.textBox1.Text = defaultModel.NumberOfFeatures.ToString();
            this.textBox2.Text = defaultModel.ScaleFactor.ToString();
            this.textBox3.Text = defaultModel.NLevels.ToString();
            this.textBox4.Text = defaultModel.EdgeThreshold.ToString();
            this.textBox5.Text = defaultModel.firstLevel.ToString();
            this.textBox6.Text = defaultModel.WTK_A.ToString();
            this.textBox7.Text = defaultModel.PatchSize.ToString();
            this.textBox8.Text = defaultModel.FastThreshold.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetPropertiesAndSetModel();
        }
    }
}
