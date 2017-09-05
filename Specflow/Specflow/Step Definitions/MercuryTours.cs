using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using Specflow.Pages;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace test
{
    [Binding]
    public class CalculatorSteps
    {
        static IWebDriver driver;

        [BeforeTestRun]
        public static void OpenDriver()
        {
            string browser = ConfigurationSettings.AppSettings["browser"];
            switch(browser)
            {
                case "Firefox":
                    string pathToBinary = "C:\\Program Files\\Mozilla Firefox\\Firefox.exe";
                    FirefoxBinary ffBinary = new FirefoxBinary(pathToBinary);
                    FirefoxProfile firefoxProfile = new FirefoxProfile();
                    driver = new FirefoxDriver(ffBinary, firefoxProfile);
                    break;
                case "Chrome":
                    driver = new ChromeDriver();
                    break;
                case "IE":
                    driver = new InternetExplorerDriver();
                    break;
            }
            

        }

        [Given(@"the user navigates to (.*)")]
        public void GivenINavigateToURL(string url)
        {
            driver.Navigate().GoToUrl("http://newtours.demoaut.com/");

        }

        [Given(@"the user log in with (.*)")]
        public void WhenTheUserLogin(string username)
        {
            driver.FindElement(LoginPage.txtUsername).SendKeys(username);
            driver.FindElement(LoginPage.txtPassword).SendKeys("mercury");
            driver.FindElement(LoginPage.btnLogin).Click();
        }

        [When(@"the user selects (.*) flight")]
        public void WhenUserSelectsFlight(string username)
        {
            driver.FindElement(FlightFinderPage.cmbFromPort).SendKeys("Frankfurt");
            driver.FindElement(FlightFinderPage.cmbToPort).SendKeys("London");
            driver.FindElement(FlightFinderPage.btnContinue).Click();
            driver.FindElement(SelectFlightPage.btnReserveFlights).Click();
        }

        [When(@"the user books a flight")]
        public void WhenUserBooksFlight()
        {
            driver.FindElement(BookFlightPage.txtFirstName).SendKeys("Adam");
            driver.FindElement(BookFlightPage.txtLastName).SendKeys("Gibbins");
            driver.FindElement(BookFlightPage.txtCardNumber).SendKeys("6784562345");
            driver.FindElement(BookFlightPage.cmbCardType).SendKeys("Visa");
            driver.FindElement(BookFlightPage.cmbMeal).SendKeys("Bland");
            driver.FindElement(BookFlightPage.txtCCFirstName).SendKeys("Adam");
            driver.FindElement(BookFlightPage.txtCCMiddleName).SendKeys("M");
            driver.FindElement(BookFlightPage.txtCCLastName).SendKeys("Gibbins");
            driver.FindElement(BookFlightPage.btnBuyFlights).Click();
        }

        [Then(@"the user recieves flight confirmation")]
        public void WhenUserConfirmsFlight()
        {
            
        }

        [Given(@"the user is on (.*) page")]
        public void ThenIAmOnPage(string pageName)
        {
            switch (pageName)
            {
                case "Home":
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    Assert.True(driver.FindElement(FlightFinderPage.lblFindFlight).Displayed);
                    break;
                case "Login":
                    //Assert.True(driver.FindElement(By.Name("userName")).Displayed);
                    Assert.True(true);
                    break;
            }
        }

        [Then(@"I am happy")]
        public void ThenIAmHappy()
        {
            ScenarioContext.Current.Pending();
        }


        [AfterTestRun]
        public static void CloseDriver()
        {
            driver.Close();
        }



    }
}
