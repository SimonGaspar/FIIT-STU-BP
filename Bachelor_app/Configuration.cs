using System;
using System.IO;

namespace Bachelor_app
{
    public static class Configuration
    {
        #region Temp directory and subdirectory
        public static string TempDirectoryPath { get; private set; } = Path.GetFullPath($"..\\..\\..\\Temp");
        public static string TempDepthMapDirectoryPath { get; private set; } = Path.Combine(TempDirectoryPath, "DepthMap");
        public static string TempLeftStackDirectoryPath { get; private set; } = Path.Combine(TempDirectoryPath, "LeftStack");
        public static string TempRightStackDirectoryPath { get; private set; } = Path.Combine(TempDirectoryPath, "RightStack");
        public static string TempDrawKeypoint { get; private set; } = Path.Combine(Configuration.TempDirectoryPath, "DrawKeypoint");
        public static string TempDrawMatches { get; private set; } = Path.Combine(Configuration.TempDirectoryPath, "DrawMatch");
        #endregion
        #region Match file
        public static string MatchFileName { get; private set; } = "AllFoundedMatches.txt";
        public static string MatchFilePath { get; private set; } = Path.Combine(TempDirectoryPath, MatchFileName);
        #endregion
        #region Tools
        public static string ToolsPath { get; private set; } = Path.GetFullPath(".\\Tools");
        public static string VisualSFMTool { get; private set; } = "VisualSFM.exe";
        public static string VisualSFMPath { get; private set; } = Path.Combine(ToolsPath, "VisualSFM_Cuda");
        public static string VisualSFMToolPath { get; private set; } = Path.Combine(VisualSFMPath, VisualSFMTool);
        public static string VisualSFMResult { get; private set; } = "Result.nvm";
        public static string VisualSFMResultPath { get; private set; } = Path.Combine(Configuration.TempDirectoryPath, VisualSFMResult);
        #endregion
        #region Calibration
        public static string CalibrationName { get; private set; } = "Calibration.json";
        public static string CalibrationPath { get; private set; } = Path.Combine(TempDirectoryPath, CalibrationName);
        #endregion

        #region We can move some properties into configuration
        //public static ECameraResolution Resolution { get; set; }
        //public static EDisplayItem LeftViewWindowItem { get; set; }
        //public static EDisplayItem RightViewWindowItem { get; set; }
        //public static bool DisplayRemapImage { get; set; } = false;
        //public static EListViewGroup ListViewerDisplay { get; set; } = EListViewGroup.Console;
        //public static EInput _inputType { get; set; } = EInput.ListViewBasicStack;
        //public static ListViewModel ListViewModel { get; private set; } = new ListViewModel();
        #endregion

        public static void GenerateFolders()
        {
            //if (Directory.Exists(TempDirectoryPath))
            //    DeleteTempFolder();

            Directory.CreateDirectory(TempDirectoryPath);
            Directory.CreateDirectory(TempDepthMapDirectoryPath);
            Directory.CreateDirectory(TempLeftStackDirectoryPath);
            Directory.CreateDirectory(TempRightStackDirectoryPath);
            Directory.CreateDirectory(TempDrawKeypoint);
            Directory.CreateDirectory(TempDrawMatches);
        }

        public static void DeleteTempFolder()
        {
            try
            {
                if (Directory.Exists(TempDirectoryPath))
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
                    di.Delete();
                }
            }
            catch (Exception e) { }
            }
    }
}
