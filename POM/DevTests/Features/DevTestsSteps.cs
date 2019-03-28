using System.IO;
using log4net;
using NUnit.Framework;
using POM.Util;
using TechTalk.SpecFlow;

namespace POM.DevTests.Features
{
    [Binding]
    public class DevTestsSteps
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(DevTestsSteps));


        [Given(@"I can get project execution directory")]
        public void GivenICanGetProjectExecutionDirectory()
        {
            var exe = FileAndFolder.GetExecutionDirectory();
            Log.Info("Execution Directory is: " + exe);
        }


        [Then(@"The ""(.*)"" Exist Under Reprt > Logs")]
        public void ThenTheExistUnderReprtLogs(string logFileName)
        {
            var exe = FileAndFolder.GetExecutionDirectory();
            var logFolder = Path.Combine(exe, SolutionFolders.Reports.ToString(), SolutionFolders.Logs.ToString());
            Assert.True(Directory.Exists(logFolder), "Log fololder should be in " + logFolder);
            var logfile = Path.Combine(logFolder, logFileName);
            Assert.True(File.Exists(logfile), $"Log File Name: {logFileName} - Full Path: {logfile}");
        }


    }
}
