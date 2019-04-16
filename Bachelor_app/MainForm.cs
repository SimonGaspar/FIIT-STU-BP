using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Bachelor_app.Manager;
using Bachelor_app.StereoVision;
using Bachelor_app.StereoVision.Calibration;
using Bakalárska_práca.Helper;
using Bakalárska_práca.Manager;
using Bakalárska_práca.StereoVision;
using Newtonsoft.Json;

namespace Bakalárska_práca
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
            cameraManager = new CameraManager(this,fileManager);
            displayManager = new DisplayManager(this, fileManager, cameraManager);

            stereoVisionManager = new StereoVisionManager(fileManager, displayManager, cameraManager, this);
            structureFromMotionManager = new SfM(fileManager, displayManager, this, cameraManager);


            mainFormManager = new MainFormManager(this, displayManager, fileManager, stereoVisionManager, structureFromMotionManager, cameraManager);
            WindowsFormHelper.SetWinForm(this);

            InitializeStringForComponents();
            Application.Idle += new EventHandler(displayManager.Display);
        }

        private void ListViewer_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            displayManager.DisplayImageFromListView(e.Item);
        }

        private void toolStripComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            mainFormManager.SetDisplaySetting(sender, e, true);
        }

        private void toolStripComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            mainFormManager.SetDisplaySetting(sender, e, false);
        }

        private void toolStripComboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            mainFormManager.SetListViewerDisplay(sender, e);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            mainFormManager.AddToListView(sender, e);
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            mainFormManager.SetStereoVisionTypeAlgorithm(sender, e);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            mainFormManager.ShowStereoVisionSettings();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            mainFormManager.RemoveFromListView();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            mainFormManager.RemoveAllFromListView();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(mainFormManager.StartSfM);
            thread.Start();
        }

        private void toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            mainFormManager.SetFeatureDetector(sender, e);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            mainFormManager.ShowFeatureMatcherSettings(sender, e);
        }

        private void toolStripComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            mainFormManager.SetFeatureDescriptor(sender, e);
        }

        private void toolStripComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            mainFormManager.SetFeatureMatcher(sender, e);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            mainFormManager.ShowFeatureDescriptorSettings(sender, e);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            mainFormManager.ShowFeatureDetectorSettings(sender, e);
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {

            Thread thread = new Thread(mainFormManager.ResumeSFM);
            thread.Start();
        }

        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void toolStripComboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            mainFormManager.SetMatching(sender, e);
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            if (toolStripButton14.Checked)
                toolStripButton14.BackColor = System.Drawing.Color.Black;
            else
                toolStripButton14.BackColor = default(System.Drawing.Color);

            mainFormManager.SetUsingParallel();
        }

        private void toolStripComboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
            mainFormManager.SetInputType(sender, e);
        }

        private void toolStripComboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            mainFormManager.SetCamera(sender, e, true);
        }

        private void toolStripComboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            mainFormManager.SetCamera(sender, e, false);
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            structureFromMotionManager.stopSFM = true;
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            stereoVisionManager.ShowCalibration();
        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            stereoVisionManager.stopStereoCorrespondence = true;
        }

        private void toolStripComboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {
            mainFormManager.SetResolution(sender, e);
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(stereoVisionManager.ComputeStereoCorrespondence);
            thread.Start();
        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            string json = JsonConvert.SerializeObject(stereoVisionManager._calibrationManager.calibrationModel);
            File.WriteAllText("CalibrationJSON.json", json);
        }

        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            var json = File.ReadAllText("CalibrationJSON.json");
            stereoVisionManager._calibrationManager = new CalibrationManager();
            stereoVisionManager._calibrationManager.calibrationModel = JsonConvert.DeserializeObject<CalibrationModel>(json);
        }

        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            displayManager.DisplayPointCloud(true);
        }

        private void toolStripButton21_Click(object sender, EventArgs e)
        {
            displayManager.DisplayPointCloud(false);
        }

        private void toolStripButton22_Click(object sender, EventArgs e)
        {
            if (toolStripButton22.Checked)
                toolStripButton22.BackColor = System.Drawing.Color.Black;
            else
                toolStripButton22.BackColor = default(System.Drawing.Color);

            mainFormManager.SetUsingParallelForStereoVision();
        }

        private void toolStripButton23_Click(object sender, EventArgs e)
        {
            stereoVisionManager._calibrationManager = null;
        }
    }
}
