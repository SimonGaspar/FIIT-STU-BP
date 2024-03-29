﻿using Bachelor_app.Model;
using Bachelor_app.StructureFromMotion.Model;
using Bachelor_app.StructureFromMotion.WindowsForm;
using Emgu.CV;
using Emgu.CV.XFeatures2D;

namespace Bachelor_app.StructureFromMotion.FeatureDetectionDescription
{
    /// <summary>
    /// BRIEF algorithm
    /// </summary>
    public class BRIEF : AbstractFeatureDetectorDescriptor, IFeatureDescriptor
    {
        public BRIEF()
            : base(new BriefModel())
        {
            WindowsForm = new BriefForm(this);
        }

        public override Mat ComputeDescriptor(KeyPointModel keyPoints)
        {
            Mat result = new Mat();

            using (Mat image = new Mat(keyPoints.InputFile.FullPath))
            using (var brief = CreateInstance())
                brief.Compute(image, keyPoints.DetectedKeyPoints, result);

            return result;
        }

        public override dynamic CreateInstance()
        {
            return new BriefDescriptorExtractor(Model.DescriptorSize);
        }
    }
}
