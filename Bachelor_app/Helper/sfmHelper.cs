using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using Bachelor_app.Model;

namespace Bachelor_app.Helper
{
    /// <summary>
    /// Helper for SfM (Structure from Motion)
    /// </summary>
    public static class SfMHelper
    {
        /// <summary>
        /// Loading point cloud from nvm file.
        /// </summary>
        /// <returns>List of nvm models<returns>
        public static List<NvmModel> LoadPointCloud()
        {
            var subjectString = File.ReadAllText(Configuration.VisualSFMResultPath);
            var lineArray = subjectString.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var resultList = new List<NvmModel>();
            //string header = lineArray[0];

            for (int indexOfLine = 1; indexOfLine < lineArray.Count();)
            {
                var model = new NvmModel();

                model.ImageCount = int.Parse(lineArray[indexOfLine++]);
                if (model.ImageCount > 0)
                    indexOfLine = AddCameraModelFromFile(indexOfLine, lineArray, model.ImageCount, model);

                model.PointCount = int.Parse(lineArray[indexOfLine++]);
                if (model.PointCount > 0)
                    indexOfLine = AddPointModelFromFile(indexOfLine, lineArray, model.PointCount, model);

                resultList.Add(model);
            }

            return resultList;
        }

        /// <summary>
        /// Loading points from nvm file.
        /// </summary>
        /// <param name="IndexOfLine">Number of line</param>
        /// <param name="lineArray">Array of lines (nvm file)</param>
        /// <param name="pointCount">Number of points in file</param>
        /// <param name="model">Current model in which we add 3D points.</param>
        /// <returns>Number of line, where it ends.</returns>
        private static int AddPointModelFromFile(int IndexOfLine, string[] lineArray, int pointCount, NvmModel model)
        {
            int currentIndex = 0;
            for (currentIndex = IndexOfLine; currentIndex < IndexOfLine + model.PointCount; currentIndex++)
            {
                var camera = lineArray[currentIndex].Split();
                var pointModel = new NvmPointModel()
                {
                    Position = new Vector3(
                        float.Parse(camera[0]),
                        float.Parse(camera[1]),
                        float.Parse(camera[2])
                        ),
                    Color = new Vector3(
                        float.Parse(camera[3]),
                        float.Parse(camera[4]),
                        float.Parse(camera[5])
                        )
                };
                model.ListPointModel.Add(pointModel);
            }
            return currentIndex;
        }

        /// <summary>
        /// Loading cameras from nvm file
        /// </summary>
        /// /// <param name="IndexOfLine">Number of line</param>
        /// <param name="lineArray">Array of lines (nvm file)</param>
        /// <param name="cameraCount">Number of cameras in file</param>
        /// <param name="model">Current model in which we add 3D points.</param>
        /// <returns>Number of line, where it ends.</returns>
        private static int AddCameraModelFromFile(int IndexOfLine, string[] lineArray, int cameraCount, NvmModel model)
        {
            int currentIndex = 0;
            for (currentIndex = IndexOfLine; currentIndex < IndexOfLine + cameraCount; currentIndex++)
            {
                var camera = lineArray[currentIndex].Split();
                var imageModel = new NvmCameraModel()
                {
                    FileName = camera[0],
                    FocalLength = float.Parse(camera[1]),
                    Quaternion = new Quaternion(
                        float.Parse(camera[2]),
                        float.Parse(camera[3]),
                        float.Parse(camera[4]),
                        float.Parse(camera[5])
                        ),
                    CameraCenter = new Vector3(
                        float.Parse(camera[6]),
                        float.Parse(camera[7]),
                        float.Parse(camera[8])
                    ),
                    RadialDistortion = float.Parse(camera[9])
                };
                model.ListImageModel.Add(imageModel);
            }
            return currentIndex;
        }
    }
}
