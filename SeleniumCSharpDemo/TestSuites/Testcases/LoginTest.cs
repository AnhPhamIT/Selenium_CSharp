using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SeleniumCSharpDemo.Base;
using SeleniumCSharpDemo.TestSuites.Pages;
using SeleniumCSharpDemo.Extensions;
using AventStack.ExtentReports;
using OpenQA.Selenium;
using SeleniumCSharpDemo.Helpers;
using System.Collections;
using SeleniumCSharpDemo.Configuration;

namespace SeleniumCSharpDemo.TestSuites.Testcases
{
    class LoginTest: TestInitializeHook
    {
        public LoginPage loginPage;
        public HomePage homePage;
        public string testData;
        [SetUp]
        public void StartTest()
        {
            loginPage = new LoginPage(driver);
            testData = ConfigReader.GetTestDataPath("Login.xlsx");
            test.Log(Status.Info, "Navigate to URL http://travelwithus.asia/");
            driver.Url = ConfigReader.GetAppURL();
            driver.WaitForPageToLoad();
        }

        [Test]
        public void SuccessLogin()
        {
            List<Dictionary<string, string>> dataArr = ExcelHelpers.ReadDataInExcel(testData, "Login", "TC01");
            string email = dataArr[0]["Email"];
            string password = dataArr[0]["Password"];


            
            test.Log(Status.Info, "Login with account " + email + " "  + password);
            homePage = loginPage.LoginWithAccount(email, password);
            
            string actualWelcomeMsg = homePage.GetWelcomeMsg(email);
            string expectedWelcomeMsg = "welcome " + email;
            Assert.AreEqual(expectedWelcomeMsg.ToUpper(), actualWelcomeMsg);
            driver.Sleep(5);
        }

        [Test]
        public void InvalidLogin()
        {
            List<Dictionary<string, string>> dataArr = ExcelHelpers.ReadDataInExcel(testData, "Login", "TC02");
            string email = dataArr[0]["Email"];
            string password = dataArr[0]["Password"];
            string expectedAlert = "The password is invalid or the user does not have a password.";

            test.Log(Status.Info, "Login with account " + email + " " + password);
            loginPage.LoginWithAccount(email, password);
            IAlert alert= driver.SwitchTo().Alert();
            Assert.AreEqual(expectedAlert, alert.Text);
            driver.Sleep(5);
            alert.Accept();
            
        }

        [TearDown]
        public void EndTest()
        {

        }

    }
}
