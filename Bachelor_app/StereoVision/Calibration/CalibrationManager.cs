using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bachelor_app.Enumerate;
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
        public CalibrationModel calibrationModel = new CalibrationModel();
        public PatternModel patternModel = new PatternModel();

        private CameraManager _cameraManager;
        private CalibrationForm _winForm;

        public VideoCapture _leftCamera;
        public VideoCapture _rightCamera;

        public Thread CalibrationProcess;
        private static object locker = new object();

        #region Image Processing
        //buffers
        int buffer_length; //define the aquasition length of the buffer 
        int buffer_savepoint; //tracks the filled partition of the buffer
        MCvPoint3D32f[][] corners_object_Points; //stores the calculated size for the chessboard
        PointF[][] corners_points_Left;//stores the calculated points from chessboard detection Camera 1
        PointF[][] corners_points_Right;//stores the calculated points from chessboard detection Camera 2

        //Calibration parmeters
        Matrix<double> fundamental; //fundemental output matrix for StereoCalibrate
        Matrix<double> essential; //essential output matrix for StereoCalibrate
        Rectangle Rec1 = new Rectangle(); //Rectangle Calibrated in camera 1
        Rectangle Rec2 = new Rectangle(); //Rectangle Caliubrated in camera 2
        ExtrinsicCameraParameters EX_Param = new ExtrinsicCameraParameters(); //Output of Extrinsics for Camera 1 & 2
        #endregion

        ECalibrationMode currentMode = ECalibrationMode.SavingFrames;
        public CalibrationManager(CameraManager cameraManager)
        {
            this._cameraManager = cameraManager;
            _leftCamera = cameraManager.LeftCamera.camera;
            _rightCamera = cameraManager.RightCamera.camera;

            _winForm = new CalibrationForm(this);
            CalibrationProcess = new Thread(ProcessFrame);
            _winForm.ShowDialog();
        }

        public CalibrationManager()
        {
        }

        public void StartCalibration() {

            buffer_length = patternModel.count;
            buffer_savepoint = 0;
            corners_object_Points = new MCvPoint3D32f[buffer_length][];
            corners_points_Left = new PointF[buffer_length][];
            corners_points_Right = new PointF[buffer_length][];

            CalibrationProcess.Start();
        }

        public void ProcessFrame()
        {
            Mat frame_S1 = new Mat();
            Mat frame_S2 = new Mat();
            bool StopCalibration = false;
            while (!StopCalibration)
            {
                _leftCamera.Grab();
                _rightCamera.Grab();
                _leftCamera.Retrieve(frame_S1);
                _rightCamera.Retrieve(frame_S2);
                
                _winForm.Video_Source1.Image = frame_S1.Bitmap;
                _winForm.Video_Source2.Image = frame_S2.Bitmap;

                switch (currentMode)
                {
                    case ECalibrationMode.SavingFrames:
                        Task.Run(async () => await SaveImageForCalibration(frame_S1, frame_S2)) ; break;
                    case ECalibrationMode.Caluculating_Stereo_Intrinsics:
                        Monitor.Enter(locker);
                        if(currentMode == ECalibrationMode.Caluculating_Stereo_Intrinsics)
                        ComputeCameraMatrix(frame_S1, frame_S2);
                        Monitor.Exit(locker);
                        break;
                    case ECalibrationMode.Calibrated:
                        StopCalibration = true; break;
                }
                Thread.Sleep(100);
            }
            _winForm.ExitWindows();
        }

        private void ComputeCameraMatrix(Mat frame_S1, Mat frame_S2)
        {
            //fill the MCvPoint3D32f with correct mesurments
            for (int k = 0; k < buffer_length; k++)
            {
                //Fill our objects list with the real world mesurments for the intrinsic calculations
                List<MCvPoint3D32f> object_list = new List<MCvPoint3D32f>();
                for (int i = 0; i < patternModel.patternSize.Height; i++)
                {
                    for (int j = 0; j < patternModel.patternSize.Width; j++)
                    {
                        object_list.Add(new MCvPoint3D32f(j * patternModel.distance, i * patternModel.distance, 0.0F));
                    }
                }
                corners_object_Points[k] = object_list.ToArray();
            }
            //If Emgu.CV.CvEnum.CALIB_TYPE == CV_CALIB_USE_INTRINSIC_GUESS and/or CV_CALIB_FIX_ASPECT_RATIO are specified, some or all of fx, fy, cx, cy must be initialized before calling the function
            //if you use FIX_ASPECT_RATIO and FIX_FOCAL_LEGNTH options, these values needs to be set in the intrinsic parameters before the CalibrateCamera function is called. Otherwise 0 values are used as default.
            CameraCalibration.StereoCalibrate(corners_object_Points, corners_points_Left, corners_points_Right, calibrationModel.IntrinsicCam1, calibrationModel.IntrinsicCam2, frame_S1.Size,
                                                             CalibType.Default, new MCvTermCriteria(0.1e5),
                                                             out EX_Param, out fundamental, out essential);

            calibrationModel.EX_Param = EX_Param;
            calibrationModel.fundamental = fundamental;
            calibrationModel.essential = essential;
            MessageBox.Show("Intrinsic Calculation Complete"); //display that the mothod has been succesful
                                                               //currentMode = Mode.Calibrated;

            //Computes rectification transforms for each head of a calibrated stereo camera.
            CvInvoke.StereoRectify(calibrationModel.IntrinsicCam1.IntrinsicMatrix,
                                     calibrationModel.IntrinsicCam1.DistortionCoeffs, calibrationModel.IntrinsicCam2.IntrinsicMatrix, calibrationModel.IntrinsicCam2.DistortionCoeffs,
                                     frame_S1.Size,
                                     calibrationModel.EX_Param.RotationVector.RotationMatrix, calibrationModel.EX_Param.TranslationVector,
                                     calibrationModel.R1, calibrationModel.R2, calibrationModel.P1, calibrationModel.P2, calibrationModel.Q,
                                     StereoRectifyType.Default, 0,
                                     frame_S1.Size, ref Rec1, ref Rec2);

            calibrationModel.Rec1 = Rec1;
            calibrationModel.Rec2 = Rec2;

            InitUndistortMatrix();

            //This will Show us the usable area from each camera
            //MessageBox.Show("Left: " + Rec1.ToString() + " \nRight: " + Rec2.ToString());
            currentMode = ECalibrationMode.Calibrated;
        }

        private void InitUndistortMatrix()
        {
            Matrix<float> MapX1 = null;
            Matrix<float> MapY1 = null;
            Matrix<float> MapX2 = null;
            Matrix<float> MapY2 = null;
            switch (_cameraManager.resolution) {
                case ECameraResolution.VGA:
                    calibrationModel.IntrinsicCam1.InitUndistortMap(640, 360,out MapX1, out MapY1);
                    calibrationModel.IntrinsicCam2.InitUndistortMap(640, 360, out MapX2, out MapY2);
                    break;
                case ECameraResolution.HD:
                    calibrationModel.IntrinsicCam1.InitUndistortMap(1280, 720, out MapX1, out MapY1);
                    calibrationModel.IntrinsicCam2.InitUndistortMap(1280, 720, out MapX2, out MapY2);
                    break;
                case ECameraResolution.FullHD:
                    calibrationModel.IntrinsicCam1.InitUndistortMap(1920, 1080, out MapX1, out MapY1);
                    calibrationModel.IntrinsicCam2.InitUndistortMap(1920, 1080, out MapX2, out MapY2);
                    break;
            }
            calibrationModel.UndistortCam1.MapX = MapX1;
            calibrationModel.UndistortCam1.MapY = MapY1;
            calibrationModel.UndistortCam2.MapX = MapX2;
            calibrationModel.UndistortCam2.MapY = MapY2;
        }

        private async Task SaveImageForCalibration(Mat frame_S1, Mat frame_S2)
        {
            Image<Bgr, byte> frameImage_S1;
            Image<Gray, Byte> Gray_frame_S1;
            Image<Bgr, byte> frameImage_S2;
            Image<Gray, Byte> Gray_frame_S2;

            frameImage_S1 = new Image<Bgr, byte>(frame_S1.Bitmap);
            Gray_frame_S1 = frameImage_S1.Convert<Gray, Byte>();
            frameImage_S2 = new Image<Bgr, byte>(frame_S2.Bitmap);
            Gray_frame_S2 = frameImage_S2.Convert<Gray, Byte>();

            VectorOfPointF cornerLeft = new VectorOfPointF();
            VectorOfPointF cornerRight = new VectorOfPointF();

            //Find the chessboard in bothe images
            CvInvoke.FindChessboardCorners(Gray_frame_S1, patternModel.patternSize, cornerLeft, CalibCbType.AdaptiveThresh);
            CvInvoke.FindChessboardCorners(Gray_frame_S2, patternModel.patternSize, cornerRight, CalibCbType.AdaptiveThresh);
            
            if (cornerLeft.Size >0 && cornerRight.Size>0) //chess board found in one of the frames?
            {
                PointF[] corners_Left;
                PointF[] corners_Right;

                corners_Left = cornerLeft.ToArray();
                corners_Right = cornerRight.ToArray();

                //make mesurments more accurate by using FindCornerSubPixel
                Gray_frame_S1.FindCornerSubPix(new PointF[1][] { corners_Left }, new Size(11, 11), new Size(-1, -1), new MCvTermCriteria(30, 0.01));
                Gray_frame_S2.FindCornerSubPix(new PointF[1][] { corners_Right }, new Size(11, 11), new Size(-1, -1), new MCvTermCriteria(30, 0.01));

                //if go button has been pressed start aquiring frames else we will just display the points
                if (patternModel.start_Flag)
                {
                    //save the calculated points into an array
                    corners_points_Left[buffer_savepoint] = corners_Left;
                    corners_points_Right[buffer_savepoint] = corners_Right;
                    buffer_savepoint++;//increase buffer positon

                    //check the state of buffer
                    if (buffer_savepoint == buffer_length) currentMode = ECalibrationMode.Caluculating_Stereo_Intrinsics; //buffer full

                    //Show state of Buffer
                    _winForm.UpdateTitle("Form1: Buffer " + buffer_savepoint.ToString() + " of " + buffer_length.ToString());
                }

                //draw the results
                frameImage_S1.Draw(new CircleF(corners_Left[0], 3), new Bgr(Color.Yellow), 10);
                frameImage_S2.Draw(new CircleF(corners_Right[0], 3), new Bgr(Color.Yellow), 10);
                for (int i = 1; i < corners_Left.Length; i++)
                {
                    //left
                    frameImage_S1.Draw(new LineSegment2DF(corners_Left[i - 1], corners_Left[i]), patternModel.line_colour_array[i], 10);
                    frameImage_S1.Draw(new CircleF(corners_Left[i], 3), new Bgr(Color.Yellow), 10);
                    //right
                    frameImage_S2.Draw(new LineSegment2DF(corners_Right[i - 1], corners_Right[i]), patternModel.line_colour_array[i], 10);
                    frameImage_S2.Draw(new CircleF(corners_Right[i], 3), new Bgr(Color.Yellow), 10);
                }

                _winForm.pictureBox1.Image = frameImage_S1.Bitmap;
                _winForm.pictureBox2.Image = frameImage_S2.Bitmap;
            }
        }

        public void UpdatePatternModel()
        {
            patternModel.width = int.Parse(_winForm.toolStripTextBox1.Text);
            patternModel.height = int.Parse(_winForm.toolStripTextBox2.Text);
            patternModel.count = int.Parse(_winForm.toolStripTextBox3.Text);
            patternModel.distance = float.Parse(_winForm.toolStripTextBox4.Text);
            patternModel.pattern = Enum.GetValues(typeof(ECalibrationPattern)).Cast<ECalibrationPattern>().First(x => x.ToString() == _winForm.toolStripComboBox1.SelectedItem.ToString());
        }


    }
}
