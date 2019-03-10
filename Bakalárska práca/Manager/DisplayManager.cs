using Bakalárska_práca.Enumerate;
using Bakalárska_práca.Model;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Kitware.VTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bakalárska_práca.Manager
{
    public class DisplayManager
    {
        public EDisplayItem LeftViewItem { get; set; }
        public EDisplayItem RightViewItem { get; set; }

        private FileManager _fileManager;
        private Form1 _winForm;
        private Image<Bgr, Byte> _lastListViewerImage;
        public Image<Bgr, Byte> _lastDepthMapImage { get; set; }

        public DisplayManager(Form1 WinForm, FileManager FileManager)
        {
            this._winForm = WinForm;
            this._fileManager = FileManager;
        }

        public void DisplayImageFromListView(ListViewItem item)
        {
            if (item.Focused)
            {
                List<InputFile> listOfInputFile = null;
                switch (item.Group.Name.ToUpper())
                {
                    case string noGroup when noGroup.Contains("NO"): listOfInputFile = _fileManager.ListOfInputFile; break;
                    case string rightGroup when rightGroup.Contains("RIGHT"): listOfInputFile = _fileManager.ListOfInputFileForRight; break;
                    case string leftGroup when leftGroup.Contains("LEFT"): listOfInputFile = _fileManager.ListOfInputFileForLeft; break;
                }
                _lastListViewerImage = new Image<Bgr, byte>((Bitmap)listOfInputFile.FirstOrDefault(x => x.fileInfo.Name == item.Text).image);

                Display();
            }
        }
        
        public void Display()
        {
            ShowItemOnView(_winForm.LeftViewBox, LeftViewItem);
            ShowItemOnView(_winForm.renderWindowControl1, LeftViewItem);
            ShowItemOnView(_winForm.renderWindowControl2, RightViewItem);
            ShowItemOnView(_winForm.RightViewBox, RightViewItem);
        }

        public void ShowItemOnView(ImageBox imageBox, EDisplayItem typeOfItem)
        {
            switch (typeOfItem)
            {
                case EDisplayItem.DepthMap: imageBox.Image = _lastDepthMapImage; break;
                case EDisplayItem.LeftCamera: break;
                case EDisplayItem.RightCamera: break;
                case EDisplayItem.ListView: imageBox.Image = _lastListViewerImage; break;
            }
        }

        public void ShowItemOnView(RenderWindowControl renderWindow, EDisplayItem typeOfItem)
        {
            switch (typeOfItem)
            {
                case EDisplayItem.PointCloud: _winForm.ReadPLY(renderWindow); break;
            }
        }
    }
}
