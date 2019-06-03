using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bachelor_app.Extension;
using Bachelor_app.Helper;
using Bachelor_app.Manager;
using Bachelor_app.StereoVision.Calibration;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace Bachelor_app.StereoVision
{
    public class CalibrationManager
    {
        private static readonly object Locker = new object();

        private readonly CameraManager cameraManager;
        private readonly VideoCapture leftCamera;
        private readonly VideoCapture rightCamera;

        private CalibrationForm winForm;
        private Thread calibrationProcess;
        private PatternModel patternModel = new PatternModel();

        #region Image Processing
        private int bufferLength;
        private int bufferSavepoint;
        private MCvPoint3D32f[][] cornersObjectPoints;
        private PointF[][] cornersPointsLeft;
        private PointF[][] cornersPointsRight;
        private ECalibrationMode currentMode = ECalibrationMode.SavingFrames;
        #endregion

        public CalibrationManager()
        {
        }

        public CalibrationManager(CameraManager cameraManager)
        {
            this.cameraManager = cameraManager;
            leftCamera = cameraManager.LeftCamera.Camera;
            rightCamera = cameraManager.RightCamera.Camera;

            winForm = new CalibrationForm(this, patternModel);
            calibrationProcess = new Thread(ProcessFrame);
            Task.Run(() => winForm.ShowDialog());
        }

        /// <summary>
        /// Kill calibration thread.
        /// </summary>
        public void KillProcess()
        {
            calibrationProcess.Abort();
        }

        /// <summary>
        /// Start calibration.
        /// </summary>
        public void StartCalibration()
        {
            SetValueForCalibration();
            calibrationProcess.Start();
        }

        /// <summary>
        /// Set value from WinForm into pattern model
        /// </summary>
        private void SetValueForCalibration()
        {
            bufferLength = patternModel.Count;
            bufferSavepoint = 0;
            cornersObjectPoints = new MCvPoint3D32f[bufferLength][];
            cornersPointsLeft = new PointF[bufferLength][];
            cornersPointsRight = new PointF[bufferLength][];
        }

        /// <summary>
        /// Start calibration and processing frames
        /// </summary>
        public void ProcessFrame()
        {
            bool stopCalibration = false;

            using (Mat frame_S1 = new Mat(), frame_S2 = new Mat())
            {
                while (!stopCalibration)
                {
                    CameraHelper.GetStereoImageSync(leftCamera, rightCamera, frame_S1, frame_S2);

                    winForm.Video_Source1.Image = frame_S1.ToImage();
                    winForm.Video_Source2.Image = frame_S1.ToImage();

                    switch (currentMode)
                    {
                        case ECalibrationMode.SavingFrames:
                            Task.Run(() => SaveImageForCalibration(frame_S1, frame_S2));
                            break;
                        case ECalibrationMode.Caluculating_Stereo_Intrinsics:
                            TryComputeCameraMatrix(frame_S1.Size);
                            break;
                        case ECalibrationMode.Calibrated:
                            stopCalibration = true; break;
                    }

                    Thread.Sleep(100);
                }
            }

            ExitWindows();
        }

        /// <summary>
        /// Kill calibration and close window
        /// </summary>
        public void ExitWindows()
        {
            if (winForm.InvokeRequired)
                winForm.Invoke((Action)delegate { ExitWindows(); });
            else
            {
                calibrationProcess.Abort();
                winForm.Close();
            }
        }

        /// <summary>
        /// Start computing camera matrix for stereo camera. Only one thread can compute.
        /// </summary>
        /// <param name="size">Size of input image from camera</param>
        private void TryComputeCameraMatrix(Size size)
        {
            Monitor.Enter(Locker);
            if (currentMode == ECalibrationMode.Caluculating_Stereo_Intrinsics)
            {
                CalibrationModel.Resolution = size;
                ComputeCameraMatrix();
            }

            Monitor.Exit(Locker);
        }

        /// <summary>
        /// Computing camera matrix for stereo camera
        /// </summary>
        /// <param name="size">Size of input image from camera</param>
        private void ComputeCameraMatrix()
        {
            for (int k = 0; k < bufferLength; k++)
            {
                List<MCvPoint3D32f> object_list = new List<MCvPoint3D32f>();
                for (int i = 0; i < patternModel.PatternSize.Height; i++)
                {
                    for (int j = 0; j < patternModel.PatternSize.Width; j++)
                    {
                        object_list.Add(new MCvPoint3D32f(j * patternModel.Distance, i * patternModel.Distance, 0.0F));
                    }
                }

                cornersObjectPoints[k] = object_list.ToArray();
            }

            CameraCalibration.StereoCalibrate(
                cornersObjectPoints,
                cornersPointsLeft,
                cornersPointsRight,
                CalibrationModel.IntrinsicCam1,
                CalibrationModel.IntrinsicCam2,
                CalibrationModel.Resolution,
                CalibType.Default,
                new MCvTermCriteria(0.1e5),
                out ExtrinsicCameraParameters eX_Param,
                out Matrix<double> fundamental,
                out Matrix<double> essential
                );

            CalibrationModel.EX_Param = eX_Param;
            CalibrationModel.Fundamental = fundamental;
            CalibrationModel.Essential = essential;
            MessageBox.Show("Intrinsic Calculation Complete");

            var rec1 = default(Rectangle);
            var rec2 = default(Rectangle);
            CvInvoke.StereoRectify(
                CalibrationModel.IntrinsicCam1.IntrinsicMatrix,
                CalibrationModel.IntrinsicCam1.DistortionCoeffs,
                CalibrationModel.IntrinsicCam2.IntrinsicMatrix,
                CalibrationModel.IntrinsicCam2.DistortionCoeffs,
                CalibrationModel.Resolution,
                CalibrationModel.EX_Param.RotationVector.RotationMatrix,
                CalibrationModel.EX_Param.TranslationVector,
                CalibrationModel.R1,
                CalibrationModel.R2,
                CalibrationModel.P1,
                CalibrationModel.P2,
                CalibrationModel.Q,
                StereoRectifyType.Default,
                0,
                CalibrationModel.Resolution,
                ref rec1,
                ref rec2
                );

            CalibrationModel.Rec1 = rec1;
            CalibrationModel.Rec2 = rec2;

            CalibrationModel.IntrinsicCam1.InitUndistortMatrix(CalibrationModel.UndistortCam1);
            CalibrationModel.IntrinsicCam2.InitUndistortMatrix(CalibrationModel.UndistortCam2);

            currentMode = ECalibrationMode.Calibrated;
            CalibrationModel.IsCalibrated = true;
        }

        /// <summary>
        /// Saving images with pattern in buffer for calibration.
        /// </summary>
        /// <param name="frame_S1">Frame from left camera</param>
        /// <param name="frame_S2">Frame from right camera</param>
        /// <returns></returns>
        private async Task SaveImageForCalibration(Mat frame_S1, Mat frame_S2)
        {
            using (Image<Bgr, byte> frameImage_S1 = new Image<Bgr, byte>(frame_S1.Bitmap), frameImage_S2 = new Image<Bgr, byte>(frame_S2.Bitmap))
            using (Image<Gray, byte> gray_frame_S1 = frameImage_S1.Convert<Gray, byte>(), gray_frame_S2 = frameImage_S2.Convert<Gray, byte>())
            using (VectorOfPointF cornerLeft = new VectorOfPointF(), cornerRight = new VectorOfPointF())
            {
                switch (patternModel.Pattern)
                {
                    case ECalibrationPattern.Chessboard:
                        CvInvoke.FindChessboardCorners(gray_frame_S1, patternModel.PatternSize, cornerLeft, CalibCbType.AdaptiveThresh);
                        CvInvoke.FindChessboardCorners(gray_frame_S2, patternModel.PatternSize, cornerRight, CalibCbType.AdaptiveThresh);
                        break;
                    default: throw new NotImplementedException();
                }

                if (cornerLeft.Size > 0 && cornerRight.Size > 0)
                {
                    PointF[] corners_Left;
                    PointF[] corners_Right;

                    corners_Left = cornerLeft.ToArray();
                    corners_Right = cornerRight.ToArray();

                    gray_frame_S1.FindCornerSubPix(new PointF[1][] { corners_Left }, new Size(11, 11), new Size(-1, -1), new MCvTermCriteria(30, 0.01));
                    gray_frame_S2.FindCornerSubPix(new PointF[1][] { corners_Right }, new Size(11, 11), new Size(-1, -1), new MCvTermCriteria(30, 0.01));

                    if (patternModel.Start_Flag)
                    {
                        cornersPointsLeft[bufferSavepoint] = corners_Left;
                        cornersPointsRight[bufferSavepoint] = corners_Right;
                        bufferSavepoint++;

                        if (bufferSavepoint == bufferLength)
                            currentMode = ECalibrationMode.Caluculating_Stereo_Intrinsics;

                        winForm.UpdateTitle("Form1: Buffer " + bufferSavepoint.ToString() + " of " + bufferLength.ToString());
                    }

                    switch (patternModel.Pattern)
                    {
                        case ECalibrationPattern.Chessboard:
                            CvInvoke.DrawChessboardCorners(frameImage_S1, patternModel.PatternSize, new VectorOfPointF(corners_Left), true);
                            CvInvoke.DrawChessboardCorners(frameImage_S2, patternModel.PatternSize, new VectorOfPointF(corners_Right), true);
                            break;
                        default: throw new NotImplementedException();
                    }

                    winForm.pictureBox1.Image = frameImage_S1.ToImage();
                    winForm.pictureBox2.Image = frameImage_S2.ToImage();
                }
            }
        }

        /// <summary>
        /// Updating pattern model with value from WinForm
        /// </summary>
        public void UpdatePatternModel()
        {
            var size = new Size(int.Parse(winForm.toolStripTextBox1.Text), int.Parse(winForm.toolStripTextBox2.Text));
            var count = int.Parse(winForm.toolStripTextBox3.Text);
            var distance = float.Parse(winForm.toolStripTextBox4.Text);
            var patternType = Enum.GetValues(typeof(ECalibrationPattern)).Cast<ECalibrationPattern>().First(x => x.ToString() == winForm.toolStripComboBox1.SelectedItem.ToString());

            patternModel = new PatternModel(size.Width, size.Height, count, distance, patternType);
        }
    }
}
