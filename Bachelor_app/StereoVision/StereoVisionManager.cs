using System.Drawing;
using Bakalárska_práca.Manager;
using Bakalárska_práca.StereoVision.StereoCorrespondence;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Bakalárska_práca.StereoVision
{
    public class StereoVisionManager
    {
        private IStereoSolver StereoSolver = new StereoBlockMatching();
        private FileManager _fileManager;
        private DisplayManager _displayManager;

        public StereoVisionManager(FileManager fileManager, DisplayManager displayManager)
        {
            this._fileManager = fileManager;
            this._displayManager = displayManager;
        }

        public void SetStereoCorrespondenceAlgorithm(EStereoCorrespondenceAlgorithm item)
        {
            switch (item)
            {
                case EStereoCorrespondenceAlgorithm.CudaStereoBM: StereoSolver = new CudaStereoBlockMatching(); break;
                case EStereoCorrespondenceAlgorithm.StereoBM: StereoSolver = new StereoBlockMatching(); break;
                case EStereoCorrespondenceAlgorithm.StereoSGBM: StereoSolver = new StereoSemiGlobalBlockMatching(); break;
                case EStereoCorrespondenceAlgorithm.CudaStereoConstantSpaceBP: StereoSolver = new CudaStereoConstantSpaceBeliefPropagation(); break;

            }
        }

        public void ComputeStereoCorrespondence()
        {
            for (int i = 0; i < _fileManager.ListOfInputFileForLeft.Count; i++)
            {
                var leftImage = _fileManager.ListOfInputFileForLeft[i];
                var rightImage = _fileManager.ListOfInputFileForRight[i];
                _displayManager._lastDepthMapImage = new Image<Bgr, byte>((Bitmap)StereoSolver.ComputeDepthMap(leftImage.image, rightImage.image));
                _displayManager.Display();
            }
        }

        public void ShowSettingForStereoSolver()
        {
            StereoSolver.ShowSettingForm();
        }
    }
}
