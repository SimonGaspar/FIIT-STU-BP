using System.IO;

namespace Bachelor_app
{
    public static class Configuration
    {
        public static string TempDirectoryPath { get; private set; } = Path.GetFullPath($"..\\..\\..\\Temp");
        public static string TempDepthMapDirectoryPath { get; private set; } = Path.Combine(TempDirectoryPath, "DepthMap");
        public static string TempLeftStackDirectoryPath { get; private set; } = Path.Combine(TempDirectoryPath, "LeftStack");
        public static string TempRightStackDirectoryPath { get; private set; } = Path.Combine(TempDirectoryPath, "RightStack");
        public static string TempDrawKeypoint { get; private set; } = Path.Combine(Configuration.TempDirectoryPath, "DrawKeypoint");
        public static string TempDrawMatches { get; private set; } = Path.Combine(Configuration.TempDirectoryPath, "DrawMatch");

        public static string MatchFileName { get; } = "AllFoundedMatches.txt";
        public static string MatchFilePath { get; } = Path.Combine(TempDirectoryPath, MatchFileName);

        public static string ToolsPath { get; private set; } = Path.GetFullPath(".\\Tools");
        public static string VisualSFMTool { get; } = "VisualSFM.exe";
        public static string VisualSFMPath { get; } = Path.Combine(ToolsPath, "VisualSFM_Cuda");
        public static string VisualSFMToolPath { get; } = Path.Combine(VisualSFMPath, VisualSFMTool);

        public static string VisualSFMResult { get; } = "Result.nvm";
        public static string VisualSFMResultPath { get; } = Path.Combine(Configuration.TempDirectoryPath, VisualSFMResult);

        public static void GenerateFolders()
        {
            Directory.CreateDirectory(TempDirectoryPath);
            Directory.CreateDirectory(TempDepthMapDirectoryPath);
            Directory.CreateDirectory(TempLeftStackDirectoryPath);
            Directory.CreateDirectory(TempRightStackDirectoryPath);
            Directory.CreateDirectory(TempDrawKeypoint);
            Directory.CreateDirectory(TempDrawMatches);
        }

        public static void DeleteTempFolder()
        {
            DirectoryInfo di = new DirectoryInfo(TempDirectoryPath);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }

            GenerateFolders();
        }
    }
}
