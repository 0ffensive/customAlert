using OpenQA.Selenium;

namespace CustomAlertBoxDemo.Selenium.Core
{
    public interface IPage
    {
        IWebDriver Driver { get; }
        string Title { get; }
        string DriverName { get; }
        string Url { get; }
    }
}