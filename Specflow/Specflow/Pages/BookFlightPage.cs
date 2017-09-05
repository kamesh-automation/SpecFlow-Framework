using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Specflow.Pages
{
    public class BookFlightPage
    {
        public static By txtFirstName = By.Name("passFirst0");
        public static By txtLastName = By.Name("passLast0");
        public static By txtCardNumber = By.Name("creditnumber");
        public static By cmbMeal = By.Name("pass.0.meal");
        public static By cmbCardType = By.Name("creditCard");
        public static By txtCCFirstName = By.Name("cc_frst_name");
        public static By txtCCMiddleName = By.Name("cc_mid_name");
        public static By txtCCLastName = By.Name("cc_last_name");
        public static By btnBuyFlights = By.Name("buyFlights");
    }
}
