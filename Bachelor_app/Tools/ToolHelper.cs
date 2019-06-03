using System.Diagnostics;
using Bachelor_app.Helper;

namespace Bachelor_app.Tools
{
    public static class ToolHelper
    {
        public static void RunVisualSFM(bool continueProcess)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(Configuration.VisualSFMToolPath)
            {
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                UseShellExecute = false
            };

            startInfo.Arguments = continueProcess
                ? $"sfm+import+resume {Configuration.VisualSFMResultPath} {Configuration.VisualSFMResultPath} {Configuration.MatchFilePath}"
                : $"sfm+import {Configuration.TempDirectoryPath} {Configuration.VisualSFMResultPath} {Configuration.MatchFilePath}";

            Process process = Process.Start(startInfo);

            while (!process.StandardOutput.EndOfStream)
            {
                var line = process.StandardOutput.ReadLine();
                if (!string.IsNullOrEmpty(line))
                    WindowsFormHelper.AddLogToConsole(line + "\n");
            }

            process.WaitForExit();
        }
    }
}
