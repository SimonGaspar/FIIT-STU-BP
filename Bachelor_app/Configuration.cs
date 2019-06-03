using System;
using System.IO;
using Bachelor_app.Helper;

namespace Bachelor_app
{
    public static class Configuration
    {
        #region Temp directory and subdirectory
        public static string TempDirectoryPath => Path.GetFullPath($".\\Temp");

        public static string TempComputedDescriptorDirectoryPath => Path.Combine(TempDirectoryPath, "ComputedDescriptors");

        public static string TempComputedMatchesMapDirectoryPath => Path.Combine(TempDirectoryPath, "ComputedMatches");

        public static string TempDepthMapDirectoryPath => Path.Combine(TempDirectoryPath, "DepthMap");

        public static string TempLeftStackDirectoryPath => Path.Combine(TempDirectoryPath, "LeftStack");

        public static string TempRightStackDirectoryPath => Path.Combine(TempDirectoryPath, "RightStack");

        public static string TempDrawKeypoint => Path.Combine(TempDirectoryPath, "DrawKeypoint");

        public static string TempDrawMatches => Path.Combine(TempDirectoryPath, "DrawMatch");
        #endregion

        #region Match file
        public static string MatchFileName => "AllFoundedMatches.txt";

        public static string MatchFilePath => Path.Combine(TempDirectoryPath, MatchFileName);
        #endregion

        #region Tools
        public static string ToolsPath => Path.GetFullPath(".\\Tools");

        public static string VisualSFMTool => "VisualSFM.exe";

        public static string VisualSFMPath => Path.Combine(ToolsPath, "VisualSFM_Cuda");

        public static string VisualSFMToolPath => Path.Combine(VisualSFMPath, VisualSFMTool);

        public static string VisualSFMResult => "Result.nvm";

        public static string VisualSFMResultPath => Path.Combine(TempDirectoryPath, VisualSFMResult);
        #endregion

        #region Calibration
        public static string CalibrationName => "Calibration.json";

        public static string CalibrationPath => Path.Combine(TempDirectoryPath, CalibrationName);
        #endregion

        public const bool SaveImagesFromProcess = true;

        #region We can move some properties into configuration

        // public static ECameraResolution Resolution { get; set; }
        // public static EDisplayItem LeftViewWindowItem { get; set; }
        // public static EDisplayItem RightViewWindowItem { get; set; }
        // public static bool DisplayRemapImage { get; set; } = false;
        // public static EListViewGroup ListViewerDisplay { get; set; } = EListViewGroup.Console;
        // public static EInput _inputType { get; set; } = EInput.ListViewBasicStack;
        // public static ListViewModel ListViewModel { get; private set; } = new ListViewModel();
        #endregion

        public static void GenerateFolders(bool tryingAgain = false)
        {
            try
            {
                Directory.CreateDirectory(TempDirectoryPath);
                Directory.CreateDirectory(TempDepthMapDirectoryPath);
                Directory.CreateDirectory(TempLeftStackDirectoryPath);
                Directory.CreateDirectory(TempRightStackDirectoryPath);
                Directory.CreateDirectory(TempDrawKeypoint);
                Directory.CreateDirectory(TempDrawMatches);

                // Directory.CreateDirectory(TempComputedDescriptorDirectoryPath);
                // Directory.CreateDirectory(TempComputedMatchesMapDirectoryPath);
            }
            catch (Exception e)
            {
                if (!tryingAgain)
                    GenerateFolders(true);
                else
                    throw e;
            }
        }

        public static void DeleteTempFolder()
        {
            try
            {
                if (Directory.Exists(TempDirectoryPath))
                {
                    DirectoryInfo di = new DirectoryInfo(TempDirectoryPath);

                    foreach (FileInfo file in di.GetFiles())
                        file.Delete();

                    foreach (DirectoryInfo dir in di.GetDirectories())
                        dir.Delete(true);

                    di.Delete();
                }
            }
            catch (Exception)
            {
                WindowsFormHelper.AddLogToConsole($"Can't clear temp folder: {TempDirectoryPath}\n");
            }
        }
    }
}
