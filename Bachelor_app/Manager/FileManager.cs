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

        public EInput InputType { get; set; } = EInput.ListViewBasicStack;

        private MainForm winForm;

        public FileManager(MainForm winForm)
        {
            this.winForm = winForm;
        }

        /// <summary>
        /// Show dialog to find image files and add to current stack in ListViewerDisplay
        /// </summary>
        public void AddToListView()
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Multiselect = true, ValidateNames = true, CheckFileExists = true, CheckPathExists = true, Filter = string.Empty })
            {
                DialogHelper.AddFilterToDialog<EImageFormat>(ofd, "All Image Files");

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    foreach (var fileName in ofd.FileNames)
                    {
                        AddInputFileToList(fileName, ListViewerDisplay);
                    }
                }
            }
        }

        /// <summary>
        /// Remove checked files from current stack in ListViewerDisplay
        /// </summary>
        /// <param name="listViewToRemove">List with item to remove</param>
        public void RemoveFromListView(ListView listViewToRemove = null)
        {
            if (listViewToRemove == null)
                listViewToRemove = winForm.ListViews[(int)ListViewerDisplay];

            foreach (ListViewItem fileName in listViewToRemove.Items)
            {
                ListViewModel.ListOfListInputFolder[(int)ListViewerDisplay].Remove(ListViewModel.ListOfListInputFolder[(int)ListViewerDisplay].Find(x => x.FileName == fileName.Text));

                var imageList = winForm.ImageList[(int)ListViewerDisplay];
                imageList.Images.RemoveByKey(fileName.Text);

                var listViewer = winForm.ListViews[(int)ListViewerDisplay];
                listViewer.Items.Remove(fileName);
            }
        }

        /// <summary>
        /// Remove checked files from current stack in ListViewerDisplay
        /// </summary>
        /// <param name="listViewToRemove">List with item to remove</param>
        public void RemoveSelectedFromListView(ListView listViewToRemove = null)
        {
            if (listViewToRemove == null)
                listViewToRemove = winForm.ListViews[(int)ListViewerDisplay];

            foreach (ListViewItem fileName in listViewToRemove.SelectedItems)
            {
                if (ListViewModel.ListOfListInputFolder[(int)ListViewerDisplay].Find(x => x.FileName == fileName.Text).UseInSFM == true)
                    continue;

                ListViewModel.ListOfListInputFolder[(int)ListViewerDisplay].Remove(ListViewModel.ListOfListInputFolder[(int)ListViewerDisplay].Find(x => x.FileName == fileName.Text));

                var imageList = winForm.ImageList[(int)ListViewerDisplay];
                imageList.Images.RemoveByKey(fileName.Text);

                var listViewer = winForm.ListViews[(int)ListViewerDisplay];
                listViewer.Items.Remove(fileName);
            }
        }

        /// <summary>
        /// Remove all files from current stack in ListViewerDisplay
        /// </summary>
        public void RemoveAllFromCurrentListView()
        {
            var currentListView = winForm.ListViews[(int)ListViewerDisplay];
            RemoveSelectedFromListView(currentListView);
        }

        /// <summary>
        /// Remove all files from all ListViewerDisplay
        /// </summary>
        public void RemoveAllFromListViews()
        {
            foreach (var item in winForm.ListViews)
                RemoveFromListView(item);
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
        /// <param name="pathToFile">Path to image file</param>
        /// <param name="type">Type of ListView gorup</param>
        public void AddInputFileToList(string pathToFile, EListViewGroup type)
        {
            var id = (int)type;
            var inputFileLeft = new InputFileModel(pathToFile);
            var imageList = winForm.ImageList[id];
            var listViewer = winForm.ListViews[id];
            AddInputFileToList(inputFileLeft, ListViewModel.ListOfListInputFolder[id], imageList, listViewer);
        }
    }
}
