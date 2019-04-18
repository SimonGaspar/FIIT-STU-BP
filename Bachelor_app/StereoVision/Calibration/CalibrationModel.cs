using System;
using System.Drawing;
using System.IO;
using Emgu.CV;
using Newtonsoft.Json;

namespace Bachelor_app.StereoVision.Calibration
{
    /// <summary>
    /// Calibration model
    /// </summary>
    public class CalibrationModel
    {
        [JsonProperty]
        public static IntrinsicCameraParameters IntrinsicCam1 { get; set; } = new IntrinsicCameraParameters();
        [JsonProperty]
        public static IntrinsicCameraParameters IntrinsicCam2 { get; set; } = new IntrinsicCameraParameters();
        [JsonProperty]
        public static UndistortCameraParameters UndistortCam1 { get; set; } = new UndistortCameraParameters();
        [JsonProperty]
        public static UndistortCameraParameters UndistortCam2 { get; set; } = new UndistortCameraParameters();
        [JsonProperty]
        public static ExtrinsicCameraParameters EX_Param { get; set; } = new ExtrinsicCameraParameters();
        [JsonProperty]
        public static Matrix<double> Fundamental { get; set; }
        [JsonProperty]
        public static Matrix<double> Essential { get; set; }
        [JsonProperty]
        public static Rectangle Rec1 { get; set; } = new Rectangle();
        [JsonProperty]
        public static Rectangle Rec2 { get; set; } = new Rectangle();
        [JsonProperty]
        public static Matrix<double> Q { get; set; } = new Matrix<double>(4, 4);
        [JsonProperty]
        public static Matrix<double> R1 { get; set; } = new Matrix<double>(3, 3);
        [JsonProperty]
        public static Matrix<double> R2 { get; set; } = new Matrix<double>(3, 3);
        [JsonProperty]
        public static Matrix<double> P1 { get; set; } = new Matrix<double>(3, 4);
        [JsonProperty]
        public static Matrix<double> P2 { get; set; } = new Matrix<double>(3, 4);
        [JsonProperty]
        public static bool IsCalibrated { get; set; } = false;

        public static void CreateJson() {
            // Serializovat len dolezite informacie

            //var json = JsonConvert.SerializeObject(new CalibrationModel(), Formatting.Indented);
            //File.WriteAllText(Configuration.CalibrationPath,json);
        }

        public static void LoadJson() {
            //var json = JsonConvert.DeserializeObject(Configuration.CalibrationPath);
        }

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
