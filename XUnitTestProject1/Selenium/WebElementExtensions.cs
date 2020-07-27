using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace XUnitTestProject1.Selenium
{
    public static class WebElementExtensions
    {
        public static void ScrollIntoView(this IRegAppPage page, IWebElement element)
        {
            IJavaScriptExecutor js = ((IJavaScriptExecutor)page.Driver);
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        public static void ScrollAndClick(this IRegAppPage page, IWebElement element)
        {
            page.ScrollIntoView(element);

            Actions action = new Actions(page.Driver);
            action.MoveToElement(element).Click().Perform();
        }

        //public static void SelectAndContinue(this IRegAppPage page, IWebElement element)
        //{
        //    // Scroll the webelement into view before clicking to avoid the action failing on mobile browsers
        //    page.ScrollAndClick(element);
        //    page.ContinueButton.Click();
        //}

        //public static void SelectAndSubmit(this IRegAppPage page, IWebElement element)
        //{
        //    // Scroll the webelement into view before clicking to avoid the action failing on mobile browsers
        //    page.ScrollAndClick(element);
        //    page.SubmitButton.Click();
        //}

        public static string Value(this IWebElement element)
        {
            return element.GetAttribute("value");
        }

        public static string Id(this IWebElement element)
        {
            return element.GetAttribute("id");
        }

        public static void CopyPasteDisplayedValue(this IWebElement element)
        {
            element.SendKeys(Keys.Control + "a");
            element.SendKeys(Keys.Control + "c");
            element.Clear();
            element.SendKeys(Keys.Control + "V");
        }
    }
}
