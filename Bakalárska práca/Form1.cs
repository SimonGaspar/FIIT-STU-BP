using Bakalárska_práca.Manager;
using Bakalárska_práca.StereoVision;
using Emgu.CV;
using Emgu.CV.Structure;
using Kitware.VTK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Bakalárska_práca
{
    public partial class Form1 : Form
    {
        private FileManager fileManager;
        private DisplayManager displayManager;
        private MenuManager menuManager;
        private StereoVisionManager stereoVisionManager;


        public Form1()
        {
            InitializeComponent();

            fileManager = new FileManager(this);
            displayManager = new DisplayManager(this,fileManager);
            stereoVisionManager = new StereoVisionManager(fileManager,displayManager);
            menuManager = new MenuManager(this, displayManager,fileManager,stereoVisionManager);

            
        }

        private void ParticleReader()
        {
            // Path to vtk data must be set as an environment variable
            // VTK_DATA_ROOT = "C:\VTK\vtkdata-5.8.0"
            vtkTesting test = vtkTesting.New();
            string root = test.GetDataRoot();
            string filePath = System.IO.Path.Combine(root, @"Data\Particles.raw");

            // Read the file
            vtkParticleReader reader = vtkParticleReader.New();
            reader.SetFileName(filePath);
            reader.SetDataByteOrderToBigEndian();
            reader.Update();
            Debug.WriteLine("NumberOfPieces: " + reader.GetOutput().GetNumberOfPieces());

            // Visualize
            vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(reader.GetOutputPort());
            mapper.SetScalarRange(4, 9);
            mapper.SetPiece(1);

            vtkActor actor = vtkActor.New();
            actor.SetMapper(mapper);
            actor.GetProperty().SetPointSize(4);
            actor.GetProperty().SetColor(1, 0, 0);
            // get a reference to the renderwindow of our renderWindowControl1
            vtkRenderWindow renderWindow = renderWindowControl1.RenderWindow;
            // renderer
            vtkRenderer renderer = renderWindow.GetRenderers().GetFirstRenderer();
            // set background color
            renderer.SetBackground(0.2, 0.3, 0.4);
            // add our actor to the renderer
            renderer.AddActor(actor);
        }

        public void ReadPLY(RenderWindowControl renderWindowControl)
        {
            // Path to vtk data must be set as an environment variable
            // VTK_DATA_ROOT = "C:\VTK\vtkdata-5.8.0"
            vtkTesting test = vtkTesting.New();
            string root = test.GetDataRoot();
            string filePath = @"C:\Users\Notebook\Desktop\ImageDataset_SceauxCastle-master\Obj.obj";
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

            //vtkPoints points = vtkPoints.New();
            //    points.InsertNextPoint(0, 1000,20000);

            //    vtkVertex vertex = vtkVertex.New();
            //    vertex.GetPointIds().SetId(0, 0);

            //    vtkCellArray vertices = vtkCellArray.New();
            //    vertices.InsertNextCell(vertex);

            //    vtkPolyData polydata = vtkPolyData.New();
            //    polydata.SetPoints(points);
            //    polydata.SetVerts(vertices);

            //    // Visualize
            //    vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
            //    mapper.SetInputConnection(polydata.GetProducerPort());
            //    vtkActor actor = vtkActor.New();
            //    actor.SetMapper(mapper);
            //    actor.GetProperty().SetPointSize(4);
            //    vtkRenderWindow renderWindow = renderWindowControl.RenderWindow;
            //    vtkRenderer renderer = renderWindow.GetRenderers().GetFirstRenderer();
            //    renderer.SetBackground(0.2, 0.3, 0.4);
            //    // Add the actor to the scene
            //    renderer.AddActor(actor);
            renderer.ResetCamera();
        }
        public void Point(RenderWindowControl renderWindowControl)
        {
            // source object
            vtkSphereSource SphereSource = vtkSphereSource.New();
            SphereSource.SetRadius(0.5);
            // mapper
            vtkPolyDataMapper SphereMapper = vtkPolyDataMapper.New();
            SphereMapper.SetInputConnection(SphereSource.GetOutputPort());
            // actor
            vtkActor SphereActor = vtkActor.New();
            SphereActor.SetMapper(SphereMapper);
            // get a reference to the renderwindow of our renderWindowControl1
            vtkRenderWindow RenderWindow = renderWindowControl.RenderWindow;
            // get a reference to the renderer
            vtkRenderer Renderer = RenderWindow.GetRenderers().GetFirstRenderer();
            // set background color
            Renderer.SetBackground(0.2, 0.3, 0.4);
            // add actor to the renderer
            Renderer.AddActor(SphereActor);
            // ensure all actors are visible (in this example not necessarely needed,
            // but in case more than one actor needs to be shown it might be a good idea)
            Renderer.ResetCamera();


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
            actor.GetProperty().SetColor(1,1,1);
            vtkRenderWindow renderWindow = renderWindowControl.RenderWindow;
            vtkRenderer renderer = renderWindow.GetRenderers().GetFirstRenderer();
            renderer.SetBackground(0.2, 1, 0.4);
            renderer.AddActor(actor);
            renderer.ResetCamera();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            fileManager.AddToListView();
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
            menuManager.MenuSetDisplaySetting(sender, e);
        }
        
        private void ListViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuManager.MenuSetListViewerSetting(sender, e);
        }

        private void stereoCorrespondenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stereoVisionManager.SetStereoCorrespondenceAlgorithm(sender, e);
        }

        private void ComputeStereo_Click(object sender, EventArgs e)
        {
            stereoVisionManager.ComputeStereoCorrespondence();
        }

        private void ShowSetting_Click(object sender, EventArgs e)
        {
            stereoVisionManager.ShowSettingForStereoSolver();
        }
    }
}
