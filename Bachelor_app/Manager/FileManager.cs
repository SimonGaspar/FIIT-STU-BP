﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Bachelor_app.Enumerate;
using Bachelor_app.Manager;
using Bachelor_app.Model;
using Bakalárska_práca.Enumerate;
using Bakalárska_práca.Model;

namespace Bakalárska_práca.Manager
{
    /// <summary>
    /// File management
    /// </summary>
    public class FileManager
    {
        private MainForm _winForm;
        private CameraManager _cameraManager;

        public ListViewerModel listViewerModel = new ListViewerModel();

        public EListViewGroup ListViewerDisplay { get; set; } = EListViewGroup.BasicStack;
        public EInput _inputType;

        public FileManager(MainForm WinForm, CameraManager CameraManager)
        {
            this._winForm = WinForm;
            this._cameraManager = CameraManager;
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
                        var inputFile = new InputFileModel(fileName);
                        var imageList = _winForm.ImageList[(int)ListViewerDisplay];
                        var listViewer = _winForm.ListViews[(int)ListViewerDisplay];

                        AddInputFileToList(inputFile, listViewerModel.ListOfListInputFolder[(int)ListViewerDisplay], imageList, listViewer);
                    }
                };
            }
        }

        /// <summary>
        /// Remove checked files from current stack in ListViewerDisplay
        /// </summary>
        public void RemoveFromListView()
        {
            var currentListView = _winForm.ListViews[(int)ListViewerDisplay];

            foreach (ListViewItem fileName in currentListView.SelectedItems)
            {
                if (listViewerModel.ListOfListInputFolder[(int)ListViewerDisplay].Find(x => x.fileInfo.Name == fileName.Text).UseInSFM == true)
                    continue;

                listViewerModel.ListOfListInputFolder[(int)ListViewerDisplay].Remove(listViewerModel.ListOfListInputFolder[(int)ListViewerDisplay].Find(x => x.fileInfo.Name == fileName.Text));

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

            foreach (ListViewItem fileName in currentListView.Items)
            {
                listViewerModel.ListOfListInputFolder[(int)ListViewerDisplay].Remove(listViewerModel.ListOfListInputFolder[(int)ListViewerDisplay].Find(x => x.fileInfo.Name == fileName.Text));

                var imageList = _winForm.ImageList[(int)ListViewerDisplay];
                imageList.Images.RemoveByKey(fileName.Text);

                var listViewer = _winForm.ListViews[(int)ListViewerDisplay];
                listViewer.Items.Remove(fileName);
            }
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
}
