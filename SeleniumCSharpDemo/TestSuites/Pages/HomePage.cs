using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumCSharpDemo.Extensions;
using SeleniumCSharpDemo.TestSuites.Selectors;
using SeleniumCSharpDemo.Helpers;

namespace SeleniumCSharpDemo.TestSuites.Pages
{
    class HomePage
    {
        public IWebDriver driver;
        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public string GetWelcomeMsg(string email)
        {
            return this.driver.getElementText(By.XPath(HomePageSelectors.getWelcome_lbl(email)));
        }


        public void CreateTrip(string title, string place, string startDate, string endDate, string members)
        {
            driver.ClickOnElement(By.XPath(HomePageSelectors.createTrip_lnk));
            fillTripInformation(title, place, startDate, endDate, members);
        }

        public void EditTrip(string curTripTitle, string newTripTitle, string place, string startDate, string endDate,string members)
        {
            doActionOnTrip(curTripTitle, "Edit");
            fillTripInformation(newTripTitle, place, startDate, endDate, members);
        }

        public void DeleteTrip(string tripTitle)
        {
            doActionOnTrip(tripTitle, "Delete");
        }

        public void doActionOnTrip(string tripTitle, string action)
        {
            driver.ClickOnElement(By.XPath(HomePageSelectors.getAction_xpath(tripTitle, action)));
        }

        public void fillTripInformation(string title, string place, string startDate, string endDate, string members)
        {
            driver.SendKeyOnElement(By.XPath(HomePageSelectors.title_txt), title);
            driver.SendKeyOnElement(By.XPath(HomePageSelectors.place_txt), place);
            //driver.SendKeyOnElement(By.XPath(HomePageSelectors.startTrip_dtp), startDate);
            selectDateTimePicker(1, startDate);
            selectDateTimePicker(2, endDate);
            driver.SendKeyOnElement(By.XPath(HomePageSelectors.members_ddl), members);
            driver.ClickOnElement(By.XPath(HomePageSelectors.submit_btn));
        }

        public string getMonth(string fullDate)
        {
            var month = fullDate.Split('/')[1];

            switch (month)
            {
                case "1": case "01":
                    month = "Jan";
                    break;
                case "2": case "02":
                    month = "Feb";
                    break;
                case "3": case "03":
                    month = "Mar";
                    break;
                case "4": case "04":
                    month = "Apr";
                    break;
                case "5": case "05":
                    month = "May";
                    break;
                case "6": case "06":
                    month = "Jun";
                    break;
                case "7": case "07":
                    month = "Jul";
                    break;
                case "8": case "08":
                    month = "Aug";
                    break;
                case "9": case "09":
                    month = "Sep";
                    break;
                case "10":
                    month = "Oct";
                    break;
                case "11":
                    month = "Nov";
                    break;
                case "12":
                    month = "Dec";
                    break;
                default:
                    LogHelpers.Write("HomePage: Month " + month + " is invalid");
                    break;
            }
            LogHelpers.Write("HomePage: Month is " + month);
            return month;
        }

        public string getDate(string fullDate)
        {
            return fullDate.Split('/')[0];
        }

        public void selectDateTimePicker(int dateTimePickerIndex, string VNDate)
        {
            string month = getMonth(VNDate);
            string date = getDate(VNDate);

            if(dateTimePickerIndex == 1)
            {
                driver.ClickOnElement(By.XPath(HomePageSelectors.startTrip_dtp));
            }
            else
            {
                driver.ClickOnElement(By.XPath(HomePageSelectors.endTrip_dtp));
            }

            driver.ClickOnElement(By.XPath(HomePageSelectors.getSwitch_xpath(dateTimePickerIndex)));
            driver.ClickOnElement(By.XPath(HomePageSelectors.getMonth_xpath(dateTimePickerIndex, month)));
            driver.ClickOnElement(By.XPath(HomePageSelectors.getDate_xpath(dateTimePickerIndex, date)));

        }
    }
}
