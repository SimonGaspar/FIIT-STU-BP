using System.Windows.Forms;
using Bachelor_app.StereoVision.StereoCorrespondence;

namespace Bachelor_app.StereoVision.WindowsForm
{
    /// <summary>
    /// Now not used, but in future, we can use video for SfM and stereo correspondence.
    /// DELETE these, when not using.
    /// </summary>
    public partial class CudaStereoConstantSpaceBPForm : Form
    {
        CudaStereoConstantSpaceBeliefPropagation _cudaStereoConstantSpaceBeliefPropagation;
        public CudaStereoConstantSpaceBPForm(CudaStereoConstantSpaceBeliefPropagation cudaStereoConstantSpaceBeliefPropagation)
        {
            this._cudaStereoConstantSpaceBeliefPropagation = cudaStereoConstantSpaceBeliefPropagation;

            InitializeComponent();
        }
    }
}
