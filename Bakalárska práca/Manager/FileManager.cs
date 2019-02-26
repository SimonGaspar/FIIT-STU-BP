using Bakalárska_práca.Enumerate;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bakalárska_práca.Manager
{
    public class FileManager
    {
        private Form1 _winForm;

        public FileManager()
        {
        }

        public FileManager(Form1 WinForm)
        {
            this._winForm = WinForm;
        }

        public void AddToListView()
        {
            using (OpenFileDialog browser = new OpenFileDialog(){Multiselect = true, ValidateNames = true, CheckFileExists = true, CheckPathExists = true, Filter = ""})
            {
                DialogHelper.AddFilterToDialog<EImageFormat>(browser,"All Image Files");
                DialogHelper.AddFilterToDialog<EVideoFormat>(browser, "All Video Files");
                
                browser.ShowDialog();
            }
        }

    }
}
