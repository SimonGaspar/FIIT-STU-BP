using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Bachelor_app.Enumerate;
using Bachelor_app.Model;

namespace Bachelor_app.Manager
{
    /// <summary>
    /// File management
    /// </summary>
    public class FileManager
    {

        public ListViewModel ListViewModel { get; private set; } = new ListViewModel();

        public EListViewGroup ListViewerDisplay { get; set; } = EListViewGroup.Console;
        public EInput _inputType { get; set; } = EInput.ListViewBasicStack;

        private MainForm _winForm;

        public FileManager(MainForm WinForm)
        {
            this._winForm = WinForm;
        }

        /// <summary>
        /// Show dialog to find image files and add to current stack in ListViewerDisplay
        /// </summary>
        public void AddToListView()
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Multiselect = true, ValidateNames = true, CheckFileExists = true, CheckPathExists = true, Filter = "" })
            {
                DialogHelper.AddFilterToDialog<EImageFormat>(ofd, "All Image Files");
                // NOT IMPLEMENTED
                // DELETE these, when not using.
                // DialogHelper.AddFilterToDialog<EVideoFormat>(ofd, "All Video Files");

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    foreach (var fileName in ofd.FileNames)
                    {
                        AddInputFileToList(fileName, ListViewerDisplay);
                    }
                };
            }
        }

        /// <summary>
        /// Remove checked files from current stack in ListViewerDisplay
        /// </summary>
        /// <param name="listViewToRemove">List with item to remove</param>
        public void RemoveFromListView(ListView listViewToRemove = null)
        {
            if (listViewToRemove == null)
                listViewToRemove = _winForm.ListViews[(int)ListViewerDisplay];

            foreach (ListViewItem fileName in listViewToRemove.SelectedItems)
            {
                if (ListViewModel.ListOfListInputFolder[(int)ListViewerDisplay].Find(x => x.FileName == fileName.Text).UseInSFM == true)
                    continue;

                ListViewModel.ListOfListInputFolder[(int)ListViewerDisplay].Remove(ListViewModel.ListOfListInputFolder[(int)ListViewerDisplay].Find(x => x.FileName == fileName.Text));

                var imageList = _winForm.ImageList[(int)ListViewerDisplay];
                imageList.Images.RemoveByKey(fileName.Text);

                var listViewer = _winForm.ListViews[(int)ListViewerDisplay];
                listViewer.Items.Remove(fileName);
            }
        }

        /// <summary>
        /// Remove all files from current stack in ListViewerDisplay
        /// </summary>
        public void RemoveAllFromListView()
        {
            var currentListView = _winForm.ListViews[(int)ListViewerDisplay];
            RemoveFromListView(currentListView);
        }

        /// <summary>
        /// Add files from dialog into ListView and InputFileModel
        /// </summary>
        /// <param name="inputFile">Image file to add</param>
        /// <param name="listOfInput">List in which add file</param>
        /// <param name="imageList">Image list in which add image from file</param>
        /// <param name="listView">ListView in which add item</param>
        public void AddInputFileToList(InputFileModel inputFile, List<InputFileModel> listOfInput, ImageList imageList, ListView listView)
        {
            if (listView.InvokeRequired)
                listView.Invoke((Action)delegate { AddInputFileToList(inputFile, listOfInput, imageList, listView); });
            else
            {
                imageList.Images.Add(inputFile.FileName, inputFile.Image);

                var listItem = new ListViewItem(inputFile.FileName, imageList.Images.IndexOfKey(inputFile.FileName))
                {
                    ImageKey = inputFile.FileName
                };

                listOfInput.Add(inputFile);
                listView.Items.Add(listItem);
            }
        }

        /// <summary>
        /// Add files from disk into ListView and InputFileModel
        /// </summary>
        /// <param name="PathToFile">Path to image file</param>
        /// <param name="type">Type of ListView gorup</param>
        public void AddInputFileToList(string PathToFile, EListViewGroup type)
        {
            var id = (int)type;
            var inputFileLeft = new InputFileModel(PathToFile);
            var imageList = _winForm.ImageList[id];
            var listViewer = _winForm.ListViews[id];
            AddInputFileToList(inputFileLeft, ListViewModel.ListOfListInputFolder[id], imageList, listViewer);
        }
    }
}
