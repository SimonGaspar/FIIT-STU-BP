using System.Drawing;
using Emgu.CV;

namespace Bachelor_app.StereoVision.Calibration
{
    /// <summary>
    /// Calibration model
    /// </summary>
    public class CalibrationModel
    {
        public IntrinsicCameraParameters IntrinsicCam1 { get; set; } = new IntrinsicCameraParameters(); 
        public IntrinsicCameraParameters IntrinsicCam2 { get; set; } = new IntrinsicCameraParameters(); 
        public UndistortCameraParameters UndistortCam1 { get; set; } = new UndistortCameraParameters();
        public UndistortCameraParameters UndistortCam2 { get; set; } = new UndistortCameraParameters();
        public ExtrinsicCameraParameters EX_Param { get; set; } = new ExtrinsicCameraParameters(); 
        public Matrix<double> fundamental { get; set; } 
        public Matrix<double> essential { get; set; } 
        public Rectangle Rec1 { get; set; } = new Rectangle(); 
        public Rectangle Rec2 { get; set; } = new Rectangle(); 
        public Matrix<double> Q { get; set; } = new Matrix<double>(4, 4); 
        public Matrix<double> R1 { get; set; } = new Matrix<double>(3, 3); 
        public Matrix<double> R2 { get; set; } = new Matrix<double>(3, 3); 
        public Matrix<double> P1 { get; set; } = new Matrix<double>(3, 4); 
        public Matrix<double> P2 { get; set; } = new Matrix<double>(3, 4);
    }

    /// <summary>
    /// Undistort parameters for camera. Used in stereo vision by remap images.
    /// </summary>
    public class UndistortCameraParameters
    {
        public Matrix<float> MapX { get; set; }
        public Matrix<float> MapY { get; set; }
    }
}
