using Bakalárska_práca.Calibration;
using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakalárska_práca
{
    public class CalibrationManager
    {
        CameraModel camera;
        public CalibrationManager(CameraModel camera) {
            this.camera = camera;
        }

        public void ComputeCameraMatrix(Size size, double fieldOfViewX, double fieldOfViewY)
        {
            var tempMatrix = new Matrix<double>(3, 3);
            var fx = (size.Width / 2) / Math.Tan(fieldOfViewX / 2);
            var fy = (size.Height / 2) / Math.Tan(fieldOfViewY / 2);
            tempMatrix.Data = new double[,] { { fx, 0, size.Width }, { 0, fy, size.Height }, { 0, 0, 1 } };
            camera.CameraMatrix = tempMatrix.Mat;
        }

        public void ComputeCameraMatrix(int width, int height, double fieldOfViewX, double fieldOfViewY)
        {
            ComputeCameraMatrix(new Size(width, height), fieldOfViewX, fieldOfViewY);
        }

        public void CalculateFundamentalMatrix(VectorOfKeyPoint leftImage, VectorOfKeyPoint rightImage) {
            camera.F = CvInvoke.FindFundamentalMat(leftImage, rightImage);
        }

        public void CalculateEssentialMAtrix(VectorOfKeyPoint leftImage, VectorOfKeyPoint rightImage, Mat cameraMatrix) {
            camera.E = CvInvoke.FindEssentialMat(leftImage, rightImage, cameraMatrix);
        }

        public void FindFeaturePointsBetweenTwoImages() {
            var filename = "";
            var filename2 = "";
            var orb = new ORBDetector(2000);
            Image<Bgr, byte> left = new Image<Bgr, byte>(filename);
            Image<Bgr, byte> right = new Image<Bgr, byte>(filename2);
            var vectorLeft = new VectorOfKeyPoint();
            var vectorRight = new VectorOfKeyPoint();
            var matLeft = new Mat();
            var matRight = new Mat();

            orb.DetectAndCompute(left, null, vectorLeft, matLeft, false);
            orb.DetectAndCompute(right, null, vectorRight, matRight, false);

            var matcher = new BFMatcher(DistanceType.Hamming2, true);
            var matches = new VectorOfVectorOfDMatch();
            matcher.Add(matLeft);
            matcher.KnnMatch(matRight, matches, 1, null);
            

            CalculateEssentialMAtrix(vectorLeft,vectorRight,camera.CameraMatrix);
            CalculateFundamentalMatrix(vectorLeft, vectorRight);
        }


    }
}
