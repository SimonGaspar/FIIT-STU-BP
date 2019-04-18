using System;
using System.Linq;
using System.Windows.Forms;
using Bachelor_app.StructureFromMotion.FeatureMatcher;
using Emgu.CV.Features2D;

namespace Bachelor_app.StructureFromMotion.WindowsForm
{
    public partial class CudaBruteForceForm : Form
    {

        private CudaBruteForce cudaBruteForce;
        private CudaBruteForceModel defaultModel = new BruteForceModel();

        public CudaBruteForceForm(CudaBruteForce cudaBruteForce)
        {
            this.cudaBruteForce = cudaBruteForce;

            InitializeComponent();
            InitializeStringForComponents();
            ShowDefaultModelSetting();
        }

        private void GetPropertiesAndSetModel()
        {
            try
            {
                var type = Enum.GetValues(typeof(DistanceType)).Cast<DistanceType>().First(x => x.ToString() == comboBox1.SelectedItem.ToString());
                var model = new BruteForceModel(type);

                cudaBruteForce.UpdateModel(model);
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
            this.comboBox1.SelectedIndex = this.comboBox1.Items.Count - 1;
        }

        private void CudaBruteForceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
