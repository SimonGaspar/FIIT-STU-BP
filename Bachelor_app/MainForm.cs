using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using Bachelor_app.Manager;
using Bakalárska_práca.Manager;
using Bakalárska_práca.StereoVision;
using Kitware.VTK;

namespace Bakalárska_práca
{
    public partial class MainForm : Form
    {
        private FileManager fileManager;
        private DisplayManager displayManager;
        private StereoVisionManager stereoVisionManager;
        private SfM structureFromMotionManager;
        private MainFormManager mainFormManager;


        public List<ListView> ListViews = new List<ListView>();
        public List<ImageList> ImageList = new List<ImageList>();

        public MainForm()
        {
            InitializeComponent();

            fileManager = new FileManager(this);
            displayManager = new DisplayManager(this, fileManager);

            stereoVisionManager = new StereoVisionManager(fileManager, displayManager);
            structureFromMotionManager = new SfM(fileManager, displayManager);

            mainFormManager = new MainFormManager(this, displayManager, fileManager, stereoVisionManager, structureFromMotionManager);


#if (DEBUG)
            InitializeStringForComponents();
#endif

        }

        public void ReadPlainText(RenderWindowControl renderWindowControl)
        {
            // Path to vtk data must be set as an environment variable
            // VTK_DATA_ROOT = "C:\VTK\vtkdata-5.8.0"
            vtkTesting test = vtkTesting.New();
            string root = test.GetDataRoot();
            string filePath = Path.Combine($"..\\..\\..\\Temp", $"Result.nvm");

            FileStream fs = null;
            StreamReader sr = null;
            String sLineBuffer;
            String[] sXYZ;
            char[] chDelimiter = new char[] { ' ', '\t', ';' };
            double[] xyz = new double[10];
            vtkPoints points = vtkPoints.New();
            int cnt = 0;

            try
            {
                // in case file must be open in another application too use "FileShare.ReadWrite"
                fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                sr = new StreamReader(fs);

                // Setup the colors array
                vtkUnsignedCharArray colors = vtkUnsignedCharArray.New();
                colors.SetNumberOfComponents(3);
                colors.SetName("Colors");

                // Read point
                while (!string.IsNullOrEmpty(sr.ReadLine()))
                {
                }

                // Read point
                while (!string.IsNullOrEmpty(sr.ReadLine()))
                {
                }
                sr.ReadLine();

                // Read point
                while (!string.IsNullOrEmpty(sLineBuffer = sr.ReadLine()))
                {
                    cnt++;
                    sXYZ = sLineBuffer.Split(chDelimiter, StringSplitOptions.RemoveEmptyEntries);
                    if (sXYZ == null)
                    {
                        MessageBox.Show("data seems to be in wrong format at line " + cnt, "Format Exception", MessageBoxButtons.OK);
                        return;
                    }
                    xyz[0] = double.Parse(sXYZ[0], CultureInfo.InvariantCulture);
                    xyz[1] = double.Parse(sXYZ[1], CultureInfo.InvariantCulture);
                    xyz[2] = double.Parse(sXYZ[2], CultureInfo.InvariantCulture);
                    colors.InsertNextValue(byte.Parse(sXYZ[3], CultureInfo.InvariantCulture));
                    colors.InsertNextValue(byte.Parse(sXYZ[4], CultureInfo.InvariantCulture));
                    colors.InsertNextValue(byte.Parse(sXYZ[5], CultureInfo.InvariantCulture));
                    points.InsertNextPoint(xyz[0], xyz[1], xyz[2]);
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
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "IOException", MessageBoxButtons.OK);
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                    sr.Dispose();
                    sr = null;
                }
            }
        }

        public void SimplePointsReader(RenderWindowControl renderWindowControl)
        {
            // Path to vtk data must be set as an environment variable
            // VTK_DATA_ROOT = "C:\VTK\vtkdata-5.8.0"
            vtkTesting test = vtkTesting.New();
            string root = test.GetDataRoot();
            string filePath = @"C:\Users\Notebook\Desktop\ImageDataset_SceauxCastle-master\pokus.txt";

            vtkSimplePointsReader reader = vtkSimplePointsReader.New();
            reader.SetFileName(filePath);
            reader.Update();
            // Visualize
            vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(reader.GetOutputPort());
            vtkActor actor = vtkActor.New();
            actor.SetMapper(mapper);
            actor.GetProperty().SetPointSize(4);
            vtkRenderWindow renderWindow = renderWindowControl1.RenderWindow;
            vtkRenderer renderer = renderWindow.GetRenderers().GetFirstRenderer();
            renderer.SetBackground(0.2, 0.3, 0.4);
            renderer.AddActor(actor);
            renderer.ResetCamera();
        }

        public void ReadPLY(RenderWindowControl renderWindowControl)
        {
            // Path to vtk data must be set as an environment variable
            // VTK_DATA_ROOT = "C:\VTK\vtkdata-5.8.0"
            vtkTesting test = vtkTesting.New();
            string root = test.GetDataRoot();
            string filePath = @"C:\Users\Notebook\Desktop\FIIT-STU-BC\FIIT-STU-BP\Bachelor_app\Temp\Result_CMVS.0.ply";
            vtkOBJReader reader = vtkOBJReader.New();
            reader.SetFileName(filePath);
            reader.Update();
            vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(reader.GetOutputPort());

            vtkActor actor = vtkActor.New();
            actor.SetMapper(mapper);

            actor.GetProperty().SetPointSize(4);
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

        public void Point(RenderWindowControl renderWindowControl)
        {
            // Path to vtk data must be set as an environment variable
            // VTK_DATA_ROOT = "C:\VTK\vtkdata-5.8.0"
            vtkTesting test = vtkTesting.New();
            string root = test.GetDataRoot();
            string filePath = @"C:\Users\Notebook\Desktop\ImageDataset_SceauxCastle-master\lettosuo_final2_group1_densified_point_cloud_part_1 - Cloud.ply";

            vtkSimplePointsReader reader = vtkSimplePointsReader.New();
            reader.SetFileName(filePath);
            reader.Update();
            // Visualize
            vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(reader.GetOutputPort());
            vtkActor actor = vtkActor.New();
            actor.SetMapper(mapper);
            actor.GetProperty().SetPointSize(4000000);
            actor.GetProperty().SetColor(1, 1, 1);
            vtkRenderWindow renderWindow = renderWindowControl.RenderWindow;
            vtkRenderer renderer = renderWindow.GetRenderers().GetFirstRenderer();
            renderer.SetBackground(0.2, 1, 0.4);
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

        private void DisplayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //menuManager.MenuSetDisplaySetting(sender, e);
        }

        private void ListViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //menuManager.MenuSetListViewerSetting(sender, e);
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
            mainFormManager.StartSfM();
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
    }
}
