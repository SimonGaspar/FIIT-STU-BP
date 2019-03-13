using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakalárska_práca
{
    public class SfM
    {
        public SfM() {
            var path = @"C:\Users\simon.gaspar\Desktop\Images";

            var orb = new ORBDetector(10,edgeThreshold:240);
            var imageLeft = new Mat(Path.Combine(path, "100_7100.JPG"));
            var imageRight = new Mat(Path.Combine(path, "100_7101.JPG"));

            var leftKeyPoints = new VectorOfKeyPoint();
            var rightKeyPoints = new VectorOfKeyPoint();
            var leftDescriptor = new Mat();
            var rightDescriptor = new Mat();

            orb.DetectAndCompute(imageLeft,null,leftKeyPoints,leftDescriptor,false);
            orb.DetectAndCompute(imageRight, null, rightKeyPoints, rightDescriptor, false);

            var matcher = new BFMatcher(DistanceType.Hamming2, true);
            var matches = new VectorOfVectorOfDMatch();
            matcher.Add(leftDescriptor);
            matcher.KnnMatch(rightDescriptor, matches, 1, null);

            var Mask = new Mat();
            var PerspectiveMatrix = new Mat();
            PerspectiveMatrix = CvInvoke.FindHomography(leftKeyPoints, rightKeyPoints, Emgu.CV.CvEnum.HomographyMethod.Ransac, 3, Mask);


            var drawMatches = new Mat();
            Features2DToolbox.DrawMatches(imageLeft, leftKeyPoints, imageRight, rightKeyPoints,  matches, drawMatches, new MCvScalar(0, 0, 255,128), new MCvScalar(0, 255, 0,128), Mask, Features2DToolbox.KeypointDrawType.DrawRichKeypoints);
            drawMatches.Save(Path.Combine(path, "Pokus.jpg"));

        }

        //const string path = @"C:\Users\simon.gaspar\Desktop\Image";
        //static void Main(string[] args)
        //{
        //    var orb = new ORBDetector(245400);
        //    var imageLeft = new Mat(Path.Combine(path, "100_7100.JPG"));
        //    var imageRight = new Mat(Path.Combine(path, "100_7101.JPG"));

        //    var leftKeyPoints = new VectorOfKeyPoint();
        //    var rightKeyPoints = new VectorOfKeyPoint();
        //    var leftDescriptor = new Mat();
        //    var rightDescriptor = new Mat();

        //    orb.DetectAndCompute(imageLeft, null, leftKeyPoints, leftDescriptor, false);
        //    orb.DetectAndCompute(imageRight, null, rightKeyPoints, rightDescriptor, false);

        //    var matcher = new BFMatcher(DistanceType.Hamming2, true);
        //    var matches = new VectorOfVectorOfDMatch();
        //    matcher.Add(leftDescriptor);
        //    matcher.KnnMatch(rightDescriptor, matches, 1, null);



        //    MDMatch[][] matchesArray = matches.ToArrayOfArray();
        //    VectorOfVectorOfDMatch filteredMatches = new VectorOfVectorOfDMatch();
        //    List<MDMatch[]> filteredMatchesList = new List<MDMatch[]>();
        //    float ms_MIN_RATIO = 0.8F, ms_MAX_DIST = 0, ms_MIN_DIST = float.MaxValue;

        //    ////Apply ratio test
        //    //for (int i = 0; i < matchesArray.Length; i++)
        //    //{
        //    //    MDMatch first = matchesArray[i][0];
        //    //    float dist1 = matchesArray[i][0].Distance;
        //    //    float dist2 = matchesArray[i][1].Distance;

        //    //        if (ms_MAX_DIST < dist1 || ms_MAX_DIST < dist2) ms_MAX_DIST = dist1 < dist2 ? dist2 : dist1;
        //    //        if (ms_MIN_DIST > dist1 || ms_MIN_DIST > dist2) ms_MIN_DIST = dist1 < dist2 ? dist1 : dist2;

        //    //    if (dist1 < ms_MIN_RATIO * dist2)
        //    //    {
        //    //        filteredMatchesList.Add(matchesArray[i]);
        //    //    }
        //    //}

        //    for (int i = 0; i < matchesArray.Length; i++)
        //    {
        //        if (matchesArray[i].Length == 0) continue;
        //        MDMatch first = matchesArray[i][0];
        //        float dist1 = matchesArray[i][0].Distance;

        //        if (ms_MAX_DIST < dist1) ms_MAX_DIST = dist1;
        //        if (ms_MIN_DIST > dist1) ms_MIN_DIST = dist1;
        //        filteredMatchesList.Add(matchesArray[i]);
        //    }


        //    //Filter by threshold
        //    MDMatch[][] defCopy = new MDMatch[filteredMatchesList.Count][];
        //    filteredMatchesList.CopyTo(defCopy);
        //    filteredMatchesList = new List<MDMatch[]>();

        //    foreach (var item in defCopy)
        //    {
        //        if (item[0].Distance < (((ms_MAX_DIST + ms_MIN_DIST) / 2) + (ms_MAX_DIST / 2)))
        //        {
        //            filteredMatchesList.Add(item);
        //        }
        //    }

        //    filteredMatches = new VectorOfVectorOfDMatch(filteredMatchesList.ToArray());


        //    var PerspectiveMatrix = new Mat();
        //    Mat Mask = new Mat();

        //    PerspectiveMatrix = new Help().GetHomography(leftKeyPoints, rightKeyPoints, filteredMatchesList, Mask);


        //    var drawMatches = new Mat();
        //    Features2DToolbox.DrawMatches(imageLeft, leftKeyPoints, imageRight, rightKeyPoints, filteredMatches, drawMatches, new MCvScalar(0, 0, 255, 128), new MCvScalar(0, 255, 0, 128), Mask, Features2DToolbox.KeypointDrawType.DrawRichKeypoints);
        //    drawMatches.Save(Path.Combine(path, "Pokus.jpg"));

        //}
        //public class Help
        //{
        //    public IOutputArray Mask { get; private set; }

        //    public Mat GetHomography(VectorOfKeyPoint keypointsModel, VectorOfKeyPoint keypointsTest, List<MDMatch[]> matches, Mat Mask)
        //    {
        //        MKeyPoint[] kptsModel = keypointsModel.ToArray();
        //        MKeyPoint[] kptsTest = keypointsTest.ToArray();

        //        PointF[] srcPoints = new PointF[matches.Count];
        //        PointF[] destPoints = new PointF[matches.Count];

        //        for (int i = 0; i < matches.Count; i++)
        //        {
        //            srcPoints[i] = kptsModel[matches[i][0].TrainIdx].Point;
        //            destPoints[i] = kptsTest[matches[i][0].QueryIdx].Point;
        //        }

        //        Mat homography = CvInvoke.FindHomography(srcPoints, destPoints, Emgu.CV.CvEnum.HomographyMethod.Ransac, 10, Mask);
        //        homography.Save(Path.Combine(path, "Homograph.jpg"));
        //        //PrintMatrix(homography);

        //        return homography;
        //    }
        //}
    }
}
