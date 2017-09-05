using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Core
{
    /// <summary>
    /// Encapsulates the web driver instance and provides methods to manage/drive the web browser
    /// </summary>
    public class WebDriver : IDisposable
    {
        internal readonly IWebDriver _driver;

        /// <summary>
        /// Base URL of the application under test
        /// </summary>
        public string BaseUrl { get; set; }

        public WebDriver(TestConfig config, string feature, string scenario, string[] tags, string driverPath = null)
        {
            BaseUrl = config.AppUrl;
            _driver = WebDriverFactory.Build(config, feature, scenario, tags, driverPath);
            _driver.Manage().Window.Maximize();
        }


        /// <summary>
        /// Initializes web elements in a page object
        /// </summary>
        /// <param name="page"></param>
        public void InitPageElements(Object page)
        {
            PageFactory.InitElements(_driver, page);
        }

        /// <summary>
        /// Switches web driver to last opened browser window
        /// </summary>
        public void SwitchToLatestWindow()
        {
            _driver.SwitchTo().Window(_driver.WindowHandles.Last<string>());
        }

        /// <summary>
        /// Switches web driver to given frame
        /// </summary>
        /// <param name="index">Index of the frame</param>
        public void SwitchToFrame(int index)
        {
            _driver.SwitchTo().Frame(index);
        }

        /// <summary>
        /// Switches web driver to given frame
        /// </summary>
        /// <param name="name">Name of the frame</param>
        public void SwitchToFrame(string name)
        {
            _driver.SwitchTo().Frame(name);
        }

        /// <summary>
        /// Switches web driver to given window
        /// </summary>
        /// <param name="name">Name of the window handle</param>
        public void SwitchToWindow(string name)
        {
            _driver.SwitchTo().Window(name);
        }

        /// <summary>
        /// Deletes all browser cookies
        /// </summary>
        public void ClearCookies()
        {
            _driver.Manage().Cookies.DeleteAllCookies();
        }

        /// <summary>
        /// Closes the current browser window
        /// </summary>
        public void CloseCurrentWindow()
        {
            _driver.Close();
        }

        /// <summary>
        /// Resizes the browser window
        /// </summary>
        /// <param name="width">Width of browser window to set</param>
        /// <param name="height">Height of browser window to set</param>
        public void ResizeWindow(int width, int height)
        {
            _driver.Manage().Window.Size = new System.Drawing.Size(width, height);
        }

        /// <summary>
        /// Gets or Sets the size of the browser window
        /// </summary>
        public Size WindowSize
        {
            get
            {
                return _driver.Manage().Window.Size;
            }
            set
            {
                _driver.Manage().Window.Size = value;
            }
        }

        /// <summary>
        /// Switches back to the parent document
        /// </summary>
        public void SwitchToDefault()
        {
            _driver.SwitchTo().DefaultContent();
        }

        /// <summary>
        /// Waits for page load to complete
        /// </summary>
        /// <param name="timeinSeconds">Time (in seconds) to wait before timeout</param>
        public void WaitForPageLoad(double timeinSeconds = 60)
        {
            try
            {
                IWait<IWebDriver> wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeinSeconds));
                wait.Until(driver1 => ((IJavaScriptExecutor)_driver).ExecuteScript("return document.readyState").Equals("complete"));
            }
            catch (StaleElementReferenceException ex)
            {
                throw;
            }
            catch (NoSuchElementException ex)
            {
                throw;
            }
            catch (ElementNotVisibleException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Waits until the expected condition is true
        /// </summary>
        /// <param name="expectedCondition">Expected condition. Use the OpenQA.Selenium.Support.UI.ExpectedConditions class</param>
        /// <param name="timeoutInSeconds">Timeout in seconds</param>
        public bool WaitUntil(Func<IWebDriver, bool> expectedCondition, int timeoutInSeconds = 5)
        {
            var expected = String.Format("{0}({1})", expectedCondition.GetMethodInfo().Name, "");

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(expectedCondition);
        }

        /// <summary>
        /// Find an element without wait
        /// </summary>
        /// <param name="by">Find by</param>
        /// <returns>Instance of web element found</returns>
        public IWebElement FindElementWithOutWait(By by)
        {
            try
            {
                return _driver.FindElement(by);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Finds an element with implicit wait
        /// </summary>
        /// <param name="by">Mechanism by which element is to be accessed</param>
        /// <param name="timeoutInSeconds">Implicit timeout period in seconds. If timeout is set to 0, it will find element w/o wait</param>
        /// <returns>Instance of web element</returns>
        public IWebElement FindElement(By by, int timeoutInSeconds = 5)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv =>
                {
                    return drv.FindElement(by);
                });
            }
            return FindElementWithOutWait(by);
        }

        /// <summary>
        /// Finds all IWebElement within the current context using the given mechanism
        /// </summary>
        /// <param name="by">The locating mechanism to use</param>
        /// <returns></returns>
        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return _driver.FindElements(by);
        }

        /// <summary>
        /// Captures a screenshot of browser window and saves it on local machine
        /// </summary>
        /// <param name="testResultDir">Path to test results directory</param>
        /// <param name="className">Fully qualified class name</param>
        /// <param name="testName">Test method name</param>
        /// <returns>Screenshot file path</returns>
        public string TakeScreenshot(string testResultDir, string className, string testName)
        {
            var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
            string fileName = string.Format("{0}_{1}.png",
                                                testName,
                                                DateTime.Now.ToString("yyyyMMdd_HHmmss"));

            var artifactDirectory = Path.Combine(testResultDir, className);
            if (!Directory.Exists(artifactDirectory))
                Directory.CreateDirectory(artifactDirectory);

            string screenshotFilePath = Path.Combine(artifactDirectory, fileName);

            screenshot.SaveAsFile(screenshotFilePath, ScreenshotImageFormat.Png);
            return screenshotFilePath;
        }

        /// <summary>
        /// Navigates browser to specified page
        /// </summary>
        /// <param name="pageUrl"></param>
        public void NavigateTo(string pageUrl, bool useBaseUrl = true)
        {
            // build or use passed in URL
            var url = useBaseUrl == true ? string.Format("{0}/{1}", BaseUrl, pageUrl) : pageUrl;
            _driver.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Navigates the browser back one step in the browser's history
        /// </summary>
        public void NavigateBackward()
        {
            _driver.Navigate().Back();
        }

        /// <summary>
        /// Navigates the browser forward one step in the browser's history
        /// </summary>
        public void NavigateForward()
        {
            _driver.Navigate().Forward();
        }

        /// <summary>
        /// Instructs the broswer to refresh the current page
        /// </summary>
        public void Refresh()
        {
            _driver.Navigate().Refresh();
        }

        /// <summary>
        /// Gets the title of current browser window
        /// </summary>
        public string WindowTitle
        {
            get { return _driver.Title; }
        }

        /// <summary>
        /// Gets the current URL 
        /// </summary>
        public string CurrentUrl
        {
            get
            {
                return _driver.Url;
            }
        }

        /// <summary>
        /// Quits the web driver and disposes it off
        /// </summary>
        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
