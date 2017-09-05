using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Specflow.Pages
{
    public class FlightFinderPage
    {

        public static By cmbFromPort = By.Name("fromPort");
        public static By cmbToPort = By.Name("toPort");
        public static By lblFindFlight = By.Name("findFlights");
        public static By btnContinue = By.Name("findFlights");
    }
}
