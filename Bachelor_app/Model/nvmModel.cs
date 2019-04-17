using System.Collections.Generic;

namespace Bachelor_app.Model
{
    /// <summary>
    /// Model for .nvm file
    /// </summary>
    public class NvmModel
    {
        public int ImageCount { get; set; }
        public int PointCount { get; set; }

        public List<NvmPointModel> ListPointModel { get; set; } = new List<NvmPointModel>();
        public List<NvmCameraModel> ListImageModel { get; set; } = new List<NvmCameraModel>();
    }
}
