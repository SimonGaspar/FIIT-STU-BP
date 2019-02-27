using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakalárska_práca.Model
{
    public class InputFile
    {
        public FileInfo fileInfo { get; set; }
        public Image image { get; set; }

        public InputFile(FileInfo fileInfo)
        {
            this.fileInfo = fileInfo;
            Initialize();
        }

        public InputFile(string fileName)
        {
            this.fileInfo = new FileInfo(fileName);
            Initialize();
        }

        private void Initialize()
        {
            image = Image.FromFile(fileInfo.FullName);
        }
    }
}
