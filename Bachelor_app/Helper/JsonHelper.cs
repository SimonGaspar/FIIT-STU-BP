using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_app.Helper
{
    public static class JsonHelper
    {
        public static void SaveJson(dynamic JsonObject, string FileName, string FilePath = null) {

            FilePath = FilePath ?? Configuration.TempDirectoryPath;
            var fullPath = Path.Combine(FilePath, FileName);

            var json = JsonConvert.SerializeObject(JsonObject);
            File.WriteAllText(fullPath, json);
        }

        public static T LoadJson<T>( string FileName, string FilePath = null) {
            FilePath = FilePath ?? Configuration.TempDirectoryPath;
            var fullPath = Path.Combine(FilePath, FileName);

            var json = File.ReadAllText(fullPath);
            var jsonObject = JsonConvert.DeserializeObject<T>(json);
            return jsonObject;
        }

        /// <summary>
        /// Save datatable to csv.
        /// </summary>
        /// <param name="dataTableToCsv">DataTable to save.</param>
        public static void ExportToCSV(this DataTable dataTableToCsv, string FileName, string FilePath=null)
        {
            FilePath = FilePath ?? Configuration.TempDirectoryPath;
            var fullPath = Path.Combine(FilePath, FileName);

            var sb = new StringBuilder();
            var columnNames = dataTableToCsv.Columns.Cast<DataColumn>().Select(column => "\"" + column.ColumnName + "\"");
            var delimiter = ",";

            sb.AppendLine(string.Join(delimiter, columnNames));

            foreach (DataRow row in dataTableToCsv.Rows)
            {
                var fields = row.ItemArray.Select(field => "\"" + field.ToString() + "\"");
                sb.AppendLine(string.Join(delimiter, fields));
            }

            File.WriteAllText(fullPath, sb.ToString(), Encoding.UTF8);
        }
    }
}
