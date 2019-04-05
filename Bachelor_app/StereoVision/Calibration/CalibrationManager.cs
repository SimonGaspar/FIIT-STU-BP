using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        public ChessboardModel chessboardModel = new ChessboardModel();

        private CameraManager _cameraManager;
        private CalibrationForm _winForm;

        public VideoCapture _leftCamera;
        public VideoCapture _rightCamera;

        #region Image Processing

        //Frames
        Mat frame_S1 = new Mat();
        Image<Bgr, byte> frameImage_S1;
        Image<Gray, Byte> Gray_frame_S1;
        Mat frame_S2 = new Mat();
        Image<Bgr, byte> frameImage_S2;
        Image<Gray, Byte> Gray_frame_S2;


        //buffers
        static int buffer_length = 100; //define the aquasition length of the buffer 
        int buffer_savepoint = 0; //tracks the filled partition of the buffer
        MCvPoint3D32f[][] corners_object_Points = new MCvPoint3D32f[buffer_length][]; //stores the calculated size for the chessboard
        PointF[][] corners_points_Left = new PointF[buffer_length][];//stores the calculated points from chessboard detection Camera 1
        PointF[][] corners_points_Right = new PointF[buffer_length][];//stores the calculated points from chessboard detection Camera 2

        //Calibration parmeters
        Matrix<double> fundamental; //fundemental output matrix for StereoCalibrate
        Matrix<double> essential; //essential output matrix for StereoCalibrate
        Rectangle Rec1 = new Rectangle(); //Rectangle Calibrated in camera 1
        Rectangle Rec2 = new Rectangle(); //Rectangle Caliubrated in camera 2
        ExtrinsicCameraParameters EX_Param = new ExtrinsicCameraParameters(); //Output of Extrinsics for Camera 1 & 2
        MCvPoint3D32f[] _points; //Computer3DPointsFromStereoPair
        #endregion

        ECalibrationMode currentMode = ECalibrationMode.SavingFrames;
        public CalibrationManager(CameraManager cameraManager)
        {
            this._cameraManager = cameraManager;
            _leftCamera = cameraManager.LeftCamera.camera;
            _rightCamera = cameraManager.RightCamera.camera;

            _winForm = new CalibrationForm(this);

            _winForm.Show();

            //mozno hodit ako _leftCamera.ImageGrabbed +=  ProcessFrame()
            Task.Run(async () => await ProcessFrame());
        }

        public CalibrationManager()
        {
        }

        public async Task ProcessFrame()
        {
            bool StopCalibration = false;
            while (!StopCalibration)
            {
                _leftCamera.Grab();
                _leftCamera.Retrieve(frame_S1);
                frameImage_S1 = new Image<Bgr, byte>(frame_S1.Bitmap);
                Gray_frame_S1 = frameImage_S1.Convert<Gray, Byte>();

                _rightCamera.Grab();
                _rightCamera.Retrieve(frame_S2);
                frameImage_S2 = new Image<Bgr, byte>(frame_S2.Bitmap);
                Gray_frame_S2 = frameImage_S2.Convert<Gray, Byte>();

                switch (currentMode)
                {
                    case ECalibrationMode.SavingFrames: SaveImageForCalibration(); break;
                    case ECalibrationMode.Caluculating_Stereo_Intrinsics: ComputeCameraMatrix(); break;
                    case ECalibrationMode.Calibrated: _winForm.Close(); StopCalibration = true; break;
                }

                _winForm.Video_Source1.Image = frameImage_S1.Bitmap;
                _winForm.Video_Source2.Image = frameImage_S2.Bitmap;
            }
        }

        private void ComputeCameraMatrix()
        {
            //fill the MCvPoint3D32f with correct mesurments
            for (int k = 0; k < buffer_length; k++)
            {
                //Fill our objects list with the real world mesurments for the intrinsic calculations
                List<MCvPoint3D32f> object_list = new List<MCvPoint3D32f>();
                for (int i = 0; i < chessboardModel.patternSize.Height; i++)
                {
                    for (int j = 0; j < chessboardModel.patternSize.Width; j++)
                    {
                        object_list.Add(new MCvPoint3D32f(j * 25.0F, i * 25.0F, 0.0F));
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

            //This will Show us the usable area from each camera
            MessageBox.Show("Left: " + Rec1.ToString() + " \nRight: " + Rec2.ToString());
            currentMode = ECalibrationMode.Calibrated;

        }

        private async Task SaveImageForCalibration()
        {
            VectorOfPointF cornerLeft = new VectorOfPointF();
            VectorOfPointF cornerRight = new VectorOfPointF();

            //Find the chessboard in bothe images
            CvInvoke.FindChessboardCorners(Gray_frame_S1, chessboardModel.patternSize, cornerLeft, CalibCbType.AdaptiveThresh);
            CvInvoke.FindChessboardCorners(Gray_frame_S2, chessboardModel.patternSize, cornerRight, CalibCbType.AdaptiveThresh);
            
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
                if (chessboardModel.start_Flag)
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
                frameImage_S1.Draw(new CircleF(corners_Left[0], 3), new Bgr(Color.Yellow), 1);
                frameImage_S2.Draw(new CircleF(corners_Right[0], 3), new Bgr(Color.Yellow), 1);
                for (int i = 1; i < corners_Left.Length; i++)
                {
                    //left
                    frameImage_S1.Draw(new LineSegment2DF(corners_Left[i - 1], corners_Left[i]), chessboardModel.line_colour_array[i], 2);
                    frameImage_S1.Draw(new CircleF(corners_Left[i], 3), new Bgr(Color.Yellow), 1);
                    //right
                    frameImage_S2.Draw(new LineSegment2DF(corners_Right[i - 1], corners_Right[i]), chessboardModel.line_colour_array[i], 2);
                    frameImage_S2.Draw(new CircleF(corners_Right[i], 3), new Bgr(Color.Yellow), 1);
                }
                //calibrate the delay bassed on size of buffer
                //if buffer small you want a big delay if big small delay
                await Task.Delay(500);//allow the user to move the board to a different position
            }
        }
    }
}
