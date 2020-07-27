namespace XUnitTestProject1.Selenium
{
    public partial class Constants
    {
        public class Driver
        {
            public class BrowserStack
            {
                public const string ProjectName = "Registration";
                public const string Uri = "http://hub-cloud.browserstack.com/wd/hub/";
            }

            public class Name
            {
                public const string Chrome = "OpenQA.Selenium.Chrome.ChromeDriver";
                public const string Edge = "OpenQA.Selenium.Edge.EdgeDriver";
                public const string Firefox = "OpenQA.Selenium.Firefox.FirefoxDriver";
                public const string IE = "OpenQA.Selenium.IE.InternetExplorerDriver";
                public const string Safari = "OpenQA.Selenium.Safari.SafariDriver";
                public const string Remote = "OpenQA.Selenium.Remote.RemoteWebDriver";
            }

            public class TimeOut
            {
                // Timeout in seconds
                public const int FindWebElement = 30;
                public const int WebDriverCommand = 120;
            }
        }
    }
}
