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
        public OrientedFastAndRotatedBriefForm(OrientedFastAndRotatedBrief orientedFastAndRotatedBrief)
        {
            orb = orientedFastAndRotatedBrief;
            InitializeComponent();
            InitializeStringForComponents();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            GetPropertiesAndSetModel();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            GetPropertiesAndSetModel();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            GetPropertiesAndSetModel();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            GetPropertiesAndSetModel();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            GetPropertiesAndSetModel();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            GetPropertiesAndSetModel();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            GetPropertiesAndSetModel();
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            GetPropertiesAndSetModel();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetPropertiesAndSetModel();
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
            }
            catch (Exception e) { }

            if (ConversionOK)
                orb.UpdateModel(model);

        }
    }
}
