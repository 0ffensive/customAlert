using OpenQA.Selenium;

namespace XUnitTestProject1.Selenium
{
    public interface IRegAppPage
    {
        IWebDriver Driver { get; }
        IWebElement ContinueButton { get; }
        IWebElement SubmitButton { get; }
    }
}