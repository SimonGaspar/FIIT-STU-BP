using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Bakalárska_práca.Enumerate;
using Bakalárska_práca.Model;
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

        public DisplayManager(MainForm WinForm, FileManager FileManager)
        {
            this._winForm = WinForm;
            this._fileManager = FileManager;
        }

        public void DisplayImageFromListView(ListViewItem item)
        {
            if (item.Focused)
            {
                List<InputFileModel> listOfInputFile = null;

                switch (_fileManager.ListViewerDisplay)
                {
                    case EListViewGroup.BasicStack: listOfInputFile = _fileManager.listViewerModel.BasicStack; break;
                    case EListViewGroup.LeftCameraStack: listOfInputFile = _fileManager.listViewerModel.LeftCameraStack; break;
                    case EListViewGroup.RightCameraStack: listOfInputFile = _fileManager.listViewerModel.RightCameraStack; break;
                    case EListViewGroup.DrawnKeyPoint: listOfInputFile = _fileManager.listViewerModel.DrawnKeypoint; break;
                    case EListViewGroup.DrawnMatches: listOfInputFile = _fileManager.listViewerModel.DrawnMatches; break;
                }
                _fileManager.listViewerModel._lastImage = new Image<Bgr, byte>((Bitmap)listOfInputFile.FirstOrDefault(x => x.fileInfo.Name == item.Text).image);

                Display();
            }
        }

        public void Display()
        {
            ShowItemOnView(_winForm.LeftViewBox, LeftViewWindowItem);
            ShowItemOnView(_winForm.renderWindowControl1, LeftViewWindowItem);
            ShowItemOnView(_winForm.renderWindowControl2, RightViewWindowItem);
            ShowItemOnView(_winForm.RightViewBox, RightViewWindowItem);
        }

        public void ShowItemOnView(ImageBox imageBox, EDisplayItem typeOfItem)
        {
            switch (typeOfItem)
            {
                case EDisplayItem.DepthMap: imageBox.Image = _fileManager.listViewerModel._lastDepthMapImage; break;
                case EDisplayItem.LeftCamera: break;
                case EDisplayItem.RightCamera: break;
                case EDisplayItem.Stack: imageBox.Image = _fileManager.listViewerModel._lastBasicStack; break;
            }
        }

        public void ShowItemOnView(RenderWindowControl renderWindow, EDisplayItem typeOfItem)
        {
            switch (typeOfItem)
            {
                //case EDisplayItem.PointCloud: _winForm.ReadPLY(renderWindow); break;
                case EDisplayItem.PointCloud: _winForm.ReadPlainText(renderWindow); break;
            }
        }
    }
}
