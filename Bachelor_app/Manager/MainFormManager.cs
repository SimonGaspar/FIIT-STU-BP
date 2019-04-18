using System;
using System.Windows.Forms;
using Bachelor_app.Enumerate;
using Bachelor_app.Extension;
using Bachelor_app.StereoVision;
using Bachelor_app.StructureFromMotion;

namespace Bachelor_app.Manager
{
    public class MainFormManager
    {
        private MainForm _winForm;
        private DisplayManager _displayManager;
        private FileManager _fileManager;
        private StereoVisionManager _stereoVisionManager;
        private SfM _sfmManager;
        private CameraManager _cameraManager;

        private const float ShowWindowSize = 50F;
        private const float HideWindowSize = 0F;

        public MainFormManager(MainForm WinForm, DisplayManager displayManager, FileManager fileManager, StereoVisionManager stereoVisionManager, SfM sfmManager, CameraManager cameraManager)
        {
            this._winForm = WinForm;
            this._displayManager = displayManager;
            this._fileManager = fileManager;
            this._stereoVisionManager = stereoVisionManager;
            this._sfmManager = sfmManager;
            this._cameraManager = cameraManager;
        }

        #region Display views in WinForm
        public void SetDisplaySetting(object sender, EventArgs e, bool LeftWindow)
        {
            var currentItem = sender as ToolStripComboBox;
            var enumItem = EnumExtension.ReturnEnumValue<EDisplayItem>(currentItem.SelectedItem.ToString());

            if (LeftWindow)
                _displayManager.LeftViewWindowItem = enumItem;
            else
                _displayManager.RightViewWindowItem = enumItem;

            if (enumItem == EDisplayItem.SfMPointCloud)
                ChangeRenderer(LeftWindow, true);
            else
                ChangeRenderer(LeftWindow, false);
        }

        private void ChangeRenderer(bool LeftWindow, bool PointCloud)
        {
            if (PointCloud)
            {
                ShowPointCloudRenderer(LeftWindow);
            }
            else
            {
                ShowImageRenderer(LeftWindow);
            }
        }

        private void ShowImageRenderer(bool LeftWindow)
        {
            if (LeftWindow)
            {
                _winForm.tableLayoutPanel2.ColumnStyles[0].Width = ShowWindowSize;
                _winForm.tableLayoutPanel2.ColumnStyles[1].Width = HideWindowSize;
            }
            else
            {
                _winForm.tableLayoutPanel2.ColumnStyles[2].Width = ShowWindowSize;
                _winForm.tableLayoutPanel2.ColumnStyles[3].Width = HideWindowSize;
            }
            _displayManager.Display();
        }

        private void ShowPointCloudRenderer(bool LeftWindow)
        {
            if (LeftWindow)
            {
                _winForm.tableLayoutPanel2.ColumnStyles[0].Width = HideWindowSize;
                _winForm.tableLayoutPanel2.ColumnStyles[1].Width = ShowWindowSize;
            }
            else
            {
                _winForm.tableLayoutPanel2.ColumnStyles[2].Width = HideWindowSize;
                _winForm.tableLayoutPanel2.ColumnStyles[3].Width = ShowWindowSize;
            }
            _displayManager.DisplayPointCloud(LeftWindow);
        }
        #endregion

        #region StereoVisionManager
        public void SetStereoVisionTypeAlgorithm(object sender, EventArgs e)
        {
            var currentItem = sender as ToolStripComboBox;
            var enumItem = EnumExtension.ReturnEnumValue<EStereoCorrespondenceAlgorithm>(currentItem.SelectedItem.ToString());

            _stereoVisionManager.SetStereoCorrespondenceAlgorithm(enumItem);
        }

        public void ShowStereoVisionSettings()
        {
            _stereoVisionManager.ShowSettingForStereoSolver();
        }


        public void SetUsingParallelForStereoVision()
        {
            _stereoVisionManager.UseParallel = _winForm.toolStripButton14.Checked;
        }

        #endregion

        #region ListViewer
        public void AddToListView(object sender, EventArgs e)
        {
            switch (_fileManager.ListViewerDisplay)
            {
                case EListViewGroup.BasicStack:
                case EListViewGroup.LeftCameraStack:
                case EListViewGroup.RightCameraStack:
                    _fileManager.AddToListView(); break;
                default: break;
            }
        }

        public void SetListViewerDisplay(object sender, EventArgs e)
        {
            var currentItem = sender as ToolStripComboBox;
            _fileManager.ListViewerDisplay = EnumExtension.ReturnEnumValue<EListViewGroup>(currentItem.SelectedItem.ToString());
            _winForm.ListViews.ForEach(x => x.Visible = false);
            if ((int)_fileManager.ListViewerDisplay < _winForm.ListViews.Count)
                _winForm.ListViews[(int)_fileManager.ListViewerDisplay].Visible = true;
        }

        public void RemoveFromListView()
        {
            switch (_fileManager.ListViewerDisplay)
            {
                case EListViewGroup.BasicStack:
                case EListViewGroup.LeftCameraStack:
                case EListViewGroup.RightCameraStack:
                    _fileManager.RemoveFromListView(); break;
                default: break;
            }
        }

        public void RemoveAllFromListView()
        {
            switch (_fileManager.ListViewerDisplay)
            {
                case EListViewGroup.BasicStack:
                case EListViewGroup.LeftCameraStack:
                case EListViewGroup.RightCameraStack:
                    _fileManager.RemoveAllFromListView(); break;
                default: break;
            }
        }
        #endregion

        #region SfM
        public void StartSfM()
        {
            _sfmManager.StartSFM();
        }

        public void SetFeatureDetector(object sender, EventArgs e)
        {
            var currentItem = sender as ToolStripComboBox;
            var enumItem = EnumExtension.ReturnEnumValue<EFeatureDetector>(currentItem.SelectedItem.ToString());

            _sfmManager.Detector = enumItem.GetDetectorInstance();
        }

        public void SetFeatureDescriptor(object sender, EventArgs e)
        {
            var currentItem = sender as ToolStripComboBox;
            var enumItem = EnumExtension.ReturnEnumValue<EFeatureDescriptor>(currentItem.SelectedItem.ToString());

            _sfmManager.Descriptor = enumItem.GetDescriptorInstance();
        }

        public void SetFeatureMatcher(object sender, EventArgs e)
        {
            var currentItem = sender as ToolStripComboBox;
            var enumItem = EnumExtension.ReturnEnumValue<EFeatureMatcher>(currentItem.SelectedItem.ToString());

            _sfmManager.Matcher = enumItem.GetMatcherInstance();
        }

        public void ShowFeatureMatcherSettings(object sender, EventArgs e)
        {
            _sfmManager.Matcher.ShowSettingForm();
        }

        public void ShowFeatureDescriptorSettings(object sender, EventArgs e)
        {
            _sfmManager.Descriptor.ShowSettingForm();
        }

        public void ShowFeatureDetectorSettings(object sender, EventArgs e)
        {
            _sfmManager.Detector.ShowSettingForm();
        }

        public void ResumeSFM()
        {
            _sfmManager.StartSFM(true);
        }

        public void SetMatching(object sender, EventArgs e)
        {
            var currentItem = sender as ToolStripComboBox;
            var enumItem = EnumExtension.ReturnEnumValue<EMatchingType>(currentItem.SelectedItem.ToString());

            _sfmManager.MatchingType = enumItem;
        }

        public void SetUsingParallel()
        {
            _sfmManager.UseParallel = _winForm.toolStripButton14.Checked;
        }

        #endregion

        #region FileManager 
        public void SetInputType(object sender, EventArgs e)
        {
            var currentItem = sender as ToolStripComboBox;
            var enumItem = EnumExtension.ReturnEnumValue<EInput>(currentItem.SelectedItem.ToString());

            _fileManager._inputType = enumItem;
        }
        #endregion

        #region CameraManager
        public void SetCamera(object sender, EventArgs e, bool IsLeft)
        {
            var currentItem = sender as ToolStripComboBox;
            if (IsLeft)
                _cameraManager.SetCamera(_cameraManager.LeftCamera, currentItem.SelectedIndex, currentItem.SelectedItem.ToString());
            else
                _cameraManager.SetCamera(_cameraManager.RightCamera, currentItem.SelectedIndex, currentItem.SelectedItem.ToString());
        }

        public void SetResolution(object sender, EventArgs e)
        {
            var currentItem = sender as ToolStripComboBox;
            var enumItem = EnumExtension.ReturnEnumValue<ECameraResolution>(currentItem.SelectedItem.ToString());

            _cameraManager.resolution = enumItem;
            _cameraManager.UpdateResolution();
        }

        #endregion
    }
}
