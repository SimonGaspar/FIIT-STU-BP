using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Bachelor_app.Helper;
using Bachelor_app.Manager;
using Bachelor_app.Model;
using Bachelor_app.StereoVision;
using Bachelor_app.StereoVision.Calibration;
using Bakalárska_práca.Helper;
using Bakalárska_práca.Manager;
using Bakalárska_práca.StereoVision;
using Kitware.VTK;
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
        private CalibrationManager calibrationManager;

        string tempDirectory = Path.GetFullPath($"..\\..\\..\\Temp");

        public List<ListView> ListViews = new List<ListView>();
        public List<ImageList> ImageList = new List<ImageList>();

        public MainForm()
        {
            InitializeComponent();

            cameraManager = new CameraManager(this);
            fileManager = new FileManager(this, cameraManager);
            displayManager = new DisplayManager(this, fileManager, cameraManager);

            stereoVisionManager = new StereoVisionManager(fileManager, displayManager, cameraManager, this);
            structureFromMotionManager = new SfM(fileManager, displayManager, this, cameraManager);


            mainFormManager = new MainFormManager(this, displayManager, fileManager, stereoVisionManager, structureFromMotionManager, cameraManager);
            WindowsFormHelper.SetWinForm(this);

            InitializeStringForComponents();
            Application.Idle += new EventHandler(displayManager.Display);
        }

        public void ReadNVM(RenderWindowControl renderWindowControl)
        {
            var nvmFile = sfmHelper.LoadPointCloud();
            foreach (var model in nvmFile)
            {
                foreach (var camera in model.listImageModel)
                    ReadImageIntoObject(renderWindowControl, camera);
                
                ReadPointIntoObject(renderWindowControl, model.listPointModel);
            }
        }
        
        public void ReadImageIntoObject(RenderWindowControl renderWindowControl, nvmImageModel camera)
        {
            vtkRenderWindow renderWindow = renderWindowControl.RenderWindow;
            vtkRenderer renderer = renderWindow.GetRenderers().GetFirstRenderer();

            string filePath = Path.Combine(tempDirectory, $"{camera.fileName}");
            vtkJPEGReader reader = vtkJPEGReader.New();
            reader.SetFileName(filePath);
            reader.Update();

            // Treba poriesit ako nasmerovat obrazky bez pokazenia textury
            var vectoris = Vector3.Transform(new Vector3(0, 0, 1), camera.quaternion);

            vtkPlaneSource planeSource = vtkPlaneSource.New();
            vtkTexture texture = new vtkTexture();
            texture.SetInputConnection(reader.GetOutputPort());
            vtkTransform transform = new vtkTransform();
            transform.RotateX(180);
            texture.SetTransform(transform);

            vtkTextureMapToPlane plane = new vtkTextureMapToPlane();
            plane.SetInputConnection(planeSource.GetOutputPort());
            planeSource.SetCenter(camera.cameraCenter.X, camera.cameraCenter.Y, camera.cameraCenter.Z);

            vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(plane.GetOutputPort());
            vtkActor actor = vtkActor.New();
            actor.SetMapper(mapper);
            actor.SetTexture(texture);

            renderer.SetBackground(0.2, 0.3, 0.4);
            renderer.AddActor(actor);

        }

        public void ReadPointIntoObject(RenderWindowControl renderWindowControl, List<nvmPointModel> listPointModel)
        {
            vtkUnsignedCharArray colors = vtkUnsignedCharArray.New();
            colors.SetNumberOfComponents(3);
            colors.SetName("Colors");
            vtkPoints points = vtkPoints.New();

            foreach (var point in listPointModel)
            {

                colors.InsertNextValue(byte.Parse(point.color.X.ToString(), CultureInfo.InvariantCulture));
                colors.InsertNextValue(byte.Parse(point.color.Y.ToString(), CultureInfo.InvariantCulture));
                colors.InsertNextValue(byte.Parse(point.color.Z.ToString(), CultureInfo.InvariantCulture));
                points.InsertNextPoint(
                    double.Parse(point.position.X.ToString(), CultureInfo.InvariantCulture),
                    double.Parse(point.position.Y.ToString(), CultureInfo.InvariantCulture),
                    double.Parse(point.position.Z.ToString(), CultureInfo.InvariantCulture));
            }

            vtkPolyData polydata = vtkPolyData.New();
            polydata.SetPoints(points);
            polydata.GetPointData().SetScalars(colors);
            vtkVertexGlyphFilter glyphFilter = vtkVertexGlyphFilter.New();
            glyphFilter.SetInputConnection(polydata.GetProducerPort());

            // Visualize
            vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(glyphFilter.GetOutputPort());
            vtkActor actor = vtkActor.New();
            actor.SetMapper(mapper);
            actor.GetProperty().SetPointSize(2);
            // get a reference to the renderwindow of our renderWindowControl1
            vtkRenderWindow renderWindow = renderWindowControl.RenderWindow;
            // renderer
            vtkRenderer renderer = renderWindow.GetRenderers().GetFirstRenderer();
            // set background color
            renderer.SetBackground(0.2, 0.3, 0.4);
            // add our actor to the renderer
            renderer.AddActor(actor);
            renderer.ResetCamera();
        }

      

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ListViewer_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            displayManager.DisplayImageFromListView(e.Item);
        }

        private void ComputeStereo_Click(object sender, EventArgs e)
        {
            stereoVisionManager.ComputeStereoCorrespondence();
        }

        private void ShowSetting_Click(object sender, EventArgs e)
        {
            stereoVisionManager.ShowSettingForStereoSolver();
        }

        private void StartSFM_Click(object sender, EventArgs e)
        {
            structureFromMotionManager.StartSFM();
        }

        private void tableLayoutPanel6_Paint(object sender, PaintEventArgs e)
        {

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

        public void SetMaximumProgressBar(string name, int maxValue)
        {
            //if (statusStrip1.InvokeRequired)
            //    statusStrip1.Invoke((Action)delegate { SetMaximumProgressBar(name, maxValue); });
            //else
            //{
            //    toolStripProgressBar1.Value = 0;
            //    toolStripProgressBar1.Maximum = maxValue;
            //    toolStripStatusLabel1.Text = name;
            //}
        }

        public void IncrementValueProgressBar()
        {
            //if (this.InvokeRequired)
            //    this.Invoke((Action)delegate { IncrementValueProgressBar(); });
            //else
            //{
            //    this.toolStripProgressBar1.Value++;
            //}
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
    }
}
