using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Core
{
    public class WebDriverFactory
    {
        private static string libPath;

        /// <summary>
        /// Builds the web driver instance with given configuration
        /// </summary>
        /// <param name="config">Test configuration</param>
        /// <param name="fearure">Name of the feature being executed</param>
        /// <param name="scenario">Name of the scenario being executed</param>
        /// <param name="tags">List of tags</param>
        /// <returns></returns>
        public static IWebDriver Build(TestConfig config, string fearure, string scenario, string[] tags, string driverPath)
        {
            libPath = string.IsNullOrWhiteSpace(driverPath) ? Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) : driverPath;

            return config.IsRemote ? GetRemoteDriver(config, fearure, scenario, tags) : GetLocalDriver(config);
        }


        private static IWebDriver GetLocalDriver(TestConfig config)
        {
            switch (config.Browser.ToLower())
            {
                case "firefox":
                    FirefoxProfile defaultProfile = (new FirefoxProfileManager()).GetProfile("default");
                    //DesiredCapabilities capabilities = new DesiredCapabilities();
                    //capabilities.SetCapability(FirefoxDriver.ProfileCapabilityName, defaultProfile);
                    //capabilities.SetCapability(CapabilityType.PageLoadStrategy, "none");
                    FirefoxOptions qwe = new FirefoxOptions();
                    qwe.AddAdditionalCapability(FirefoxDriver.ProfileCapabilityName, defaultProfile);
                    qwe.AddAdditionalCapability(CapabilityType.PageLoadStrategy, "none");
                    return new FirefoxDriver(qwe);
                case "chrome":
                    return new ChromeDriver(libPath);
                case "internet explorer":
                    var options = new InternetExplorerOptions()
                    {
                        InitialBrowserUrl = null,
                        IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                        IgnoreZoomLevel = true,
                        EnableNativeEvents = false
                    };
                    return new InternetExplorerDriver(libPath, options);
                case "phantomjs":
                    return new PhantomJSDriver(libPath);
                default:
                    throw new PlatformNotSupportedException("Framework does not support browser: " + config.Browser);
            }
        }

        private static IWebDriver GetRemoteDriver(TestConfig config, string fearure, string scenario, string[] tags)
        {
            DesiredCapabilities capabilities = new DesiredCapabilities();
            capabilities.SetCapability(CapabilityType.PageLoadStrategy, "none");
            capabilities.SetCapability(CapabilityType.BrowserName, config.Browser);
            if (!string.IsNullOrWhiteSpace(config.Platform)) capabilities.SetCapability(CapabilityType.Platform, config.Platform);
            if (!string.IsNullOrWhiteSpace(config.Version)) capabilities.SetCapability(CapabilityType.Version, config.Version);
            if (!string.IsNullOrWhiteSpace(config.DeviceName)) capabilities.SetCapability("deviceName", config.DeviceName);
            if (!string.IsNullOrWhiteSpace(config.HubUsername)) capabilities.SetCapability("username", config.HubUsername);
            if (!string.IsNullOrWhiteSpace(config.HubAccessKey)) capabilities.SetCapability("accessKey", config.HubAccessKey);

            capabilities.SetCapability("name", string.Format("{0} > {1} > {2}", config.Environment, fearure, scenario));
            capabilities.SetCapability("build", config.Build);
            foreach (var tag in tags)
            {
                capabilities.SetCapability("tags", tag);
            }


            return new RemoteWebDriver(new Uri(config.HubUrl), capabilities);
        }

    }
}
