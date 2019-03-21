using System;
using System.Drawing;
using System.IO;
using Bakalárska_práca.Enumerate;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Bakalárska_práca.Model
{
    public class InputFileModel
    {
        public FileInfo fileInfo { get; set; }
        public Image image { get; set; }

        public InputFileModel(FileInfo fileInfo)
        {
            this.fileInfo = fileInfo;
            Initialize();
        }

        public InputFileModel(string fileName)
        {
            this.fileInfo = new FileInfo(fileName);
            Initialize();
        }

        private void Initialize()
        {
            if (Enum.IsDefined(typeof(EImageFormat), fileInfo.Extension.Replace(".", "").ToUpper()))
                image = Image.FromFile(fileInfo.FullName);

            if (Enum.IsDefined(typeof(EVideoFormat), fileInfo.Extension.Replace(".", "").ToUpper()))
            {
                var capture = new VideoCapture(fileInfo.FullName);
                var x = capture.QueryFrame();
                image = x.ToImage<Bgr, byte>().ToBitmap();
            }
        }
    }
}
