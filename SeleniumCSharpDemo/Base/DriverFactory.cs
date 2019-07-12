using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SeleniumCSharpDemo.Configuration;

namespace SeleniumCSharpDemo.Base
{
    class DriverFactory
    {

        private static string browser=ConfigReader.GetBrowser();
        public static IWebDriver GetDriver()
        {
            IWebDriver webDriver = null;
            browser = browser.ToLower();
            string driverPath = ConfigReader.GetDriverPath();

            switch (browser)
            {
                case "chrome":
                    {
                        //Change download folder of Chrome
                        var chromeOptions = new ChromeOptions();
                        //chromeOptions.AddUserProfilePreference("download.default_directory", downloadDirectory);
                        chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
                        chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");
                        chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", "false");

                        webDriver = new ChromeDriver(driverPath, chromeOptions, TimeSpan.FromSeconds(180));

                        break;
                    }

                case "ie":
                    webDriver = new InternetExplorerDriver();
                    break;

                case "firefox":
                    {
                        FirefoxOptions options = new FirefoxOptions();
                        //options.SetPreference("browser.download.dir", downloadDirectory);
                        options.SetPreference("browser.download.folderList", 2);
                        options.SetPreference("browser.download.manager.showWhenStarting", false);
                        options.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;");
                        options.SetPreference("pdfjs.disabled", true);

                        webDriver = new FirefoxDriver(driverPath, options, TimeSpan.FromSeconds(180));

                        break;
                    }
            }
            webDriver.Manage().Window.Maximize();
            return webDriver;
        }
    }
}
