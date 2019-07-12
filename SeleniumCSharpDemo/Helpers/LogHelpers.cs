using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCSharpDemo.Helpers
{
    class LogHelpers
    {
        //Global Declaration
        private static string _logFileName = "\\"+ string.Format("{0:yyyymmddhhmmss}", DateTime.Now);
        private static StreamWriter _streamw = null;

        //Create a file which can store the log information
        public static void CreateLogFile(string reportPath)
        {
            //var dir = Directory.GetParent(TestContext.CurrentContext.TestDirectory).Parent.FullName+ "\\TestSuites\\Reports";

            if (Directory.Exists(reportPath))
            {
                try
                {
                    if(_streamw==null)
                        _streamw = File.AppendText(reportPath + _logFileName + ".log");
                }
                catch (Exception)
                {

                    throw;
                }
                
            }
            else
            {
                Directory.CreateDirectory(reportPath);
                _streamw = File.AppendText(reportPath + _logFileName + ".log");
            }
        }



        //Create a method which can write the text in the log file
        public static void Write(string logMessage)
        {
            string testName = TestContext.CurrentContext.Test.ClassName + ": " + TestContext.CurrentContext.Test.Name;
            _streamw.Write("{0} {1} {2}", testName, DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
            _streamw.WriteLine("    {0}", logMessage);
            _streamw.Flush();
        }
    }
}
