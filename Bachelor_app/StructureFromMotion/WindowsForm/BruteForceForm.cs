using System;
using System.Linq;
using System.Windows.Forms;
using Bakalárska_práca.StructureFromMotion.FeatureMatcher;
using Emgu.CV.Features2D;

namespace Bachelor_app.StructureFromMotion.WindowsForm
{
    public partial class BruteForceForm : Form
    {

        BruteForce bruteForce;
        public BruteForceForm(BruteForce bruteForce)
        {
            this.bruteForce = bruteForce;

            InitializeComponent();
            InitializeStringForComponents();
        }

        private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            GetPropertiesAndSetModel();
        }

        private void GetPropertiesAndSetModel()
        {
            var type =  Enum.GetValues(typeof(DistanceType)).Cast<DistanceType>().First(x => x.ToString() == comboBox1.SelectedItem.ToString());

            var model = new BruteForceModel()
            {
                Type=type,
                CrossCheck=checkBox1.Checked
            };

            bruteForce.UpdateModel(model);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetPropertiesAndSetModel();
        }
    }
}
