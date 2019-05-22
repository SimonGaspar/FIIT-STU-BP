using System.Collections.Generic;

namespace Bachelor_app.Model
{
    /// <summary>
    /// Model for .nvm file
    /// </summary>
    public class NvmModel
    {
        public int ImageCount { get; private set; }
        public int PointCount { get; private set; }
        public List<NvmPointModel> ListPointModel { get; private set; } = new List<NvmPointModel>();
        public List<NvmCameraModel> ListCameraModel { get; private set; } = new List<NvmCameraModel>();

        public void SetImageCount(int count)
        {
            ImageCount = count;
        }

        public void SetPointCount(int count)
        {
            PointCount = count;
        }
    }
}
