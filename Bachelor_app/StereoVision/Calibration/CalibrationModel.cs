using System.Drawing;
using Emgu.CV;

namespace Bachelor_app.StereoVision.Calibration
{
    /// <summary>
    /// Calibration model
    /// </summary>
    public static class CalibrationModel
    {
        public static IntrinsicCameraParameters IntrinsicCam1 { get; set; } = new IntrinsicCameraParameters();
        public static IntrinsicCameraParameters IntrinsicCam2 { get; set; } = new IntrinsicCameraParameters();
        public static UndistortCameraParameters UndistortCam1 { get; set; } = new UndistortCameraParameters();
        public static UndistortCameraParameters UndistortCam2 { get; set; } = new UndistortCameraParameters();
        public static ExtrinsicCameraParameters EX_Param { get; set; } = new ExtrinsicCameraParameters();
        public static Matrix<double> Fundamental { get; set; }
        public static Matrix<double> Essential { get; set; }
        public static Rectangle Rec1 { get; set; } = new Rectangle();
        public static Rectangle Rec2 { get; set; } = new Rectangle();
        public static Matrix<double> Q { get; set; } = new Matrix<double>(4, 4);
        public static Matrix<double> R1 { get; set; } = new Matrix<double>(3, 3);
        public static Matrix<double> R2 { get; set; } = new Matrix<double>(3, 3);
        public static Matrix<double> P1 { get; set; } = new Matrix<double>(3, 4);
        public static Matrix<double> P2 { get; set; } = new Matrix<double>(3, 4);
        public static bool IsCalibrated { get; set; } = false;
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
