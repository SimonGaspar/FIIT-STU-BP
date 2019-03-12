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
            var path = @"C:\Users\Notebook\Desktop\ImageDataset_SceauxCastle-master\images";

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

            var drawMatches = new Mat();
            Features2DToolbox.DrawMatches(imageLeft, leftKeyPoints, imageRight, rightKeyPoints,  matches, drawMatches, new MCvScalar(0, 0, 255,128), new MCvScalar(0, 255, 0,128), null,Features2DToolbox.KeypointDrawType.DrawRichKeypoints);
            drawMatches.Save(Path.Combine(path, "Pokus.jpg"));

        }
    }
}
