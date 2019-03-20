using System;
using System.Windows.Forms;
using Bakalárska_práca.Enumerate;
using Bakalárska_práca.Helper;
using Bakalárska_práca.StereoVision;

namespace Bakalárska_práca.Manager
{
    public class MenuManager
    {
        private MainForm _winForm;
        private DisplayManager _displayManager;
        private FileManager _fileManager;
        private StereoVisionManager _stereoVisionManager;

        public MenuManager(MainForm WinForm, DisplayManager displayManager, FileManager fileManager, StereoVisionManager stereoVisionManager)
        {
            this._winForm = WinForm;
            this._displayManager = displayManager;
            this._fileManager = fileManager;
            this._stereoVisionManager = stereoVisionManager;
        }

        public void MenuSetListViewerSetting(object sender, EventArgs e)
        {
            var currentItem = sender as ToolStripMenuItem;
            MenuHelper.OnlyOneCheck(sender, e);
            SetListViewerSetting(currentItem);
        }

        private void SetListViewerSetting(ToolStripMenuItem currentItem)
        {
            EListViewGroup ListViewGroup = EListViewGroup.BasicStack;
            switch (currentItem.Name.ToUpper())
            {
                case string noGroup when noGroup.Contains("NO"): ListViewGroup = EListViewGroup.BasicStack; break;
                case string leftGroup when leftGroup.Contains("LEFT"): ListViewGroup = EListViewGroup.LeftCameraStack; break;
                case string rightGroup when rightGroup.Contains("RIGHT"): ListViewGroup = EListViewGroup.RightCameraStack; break;
            }
            _fileManager.ListViewerDisplay = ListViewGroup;
        }

        public void MenuSetDisplaySetting(object sender, EventArgs e)
        {
            var currentItem = sender as ToolStripMenuItem;
            MenuHelper.OnlyOneCheck(sender, e);
            ChangeRenderer(currentItem);
            SetDisplaySetting(currentItem);
        }

        private void ChangeRenderer(ToolStripMenuItem Item)
        {
            if (Item.Name.ToUpper().Contains("POINT"))
                switch (Item.OwnerItem.Name.ToUpper())
                {
                    case "DISPLAYLEFT": _winForm.tableLayoutPanel2.ColumnStyles[0].Width = 0F; _winForm.tableLayoutPanel2.ColumnStyles[1].Width = 50F; break;
                    case "DISPLAYRIGHT": _winForm.tableLayoutPanel2.ColumnStyles[2].Width = 0F; _winForm.tableLayoutPanel2.ColumnStyles[3].Width = 50F; break;
                }
            else
                switch (Item.OwnerItem.Name.ToUpper())
                {
                    case "DISPLAYLEFT": _winForm.tableLayoutPanel2.ColumnStyles[0].Width = 50F; _winForm.tableLayoutPanel2.ColumnStyles[1].Width = 0F; break;
                    case "DISPLAYRIGHT": _winForm.tableLayoutPanel2.ColumnStyles[2].Width = 50F; _winForm.tableLayoutPanel2.ColumnStyles[3].Width = 0F; break;
                }
        }

        private void SetDisplaySetting(ToolStripMenuItem Item)
        {
            switch (Item.OwnerItem.Name.ToUpper())
            {
                case "DISPLAYLEFT": _displayManager.LeftViewWindowItem = ReturnDisplayEnum(Item); break;
                case "DISPLAYRIGHT": _displayManager.RightViewWindowItem = ReturnDisplayEnum(Item); break;
            }
            _displayManager.Display();
        }

        private EDisplayItem ReturnDisplayEnum(ToolStripMenuItem Item)
        {
            EDisplayItem DisplayEnumToReturn = EDisplayItem.ListViewer;
            switch (Item.Name.ToUpper())
            {
                case string leftCamera when leftCamera.Contains("LEFTCAMERA"): DisplayEnumToReturn = EDisplayItem.LeftCamera; break;
                case string rightCamera when rightCamera.Contains("RIGHTCAMERA"): DisplayEnumToReturn = EDisplayItem.RightCamera; break;
                case string listViewer when listViewer.Contains("LISTVIEWER"): DisplayEnumToReturn = EDisplayItem.ListViewer; break;
                case string depthMap when depthMap.Contains("DEPTHMAP"): DisplayEnumToReturn = EDisplayItem.DepthMap; break;
                case string pointCloud when pointCloud.Contains("POINT"): DisplayEnumToReturn = EDisplayItem.PointCloud; break;
            }
            return DisplayEnumToReturn;
        }
    }
}
