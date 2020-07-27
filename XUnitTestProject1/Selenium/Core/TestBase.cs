using System;
using System.IO;
using System.Runtime.InteropServices;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using Xunit;
using Xunit.Abstractions;

#pragma warning disable 0168

namespace XUnitTestProject1.Selenium
{
    public class TestBase : XunitContextBase, IDisposable
    {
        public bool CloseBrowser { get; set; }

        // Driver factory and web driver
        public DriverFactory driverFactory;
        public IWebDriver driver;

        // Enable output logging in Xunit test
        protected readonly ITestOutputHelper output;

        public TestBase(ITestOutputHelper output) : base(output)
        {
            // Identify name of the test
            string testName = Context.Test.DisplayName;
            
            // Driver factory and web driver
            driverFactory = new DriverFactory();
            driver = driverFactory.CreateDriver();

            // Enable output logging in Xunit test
            this.output = output;

            // Enable capturing of exceptions for taking screenshots for failed tests
            XunitContext.EnableExceptionCapture();
        }

        public override void Dispose()
        {
            // Detect test failure, take screenshot and generate error log
            // Intercept test exception using XunitContext and other exceptions using Marshal.GetExceptionPointers
            if (Context.TestException != null || Marshal.GetExceptionPointers() != IntPtr.Zero)
            {
                string fileName = Context.Test.DisplayName + ".png";

                // Clean-up filename of illegal characters
                fileName = string.Join("_", fileName.Split(Path.GetInvalidFileNameChars()));

                if (!Directory.Exists(@"screenshots\"))
                    Directory.CreateDirectory(@"screenshots\");

                // Generate screenshot
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile($"screenshots\\{fileName}");

                // Capture page heading
                string pageHeading = "not found";
                try
                {
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(100);
                    pageHeading = driver.FindElement(By.CssSelector("h1")).Text;
                }
                catch (NoSuchElementException e)
                {
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Constants.Driver.TimeOut.FindWebElement);
                }

                // Capture page URL
                string pageUrl = driver.Url.Replace(driverFactory.ServiceUrl, "");
                if (!string.IsNullOrEmpty(pageUrl))
                    pageUrl = pageUrl.Substring(0, Math.Min(pageUrl.Length, 100));

                // Generate error log
                output.WriteLine($"##[error]\t{DateTime.UtcNow:dd/MM/yyyy HH:mm:ss}\tURL: {pageUrl}\tPageHeading: {pageHeading}\tTest: {Context.Test.DisplayName}");
            }

            // Close browser window and dispose webdriver and driver factory object
            if (CloseBrowser)
            {
                driver.Dispose();
                driverFactory.Dispose();
            }
        }
    }
}
