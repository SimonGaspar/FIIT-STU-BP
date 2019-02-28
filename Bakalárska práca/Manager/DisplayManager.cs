using Bakalárska_práca.Enumerate;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
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

        public DisplayManager(Form1 WinForm, FileManager FileManager)
        {
            this._winForm = WinForm;
            this._fileManager = FileManager;
        }

        public void DisplayImageFromListView(ListViewItem item)
        {
            if (item.Focused)
            {
                _lastListViewerImage = new Image<Bgr, byte>((Bitmap)_fileManager.ListOfInputFile.FirstOrDefault(x => x.fileInfo.Name == item.Name).image);

                Display();
            }
        }
        
        public void Display()
        {
            ShowItemOnView(_winForm.LeftViewBox, LeftViewItem);
            ShowItemOnView(_winForm.RightViewBox, RightViewItem);
        }

        public void ShowItemOnView(ImageBox imageBox, EDisplayItem typeOfItem)
        {
            switch (typeOfItem)
            {
                case EDisplayItem.DepthMap:break;
                case EDisplayItem.LeftCamera: break;
                case EDisplayItem.RightCamera: break;
                case EDisplayItem.ListView: imageBox.Image = _lastListViewerImage; break;
            }
        }
    }
}
