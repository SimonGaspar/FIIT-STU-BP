using System;
using System.Drawing;
using System.IO;
using Bakalárska_práca.Enumerate;

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
            if (Enum.IsDefined(typeof(EImageFormat), fileInfo.Extension.Replace(".", "").ToUpper()))
                image = Image.FromFile(fileInfo.FullName);

            if (Enum.IsDefined(typeof(EImageFormat), fileInfo.Extension.Replace(".", "").ToUpper()))
            {
                //image = Image.FromFile(fileInfo.FullName);
            }
        }
    }
}
