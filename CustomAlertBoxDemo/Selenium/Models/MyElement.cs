using System;
using CustomAlertBoxDemo.Selenium.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CustomAlertBoxDemo.Selenium.Models
{
    public class MyElement
    {
        private By locator;
        private IPage page;
        private IWebDriver driver => page.Driver;

        public IWebElement Element => driver.FindElement(locator);

        public MyElement(IPage page, By locator)
        {
            this.page = page;
            this.locator = locator;
        }

        public SelectElement AsSelect => new SelectElement(Element);

        public bool Exists(int seconds = 3)
        {
            try
            {
                var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 0, seconds));
                wait.Until(ExpectedConditions.ElementIsVisible(locator));
                return true;
            }
            catch(WebDriverTimeoutException tex){
                return false;
            }
        }

        public bool IsEnabled => Element.Enabled;
    }
}