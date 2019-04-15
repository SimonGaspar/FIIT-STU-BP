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
        public static List<nvmModel> LoadPointCloud()
        {
            string filePath = Path.Combine($"..\\..\\..\\Temp", $"Result.nvm");
            var subjectString = File.ReadAllText(filePath);
            var lineArray = subjectString.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var resultList = new List<nvmModel>();
            string header = lineArray[0];

            for (int i = 1; i < lineArray.Count();)
            {
                var model = new nvmModel();
                model.imageCount = int.Parse(lineArray[i++]);

                if (model.imageCount == 0) break;

                i = AddCameraModelFromFile(i, lineArray, model.imageCount, model);


                model.pointCount = int.Parse(lineArray[i++]);
                if (model.pointCount == 0) break;

                i = AddPointModelFromFile(i, lineArray, model.pointCount, model);

                resultList.Add(model);
            }


            return resultList;
        }

        /// <summary>
        /// Loading points from nvm file.
        /// </summary>
        /// <param name="i">Number of line</param>
        /// <param name="lineArray">Array of lines (nvm file)</param>
        /// <param name="pointCount">Number of points in file</param>
        /// <param name="model">Current model in which we add 3D points.</param>
        /// <returns>Number of line, where it ends.</returns>
        private static int AddPointModelFromFile(int i, string[] lineArray, int pointCount, nvmModel model)
        {
            int y = 0;
            for (y = i; y < i + model.pointCount; y++)
            {
                var camera = lineArray[y].Split();
                var pointModel = new nvmPointModel()
                {
                    position = new Vector3(
                        float.Parse(camera[0]),
                        float.Parse(camera[1]),
                        float.Parse(camera[2])
                        ),
                    color = new Vector3(
                        float.Parse(camera[3]),
                        float.Parse(camera[4]),
                        float.Parse(camera[5])
                        )
                };
                model.listPointModel.Add(pointModel);
            }
            return y;
        }

        /// <summary>
        /// Loading cameras from nvm file
        /// </summary>
        /// /// <param name="i">Number of line</param>
        /// <param name="lineArray">Array of lines (nvm file)</param>
        /// <param name="cameraCount">Number of cameras in file</param>
        /// <param name="model">Current model in which we add 3D points.</param>
        /// <returns>Number of line, where it ends.</returns>
        private static int AddCameraModelFromFile(int i, string[] lineArray, int cameraCount, nvmModel model)
        {
            int x = 0;
            for (x = i; x < i + cameraCount; x++)
            {
                var camera = lineArray[x].Split();
                var imageModel = new nvmCameraModel()
                {
                    fileName = camera[0],
                    focalLength = float.Parse(camera[1]),
                    quaternion = new Quaternion(
                        float.Parse(camera[2]),
                        float.Parse(camera[3]),
                        float.Parse(camera[4]),
                        float.Parse(camera[5])
                        ),
                    cameraCenter = new Vector3(
                        float.Parse(camera[6]),
                        float.Parse(camera[7]),
                        float.Parse(camera[8])
                    ),
                    radialDistortion = float.Parse(camera[9])
                };
                model.listImageModel.Add(imageModel);
            }
            return x;
        }
    }
}
