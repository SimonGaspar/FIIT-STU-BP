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
                        var imageList = _winForm.ImageList[(int)ListViewerDisplay];
                        var listViewer = _winForm.ListViews[(int)ListViewerDisplay];
                        
                        AddInputFileToList(inputFile, listViewerModel.ListOfListInputFolder[(int)ListViewerDisplay], imageList, listViewer); break;
                    }
                };
            }
        }

        public void RemoveFromListView()
        {
            var currentListView = _winForm.ListViews[(int)ListViewerDisplay];

            foreach (ListViewItem fileName in currentListView.SelectedItems)
            {
                listViewerModel.ListOfListInputFolder[(int)ListViewerDisplay].Remove(listViewerModel.ListOfListInputFolder[(int)ListViewerDisplay].Find(x => x.fileInfo.Name == fileName.Text));

                var imageList = _winForm.ImageList[(int)ListViewerDisplay];
                imageList.Images.RemoveByKey(fileName.Text);

                var listViewer = _winForm.ListViews[(int)ListViewerDisplay];
                listViewer.Items.Remove(fileName);
            }
        }

        public void RemoveAllFromListView()
        {
            var currentListView = _winForm.ListViews[(int)ListViewerDisplay];

            foreach (ListViewItem fileName in currentListView.Items)
            {
                listViewerModel.ListOfListInputFolder[(int)ListViewerDisplay].Remove(listViewerModel.ListOfListInputFolder[(int)ListViewerDisplay].Find(x => x.fileInfo.Name == fileName.Text));

                var imageList = _winForm.ImageList[(int)ListViewerDisplay];
                imageList.Images.RemoveByKey(fileName.Text);

                var listViewer = _winForm.ListViews[(int)ListViewerDisplay];
                listViewer.Items.Remove(fileName);
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
