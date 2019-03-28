using System.IO;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using BoDi;
using log4net.Config;
using TechTalk.SpecFlow;

namespace POM.Util
{
    [Binding]
    public class TestConfiguration
    {
        private readonly IObjectContainer _objectContainer;
        private static ExtentTest _scenario;
        private static ExtentTest _featureName;
        private static ExtentReports _extent;

        public TestConfiguration(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }
        [BeforeTestRun(Order = 1)]
        public static void SetupLog4Net()
        {
            // Configure Log4Net
            XmlConfigurator.Configure();
        }

        [BeforeTestRun(Order = 2)]
        public static void SetupExtentReport()
        {
            var exeDir = FileAndFolder.GetExecutionDirectory();
            var extReportPath = Path.Combine(exeDir, SolutionFolders.Reports.ToString(),
                SolutionFolders.Reports.ToString(), "Extent_report.html");

            var htmlReporter = new ExtentHtmlReporter(extReportPath);
            htmlReporter.Config.DocumentTitle = "Behrang Bina - Framework Report";
            htmlReporter.Config.Theme = Theme.Dark;

            _extent = new ExtentReports();
            _extent.AttachReporter(htmlReporter);
        }

        [BeforeFeature]
        public static void SetFeatureName()
        {
            _featureName = _extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);
        }

        [BeforeScenario]
        public static void SetScenarioName()
        {
            _scenario = _featureName.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
        }

        [AfterStep]
        public static void InsertReportingSteps()
        {
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            var exeStatus = ScenarioContext.Current.ScenarioExecutionStatus;



            if (ScenarioContext.Current.TestError == null)
            {
                if (stepType == "Given")
                    
                if (exeStatus == ScenarioExecutionStatus.StepDefinitionPending)
                {
                   _scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                }
                else
                {
                   _scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                }
                else if (stepType == "When")
                    if (exeStatus == ScenarioExecutionStatus.StepDefinitionPending)
                    {
                        _scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                    }
                    else
                    {
                        _scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                    }
                else if (stepType == "Then")
                    if (exeStatus == ScenarioExecutionStatus.StepDefinitionPending)
                    {
                        _scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text)
                            .Skip("Step Definition Pending");
                    }
                    else
                    {
                        _scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);

                    }
                else if (stepType == "And")
                    if (exeStatus == ScenarioExecutionStatus.StepDefinitionPending)
                    {
                        _scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text)
                            .Skip("Step Definition Pending");
                    }
                    else
                    {
                        _scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
                    }
            }
            else if (ScenarioContext.Current.TestError != null)
            {
                if (stepType == "Given")
                    _scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text)
                        .Fail(ScenarioContext.Current.TestError.InnerException);
                else if (stepType == "When")
                    _scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text)
                        .Fail(ScenarioContext.Current.TestError.InnerException);
                else if (stepType == "Then")
                    _scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text)
                        .Fail(ScenarioContext.Current.TestError.Message);
                else if (stepType == "And")
                    _scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text)
                        .Fail(ScenarioContext.Current.TestError.Message);
            }
           
        }

        [AfterTestRun]
        public static void Cleanup()
        {
            _extent.Flush();
        }
    }
}
