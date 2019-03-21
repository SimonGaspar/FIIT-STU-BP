using System.Collections.Generic;
using System.Windows.Forms;
using Bachelor_app.Model;
using Bakalárska_práca.Enumerate;
using Bakalárska_práca.Model;

namespace Bakalárska_práca.Manager
{
    public class FileManager
    {
        private MainForm _winForm;


        public ListViewerModel listViewerModel = new ListViewerModel();

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
                        var inputFile = new InputFileModel(fileName);
                        switch (ListViewerDisplay)
                        {
                            case EListViewGroup.BasicStack:
                                AddInputFileToList(inputFile, listViewerModel.BasicStack, _winForm.ImageList0, _winForm.ListViewer0); break;
                            case EListViewGroup.LeftCameraStack:
                                AddInputFileToList(inputFile, listViewerModel.LeftCameraStack, _winForm.ImageList1, _winForm.ListViewer1); break;
                            case EListViewGroup.RightCameraStack:
                                AddInputFileToList(inputFile, listViewerModel.RightCameraStack, _winForm.ImageList2, _winForm.ListViewer2); break;
                        }

                    }
                };
            }
        }

        private void AddInputFileToList(InputFileModel inputFile, List<InputFileModel> listOfInput, ImageList imageList, ListView listView)
        {
            imageList.Images.Add(inputFile.fileInfo.Name, inputFile.image);

            var listItem = new ListViewItem(inputFile.fileInfo.Name, imageList.Images.IndexOfKey(inputFile.fileInfo.Name))
            {
                ImageKey = inputFile.fileInfo.Name
            };

            listOfInput.Add(inputFile);

            listView.Items.Add(listItem);
        }
    }
}
