using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bachelor_app.Enumerate;
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
        // ak bude mat global configuration aj typ resolution netreba si pamatat.
        private CameraManager _cameraManager;
        private CalibrationForm _winForm;

        private VideoCapture _leftCamera;
        private VideoCapture _rightCamera;

        private Thread CalibrationProcess;
        private PatternModel patternModel = new PatternModel();

        private static object locker = new object();

        #region Image Processing
        private int buffer_length;
        private int buffer_savepoint;
        private MCvPoint3D32f[][] corners_object_Points;
        private PointF[][] corners_points_Left;
        private PointF[][] corners_points_Right;
        private ECalibrationMode currentMode = ECalibrationMode.SavingFrames;
        #endregion


        public CalibrationManager()
        {
        }

        public CalibrationManager(CameraManager cameraManager)
        {
            this._cameraManager = cameraManager;
            _leftCamera = cameraManager.LeftCamera.Camera;
            _rightCamera = cameraManager.RightCamera.Camera;

            _winForm = new CalibrationForm(this,patternModel);
            CalibrationProcess = new Thread(ProcessFrame);
            _winForm.ShowDialog();
        }

        public void KillProcess()
        {
            CalibrationProcess.Abort();
        }


        /// <summary>
        /// Start calibration.
        /// </summary>
        public void StartCalibration()
        {
            SetValueForCalibration();
            CalibrationProcess.Start();
        }

        /// <summary>
        /// Set value from WinForm into pattern model
        /// </summary>
        private void SetValueForCalibration()
        {
            buffer_length = patternModel.Count;
            buffer_savepoint = 0;
            corners_object_Points = new MCvPoint3D32f[buffer_length][];
            corners_points_Left = new PointF[buffer_length][];
            corners_points_Right = new PointF[buffer_length][];
        }

        /// <summary>
        /// Start calibration and processing frames
        /// </summary>
        public void ProcessFrame()
        {
            Mat frame_S1 = new Mat();
            Mat frame_S2 = new Mat();
            bool StopCalibration = false;

            while (!StopCalibration)
            {
                CameraHelper.GetStereoImageSync(_leftCamera, _rightCamera, ref frame_S1, ref frame_S2);

                _winForm.Video_Source1.Image = frame_S1.ToImage();
                _winForm.Video_Source2.Image = frame_S1.ToImage();

                switch (currentMode)
                {
                    case ECalibrationMode.SavingFrames:
                        Task.Run(() => SaveImageForCalibration(frame_S1, frame_S2));
                        break;
                    case ECalibrationMode.Caluculating_Stereo_Intrinsics:
                        TryComputeCameraMatrix(frame_S1.Size);
                        break;
                    case ECalibrationMode.Calibrated:
                        StopCalibration = true; break;
                }
                Thread.Sleep(100);
            }
            ExitWindows();
        }

        /// <summary>
        /// Kill calibration and close window
        /// </summary>
        public void ExitWindows()
        {
            if (_winForm.InvokeRequired)
                _winForm.Invoke((Action)delegate { ExitWindows(); });
            else
            {
                CalibrationProcess.Abort();
                _winForm.Close();
            }
        }

        /// <summary>
        /// Start computing camera matrix for stereo camera. Only one thread can compute.
        /// </summary>
        /// <param name="size">Size of input image from camera</param>
        private void TryComputeCameraMatrix(Size size)
        {
            Monitor.Enter(locker);
            if (currentMode == ECalibrationMode.Caluculating_Stereo_Intrinsics)
                ComputeCameraMatrix(size);
            Monitor.Exit(locker);
        }

        /// <summary>
        /// Computing camera matrix for stereo camera
        /// </summary>
        /// <param name="size">Size of input image from camera</param>
        private void ComputeCameraMatrix(Size size)
        {
            for (int k = 0; k < buffer_length; k++)
            {
                List<MCvPoint3D32f> object_list = new List<MCvPoint3D32f>();
                for (int i = 0; i < patternModel.PatternSize.Height; i++)
                {
                    for (int j = 0; j < patternModel.PatternSize.Width; j++)
                    {
                        object_list.Add(new MCvPoint3D32f(j * patternModel.Distance, i * patternModel.Distance, 0.0F));
                    }
                }
                corners_object_Points[k] = object_list.ToArray();
            }

            CameraCalibration.StereoCalibrate(corners_object_Points, corners_points_Left, corners_points_Right, CalibrationModel.IntrinsicCam1, CalibrationModel.IntrinsicCam2, size,
                                                             CalibType.Default, new MCvTermCriteria(0.1e5),
                                                             out ExtrinsicCameraParameters EX_Param, out Matrix<double> fundamental, out Matrix<double> essential);

            CalibrationModel.EX_Param = EX_Param;
            CalibrationModel.Fundamental = fundamental;
            CalibrationModel.Essential = essential;
            MessageBox.Show("Intrinsic Calculation Complete");

            var Rec1 = new Rectangle();
            var Rec2 = new Rectangle();
            CvInvoke.StereoRectify(CalibrationModel.IntrinsicCam1.IntrinsicMatrix,
                                     CalibrationModel.IntrinsicCam1.DistortionCoeffs, CalibrationModel.IntrinsicCam2.IntrinsicMatrix, CalibrationModel.IntrinsicCam2.DistortionCoeffs,
                                     size,
                                     CalibrationModel.EX_Param.RotationVector.RotationMatrix, CalibrationModel.EX_Param.TranslationVector,
                                     CalibrationModel.R1, CalibrationModel.R2, CalibrationModel.P1, CalibrationModel.P2, CalibrationModel.Q,
                                     StereoRectifyType.Default, 0,
                                     size, ref Rec1, ref Rec2);

            CalibrationModel.Rec1 = Rec1;
            CalibrationModel.Rec2 = Rec2;

            InitUndistortMatrix(CalibrationModel.IntrinsicCam1, CalibrationModel.UndistortCam1);
            InitUndistortMatrix(CalibrationModel.IntrinsicCam2, CalibrationModel.UndistortCam2);

            currentMode = ECalibrationMode.Calibrated;
        }

        /// <summary>
        /// Setting undistort matrix for camera
        /// </summary>
        /// <param name="intrinsicCameraParameters">Intrinsic parameters of camera</param>
        /// <param name="undistortCameraParameters">Undistort parameters of camera</param>
        private void InitUndistortMatrix(IntrinsicCameraParameters intrinsicCameraParameters, UndistortCameraParameters undistortCameraParameters)
        {
            var size = _cameraManager.resolution.GetResolution();

            intrinsicCameraParameters.InitUndistortMap(size.Width, size.Height, out Matrix<float> MapX, out Matrix<float> MapY);

            undistortCameraParameters.MapX = MapX;
            undistortCameraParameters.MapY = MapY;
        }

        /// <summary>
        /// Saving images with pattern in buffer for calibration.
        /// </summary>
        /// <param name="frame_S1">Frame from left camera</param>
        /// <param name="frame_S2">Frame from right camera</param>
        /// <returns></returns>
        private async Task SaveImageForCalibration(Mat frame_S1, Mat frame_S2)
        {
            var frameImage_S1 = new Image<Bgr, byte>(frame_S1.Bitmap);
            var Gray_frame_S1 = frameImage_S1.Convert<Gray, byte>();
            var frameImage_S2 = new Image<Bgr, byte>(frame_S2.Bitmap);
            var Gray_frame_S2 = frameImage_S2.Convert<Gray, byte>();

            VectorOfPointF cornerLeft = new VectorOfPointF();
            VectorOfPointF cornerRight = new VectorOfPointF();

            switch (patternModel.pattern)
            {
                case ECalibrationPattern.Chessboard:
                    CvInvoke.FindChessboardCorners(Gray_frame_S1, patternModel.PatternSize, cornerLeft, CalibCbType.AdaptiveThresh);
                    CvInvoke.FindChessboardCorners(Gray_frame_S2, patternModel.PatternSize, cornerRight, CalibCbType.AdaptiveThresh);
                    break;
                default: throw new NotImplementedException();
            }

            if (cornerLeft.Size > 0 && cornerRight.Size > 0)
            {
                PointF[] corners_Left;
                PointF[] corners_Right;

                corners_Left = cornerLeft.ToArray();
                corners_Right = cornerRight.ToArray();

                Gray_frame_S1.FindCornerSubPix(new PointF[1][] { corners_Left }, new Size(11, 11), new Size(-1, -1), new MCvTermCriteria(30, 0.01));
                Gray_frame_S2.FindCornerSubPix(new PointF[1][] { corners_Right }, new Size(11, 11), new Size(-1, -1), new MCvTermCriteria(30, 0.01));

                if (patternModel.start_Flag)
                {
                    corners_points_Left[buffer_savepoint] = corners_Left;
                    corners_points_Right[buffer_savepoint] = corners_Right;
                    buffer_savepoint++;

                    if (buffer_savepoint == buffer_length) currentMode = ECalibrationMode.Caluculating_Stereo_Intrinsics;

                    _winForm.UpdateTitle("Form1: Buffer " + buffer_savepoint.ToString() + " of " + buffer_length.ToString());
                }
                switch (patternModel.pattern)
                {
                    case ECalibrationPattern.Chessboard:
                        CvInvoke.DrawChessboardCorners(frameImage_S1, patternModel.PatternSize, new VectorOfPointF(corners_Left), true);
                        CvInvoke.DrawChessboardCorners(frameImage_S2, patternModel.PatternSize, new VectorOfPointF(corners_Right), true);
                        break;
                    default: throw new NotImplementedException();
                }

                // Own drawing method
                //
                //frameImage_S1.Draw(new CircleF(corners_Left[0], 3), new Bgr(Color.Yellow), 10);
                //frameImage_S2.Draw(new CircleF(corners_Right[0], 3), new Bgr(Color.Yellow), 10);
                //for (int i = 1; i < corners_Left.Length; i++)
                //{
                //    //left
                //    frameImage_S1.Draw(new LineSegment2DF(corners_Left[i - 1], corners_Left[i]), patternModel.line_colour_array[i], 10);
                //    frameImage_S1.Draw(new CircleF(corners_Left[i], 3), new Bgr(Color.Yellow), 10);
                //    //right
                //    frameImage_S2.Draw(new LineSegment2DF(corners_Right[i - 1], corners_Right[i]), patternModel.line_colour_array[i], 10);
                //    frameImage_S2.Draw(new CircleF(corners_Right[i], 3), new Bgr(Color.Yellow), 10);
                //}

                _winForm.pictureBox1.Image = frameImage_S1.ToImage();
                _winForm.pictureBox2.Image = frameImage_S2.ToImage();
            }
        }

        /// <summary>
        /// Updating pattern model with value from WinForm
        /// </summary>
        public void UpdatePatternModel()
        {
            patternModel.Width = int.Parse(_winForm.toolStripTextBox1.Text);
            patternModel.Height = int.Parse(_winForm.toolStripTextBox2.Text);
            patternModel.Count = int.Parse(_winForm.toolStripTextBox3.Text);
            patternModel.Distance = float.Parse(_winForm.toolStripTextBox4.Text);
            patternModel.pattern = Enum.GetValues(typeof(ECalibrationPattern)).Cast<ECalibrationPattern>().First(x => x.ToString() == _winForm.toolStripComboBox1.SelectedItem.ToString());
        }


    }
}
