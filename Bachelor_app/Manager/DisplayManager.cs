using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Bachelor_app.Extension;
using Bachelor_app.Manager;
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
                case EDisplayItem.SfMPointCloud: _winForm.ReadNVM(renderWindow); break;
                case EDisplayItem.DepthMapPointCloud: throw new NotImplementedException();
                default: throw new NotImplementedException();
            }
        }
    }
}
