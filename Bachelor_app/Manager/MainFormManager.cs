using System;
using System.Windows.Forms;
using Bachelor_app.Enumerate;
using Bachelor_app.Extension;
using Bachelor_app.StereoVision;
using Bachelor_app.StructureFromMotion;

namespace Bachelor_app.Manager
{
    /// <summary>
    /// Controller of MainForm components.
    /// </summary>
    public class MainFormManager
    {
        private const float ShowWindowSize = 50F;
        private const float HideWindowSize = 0F;

        private MainForm winForm;
        private DisplayManager displayManager;
        private FileManager fileManager;
        private StereoVisionManager stereoVisionManager;
        private SfM sfmManager;
        private CameraManager cameraManager;

        private int leftCamera = -1;
        private int rightCamera = -1;

        public MainFormManager(MainForm winForm, DisplayManager displayManager, FileManager fileManager, StereoVisionManager stereoVisionManager, SfM sfmManager, CameraManager cameraManager)
        {
            this.winForm = winForm;
            this.displayManager = displayManager;
            this.fileManager = fileManager;
            this.stereoVisionManager = stereoVisionManager;
            this.sfmManager = sfmManager;
            this.cameraManager = cameraManager;
        }

        #region Display views in WinForm
        public void SetDisplaySetting(object sender, EventArgs e, bool leftWindow)
        {
            var currentItem = sender as ToolStripComboBox;
            var enumItem = EnumExtension.ReturnEnumValue<EDisplayItem>(currentItem.SelectedItem.ToString());

            if (leftWindow)
                displayManager.LeftViewWindowItem = enumItem;
            else
                displayManager.RightViewWindowItem = enumItem;

            switch (enumItem)
            {
                case EDisplayItem.SfMPointCloud:
                case EDisplayItem.DepthMapPointCloud:
                    ChangeRenderer(leftWindow, true); break;
                default: ChangeRenderer(leftWindow, false); break;
            }
        }

        private void ChangeRenderer(bool leftWindow, bool pointCloud)
        {
            if (pointCloud)
            {
                ShowPointCloudRenderer(leftWindow);
            }
            else
            {
                ShowImageRenderer(leftWindow);
            }
        }

        private void ShowImageRenderer(bool leftWindow)
        {
            if (leftWindow)
            {
                winForm.tableLayoutPanel2.ColumnStyles[0].Width = ShowWindowSize;
                winForm.tableLayoutPanel2.ColumnStyles[1].Width = HideWindowSize;
            }
            else
            {
                winForm.tableLayoutPanel2.ColumnStyles[2].Width = ShowWindowSize;
                winForm.tableLayoutPanel2.ColumnStyles[3].Width = HideWindowSize;
            }

            displayManager.Display();
        }

        private void ShowPointCloudRenderer(bool leftWindow)
        {
            if (leftWindow)
            {
                winForm.tableLayoutPanel2.ColumnStyles[0].Width = HideWindowSize;
                winForm.tableLayoutPanel2.ColumnStyles[1].Width = ShowWindowSize;
            }
            else
            {
                winForm.tableLayoutPanel2.ColumnStyles[2].Width = HideWindowSize;
                winForm.tableLayoutPanel2.ColumnStyles[3].Width = ShowWindowSize;
            }

            displayManager.DisplayPointCloud(leftWindow);
        }
        #endregion

        #region StereoVisionManager
        public void SetStereoVisionTypeAlgorithm(object sender, EventArgs e)
        {
            var currentItem = sender as ToolStripComboBox;
            var enumItem = EnumExtension.ReturnEnumValue<EStereoCorrespondenceAlgorithm>(currentItem.SelectedItem.ToString());

            stereoVisionManager.SetStereoCorrespondenceAlgorithm(enumItem);
        }

        public void ShowStereoVisionSettings()
        {
            stereoVisionManager.ShowSettingForStereoSolver();
        }

        public void SetUsingParallelForStereoVision()
        {
            stereoVisionManager.UseParallel = winForm.toolStripButton14.Checked;
        }

        #endregion

        #region ListViewer
        public void AddToListView(object sender, EventArgs e)
        {
            switch (fileManager.ListViewerDisplay)
            {
                case EListViewGroup.BasicStack:
                case EListViewGroup.LeftCameraStack:
                case EListViewGroup.RightCameraStack:
                    fileManager.AddToListView(); break;
                default: break;
            }
        }

        public void SetListViewerDisplay(object sender, EventArgs e)
        {
            var currentItem = sender as ToolStripComboBox;
            fileManager.ListViewerDisplay = EnumExtension.ReturnEnumValue<EListViewGroup>(currentItem.SelectedItem.ToString());
            winForm.ListViews.ForEach(x => x.Visible = false);
            if ((int)fileManager.ListViewerDisplay < winForm.ListViews.Count)
                winForm.ListViews[(int)fileManager.ListViewerDisplay].Visible = true;
        }

        public void RemoveFromListView()
        {
            switch (fileManager.ListViewerDisplay)
            {
                case EListViewGroup.BasicStack:
                case EListViewGroup.LeftCameraStack:
                case EListViewGroup.RightCameraStack:
                    fileManager.RemoveSelectedFromListView(); break;
                default: break;
            }
        }

        public void RemoveAllFromListView()
        {
            switch (fileManager.ListViewerDisplay)
            {
                case EListViewGroup.BasicStack:
                case EListViewGroup.LeftCameraStack:
                case EListViewGroup.RightCameraStack:
                    fileManager.RemoveAllFromCurrentListView(); break;
                default: break;
            }
        }
        #endregion

        #region SfM
        public void StartSfM()
        {
            sfmManager.StartSFM();
        }

        public void SetFeatureDetector(object sender, EventArgs e)
        {
            var currentItem = sender as ToolStripComboBox;
            var enumItem = EnumExtension.ReturnEnumValue<EFeatureDetector>(currentItem.SelectedItem.ToString());

            sfmManager.Detector = enumItem.GetDetectorInstance();
        }

        public void SetFeatureDescriptor(object sender, EventArgs e)
        {
            var currentItem = sender as ToolStripComboBox;
            var enumItem = EnumExtension.ReturnEnumValue<EFeatureDescriptor>(currentItem.SelectedItem.ToString());

            sfmManager.Descriptor = enumItem.GetDescriptorInstance();
        }

        public void SetFeatureMatcher(object sender, EventArgs e)
        {
            var currentItem = sender as ToolStripComboBox;
            var enumItem = EnumExtension.ReturnEnumValue<EFeatureMatcher>(currentItem.SelectedItem.ToString());

            sfmManager.Matcher = enumItem.GetMatcherInstance();
        }

        public void ShowFeatureMatcherSettings(object sender, EventArgs e)
        {
            sfmManager.Matcher.ShowSettingForm();
        }

        public void ShowFeatureDescriptorSettings(object sender, EventArgs e)
        {
            sfmManager.Descriptor.ShowSettingForm();
        }

        public void ShowFeatureDetectorSettings(object sender, EventArgs e)
        {
            sfmManager.Detector.ShowSettingForm();
        }

        public void ResumeSFM()
        {
            sfmManager.StartSFM(true);
        }

        public void SetMatching(object sender, EventArgs e)
        {
            var currentItem = sender as ToolStripComboBox;
            var enumItem = EnumExtension.ReturnEnumValue<EMatchingType>(currentItem.SelectedItem.ToString());

            sfmManager.MatchingType = enumItem;
        }

        public void SetUsingParallel()
        {
            sfmManager.UseParallel = winForm.toolStripButton14.Checked;
        }

        #endregion

        #region FileManager
        public void SetInputType(object sender, EventArgs e)
        {
            var currentItem = sender as ToolStripComboBox;
            var enumItem = EnumExtension.ReturnEnumValue<EInput>(currentItem.SelectedItem.ToString());

            fileManager.InputType = enumItem;
        }
        #endregion

        #region CameraManager
        public void SetCamera(object sender, EventArgs e, bool isLeft)
        {
            var currentItem = sender as ToolStripComboBox;
            var index = currentItem.SelectedIndex != currentItem.Items.IndexOf("Empty") ? currentItem.SelectedIndex : -1;

            if (isLeft)
            {
                if (rightCamera != index)
                {
                    cameraManager.SetCamera(cameraManager.LeftCamera, index, currentItem.SelectedItem.ToString());
                    leftCamera = index;
                }
                else
                {
                    currentItem.SelectedItem = currentItem.Items[leftCamera == -1 ? currentItem.Items.IndexOf("Empty") : leftCamera];
                    if (index != -1)
                        MessageBox.Show("Can't set the same camera. It was set as right camera");
                }
            }
            else
            {
                if (leftCamera != index)
                {
                    cameraManager.SetCamera(cameraManager.RightCamera, index, currentItem.SelectedItem.ToString());
                    rightCamera = index;
                }
                else
                {
                    currentItem.SelectedItem = currentItem.Items[rightCamera == -1 ? currentItem.Items.IndexOf("Empty") : rightCamera];
                    if (index != -1)
                        MessageBox.Show("Can't set the same camera. It was set as left camera.");
                }
            }
        }

        public void SetResolution(object sender, EventArgs e)
        {
            var currentItem = sender as ToolStripComboBox;
            var enumItem = EnumExtension.ReturnEnumValue<ECameraResolution>(currentItem.SelectedItem.ToString());

            cameraManager.Resolution = enumItem;
            cameraManager.UpdateResolution();
        }

        #endregion
    }
}
