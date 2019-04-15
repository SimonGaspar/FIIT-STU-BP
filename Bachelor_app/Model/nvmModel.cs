using System.Collections.Generic;

namespace Bachelor_app.Model
{
    /// <summary>
    /// Model for .nvm file
    /// </summary>
    public class nvmModel
    {
        public int imageCount { get; set; }
        public int pointCount { get; set; }

        public List<nvmPointModel> listPointModel { get; set; } = new List<nvmPointModel>();
        public List<nvmCameraModel> listImageModel { get; set; } = new List<nvmCameraModel>();
    }
}
