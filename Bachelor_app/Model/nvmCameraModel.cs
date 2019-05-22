namespace Bachelor_app.Model
{
    /// <summary>
    /// Model for camera (.nvm file)
    /// </summary>
    public class NvmCameraModel
    {
        public string FileName { get; private set; }
        public float FocalLength { get; private set; }
        public Quaternion Quaternion { get; private set; }
        public Vector3 CameraCenter { get; private set; }
        public float RadialDistortion { get; private set; }

        public NvmCameraModel(string fileName, float focalLength, Quaternion quaternion, Vector3 cameraCenter, float radialDistortion)
        {
            FileName = fileName;
            focalLength = FocalLength;
            Quaternion = quaternion;
            CameraCenter = cameraCenter;
            RadialDistortion = radialDistortion;
        }
    }
}
