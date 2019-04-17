﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using Bachelor_app.Helper;
using Bachelor_app.Manager;
using Bachelor_app.StereoVision;
using Bachelor_app.StereoVision.Calibration;

namespace Bachelor_app
{
    public partial class MainForm : Form
    {
        private FileManager fileManager;
        private DisplayManager displayManager;
        private StereoVisionManager stereoVisionManager;
        private SfM structureFromMotionManager;
        private MainFormManager mainFormManager;
        private CameraManager cameraManager;

        public List<ListView> ListViews = new List<ListView>();
        public List<ImageList> ImageList = new List<ImageList>();

        public MainForm()
        {
            InitializeComponent();

            fileManager = new FileManager(this);
            cameraManager = new CameraManager(fileManager);

            stereoVisionManager = new StereoVisionManager(fileManager, cameraManager);
            structureFromMotionManager = new SfM(fileManager, cameraManager);
            displayManager = new DisplayManager(this, fileManager, cameraManager);
            mainFormManager = new MainFormManager(this, displayManager, fileManager, stereoVisionManager, structureFromMotionManager, cameraManager);

            WindowsFormHelper.SetWinForm(this);

            InitializeStringForComponents();
            Application.Idle += new EventHandler(displayManager.Display);
        }

        private void ListViewer_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            displayManager.DisplayImageFromListView(e.Item);
        }

        private void ToolStripComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            mainFormManager.SetDisplaySetting(sender, e, true);
        }

        private void ToolStripComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            mainFormManager.SetDisplaySetting(sender, e, false);
        }

        private void ToolStripComboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            mainFormManager.SetListViewerDisplay(sender, e);
        }

        private void ToolStripButton5_Click(object sender, EventArgs e)
        {
            mainFormManager.AddToListView(sender, e);
        }

        private void ToolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            mainFormManager.SetStereoVisionTypeAlgorithm(sender, e);
        }

        private void ToolStripButton4_Click(object sender, EventArgs e)
        {
            mainFormManager.ShowStereoVisionSettings();
        }

        private void ToolStripButton6_Click(object sender, EventArgs e)
        {
            mainFormManager.RemoveFromListView();
        }

        private void ToolStripButton9_Click(object sender, EventArgs e)
        {
            mainFormManager.RemoveAllFromListView();
        }

        private void ToolStripButton10_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(mainFormManager.StartSfM);
            thread.Start();
        }

        private void ToolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            mainFormManager.SetFeatureDetector(sender, e);
        }

        private void ToolStripButton3_Click(object sender, EventArgs e)
        {
            mainFormManager.ShowFeatureMatcherSettings(sender, e);
        }

        private void ToolStripComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            mainFormManager.SetFeatureDescriptor(sender, e);
        }

        private void ToolStripComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            mainFormManager.SetFeatureMatcher(sender, e);
        }

        private void ToolStripButton2_Click(object sender, EventArgs e)
        {
            mainFormManager.ShowFeatureDescriptorSettings(sender, e);
        }

        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            mainFormManager.ShowFeatureDetectorSettings(sender, e);
        }

        private void ToolStripButton12_Click(object sender, EventArgs e)
        {

            Thread thread = new Thread(mainFormManager.ResumeSFM);
            thread.Start();
        }

        private void RichTextBox_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        private void ToolStripButton13_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void ToolStripComboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            mainFormManager.SetMatching(sender, e);
        }

        private void ToolStripButton14_Click(object sender, EventArgs e)
        {
            if (toolStripButton14.Checked)
                toolStripButton14.BackColor = System.Drawing.Color.Black;
            else
                toolStripButton14.BackColor = default;

            mainFormManager.SetUsingParallel();
        }

        private void ToolStripComboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
            mainFormManager.SetInputType(sender, e);
        }

        private void ToolStripComboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            mainFormManager.SetCamera(sender, e, true);
        }

        private void ToolStripComboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            mainFormManager.SetCamera(sender, e, false);
        }

        private void ToolStripButton15_Click(object sender, EventArgs e)
        {
            structureFromMotionManager.stopSFM = true;
        }

        private void ToolStripButton16_Click(object sender, EventArgs e)
        {
            stereoVisionManager.ShowCalibration();
        }

        private void ToolStripButton17_Click(object sender, EventArgs e)
        {
            stereoVisionManager.stopStereoCorrespondence = true;
        }

        private void ToolStripComboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {
            mainFormManager.SetResolution(sender, e);
        }

        private void ToolStripButton11_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(stereoVisionManager.ComputeStereoCorrespondence);
            thread.Start();
        }


        private void ToolStripButton18_Click(object sender, EventArgs e)
        {
            //string json = JsonConvert.SerializeObject(CalibrationModel);
            //File.WriteAllText("CalibrationJSON.json", json);
        }

        private void ToolStripButton19_Click(object sender, EventArgs e)
        {
            //var json = File.ReadAllText("CalibrationJSON.json");
            //stereoVisionManager._calibrationManager = new CalibrationManager();
            //stereoVisionManager._calibrationManager.calibrationModel = JsonConvert.DeserializeObject<CalibrationModel>(json);
        }

        private void ToolStripButton20_Click(object sender, EventArgs e)
        {
            displayManager.DisplayPointCloud(true);
        }

        private void ToolStripButton21_Click(object sender, EventArgs e)
        {
            displayManager.DisplayPointCloud(false);
        }

        private void ToolStripButton22_Click(object sender, EventArgs e)
        {
            if (toolStripButton22.Checked)
                toolStripButton22.BackColor = System.Drawing.Color.Black;
            else
                toolStripButton22.BackColor = default;

            mainFormManager.SetUsingParallelForStereoVision();
        }

        private void ToolStripButton23_Click(object sender, EventArgs e)
        {
            CalibrationModel.IsCalibrated = false;
        }

        private void ToolStripButton24_Click(object sender, EventArgs e)
        {
            if (toolStripButton24.Checked)
                toolStripButton24.BackColor = System.Drawing.Color.Black;
            else
                toolStripButton24.BackColor = default;

            displayManager.DisplayRemapImage = toolStripButton24.Checked;
        }
    }
}
