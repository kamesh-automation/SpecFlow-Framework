
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Specflow.Pages
{
    public class LoginPage
    {
        public static By txtUsername = By.Name("userName");
        public static By txtPassword = By.Name("password");
        public static By btnLogin = By.Name("login");
    }
}
