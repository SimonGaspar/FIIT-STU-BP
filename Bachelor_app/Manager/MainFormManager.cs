using System;
using System.Windows.Forms;
using Bachelor_app.Extension;
using Bakalárska_práca;
using Bakalárska_práca.Enumerate;
using Bakalárska_práca.Manager;
using Bakalárska_práca.StereoVision;
using Bakalárska_práca.StructureFromMotion;
using Bakalárska_práca.StructureFromMotion.FeatureDetectionDescription;
using Bakalárska_práca.StructureFromMotion.FeatureMatcher;

namespace Bachelor_app.Manager
{
    public class MainFormManager
    {
        private MainForm _winForm;
        private DisplayManager _displayManager;
        private FileManager _fileManager;
        private StereoVisionManager _stereoVisionManager;
        private SfM _sfmManager;

        public MainFormManager(MainForm WinForm, DisplayManager displayManager, FileManager fileManager, StereoVisionManager stereoVisionManager, SfM sfmManager)
        {
            this._winForm = WinForm;
            this._displayManager = displayManager;
            this._fileManager = fileManager;
            this._stereoVisionManager = stereoVisionManager;
            this._sfmManager = sfmManager;
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

            if (enumItem == EDisplayItem.PointCloud)
                ChangeRenderer(LeftWindow, true);
            else
                ChangeRenderer(LeftWindow, false);

            _displayManager.Display();
        }

        private void ChangeRenderer(bool LeftWindow, bool PointCloud)
        {
            if (PointCloud)
            {
                if (LeftWindow)
                {
                    _winForm.tableLayoutPanel2.ColumnStyles[0].Width = 0F;
                    _winForm.tableLayoutPanel2.ColumnStyles[1].Width = 50F;
                }
                else
                {
                    _winForm.tableLayoutPanel2.ColumnStyles[2].Width = 0F;
                    _winForm.tableLayoutPanel2.ColumnStyles[3].Width = 50F;
                }
            }
            else
            {
                if (LeftWindow)
                {
                    _winForm.tableLayoutPanel2.ColumnStyles[0].Width = 50F;
                    _winForm.tableLayoutPanel2.ColumnStyles[1].Width = 0F;
                }
                else
                {
                    _winForm.tableLayoutPanel2.ColumnStyles[2].Width = 50F;
                    _winForm.tableLayoutPanel2.ColumnStyles[3].Width = 0F;
                }
            }
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
            var enumItem = EnumExtension.ReturnEnumValue<EFeaturesDetector>(currentItem.SelectedItem.ToString());

            IFeatureDetector tempItem = null;

            switch (enumItem)
            {
                case EFeaturesDetector.ORB: tempItem = new OrientedFastAndRotatedBrief(); break;
            }

            _sfmManager._detector = tempItem;
        }

        public void SetFeatureDescriptor(object sender, EventArgs e)
        {
            var currentItem = sender as ToolStripComboBox;
            var enumItem = EnumExtension.ReturnEnumValue<EFeaturesDescriptor>(currentItem.SelectedItem.ToString());

            IFeatureDescriptor tempItem = null;

            switch (enumItem)
            {
                case EFeaturesDescriptor.ORB: tempItem = new OrientedFastAndRotatedBrief(); break;
            }

            _sfmManager._descriptor = tempItem;
        }

        public void SetFeatureMatcher(object sender, EventArgs e)
        {
            var currentItem = sender as ToolStripComboBox;
            var enumItem = EnumExtension.ReturnEnumValue<EFeaturesMatcher>(currentItem.SelectedItem.ToString());

            IFeatureMatcher tempItem = null;

            switch (enumItem)
            {
                case EFeaturesMatcher.BruteForce: tempItem = new BruteForce(); break;
            }

            _sfmManager._matcher = tempItem;
        }

        public void ShowFeatureMatcherSettings(object sender, EventArgs e)
        {
            _sfmManager._matcher.ShowSettingForm();
        }

        public void ShowFeatureDescriptorSettings(object sender, EventArgs e)
        {
            _sfmManager._descriptor.ShowSettingForm();
        }

        public void ShowFeatureDetectorSettings(object sender, EventArgs e)
        {
            _sfmManager._detector.ShowSettingForm();
        }
        #endregion
    }
}
