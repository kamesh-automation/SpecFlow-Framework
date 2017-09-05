using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Core
{
    public class TestConfig
    {
        public TestConfig()
        {
            Modules = new Dictionary<string, Module>();
        }

        /// <summary>
        /// Name of the LA product (e.g. - US, UK) for which tests are to be executed
        /// </summary>
        public string Product { get; set; }

        /// <summary>
        /// Environment against which tests are run (e.g. - DDC1, DDC2, CERT1, CERT2,...)
        /// </summary>
        public string Environment { get; set; }

        /// <summary>
        /// If running tests on remote/grid, OS to be used (e.g. - Windows, Linux)
        /// </summary>
        public string Platform { get; set; }

        /// <summary>
        /// Name of the browser to be used for test execution
        /// </summary>
        public string Browser { get; set; }

        /// <summary>
        /// Version of the browser to be used (if running on saucelab)
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Name of the device, if the rests are to be run on a handheld device (for saucelabs)
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// Flag indicating whether tests are to be executed locally or remotely
        /// </summary>
        public bool IsRemote { get; set; }

        /// <summary>
        /// Build name/number
        /// </summary>
        public string Build { get; set; }

        /// <summary>
        /// Selenium grid/Saucelabs hub URL
        /// </summary>
        public string HubUrl { get; set; }

        /// <summary>
        /// Saucelabs hub username
        /// </summary>
        public string HubUsername { get; set; }

        /// <summary>
        /// Saucelabs access key
        /// </summary>
        public string HubAccessKey { get; set; }

        /// <summary>
        /// URL of the application under test
        /// </summary>
        public string AppUrl { get; set; }

        /// <summary>
        /// List of modules with the app credentials to be used
        /// </summary>
        public Dictionary<string, Module> Modules { get; set; }

        public class Module
        {
            public string Username { get; set; }
            public string Password { get; set; }

        }
    }
}
