using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCSharpDemo.TestSuites.Selectors
{
    class HomePageSelectors
    {
        public static string getWelcome_lbl(string email)
        {
            return "//a[contains(text(),'Welcome " + email + "')]";
        }

        public static string createTrip_lnk = "//a[text()='Create Trip']";
        public static string title_txt = "//input[@id='title']";
        public static string place_txt = "//input[@id='place']";

        public static string startTrip_dtp = "//input[@id='start_date_val']";
        public static string endTrip_dtp = "//input[@id='end_date_val']";

        public static string members_ddl = "//select[@id='members']";
        
        public static string trip_tbl = "//table[contains(@class,'my-trip-table')]";

        public static string submit_btn = "//form[@name='tripForm']//descendant::button[text()='Submit']";

        public static string getToday_xpath(int dateTimePickerIndex)
        {
            return "//div[contains(@class,'datetimepicker')][" + dateTimePickerIndex + "]/div[@class='datetimepicker-days']//descendant::tfoot/tr/th[text()='Today']";
        }

        public static string getSwitch_xpath(int dateTimePickerIndex)
        {
            return "//div[contains(@class,'datetimepicker')][" + dateTimePickerIndex + "]/div[@class='datetimepicker-days']//descendant::thead/tr/th[@class='switch']";
        }

        public static string getMonth_xpath(int dateTimePickerIndex, string month)
        {
            return "//div[contains(@class,'datetimepicker')][" + dateTimePickerIndex + "]/div[@class='datetimepicker-months']//descendant::tbody/tr/td/span[text()='" + month + "']";
        }

        public static string getDate_xpath(int dateTimePickerIndex, string date)
        {
            return "//div[contains(@class,'datetimepicker')][" + dateTimePickerIndex + "]/div[@class='datetimepicker-days']//descendant::tbody/tr/td[@class='day'][text()='" + date + "']";
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="tripName"></param>
        /// <param name="action">Edit or Delete ot Todo List</param>
        /// <returns>action xpath with corresponding trip name</returns>
        public static string getAction_xpath(string tripName, string action)
        {
            return "//tbody/tr/td[text()='" + tripName + "']/following-sibling::td/button[@title='" + action + "']";
        }
    }
}
