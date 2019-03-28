using System;
using System.IO;
using log4net;

namespace POM.Util
{
    public class FileAndFolder
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(FileAndFolder));

        /// <summary>
        ///     Copy file from source to destination and returns the destination file location
        /// </summary>
        /// <param name="fileName">File Name</param>
        /// <param name="source">Source Directory</param>
        /// <param name="destination">Destination Directory</param>
        /// <param name="destFileName">Destination FileName</param>
        /// <returns></returns>
        public static string CopyFile(string fileName, string source, string destFileName, string destination)
        {
            // Use Path class to manipulate file and directory paths.
            var sourceFile = Path.Combine(source, fileName);
            Log.Info($"Source File: {sourceFile}");
            var destFile = Path.Combine(destination, destFileName);
            Log.Info($"Destination File: {destFile}");
            if (!File.Exists(sourceFile))
            {
                Log.Error($"Source file cound not found in {sourceFile}");
                throw new FileNotFoundException($"Source file cound not found in {sourceFile}");
            }
            // To copy a file to another location and 
            // overwrite the destination file if it already exists.
            File.Copy(sourceFile, destFile, true);
            Log.Info("File copied from source to destination");
            return destFile;
        }
        /// <summary>
        /// Get The Execution path 
        /// </summary>
        /// <returns>Execution Path</returns>
        public static string GetExecutionDirectory()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            Log.Info("Base Director: " + baseDirectory);
            /* var solutionFullPath = Path.GetFullPath(Path.Combine(baseDirectory, "..\\..\\"));
             Log.Info("Solution Full Path: " + solutionFullPath);*/
            return baseDirectory;
        }
        /// <summary>
        /// Get The project location
        /// </summary>
        /// <returns>Returns Project Path</returns>
        public static string GetProjectPath()
        {
            var exeDir = GetExecutionDirectory();
            var projectPath = Path.GetFullPath(Path.Combine(exeDir, "..\\..\\"));
            return projectPath;
        }
    }
}
