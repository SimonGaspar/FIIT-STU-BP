using System.Windows.Forms;
using Bakalárska_práca.StereoVision.StereoCorrespondence;

namespace Bakalárska_práca.StereoVision.WindowsForm
{
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
