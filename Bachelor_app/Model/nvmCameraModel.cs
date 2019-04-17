using System.Numerics;

namespace Bachelor_app.Model
{
    /// <summary>
    /// Model for camera (.nvm file)
    /// </summary>
    public class NvmCameraModel
    {
        public string FileName { get; set; }
        public float FocalLength { get; set; }
        public Quaternion Quaternion { get; set; }
        public Vector3 CameraCenter { get; set; }
        public float RadialDistortion { get; set; }

    }
}
