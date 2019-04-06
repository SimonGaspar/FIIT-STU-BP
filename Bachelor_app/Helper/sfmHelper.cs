using Bachelor_app.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bachelor_app.Helper
{
    public static class sfmHelper
    {
        public static List<nvmModel> LoadPointCloud()
        {
            string header;

            string filePath = Path.Combine($"..\\..\\..\\Temp", $"Result.nvm");
            var subjectString = File.ReadAllText(filePath);
            var lineArray = subjectString.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var resultList = new List<nvmModel>();

            for (int i = 1; i < lineArray.Count();)
            {
                var model = new nvmModel();
                model.imageCount = int.Parse(lineArray[i++]);

                if (model.imageCount == 0) break;

                i = AddCameraModelFromFile(i, lineArray, model.imageCount,model);


                model.pointCount = int.Parse(lineArray[i++]);
                if (model.pointCount == 0) break;

                i = AddPointModelFromFile(i, lineArray, model.pointCount, model);

                resultList.Add(model);
            }
            

            return resultList;
        }

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

        private static int AddCameraModelFromFile(int iter, string[] lineArray, int cameraCount, nvmModel model )
        {
            int x = 0;
            for (x = iter; x < iter + cameraCount; x++)
            {
                var camera = lineArray[x].Split();
                var imageModel = new nvmImageModel()
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
