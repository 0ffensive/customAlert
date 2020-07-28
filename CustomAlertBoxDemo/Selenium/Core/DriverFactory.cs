using System;
using System.Drawing;
using System.Net;
using CustomAlertBoxDemo.Selenium.Settings;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

#pragma warning disable 0618

namespace CustomAlertBoxDemo.Selenium.Core
{
    public partial class DriverFactory : IDisposable
    {
        private string headlessMode;
        private string buildNumber = "Local build - " + Dns.GetHostName() + " #" + DateTime.Now.ToString("yyyyMMddHHmmss");

        public string Browser { get; private set; }
        public string ServiceUrl { get; private set; }

        public DriverFactory()
        {
            SetupBrowser();
            SetupBuildNumber();
            SetupHeadlessMode();
            SetupServiceUrl();
        }

        public IWebDriver CreateDriver()
        {
            switch (Browser.ToUpperInvariant()) 
            {
                case "WINDOWS CHROME": return CreateChromeDriver();
                default: throw new ArgumentException($"{Browser} browser not yet implemented");
            };
        }

        private IWebDriver CreateChromeDriver()
        {
            var chromeOptions = new ChromeOptions();

            // Run tests without the browser UI being visible
            if (headlessMode.ToUpperInvariant() == "TRUE")
                chromeOptions.AddArgument("headless");
            
            // Disable sandbox
            chromeOptions.AddArgument("no-sandbox");

            // Start maximised
            //chromeOptions.AddArgument("start-maximized");
            chromeOptions.AddArgument("start-minimized");

            chromeOptions.AddArgument("disable-popup-blocking");
            
            chromeOptions.AddArgument("no-proxy-server");

            // Prevent error "Loading of unpacked extensions is disabled by the administrator"
            chromeOptions.AddAdditionalCapability("useAutomationExtension", false);

            // Suppress warnings displayed on the command line while starting up WebDriver service on an Azure DevOps agent
            var chromeDriverService = ChromeDriverService.CreateDefaultService(Environment.GetEnvironmentVariable("ChromeWebDriver"));
            chromeDriverService.SuppressInitialDiagnosticInformation = true;
            chromeDriverService.HideCommandPromptWindow = true;

            var chromeDriver = new ChromeDriver(chromeDriverService, chromeOptions, TimeSpan.FromSeconds(Constants.Driver.TimeOut.WebDriverCommand));
            chromeDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Constants.Driver.TimeOut.FindWebElement);

            chromeDriver.Manage().Window.Position = new Point(-1500, 0);

            return chromeDriver;
        }

        //public IWebDriver CreateDriver(string testName)
        //{
        //    return (Browser.ToUpperInvariant()) switch
        //    {
        //        "WINDOWS CHROME" => CreateChromeDriver(),
        //        "WINDOWS FIREFOX" => CreateFirefoxDriver(),
        //        "WINDOWS EDGE" => CreateEdgeDriver(),
        //        "WINDOWS IE" => CreateIEDriver(),
        //        "MACOS SAFARI" => CreateMacSafariDriver(),
        //        "IPHONE SAFARI - BROWSERSTACK" => CreateRemoteiPhoneSafariDriver(testName),
        //        "ANDROID CHROME - BROWSERSTACK" => CreateRemoteAndroidChromeDriver(testName),
        //        "WINDOWS EDGE - BROWSERSTACK" => CreateRemoteWindowsEdgeDriver(testName),
        //        "MACOS SAFARI - BROWSERSTACK" => CreateRemoteMacSafariDriver(testName),
        //        "MACOS CHROME - BROWSERSTACK" => CreateRemoteMacChromeDriver(testName),
        //        "MACOS FIREFOX - BROWSERSTACK" => CreateRemoteMacFirefoxDriver(testName),
        //        _ => throw new ArgumentException($"{Browser} browser not yet implemented"),
        //    };
        //}

        private void SetupBrowser()
        {
            if (Environment.GetEnvironmentVariable("Browser") == null)
            {
                Browser = Constants.LocalSettings.Browser;
                Environment.SetEnvironmentVariable("Browser", Browser);
            }
            else
                Browser = Environment.GetEnvironmentVariable("Browser");
        }

        private void SetupBuildNumber()
        {
            if (Environment.GetEnvironmentVariable("BUILD_DEFINITIONNAME") != null)
                buildNumber = Environment.GetEnvironmentVariable("BUILD_DEFINITIONNAME") + " #" + Environment.GetEnvironmentVariable("BUILD_BUILDNUMBER");
        }

        private void SetupHeadlessMode()
        {
            if (Environment.GetEnvironmentVariable("HeadlessMode") == null)
            {
                headlessMode = "False";
                Environment.SetEnvironmentVariable("HeadlessMode", headlessMode);
            }
            else
                headlessMode = Environment.GetEnvironmentVariable("HeadlessMode");
        }

        private void SetupServiceUrl()
        {
            if (Environment.GetEnvironmentVariable("TestURL") == null)
            {
                ServiceUrl = Constants.LocalSettings.TestUrl;
                Environment.SetEnvironmentVariable("TestURL", ServiceUrl);
            }
            else
                ServiceUrl = Environment.GetEnvironmentVariable("TestURL");
        }

        public void Dispose() { }
    }
}
