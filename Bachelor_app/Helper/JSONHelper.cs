using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_app.Helper
{
    public static class JSONHelper
    {
        public static void SaveJson(dynamic JsonObject, string FileName, string FilePath = null)
        {

        FilePath = FilePath ?? Configuration.TempDirectoryPath;
        var fullPath = Path.Combine(FilePath, FileName);

        var json = JsonConvert.SerializeObject(JsonObject);
        File.WriteAllText(fullPath, json);
        }

        public static T LoadJson<T>(string FileName, string FilePath = null)
        {
            FilePath = FilePath ?? Configuration.TempDirectoryPath;
            var fullPath = Path.Combine(FilePath, FileName);

            var json = File.ReadAllText(fullPath);
            var jsonObject = JsonConvert.DeserializeObject<T>(json);
            return jsonObject;
        }
    }
}
