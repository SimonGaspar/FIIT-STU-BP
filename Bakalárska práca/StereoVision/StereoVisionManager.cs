using Bakalárska_práca.Helper;
using Bakalárska_práca.Manager;
using Bakalárska_práca.StereoVision.StereoCorrespondence;
using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bakalárska_práca.StereoVision
{
    public class StereoVisionManager
    {
        private IStereoSolver StereoSolver = new StereoBlockMatching();
        private FileManager _fileManager;
        private DisplayManager _displayManager;

        public StereoVisionManager(FileManager fileManager,DisplayManager displayManager) {
            this._fileManager = fileManager;
            this._displayManager = displayManager;
        }

        public void SetStereoCorrespondenceAlgorithm(object sender, EventArgs e)
        {
            MenuHelper.OnlyOneCheck(sender, e);
            var currentItem = sender as ToolStripMenuItem;
            switch (currentItem.Name)
            {
                case nameof(EStereoCorrespondenceAlgorithm.StereoBM): StereoSolver = new StereoBlockMatching(); break;
                //case nameof(EStereoCorrespondenceAlgorithm.StereoSGBM): StereoSolver = new StereoSemiGlobalBlockMatching(); break;
                //case nameof(EStereoCorrespondenceAlgorithm.CudaStereoBM): StereoSolver = new CudaStereoBlockMatching(); break;
                //case nameof(EStereoCorrespondenceAlgorithm.CudaStereoConstantSpaceBP): StereoSolver = new CudaStereoConstantSpaceBeliefPropagation(); break;
            }
        }

        public void ComputeStereoCorrespondence() {
            for (int i = 0; i < _fileManager.ListOfInputFileForLeft.Count; i++)
            {
                var leftImage = _fileManager.ListOfInputFileForLeft[i];
                var rightImage = _fileManager.ListOfInputFileForRight[i];
                _displayManager._lastDepthMapImage=new Image<Bgr,byte>((Bitmap)StereoSolver.ComputeDepthMap(leftImage.image,rightImage.image));
                _displayManager.Display();
            }
        }

    }
}
