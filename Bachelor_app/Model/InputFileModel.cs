using System;
using System.Drawing;
using System.IO;
using Bakalárska_práca.Enumerate;

namespace Bakalárska_práca.Model
{
    /// <summary>
    /// Model for input file
    /// </summary>
    public class InputFileModel
    {
        public FileInfo fileInfo { get; set; }
        public Image image { get; set; }

        public bool UseInSFM { get; set; } = false;

        public InputFileModel(FileInfo fileInfo)
        {
            this.fileInfo = fileInfo;
            GetImageFromInputFile();
        }

        public InputFileModel(string fileName)
        {
            this.fileInfo = new FileInfo(fileName);
            GetImageFromInputFile();
        }

        /// <summary>
        /// Set image for this model
        /// </summary>
        private void GetImageFromInputFile()
        {
            if (Enum.IsDefined(typeof(EImageFormat), fileInfo.Extension.Replace(".", "").ToUpper()))
                image = Image.FromFile(fileInfo.FullName);


            /// <summary>
            /// Now not used, but in future, we can use video for SfM and stereo correspondence.
            /// DELETE these, when not using.
            /// </summary>
            /// 
            //if (Enum.IsDefined(typeof(EVideoFormat), fileInfo.Extension.Replace(".", "").ToUpper()))
            //{
            //    var capture = new VideoCapture(fileInfo.FullName);
            //    var x = capture.QueryFrame();
            //    image = x.ToImage<Bgr, byte>().ToBitmap();
            //}
        }
    }
}
