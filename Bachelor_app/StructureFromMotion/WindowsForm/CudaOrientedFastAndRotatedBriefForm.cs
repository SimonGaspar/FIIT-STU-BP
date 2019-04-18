using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Bachelor_app.StructureFromMotion.FeatureDetectionDescription;
using Bachelor_app.StructureFromMotion.Model;
using static Emgu.CV.Features2D.ORBDetector;

namespace Bachelor_app.StructureFromMotion.WindowsForm
{
    public partial class CudaOrientedFastAndRotatedBriefForm : Form
    {
        CudaOrientedFastAndRotatedBrief _cudaORB;
        public CudaOrientedFastAndRotatedBriefModel defaultModel = new CudaOrientedFastAndRotatedBriefModel();

        public CudaOrientedFastAndRotatedBriefForm(CudaOrientedFastAndRotatedBrief cudaOrientedFastAndRotatedBrief)
        {
            _cudaORB = cudaOrientedFastAndRotatedBrief;
            InitializeComponent();
            InitializeStringForComponents();
        }

        private void GetPropertiesAndSetModel()
        {
            CudaOrientedFastAndRotatedBriefModel model = null;
            try
            {
                var type = Enum.GetValues(typeof(ScoreType)).Cast<ScoreType>().First(x => x.ToString() == comboBox1.SelectedItem.ToString());
                var NumberOfFeatures = int.Parse(textBox1.Text);
                var ScaleFactor = float.Parse(textBox2.Text);
                var NLevels = int.Parse(textBox3.Text);
                var EdgeThreshold = int.Parse(textBox4.Text);
                var FirstLevel = int.Parse(textBox5.Text);
                var WTK_A = int.Parse(textBox6.Text);
                var PatchSize = int.Parse(textBox7.Text);
                var FastThreshold = int.Parse(textBox8.Text);
                var BlurForDescriptor = checkBox1.Checked;


                model = new CudaOrientedFastAndRotatedBriefModel(
                    NumberOfFeatures,
                    ScaleFactor,
                    NLevels,
                    EdgeThreshold,
                    FirstLevel,
                    WTK_A,
                    type,
                    PatchSize,
                    FastThreshold,
                    BlurForDescriptor
                    );

                _cudaORB.UpdateModel(model);

                this.Hide();

            }
            catch (Exception)
            {
                MessageBox.Show("Unable to set these parameters.");
            }

        }

        private void Button2_Click(object sender, EventArgs e)
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
            this.textBox5.Text = defaultModel.FirstLevel.ToString();
            this.textBox6.Text = defaultModel.WTK_A.ToString();
            this.textBox7.Text = defaultModel.PatchSize.ToString();
            this.textBox8.Text = defaultModel.FastThreshold.ToString();
            this.checkBox1.Checked = defaultModel.BlurForDescriptor;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            GetPropertiesAndSetModel();
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TextBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void Label8_Click(object sender, EventArgs e)
        {

        }

        private void CudaOrientedFastAndRotatedBriefForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
