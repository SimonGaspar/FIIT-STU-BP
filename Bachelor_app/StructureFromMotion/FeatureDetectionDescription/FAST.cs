﻿namespace Bachelor_app.StructureFromMotion.FeatureDetectionDescription
{
    /// <summary>
    /// FAST algorithm
    /// </summary>
    public class FAST : AbstractFeatureDetectorDescriptor, IFeatureDetector
    {
        public FAST() : base(new FastModel())
        {
            WindowsForm = new FastForm(this);
        }

        public override MKeyPoint[] DetectKeyPoints(IInputArray image)
        {
            MKeyPoint[] result;

            using (var _fast = CreateInstance())
                result = _fast.Detect(image);

            return result;
        }

        public override dynamic CreateInstance()
        {
            return new FastDetector(
                Model.Threshold,
                Model.NonMaxSupression,
                Model.Type
                );
        }
    }
}
