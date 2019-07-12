using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using NUnit.Framework;
using System.Reflection;

namespace SeleniumCSharpDemo.Configuration
{
    public class ConfigReader
    {
        public static string GetAppURL()
        {
            return ConfigurationManager.AppSettings["URL"].ToString();
        }

        public static string GetBrowser()
        {
            return ConfigurationManager.AppSettings["browser"].ToString();
        }

        public static string GetDriverPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Drivers";
        }

        public static string GetReportPath()
        {

            var reportFileName = string.Format("{0:yyyymmddhhmmss}", DateTime.Now);
            var projectDirectory = Directory.GetParent(TestContext.CurrentContext.TestDirectory).Parent.FullName;
            var reportPath = Directory.CreateDirectory(projectDirectory + ConfigurationManager.AppSettings["logPath"].ToString() + reportFileName).FullName;
            if (!Directory.Exists(reportPath))
            {
                Directory.CreateDirectory(reportPath);
            }
            return reportPath;
        }

        public static string GetTestDataPath(string fileName)
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + ConfigurationManager.AppSettings["testDataPath"] + fileName;
        }
    }
}
