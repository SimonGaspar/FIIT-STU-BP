using System.Numerics;

namespace Bachelor_app.Model
{
    /// <summary>
    /// Model for camera (.nvm file)
    /// </summary>
    public class nvmCameraModel
    {
        public string fileName { get; set; }
        public float focalLength { get; set; }
        public Quaternion quaternion { get; set; }
        public Vector3 cameraCenter { get; set; }
        public float radialDistortion { get; set; }

    }
}
