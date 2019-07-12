using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumCSharpDemo.Helpers;
using SeleniumCSharpDemo.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using NUnit.Framework.Interfaces;
using System.IO;
using System.Threading;
using SeleniumCSharpDemo.Extensions;

namespace SeleniumCSharpDemo
{
    [Parallelizable]
    public class Class1 : TestInitializeHook
    {
        //IWebDriver driver;

        [SetUp]
        public new  void BeforeTest()
        {
            LogHelpers.Write("starting browser");
            test.Log(Status.Info, "starting browser");
        }

        [Test]
        public void GoogleTest1()
        {
            test.Log(Status.Info, "navigate to the URL");
            LogHelpers.Write("navigating to URL");
            driver.Url = "http://www.google.co.in";
            driver.WaitForPageToLoad();
            driver.FindElement(By.Name("q")).SendKeys("Selenium C#");
            driver.FindElement(By.Name("q")).SendKeys(Keys.Enter);
        }
        [Test]
        public void GoogleTest2()
        {
            test.Log(Status.Info, "navigate to the URL");
            LogHelpers.Write("navigating to URL");
            driver.Url = "http://www.google.co.in";

            driver.SendKeyOnElement(By.Name("q"),"Selenium C#", 10000);
            driver.ClickOnElement(By.Id("logo"),10000);
            
        }

        [TearDown]
        public new void AfterTest()
        {
        }

        
    }
}
