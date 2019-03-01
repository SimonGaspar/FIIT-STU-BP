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
        public List<InputFile> ListOfInputFileForLeft = new List<InputFile>();
        public List<InputFile> ListOfInputFileForRight = new List<InputFile>();
        public List<InputFile> ListOfInputFile = new List<InputFile>();

        public EListViewGroup AddForListType { get; set; } = EListViewGroup.NoGroup;

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
                    foreach (var fileName in ofd.FileNames)
                    {
                        var inputFile = new InputFile(fileName);
                        switch (AddForListType)
                        {
                            case EListViewGroup.NoGroup:
                                AddInputFileToList(inputFile,ListOfInputFile); break;
                            case EListViewGroup.LeftListGroup:
                                AddInputFileToList(inputFile, ListOfInputFileForLeft); break;
                            case EListViewGroup.RightListGroup:
                                AddInputFileToList(inputFile, ListOfInputFileForRight); break;
                        }
                        
                    }
                };
            }
        }

        private void AddInputFileToList(InputFile inputFile, List<InputFile> listOfInput)
        {
            _winForm.ImageList.Images.Add(inputFile.fileInfo.Name, inputFile.image);

            var listItem = new ListViewItem(inputFile.fileInfo.Name, _winForm.ImageList.Images.IndexOfKey(inputFile.fileInfo.Name))
            {
                Group = _winForm.ListViewer.Groups[(int)AddForListType],
                ImageKey = inputFile.fileInfo.Name
            };
            listItem.Group = _winForm.ListViewer.Groups[(int)AddForListType];

            listOfInput.Add(inputFile);
            
            _winForm.ListViewer.Items.Add(listItem);
        }
    }
}
