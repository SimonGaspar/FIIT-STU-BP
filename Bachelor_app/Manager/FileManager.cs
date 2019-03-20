using System.Collections.Generic;
using System.Windows.Forms;
using Bakalárska_práca.Enumerate;
using Bakalárska_práca.Model;

namespace Bakalárska_práca.Manager
{
    public class FileManager
    {
        private MainForm _winForm;
        public List<InputFile> ListOfInputFileForLeft = new List<InputFile>();
        public List<InputFile> ListOfInputFileForRight = new List<InputFile>();
        public List<InputFile> ListOfInputFile = new List<InputFile>();

        public EListViewGroup ListViewerDisplay { get; set; } = EListViewGroup.BasicStack;

        public FileManager()
        {
        }

        public FileManager(MainForm WinForm)
        {
            this._winForm = WinForm;
        }

        public void AddToListView()
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Multiselect = true, ValidateNames = true, CheckFileExists = true, CheckPathExists = true, Filter = "" })
            {
                DialogHelper.AddFilterToDialog<EImageFormat>(ofd, "All Image Files");
                DialogHelper.AddFilterToDialog<EVideoFormat>(ofd, "All Video Files");

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    foreach (var fileName in ofd.FileNames)
                    {
                        var inputFile = new InputFile(fileName);
                        switch (ListViewerDisplay)
                        {
                            case EListViewGroup.BasicStack:
                                AddInputFileToList(inputFile, ListOfInputFile); break;
                            case EListViewGroup.LeftCameraStack:
                                AddInputFileToList(inputFile, ListOfInputFileForLeft); break;
                            case EListViewGroup.RightCameraStack:
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
                Group = _winForm.ListViewer.Groups[(int)ListViewerDisplay],
                ImageKey = inputFile.fileInfo.Name
            };
            listItem.Group = _winForm.ListViewer.Groups[(int)ListViewerDisplay];

            listOfInput.Add(inputFile);

            _winForm.ListViewer.Items.Add(listItem);
        }
    }
}
