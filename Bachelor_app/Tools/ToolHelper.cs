using Bachelor_app.Helper;
using System.Diagnostics;

namespace Bachelor_app.Tools
{
    public static class ToolHelper
    {
        public static void RunVisualSFM(bool ContinueProcess)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(Configuration.VisualSFMToolPath)
            {
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                UseShellExecute = false
            };

            if (ContinueProcess)
                startInfo.Arguments = $"sfm+import+resume {Configuration.VisualSFMResultPath} {Configuration.VisualSFMResultPath} {Configuration.MatchFilePath}";
            else
                startInfo.Arguments = $"sfm+import {Configuration.TempDirectoryPath} {Configuration.VisualSFMResultPath} {Configuration.MatchFilePath}";

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
