using OpenQA.Selenium;

namespace XUnitTestProject1.Selenium
{
    public interface IRegAppPage
    {
        IWebDriver Driver { get; }
        string Title { get; }
        string DriverName { get; }
        string Url { get; }

        //IWebElement ContinueButton { get; }
        //IWebElement SubmitButton { get; }
    }
}