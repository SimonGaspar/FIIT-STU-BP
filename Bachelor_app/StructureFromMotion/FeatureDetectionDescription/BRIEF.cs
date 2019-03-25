using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bachelor_app.StructureFromMotion.Model;
using Bachelor_app.StructureFromMotion.WindowsForm;
using Bakalárska_práca.Model;
using Bakalárska_práca.StructureFromMotion;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.XFeatures2D;

namespace Bachelor_app.StructureFromMotion.FeatureDetectionDescription
{
    public class BRIEF : IFeatureDetector, IFeatureDescriptor
    {
        BriefDescriptorExtractor _brief;
        private BriefForm _windowsForm;
        private BriefModel model = new BriefModel();

        public BRIEF()
        {
            UpdateModel(model);
        }

        public Mat ComputeDescriptor(KeyPointModel keyPoints)
        {
            Mat result = new Mat();
            Mat image = new Mat(keyPoints.InputFile.fileInfo.FullName);
            _brief.Compute(image, keyPoints.DetectedKeyPoints, result);
            return result;
        }

        public MKeyPoint[] DetectKeyPoints(IInputArray image)
        {
            MKeyPoint[] result;
            result = _brief.Detect(image);

            return result;
        }

        public void ShowSettingForm()
        {
            _windowsForm = new BriefForm(this);
            _windowsForm.Show();
        }

        public void UpdateModel<T>(T model)
        {
            this.model = model as BriefModel;
            _brief = new BriefDescriptorExtractor(
                
                );
        }
    }
}
