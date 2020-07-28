using CustomAlertBoxDemo.Selenium.Core;
using OpenQA.Selenium;

namespace CustomAlertBoxDemo.Selenium.Models
{
    public abstract class BasePage : IPage
    {
        public IWebDriver Driver { get; }

        public BasePage(IWebDriver driver)
        {
            this.Driver = driver;
        }

        public string Title => Driver.Title;
        public string DriverName => Driver.GetType().ToString();
        
        public string Url { get; set; }

    }
}