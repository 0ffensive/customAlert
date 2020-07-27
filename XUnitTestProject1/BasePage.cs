using OpenQA.Selenium;

namespace XUnitTestProject1.Selenium
{
    public abstract class BasePage : IRegAppPage
    {
        public IWebDriver Driver { get; }

        public BasePage(IWebDriver driver)
        {
            this.Driver = driver;
        }

        public string Title => Driver.Title;
        public string DriverName => Driver.GetType().ToString();
        
        public string Url { get; set; }

        public bool IsElementPresent(By by)
        {
            try{
                Driver.FindElement(by);
                return true;
            }
            catch(NoSuchElementException e){
                return false;
            }
        }

    }
}