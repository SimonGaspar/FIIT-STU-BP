using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Bachelor_app.StereoVision.Calibration
{
    public class CalibrationModel
    {
        public IntrinsicCameraParameters IntrinsicCam1 { get; set; } = new IntrinsicCameraParameters(); //Camera 1
        public IntrinsicCameraParameters IntrinsicCam2 { get; set; } = new IntrinsicCameraParameters(); //Camera 2
        public ExtrinsicCameraParameters EX_Param { get; set; } = new ExtrinsicCameraParameters(); //Output of Extrinsics for Camera 1 & 2
        public Matrix<double> fundamental { get; set; } //fundemental output matrix for StereoCalibrate
        public Matrix<double> essential { get; set; } //essential output matrix for StereoCalibrate
        public Rectangle Rec1 { get; set; } = new Rectangle(); //Rectangle Calibrated in camera 1
        public Rectangle Rec2 { get; set; } = new Rectangle(); //Rectangle Caliubrated in camera 2
        public Matrix<double> Q { get; set; } = new Matrix<double>(4, 4); //This is what were interested in the disparity-to-depth mapping matrix
        public Matrix<double> R1 { get; set; } = new Matrix<double>(3, 3); //rectification transforms (rotation matrices) for Camera 1.
        public Matrix<double> R2 { get; set; } = new Matrix<double>(3, 3); //rectification transforms (rotation matrices) for Camera 1.
        public Matrix<double> P1 { get; set; } = new Matrix<double>(3, 4); //projection matrices in the new (rectified) coordinate systems for Camera 1.
        public Matrix<double> P2 { get; set; } = new Matrix<double>(3, 4); //projection matrices in the new (rectified) coordinate systems for Camera 2.
        public MCvPoint3D32f[] _points { get; set; } //Computer3DPointsFromStereoPair
    }
}
