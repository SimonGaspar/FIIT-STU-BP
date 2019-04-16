using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;
using Bachelor_app;
using Bachelor_app.Extension;
using Bachelor_app.Helper;
using Bachelor_app.Manager;
using Bachelor_app.Model;
using Bakalárska_práca.Enumerate;
using Bakalárska_práca.Extension;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Kitware.VTK;

namespace Bakalárska_práca.Manager
{
    /// <summary>
    /// Manager for displays in MainForm, which show things from EDisplayItem
    /// </summary>
    public class DisplayManager
    {
        public EDisplayItem LeftViewWindowItem { get; set; }
        public EDisplayItem RightViewWindowItem { get; set; }

        private FileManager _fileManager;
        private MainForm _winForm;
        private CameraManager _cameraManager;

        public DisplayManager(MainForm WinForm, FileManager FileManager, CameraManager CameraManager)
        {
            this._winForm = WinForm;
            this._fileManager = FileManager;
            this._cameraManager = CameraManager;
        }

        /// <summary>
        /// Display focused item in ListView.
        /// </summary>
        /// <param name="item">Item which should be focused.</param>
        public void DisplayImageFromListView(ListViewItem item)
        {
            if (item.Focused)
            {
                var listOfInputFile = _fileManager.listViewerModel.ListOfListInputFolder[(int)_fileManager.ListViewerDisplay];
                _fileManager.listViewerModel._lastImage = new Image<Bgr, byte>((Bitmap)listOfInputFile.FirstOrDefault(x => x.fileInfo.Name == item.Text).image);

                Display();
            }
        }

        /// <summary>
        /// Idle method for application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Display(object sender, EventArgs e)
        {
            Display();
        }

        /// <summary>
        /// Display item on EmguCV ImageBox
        /// </summary>
        public void Display()
        {
            ShowItemOnView(_winForm.LeftViewBox, LeftViewWindowItem);
            ShowItemOnView(_winForm.RightViewBox, RightViewWindowItem);
        }

        /// <summary>
        /// Show image in EmguCV ImageBox
        /// </summary>
        /// <param name="imageBox">Which ImageBox</param>
        /// <param name="typeOfItem">Which type if image</param>
        public void ShowItemOnView(ImageBox imageBox, EDisplayItem typeOfItem)
        {
            switch (typeOfItem)
            {
                case EDisplayItem.DepthMap:
                    imageBox.Image = _fileManager.listViewerModel._lastDepthMapImage;
                    break;
                case EDisplayItem.LeftCamera:
                    imageBox.Image = _cameraManager.LeftCamera.camera
                        .GetImageInMat()
                        .Image2ImageBGR();
                    break;
                case EDisplayItem.RightCamera:
                    imageBox.Image = _cameraManager.RightCamera.camera
                        .GetImageInMat()
                        .Image2ImageBGR();
                    break;
                case EDisplayItem.Stack:
                    imageBox.Image = _fileManager.listViewerModel._lastImage;
                    break;
                case EDisplayItem.KeyPoints:
                    imageBox.Image = _fileManager.listViewerModel._lastDrawnKeypoint;
                    break;
                case EDisplayItem.DescriptorsMatches:
                    imageBox.Image = _fileManager.listViewerModel._lastDrawnMatches;
                    break;
                default: throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Display item on VTK window renderer
        /// </summary>
        /// <param name="LeftViewWindow">Show on left VTK window?</param>
        public void DisplayPointCloud(bool LeftViewWindow)
        {
            if (LeftViewWindow)
                ShowItemOnView(_winForm.renderWindowControl1, LeftViewWindowItem);
            else
                ShowItemOnView(_winForm.renderWindowControl2, RightViewWindowItem);
        }

        /// <summary>
        /// Show point cloud in application
        /// </summary>
        /// <param name="renderWindow">Which VTK window renderer</param>
        /// <param name="typeOfItem">Type of point cloud </param>
        public void ShowItemOnView(RenderWindowControl renderWindow, EDisplayItem typeOfItem)
        {
            switch (typeOfItem)
            {
                case EDisplayItem.SfMPointCloud: DisplayPointCloudNVM(renderWindow); break;
                case EDisplayItem.DepthMapPointCloud: throw new NotImplementedException();
                default: throw new NotImplementedException();
            }
        }

        public void DisplayPointCloudNVM(RenderWindowControl renderWindowControl)
        {
            var nvmFile = SfMHelper.LoadPointCloud();
            foreach (var model in nvmFile)
            {
                foreach (var camera in model.listImageModel)
                    ReadCameraIntoObject(renderWindowControl, camera);

                ReadPointIntoObject(renderWindowControl, model.listPointModel);
            }
        }

        #region VTK loading methods
        public void ReadCameraIntoObject(RenderWindowControl renderWindowControl, nvmCameraModel camera)
        {
            vtkRenderWindow renderWindow = renderWindowControl.RenderWindow;
            vtkRenderer renderer = renderWindow.GetRenderers().GetFirstRenderer();

            string filePath = Path.Combine(Configuration.TempDirectoryPath, $"{camera.fileName}");
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
        #endregion
    }
}
