using Bakalárska_práca.Enumerate;
using Bakalárska_práca.Model;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bakalárska_práca.Manager
{
    public class FileManager
    {
        private Form1 _winForm;
        public List<InputFile> ListOfInputFile = new List<InputFile>();

        public FileManager()
        {
        }

        public FileManager(Form1 WinForm)
        {
            this._winForm = WinForm;
        }

        public void AddToListView()
        {
            using (OpenFileDialog ofd = new OpenFileDialog(){Multiselect = true, ValidateNames = true, CheckFileExists = true, CheckPathExists = true, Filter = ""})
            {
                DialogHelper.AddFilterToDialog<EImageFormat>(ofd,"All Image Files");
                DialogHelper.AddFilterToDialog<EVideoFormat>(ofd, "All Video Files");

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    //_winForm.ListViewer.View = View.Details;
                    foreach (var fileName in ofd.FileNames)
                    {
                        var inputFile = new InputFile(fileName);
                        ListOfInputFile.Add(new InputFile(fileName));
                        _winForm.ImageList.Images.Add(inputFile.fileInfo.Name, inputFile.image);
                        _winForm.ListViewer.Items.Add(inputFile.fileInfo.Name, inputFile.fileInfo.Name, _winForm.ImageList.Images.IndexOfKey(inputFile.fileInfo.Name));
                        _winForm.ListViewer.Items[inputFile.fileInfo.Name].ToolTipText = inputFile.fileInfo.Name;
                    }
                };
            }
        }

    }
}
