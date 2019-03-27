﻿using System;
using System.Linq;
using System.Windows.Forms;
using Bakalárska_práca.StructureFromMotion.FeatureMatcher;
using Emgu.CV.Features2D;

namespace Bachelor_app.StructureFromMotion.WindowsForm
{
    public partial class CudaBruteForceForm : Form
    {

        CudaBruteForce cudaBruteForce;
        CudaBruteForceModel defaultModel = new BruteForceModel();

        public CudaBruteForceForm(CudaBruteForce cudaBruteForce)
        {
            this.cudaBruteForce = cudaBruteForce;

            InitializeComponent();
            InitializeStringForComponents();
        }

        private void GetPropertiesAndSetModel()
        {
            try
            {
                var type = Enum.GetValues(typeof(DistanceType)).Cast<DistanceType>().First(x => x.ToString() == comboBox1.SelectedItem.ToString());

                var model = new BruteForceModel()
                {
                    Type = type
                };

                cudaBruteForce.UpdateModel(model);
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
            this.comboBox1.SelectedIndex = this.comboBox1.Items.Count-1;
        }
    }
}