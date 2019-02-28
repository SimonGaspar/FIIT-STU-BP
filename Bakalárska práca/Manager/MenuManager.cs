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

        public MenuManager(Form1 WinForm,DisplayManager displayManager)
        {
            this._winForm = WinForm;
            this._displayManager = displayManager;
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
                SetDisplaySetting(currentItem);
            }
        }

        private void SetDisplaySetting(ToolStripMenuItem Item)
        {
            switch (Item.OwnerItem.Name.ToUpper())
            {
                case "DISPLAYLEFT": SetDisplayEnum(_displayManager.LeftViewItem, Item ); break;
                case "DISPLAYRIGHT": SetDisplayEnum(_displayManager.RightViewItem, Item); break;
            }
        }

        private void SetDisplayEnum(EDisplayItem display, ToolStripMenuItem Item)
        {
            switch (Item.Name.ToUpper())
            {
                case string leftCamera when leftCamera.Contains("LEFTCAMERA"): display = EDisplayItem.LeftCamera; break;
                case string rightCamera when rightCamera.Contains("RIGHTCAMERA"): display = EDisplayItem.RightCamera; break;
                case string listViewer when listViewer.Contains("LISTVIEWER"): display = EDisplayItem.ListView; break;
                case string depthMap when depthMap.Contains("DEPTHMAP"): display = EDisplayItem.DepthMap; break;
            }
            _displayManager.Display();
        }
    }
}
