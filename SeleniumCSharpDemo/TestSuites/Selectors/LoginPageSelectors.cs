using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCSharpDemo.TestSuites.Selectors
{
    public class LoginPageSelectors
    {
        public static string signIn_lnk = "//a[normalize-space()='Sign In']";
        public static string login_btn = "//button[text()='Login']";
        public static string email_txt = "//input[@id='email']";
        public static string password_txt = "//input[@id='pwd']";

    }
}
