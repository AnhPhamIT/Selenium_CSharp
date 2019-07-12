using AventStack.ExtentReports;
using NUnit.Framework;
using SeleniumCSharpDemo.Base;
using SeleniumCSharpDemo.Configuration;
using SeleniumCSharpDemo.Extensions;
using SeleniumCSharpDemo.TestSuites.Common;
using SeleniumCSharpDemo.TestSuites.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCSharpDemo.TestSuites.Testcases
{
    class HomePageTest:TestInitializeHook
    {
        public HomePage homePage;
        public LoginPage loginPage;
        public AlertHandling alertHandling;
        [SetUp]
        public void StartTest()
        {
            loginPage = new LoginPage(driver);
            alertHandling = new AlertHandling();
        }

        [Test]
        public void CreateTrip()
        {
            test.Log(Status.Info, "Navigate to URL http://travelwithus.asia/");
            driver.Url = ConfigReader.GetAppURL();
            driver.WaitForPageToLoad();
            homePage = loginPage.LoginWithAccount("ptvanh@mailinator.com", "123456");
            homePage.CreateTrip("Ninh Binh trip", "Ninh Binh province", "1/1/2020", "10/1/2020", "4");

            alertHandling.verifyAndAcceptAlert("A new trip has been created!");
            driver.Sleep(5);
        }

        [Test]
        public void EditTrip()
        {
            test.Log(Status.Info, "Navigate to URL http://travelwithus.asia/");
            driver.Url = ConfigReader.GetAppURL();
            driver.WaitForPageToLoad();

            homePage = loginPage.LoginWithAccount("ptvanh@mailinator.com", "123456");
            homePage.EditTrip("Ninh Binh trip", "Da Lat trip", "Lam Dong province", "1/11/2019", "10/11/2019", "8");

            alertHandling.verifyAndAcceptAlert("The trip has been updated!");
            driver.Sleep(5);
        }

        [TearDown]
        public void EndTest()
        {
        }
    }
}
