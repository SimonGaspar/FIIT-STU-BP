using Bakalárska_práca.Enumerate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bakalárska_práca.Manager
{
    public class MenuManager
    {
        private Form1 _winForm;
        private DisplayManager _displayManager;
        private FileManager _fileManager;

        public MenuManager(Form1 WinForm,DisplayManager displayManager, FileManager fileManager)
        {
            this._winForm = WinForm;
            this._displayManager = displayManager;
            this._fileManager = fileManager;
        }

        public void MenuSetListViewerSetting(object sender, EventArgs e)
        {
            var currentItem = sender as ToolStripMenuItem;
            OnlyOneCheck(sender, e);
            SetListViewerSetting(currentItem);
        }

        private void SetListViewerSetting(ToolStripMenuItem currentItem)
        {
            EListViewGroup ListViewGroup = EListViewGroup.NoGroup;
            switch (currentItem.Name.ToUpper())
            {
                case string noGroup when noGroup.Contains("NO"): ListViewGroup = EListViewGroup.NoGroup; break;
                case string leftGroup when leftGroup.Contains("LEFT"): ListViewGroup = EListViewGroup.LeftListGroup; break;
                case string rightGroup when rightGroup.Contains("RIGHT"): ListViewGroup = EListViewGroup.RightListGroup; break;
            }
            _fileManager.AddForListType = ListViewGroup;
        }

        public void MenuSetDisplaySetting(object sender, EventArgs e)
        {
            var currentItem = sender as ToolStripMenuItem;
            OnlyOneCheck(sender, e);
            SetDisplaySetting(currentItem);
        }

        public void OnlyOneCheck(object sender, EventArgs e)
        {
            var currentItem = sender as ToolStripMenuItem;
            if (currentItem != null)
            {
                ((ToolStripMenuItem)currentItem.OwnerItem).DropDownItems
                     .OfType<ToolStripMenuItem>().ToList()
                    .ForEach(item =>
                    {
                        item.Checked = false;
                    });

                currentItem.Checked = true;
            }
        }

        private void SetDisplaySetting(ToolStripMenuItem Item)
        {
            switch (Item.OwnerItem.Name.ToUpper())
            {
                case "DISPLAYLEFT": _displayManager.LeftViewItem = ReturnDisplayEnum(Item ); break;
                case "DISPLAYRIGHT": _displayManager.RightViewItem = ReturnDisplayEnum(Item); break;
            }
            _displayManager.Display();
        }

        private EDisplayItem ReturnDisplayEnum(ToolStripMenuItem Item)
        {
            EDisplayItem DisplayEnumToReturn = EDisplayItem.ListView;
            switch (Item.Name.ToUpper())
            {
                case string leftCamera when leftCamera.Contains("LEFTCAMERA"): DisplayEnumToReturn = EDisplayItem.LeftCamera; break;
                case string rightCamera when rightCamera.Contains("RIGHTCAMERA"): DisplayEnumToReturn = EDisplayItem.RightCamera; break;
                case string listViewer when listViewer.Contains("LISTVIEWER"): DisplayEnumToReturn = EDisplayItem.ListView; break;
                case string depthMap when depthMap.Contains("DEPTHMAP"): DisplayEnumToReturn = EDisplayItem.DepthMap; break;
            }
            return DisplayEnumToReturn;
        }
    }
}
