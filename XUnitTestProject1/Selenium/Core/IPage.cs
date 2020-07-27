using OpenQA.Selenium;

namespace XUnitTestProject1.Selenium
{
    public interface IPage
    {
        IWebDriver Driver { get; }
        string Title { get; }
        string DriverName { get; }
        string Url { get; }
    }
}