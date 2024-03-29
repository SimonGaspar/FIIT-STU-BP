﻿using System;
using System.Linq;
using System.Windows.Forms;
using Bachelor_app.StructureFromMotion.FeatureMatcher;
using Emgu.CV.Features2D;

namespace Bachelor_app.StructureFromMotion.WindowsForm
{
    public partial class BruteForceForm : Form
    {
        private BruteForce bruteForce;
        private BruteForceModel defaultModel = new BruteForceModel();

        public BruteForceForm(BruteForce bruteForce)
        {
            this.bruteForce = bruteForce;

            InitializeComponent();
            InitializeStringForComponents();
            ShowDefaultModelSetting();
        }

        private void GetPropertiesAndSetModel()
        {
            try
            {
                var type = Enum.GetValues(typeof(DistanceType)).Cast<DistanceType>().First(x => x.ToString() == comboBox1.SelectedItem.ToString());

                var model = new BruteForceModel(type, checkBox1.Checked);
                bruteForce.UpdateModel(model);

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
            comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
            checkBox1.Checked = defaultModel.CrossCheck;
        }

        private void BruteForceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
