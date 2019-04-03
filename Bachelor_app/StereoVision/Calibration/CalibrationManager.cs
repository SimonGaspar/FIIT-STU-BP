using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Bachelor_app.Manager;
using Bachelor_app.StereoVision.Calibration;
using Emgu.CV;
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
            _leftCamera = new VideoCapture(cameraManager.LeftCamera.ID);
            _rightCamera = new VideoCapture(cameraManager.RightCamera.ID);

            _winForm = new CalibrationForm(this);
        }


        /// <summary>
        /// Is called to process frame from camera
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        public void ProcessFrame(object sender, EventArgs arg)
        {
            #region Frame Aquasition
            //Aquire the frames or calculate two frames from one camera
            _leftCamera.Retrieve(frame_S1);
            frameImage_S1 = new Image<Bgr, byte>(frame_S1.Bitmap);
            Gray_frame_S1 = frameImage_S1.Convert<Gray, Byte>();
            _rightCamera.Retrieve(frame_S2);
            frameImage_S2 = new Image<Bgr, byte>(frame_S2.Bitmap);
            Gray_frame_S2 = frameImage_S2.Convert<Gray, Byte>();
            #endregion

            #region Saving Chessboard Corners in Buffer
            if (currentMode == ECalibrationMode.SavingFrames)
            {
                VectorOfPointF cornerLeft = new VectorOfPointF();
                VectorOfPointF cornerRight = new VectorOfPointF();
                //Find the chessboard in bothe images
                CvInvoke.FindChessboardCorners(Gray_frame_S1, chessboardModel.patternSize, cornerLeft);
                CvInvoke.FindChessboardCorners(Gray_frame_S2, chessboardModel.patternSize, cornerRight);

                chessboardModel.corners_Left = cornerLeft.ToArray();
                chessboardModel.corners_Left = cornerRight.ToArray();
                //we use this loop so we can show a colour image rather than a gray: //CameraCalibration.DrawChessboardCorners(Gray_Frame, patternSize, corners);
                //we we only do this is the chessboard is present in both images
                if (chessboardModel.corners_Left != null && chessboardModel.corners_Right != null) //chess board found in one of the frames?
                {
                    //make mesurments more accurate by using FindCornerSubPixel
                    Gray_frame_S1.FindCornerSubPix(new PointF[1][] { chessboardModel.corners_Left }, new Size(11, 11), new Size(-1, -1), new MCvTermCriteria(30, 0.01));
                    Gray_frame_S2.FindCornerSubPix(new PointF[1][] { chessboardModel.corners_Right }, new Size(11, 11), new Size(-1, -1), new MCvTermCriteria(30, 0.01));

                    //if go button has been pressed start aquiring frames else we will just display the points
                    if (chessboardModel.start_Flag)
                    {
                        //save the calculated points into an array
                        corners_points_Left[buffer_savepoint] = chessboardModel.corners_Left;
                        corners_points_Right[buffer_savepoint] = chessboardModel.corners_Right;
                        buffer_savepoint++;//increase buffer positon

                        //check the state of buffer
                        if (buffer_savepoint == buffer_length) currentMode = ECalibrationMode.Caluculating_Stereo_Intrinsics; //buffer full

                        //Show state of Buffer
                        _winForm.UpdateTitle("Form1: Buffer " + buffer_savepoint.ToString() + " of " + buffer_length.ToString());
                    }

                    //draw the results
                    frameImage_S1.Draw(new CircleF(chessboardModel.corners_Left[0], 3), new Bgr(Color.Yellow), 1);
                    frameImage_S2.Draw(new CircleF(chessboardModel.corners_Right[0], 3), new Bgr(Color.Yellow), 1);
                    for (int i = 1; i < chessboardModel.corners_Left.Length; i++)
                    {
                        //left
                        frameImage_S1.Draw(new LineSegment2DF(chessboardModel.corners_Left[i - 1], chessboardModel.corners_Left[i]), chessboardModel.line_colour_array[i], 2);
                        frameImage_S1.Draw(new CircleF(chessboardModel.corners_Left[i], 3), new Bgr(Color.Yellow), 1);
                        //right
                        frameImage_S2.Draw(new LineSegment2DF(chessboardModel.corners_Right[i - 1], chessboardModel.corners_Right[i]), chessboardModel.line_colour_array[i], 2);
                        frameImage_S2.Draw(new CircleF(chessboardModel.corners_Right[i], 3), new Bgr(Color.Yellow), 1);
                    }
                    //calibrate the delay bassed on size of buffer
                    //if buffer small you want a big delay if big small delay
                    Thread.Sleep(100);//allow the user to move the board to a different position
                }
                chessboardModel.corners_Left = null;
                chessboardModel.corners_Right = null;
            }
            #endregion
            #region Calculating Stereo Cameras Relationship
            if (currentMode == ECalibrationMode.Caluculating_Stereo_Intrinsics)
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
                            object_list.Add(new MCvPoint3D32f(j * 20.0F, i * 20.0F, 0.0F));
                        }
                    }
                    corners_object_Points[k] = object_list.ToArray();
                }
                //If Emgu.CV.CvEnum.CALIB_TYPE == CV_CALIB_USE_INTRINSIC_GUESS and/or CV_CALIB_FIX_ASPECT_RATIO are specified, some or all of fx, fy, cx, cy must be initialized before calling the function
                //if you use FIX_ASPECT_RATIO and FIX_FOCAL_LEGNTH options, these values needs to be set in the intrinsic parameters before the CalibrateCamera function is called. Otherwise 0 values are used as default.
                CameraCalibration.StereoCalibrate(corners_object_Points, corners_points_Left, corners_points_Right, calibrationModel.IntrinsicCam1, calibrationModel.IntrinsicCam2, frame_S1.Size,
                                                                 Emgu.CV.CvEnum.CalibType.Default, new MCvTermCriteria(0.1e5),
                                                                 out EX_Param, out fundamental, out essential);

                calibrationModel.EX_Param = EX_Param;
                calibrationModel.fundamental = fundamental;
                calibrationModel.essential = essential;
                MessageBox.Show("Intrinsic Calculation Complete"); //display that the mothod has been succesful
                //currentMode = Mode.Calibrated;

                //Computes rectification transforms for each head of a calibrated stereo camera.
                CvInvoke.StereoRectify(calibrationModel.IntrinsicCam1.IntrinsicMatrix, calibrationModel.IntrinsicCam2.IntrinsicMatrix,
                                         calibrationModel.IntrinsicCam1.DistortionCoeffs, calibrationModel.IntrinsicCam2.DistortionCoeffs,
                                         frame_S1.Size,
                                         calibrationModel.EX_Param.RotationVector.RotationMatrix, calibrationModel.EX_Param.TranslationVector,
                                         calibrationModel.R1, calibrationModel.R2, calibrationModel.P1, calibrationModel.P2, calibrationModel.Q,
                                         Emgu.CV.CvEnum.StereoRectifyType.Default, 0,
                                         frame_S1.Size, ref Rec1, ref Rec2);

                calibrationModel.Rec1 = Rec1;
                calibrationModel.Rec2 = Rec2;

                //This will Show us the usable area from each camera
                MessageBox.Show("Left: " + Rec1.ToString() + " \nRight: " + Rec2.ToString());
                currentMode = ECalibrationMode.Calibrated;

            }
            #endregion
            #region Caluclating disparity map after calibration
            if (currentMode == ECalibrationMode.Calibrated)
            {
                Image<Gray, short> disparityMap;

                Computer3DPointsFromStereoPair(Gray_frame_S1, Gray_frame_S2, out disparityMap, out _points);
                calibrationModel._points = _points;

                //Display the disparity map
                _winForm.DisparityMap.Image = disparityMap.ToBitmap();
                //Draw the accurate area
                frameImage_S1.Draw(Rec1, new Bgr(Color.LimeGreen), 1);
                frameImage_S2.Draw(Rec2, new Bgr(Color.LimeGreen), 1);
            }
            #endregion
            //display image
            _winForm.Video_Source1.Image = frameImage_S1.Bitmap;
            _winForm.Video_Source2.Image = frameImage_S2.Bitmap;


        }

        /// <summary>
        /// Given the left and right image, computer the disparity map and the 3D point cloud.
        /// </summary>
        /// <param name="left">The left image</param>
        /// <param name="right">The right image</param>
        /// <param name="disparityMap">The left disparity map</param>
        /// <param name="points">The 3D point cloud within a [-0.5, 0.5] cube</param>
        private void Computer3DPointsFromStereoPair(Image<Gray, Byte> left, Image<Gray, Byte> right, out Image<Gray, short> disparityMap, out MCvPoint3D32f[] points)
        {
            Size size = left.Size;

            disparityMap = new Image<Gray, short>(size);
            //thread safe calibration values


            /*This is maximum disparity minus minimum disparity. Always greater than 0. In the current implementation this parameter must be divisible by 16.*/
            int numDisparities = _winForm.GetSliderValue(_winForm.Num_Disparities);

            /*The minimum possible disparity value. Normally it is 0, but sometimes rectification algorithms can shift images, so this parameter needs to be adjusted accordingly*/
            int minDispatities = _winForm.GetSliderValue(_winForm.Min_Disparities);

            /*The matched block size. Must be an odd number >=1 . Normally, it should be somewhere in 3..11 range*/
            int SAD = _winForm.GetSliderValue(_winForm.SAD_Window);

            /*P1, P2 – Parameters that control disparity smoothness. The larger the values, the smoother the disparity. 
             * P1 is the penalty on the disparity change by plus or minus 1 between neighbor pixels. 
             * P2 is the penalty on the disparity change by more than 1 between neighbor pixels. 
             * The algorithm requires P2 > P1 . 
             * See stereo_match.cpp sample where some reasonably good P1 and P2 values are shown 
             * (like 8*number_of_image_channels*SADWindowSize*SADWindowSize and 32*number_of_image_channels*SADWindowSize*SADWindowSize , respectively).*/

            int P1 = 8 * 1 * SAD * SAD;//GetSliderValue(P1_Slider);
            int P2 = 32 * 1 * SAD * SAD;//GetSliderValue(P2_Slider);

            /* Maximum allowed difference (in integer pixel units) in the left-right disparity check. Set it to non-positive value to disable the check.*/
            int disp12MaxDiff = _winForm.GetSliderValue(_winForm.Disp12MaxDiff);

            /*Truncation value for the prefiltered image pixels. 
             * The algorithm first computes x-derivative at each pixel and clips its value by [-preFilterCap, preFilterCap] interval. 
             * The result values are passed to the Birchfield-Tomasi pixel cost function.*/
            int PreFilterCap = _winForm.GetSliderValue(_winForm.pre_filter_cap);

            /*The margin in percents by which the best (minimum) computed cost function value should “win” the second best value to consider the found match correct. 
             * Normally, some value within 5-15 range is good enough*/
            int UniquenessRatio = _winForm.GetSliderValue(_winForm.uniquenessRatio);

            /*Maximum disparity variation within each connected component. 
             * If you do speckle filtering, set it to some positive value, multiple of 16. 
             * Normally, 16 or 32 is good enough*/
            int Speckle = _winForm.GetSliderValue(_winForm.Speckle_Window);

            /*Maximum disparity variation within each connected component. If you do speckle filtering, set it to some positive value, multiple of 16. Normally, 16 or 32 is good enough.*/
            int SpeckleRange = _winForm.GetSliderValue(_winForm.specklerange);

            /*Set it to true to run full-scale 2-pass dynamic programming algorithm. It will consume O(W*H*numDisparities) bytes, 
             * which is large for 640x480 stereo and huge for HD-size pictures. By default this is usually false*/
            //Set globally for ease
            //bool fullDP = true;

            using (StereoSGBM stereoSolver = new StereoSGBM(minDispatities, numDisparities, SAD, P1, P2, disp12MaxDiff, PreFilterCap, UniquenessRatio, Speckle, SpeckleRange, StereoSGBM.Mode.HH))
            //using (StereoBM stereoSolver = new StereoBM(Emgu.CV.CvEnum.STEREO_BM_TYPE.BASIC, 0))
            {
                stereoSolver.Compute(left, right, disparityMap);//Computes the disparity map using: 
                /*GC: graph cut-based algorithm
                  BM: block matching algorithm
                  SGBM: modified H. Hirschmuller algorithm HH08*/
                points = PointCollection.ReprojectImageTo3D(disparityMap, calibrationModel.Q); //Reprojects disparity image to 3D space.
            }
        }


    }
}
