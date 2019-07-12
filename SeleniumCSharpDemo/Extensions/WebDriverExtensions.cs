using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeleniumCSharpDemo.Extensions
{
    public static class WebDriverExtensions
    {
        public const int defaultTimeout = 30000;
        public static void WaitForPageToLoad(this IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(defaultTimeout));


            IJavaScriptExecutor javascript = driver as IJavaScriptExecutor;
            if (javascript == null)
                throw new ArgumentException("driver", "Driver must support javascript execution");

            wait.Until((d) =>
            {
                try
                {
                    string readyState = javascript.ExecuteScript(
                    "if (document.readyState) return document.readyState;").ToString();
                    return readyState.ToLower() == "complete";
                }
                catch (InvalidOperationException e)
                {
                    //Window is no longer available
                    return e.Message.ToLower().Contains("unable to get browser");
                }
                catch (WebDriverException e)
                {
                    //Browser is no longer available
                    return e.Message.ToLower().Contains("unable to connect");
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        public static void Sleep(this IWebDriver driver, int sleepTimeInSeconds)
        {
            Thread.Sleep(sleepTimeInSeconds * 1000);
        }

        public static object ExecuteJavaScript(this IWebDriver driver, string script)
        {
            return ((IJavaScriptExecutor)driver).ExecuteScript(script);
        }

        public static string SwitchWindow(this IWebDriver driver, string title)
        {
            string mainHandle = driver.CurrentWindowHandle;
            foreach (var handle in driver.WindowHandles)
            {
                driver.SwitchTo().Window(handle);
                if (driver.Title == title)
                {
                    return mainHandle;
                }
            }

            throw new ArgumentException(string.Format("Unable to find window with title: '{0}'", title));
        }

        public static bool WaitForCondition<T>(this T obj, Func<T, bool> condition, int timeOut=defaultTimeout)
        {
            bool conResult = false;
            Func<T, bool> execute =
                (arg) =>
                {
                    try
                    {
                        return condition(arg);
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                };

            var stopWatch = Stopwatch.StartNew();
            while (stopWatch.ElapsedMilliseconds < timeOut)
            {
                if (execute(obj))
                {
                    conResult = true;
                    break;
                }
            }
            return conResult;
        }

        public static bool WaitForEnable(this IWebDriver driver, By by, int timeOut= defaultTimeout)
        {
            return driver.WaitForCondition(dri => driver.FindElement(by).Displayed && driver.FindElement(by).Enabled, timeOut);
        }

        /// <summary>
		/// An expectation for checking the AlterIsPresent
		/// </summary>
		/// <returns>Alert </returns>
		public static Func<IWebDriver, bool> AlertIsPresent()
        {
            return (driver) =>
            {
                try
                {
                    driver.SwitchTo().Alert();
                    return true;
                }
                catch (NoAlertPresentException)
                {
                    return false;
                }
            };
        }

        public static bool WaitForAlertPresent(this IWebDriver driver, int timeOut = defaultTimeout)
        {
            return driver.WaitForCondition(AlertIsPresent(), timeOut);
        }
        public static bool WaitForVisible(this IWebDriver driver, By by, int timeOut)
        {
            return driver.WaitForCondition(dri=> driver.FindElement(by).Displayed, timeOut);
        }

        public static void ClickOnElement(this IWebDriver driver, By by, int timeOut= defaultTimeout)
        {
            try
            {
                driver.WaitForEnable(by, timeOut);
                driver.FindElement(by).Click();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void SendKeyOnElement(this IWebDriver driver, By by, string data, int timeOut= defaultTimeout)
        {
            try
            {
                driver.WaitForVisible(by, timeOut);
                driver.Sleep(1);
                driver.FindElement(by).SendKeys(data);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public static string getElementText(this IWebDriver driver, By by, int timeOut = defaultTimeout)
        {
            try
            {
                driver.WaitForVisible(by, timeOut);
                return driver.FindElement(by).Text;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
