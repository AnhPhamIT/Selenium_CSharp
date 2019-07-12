using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumCSharpDemo.Extensions;
using SeleniumCSharpDemo.TestSuites.Selectors;

namespace SeleniumCSharpDemo.TestSuites.Pages
{
    class LoginPage
    {
        public IWebDriver driver;
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;

        }

        public HomePage LoginWithAccount(string username, string password)
        {
            this.driver.ClickOnElement(By.XPath(LoginPageSelectors.signIn_lnk));
            this.driver.SendKeyOnElement(By.XPath(LoginPageSelectors.email_txt), username);
            this.driver.SendKeyOnElement(By.XPath(LoginPageSelectors.password_txt), password);
            this.driver.ClickOnElement(By.XPath(LoginPageSelectors.login_btn));
            return new HomePage(driver);
        }


    }
}
