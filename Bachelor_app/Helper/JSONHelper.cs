using System.IO;
using Newtonsoft.Json;

namespace Bachelor_app.Helper
{
    public static class JSONHelper
    {
        public static void SaveJson(dynamic jsonObject, string fileName, string filePath = null)
        {
            filePath = filePath ?? Configuration.TempDirectoryPath;
            var fullPath = Path.Combine(filePath, fileName);

            var json = JsonConvert.SerializeObject(jsonObject);
            File.WriteAllText(fullPath, json);
        }

        public static T LoadJson<T>(string fileName, string filePath = null)
        {
            filePath = filePath ?? Configuration.TempDirectoryPath;
            var fullPath = Path.Combine(filePath, fileName);

            var json = File.ReadAllText(fullPath);
            var jsonObject = JsonConvert.DeserializeObject<T>(json);
            return jsonObject;
        }
    }
}
