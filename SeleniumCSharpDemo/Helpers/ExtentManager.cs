using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumCSharpDemo.Configuration;

namespace SeleniumCSharpDemo.Helpers
{
    class ExtentManager
    {
        private static ExtentReports extentReport;
        public static ExtentHtmlReporter htmlReporter;
        public static string reportPath = ConfigReader.GetReportPath();
        public static ExtentReports getReport()
        {
            try
            {
                if (extentReport == null)
                {
                    extentReport = new ExtentReports();


                    htmlReporter = new ExtentHtmlReporter(reportPath + "\\Extent.html");

                    htmlReporter.Config.DocumentTitle = "Automation Report";
                    htmlReporter.Config.ReportName = "Demo Testing";
                    htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;

                    // Passing General information
                    extentReport.AddSystemInfo("Host name", "localhost");
                    extentReport.AddSystemInfo("Environemnt", "QA");
                    extentReport.AddSystemInfo("User Name", "Anh Pham");
                    extentReport.AttachReporter(htmlReporter);

                }
                return extentReport;
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        //public static string GetReportPath()
        //{
        //    var reportFileName = string.Format("{0:yyyymmddhhmmss}", DateTime.Now);
        //    var projectDirectory = Directory.GetParent(TestContext.CurrentContext.TestDirectory).Parent.FullName;
        //    var reportPath = Directory.CreateDirectory(projectDirectory + ConfigReader.reportPath() + reportFileName).FullName;
        //    return reportPath;
        //}
   
    }
}
