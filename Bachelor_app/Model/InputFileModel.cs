using System;
using System.Drawing;
using System.IO;
using Bachelor_app.Enumerate;

namespace Bachelor_app.Model
{
    public class InputFileModel
    {
        public Image Image { get; private set; }
        public bool UseInSFM { get; set; }
        public string FullPath { get { return fileInfo.FullName; } }
        public string FileName { get { return fileInfo.Name; } }
        public string FileNameWithoutExtension { get { return Path.GetFileNameWithoutExtension(FullPath); } }

        private FileInfo fileInfo;

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

        public void SetFileInfo(FileInfo fileInfo)
        {
            this.fileInfo = fileInfo;
        }

        /// <summary>
        /// Set image for this model
        /// </summary>
        private void GetImageFromInputFile()
        {
            //if (Enum.IsDefined(typeof(EImageFormat), fileInfo.Extension.Replace(".", "").ToUpper()))
            //    Image = Image.FromFile(fileInfo.FullName);
        }
    }
}
