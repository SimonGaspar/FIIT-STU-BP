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
    }
}
