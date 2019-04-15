using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_app
{
    public static class Configuration
    {
        public static string TempDirectoryPath { get; private set; } = Path.GetFullPath($"..\\..\\..\\Temp");
        public static string TempDepthMapDirectoryPath { get; private set; } = Path.Combine(TempDirectoryPath, "DepthMap");
        public static string TempLeftStackDirectoryPath { get; private set; } = Path.Combine(TempDirectoryPath, "LeftStack");
        public static string TempRightStackDirectoryPath { get; private set; } = Path.Combine(TempDirectoryPath, "RightStack");

        public static void GenerateFolders() {
            Directory.CreateDirectory(TempDirectoryPath);
            Directory.CreateDirectory(TempDepthMapDirectoryPath);
            Directory.CreateDirectory(TempLeftStackDirectoryPath);
            Directory.CreateDirectory(TempRightStackDirectoryPath);
        }
    }
}
