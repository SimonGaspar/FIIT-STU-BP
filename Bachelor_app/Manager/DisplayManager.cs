using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Bachelor_app.Manager;
using Bakalárska_práca.Enumerate;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Kitware.VTK;

namespace Bakalárska_práca.Manager
{
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

        public void DisplayImageFromListView(ListViewItem item)
        {
            if (item.Focused)
            {
                var listOfInputFile = _fileManager.listViewerModel.ListOfListInputFolder[(int)_fileManager.ListViewerDisplay];
                _fileManager.listViewerModel._lastImage = new Image<Bgr, byte>((Bitmap)listOfInputFile.FirstOrDefault(x => x.fileInfo.Name == item.Text).image);

                Display();
            }
        }


        public void Display(object sender, EventArgs e)
        {
            Display();
        }
        public void Display()
        {
            ShowItemOnView(_winForm.LeftViewBox, LeftViewWindowItem);
            ShowItemOnView(_winForm.RightViewBox, RightViewWindowItem);
        }

        public void ShowItemOnView(ImageBox imageBox, EDisplayItem typeOfItem)
        {
            switch (typeOfItem)
            {
                case EDisplayItem.DepthMap: imageBox.Image = _fileManager.listViewerModel._lastDepthMapImage; break;
                case EDisplayItem.LeftCamera:
                    Mat inputLeft = new Mat();
                    _cameraManager.LeftCamera.camera.Grab();
                    _cameraManager.LeftCamera.camera.Retrieve(inputLeft);
                    imageBox.Image = new Image<Bgr,byte>(inputLeft.Bitmap);
                    break;
                case EDisplayItem.RightCamera:
                    Mat inputRight = new Mat();
                    _cameraManager.RightCamera.camera.Grab();
                    _cameraManager.RightCamera.camera.Retrieve(inputRight);
                    imageBox.Image = new Image<Bgr, byte>(inputRight.Bitmap);
                    break;
                case EDisplayItem.Stack: imageBox.Image = _fileManager.listViewerModel._lastImage; break;
                case EDisplayItem.KeyPoints: imageBox.Image = _fileManager.listViewerModel._lastDrawnKeypoint; break;
                case EDisplayItem.DescriptorsMatches: imageBox.Image = _fileManager.listViewerModel._lastDrawnMatches; break;
            }
        }

        public void DisplayPointCloud(bool LeftViewWindow) {
            if(LeftViewWindow)
            ShowItemOnView(_winForm.renderWindowControl1, LeftViewWindowItem);
            else
            ShowItemOnView(_winForm.renderWindowControl2, RightViewWindowItem);
        }

        public void ShowItemOnView(RenderWindowControl renderWindow, EDisplayItem typeOfItem)
        {
            switch (typeOfItem)
            {
                case EDisplayItem.PointCloud: _winForm.ReadNVM(renderWindow); break;
            }
        }
    }
}
