using AventStack.ExtentReports;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using SeleniumCSharpDemo.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeleniumCSharpDemo.Base
{
    public class TestInitializeHook
    {
        public static IWebDriver driver;
        public ExtentReports extentReports;
        public ExtentTest test;


        ///For report directory creation and HTML report template creation
        ///For driver instantiation
        [OneTimeSetUp]
        public void BeforeClass()
        {
            extentReports = ExtentManager.getReport();
            LogHelpers.CreateLogFile(ExtentManager.reportPath);
        }

        [SetUp]
        public void BeforeTest()
        {

            ///Getting the name of current running test to extent report
            driver = DriverFactory.GetDriver();
            string testName = TestContext.CurrentContext.Test.ClassName + ": " + TestContext.CurrentContext.Test.Name;
            test = extentReports.CreateTest(testName, TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void AfterTest()
        {
            //LogHelpers.Write("closing browser");
            test.Log(Status.Info, "TestInitializeHook: closing browser");
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = "" + TestContext.CurrentContext.Result.StackTrace + "";
            var errorMessage = TestContext.CurrentContext.Result.Message;
            Status logstatus;
            try
            {
                
                switch (status)
                {
                    case TestStatus.Failed:
                        logstatus = Status.Fail;
                        string screenShotPath = Capture(driver, TestContext.CurrentContext.Test.Name);
                        test.Log(logstatus, "Test ended with " + logstatus + " – " + errorMessage);
                        test.Log(logstatus, "Snapshot below: " + test.AddScreenCaptureFromPath(screenShotPath));
                        break;
                    case TestStatus.Skipped:
                        logstatus = Status.Skip;
                        test.Log(logstatus, "Test ended with " + logstatus);
                        break;
                    default:
                        logstatus = Status.Pass;
                        test.Log(logstatus, "Test ended with " + logstatus);
                        break;
                }
                driver.Quit();
            }
            catch (Exception e)
            {
                logstatus = Status.Fail;
                test.Log(logstatus, "Test ended with " + logstatus + " – " + errorMessage);
                driver.Quit();
                throw (e);
            }
            
        }

        /// To capture the screenshot for extent report and return actual file path
        private string Capture(IWebDriver driver, string screenShotName)
        {
            string localpath = "";
            try
            {
                Thread.Sleep(4000);
                ITakesScreenshot ts = (ITakesScreenshot)driver;
                Screenshot screenshot = ts.GetScreenshot();

                //This will get the current WORKING directory(i.e. \bin\Debug)
                string workingDirectory = TestContext.CurrentContext.TestDirectory;

                // This will get the current PROJECT directory
                string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;

                var screenShotPath = Directory.CreateDirectory(ExtentManager.reportPath + "\\Defect_Screenshots\\").FullName + screenShotName + ".png";

                localpath = new Uri(screenShotPath).LocalPath;
                screenshot.SaveAsFile(localpath);
            }
            catch (Exception e)
            {
                throw (e);
            }
            return localpath;
        }

        [OneTimeTearDown]
        public void AfterClass()
        {
            try
            {
                extentReports.Flush();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
    }
}
