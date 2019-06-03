﻿using System;
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
        private CudaOrientedFastAndRotatedBrief cudaORB;
        private CudaOrientedFastAndRotatedBriefModel defaultModel = new CudaOrientedFastAndRotatedBriefModel();

        public CudaOrientedFastAndRotatedBriefForm(CudaOrientedFastAndRotatedBrief cudaOrientedFastAndRotatedBrief)
        {
            cudaORB = cudaOrientedFastAndRotatedBrief;
            InitializeComponent();
            InitializeStringForComponents();
            ShowDefaultModelSetting();
        }

        private void GetPropertiesAndSetModel()
        {
            try
            {
                var type = Enum.GetValues(typeof(ScoreType)).Cast<ScoreType>().First(x => x.ToString() == comboBox1.SelectedItem.ToString());

                var model = new CudaOrientedFastAndRotatedBriefModel(
                    int.Parse(textBox1.Text),
                    float.Parse(textBox2.Text),
                    int.Parse(textBox3.Text),
                    int.Parse(textBox4.Text),
                    int.Parse(textBox5.Text),
                    int.Parse(textBox6.Text),
                    type,
                    int.Parse(textBox7.Text),
                    int.Parse(textBox8.Text),
                    checkBox1.Checked
                    );

                cudaORB.UpdateModel(model);

                Hide();
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
            comboBox1.SelectedIndex = (int)defaultModel.ScoreType;
            textBox1.Text = defaultModel.NumberOfFeatures.ToString();
            textBox2.Text = defaultModel.ScaleFactor.ToString();
            textBox3.Text = defaultModel.NLevels.ToString();
            textBox4.Text = defaultModel.EdgeThreshold.ToString();
            textBox5.Text = defaultModel.FirstLevel.ToString();
            textBox6.Text = defaultModel.WTK_A.ToString();
            textBox7.Text = defaultModel.PatchSize.ToString();
            textBox8.Text = defaultModel.FastThreshold.ToString();
            checkBox1.Checked = defaultModel.BlurForDescriptor;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            GetPropertiesAndSetModel();
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
