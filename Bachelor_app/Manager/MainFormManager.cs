using System;
using System.Windows.Forms;
using Bachelor_app.Extension;
using Bakalárska_práca;
using Bakalárska_práca.Enumerate;
using Bakalárska_práca.Manager;
using Bakalárska_práca.StereoVision;

namespace Bachelor_app.Manager
{
    public class MainFormManager
    {
        private MainForm _winForm;
        private DisplayManager _displayManager;
        private FileManager _fileManager;
        private StereoVisionManager _stereoVisionManager;

        public MainFormManager(MainForm WinForm, DisplayManager displayManager, FileManager fileManager, StereoVisionManager stereoVisionManager)
        {
            this._winForm = WinForm;
            this._displayManager = displayManager;
            this._fileManager = fileManager;
            this._stereoVisionManager = stereoVisionManager;
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
        public void SetListViewerDisplay(object sender, EventArgs e)
        {
            var currentItem = sender as ToolStripComboBox;
            _fileManager.ListViewerDisplay = EnumExtension.ReturnEnumValue<EListViewGroup>(currentItem.SelectedItem.ToString());
        }
        #endregion
    }
}
