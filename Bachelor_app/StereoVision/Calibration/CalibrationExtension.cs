﻿namespace Bachelor_app.StereoVision.Calibration
{
    public static class CalibrationExtension
    {
        /// <summary>
        /// Setting undistort matrix for camera
        /// </summary>
        /// <param name="intrinsicCameraParameters">Intrinsic parameters of camera</param>
        /// <param name="undistortCameraParameters">Undistort parameters of camera</param>
        public static void InitUndistortMatrix(this IntrinsicCameraParameters intrinsicCameraParameters, UndistortCameraParameters undistortCameraParameters)
        {
            var size = CalibrationModel.Resolution;
            intrinsicCameraParameters.InitUndistortMap(size.Width, size.Height, out Matrix<float> MapX, out Matrix<float> MapY);

            undistortCameraParameters.MapX = MapX;
            undistortCameraParameters.MapY = MapY;
        }
    }
}
