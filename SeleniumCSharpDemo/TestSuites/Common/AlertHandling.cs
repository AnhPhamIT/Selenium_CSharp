using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumCSharpDemo.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumCSharpDemo.Extensions;
using SeleniumCSharpDemo.Helpers;

namespace SeleniumCSharpDemo.TestSuites.Common
{
    public class AlertHandling: TestInitializeHook
    {
        //neu de public void static thi ko thay bien driver from TestInitializeHook
        public void verifyAndAcceptAlert(string expectedValue)
        {
            driver.WaitForAlertPresent();
            IAlert alert = driver.SwitchTo().Alert();
            string alertMsg = alert.Text;
            Assert.AreEqual(expectedValue, alertMsg);
            LogHelpers.Write("AlertHandling: Current alert message " + alertMsg);
            alert.Accept();
        }
    }
}
