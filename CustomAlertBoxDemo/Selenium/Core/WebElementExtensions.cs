using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace CustomAlertBoxDemo.Selenium.Core
{
    public static class WebElementExtensions
    {
        public static void ScrollIntoView(this IPage page, IWebElement element)
        {
            IJavaScriptExecutor js = ((IJavaScriptExecutor)page.Driver);
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            //js.executeScript("window.scrollTo(0, document.body.scrollHeight)");
        }

        public static void ScrollToBottom(this IPage page)
        {
            //Does not work
            //IJavaScriptExecutor js = ((IJavaScriptExecutor)page.Driver);
            //js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");

            Actions actions = new Actions(page.Driver);
            actions.KeyDown(Keys.Control).SendKeys(Keys.End).Perform();
        }

        public static void ScrollAndClick(this IPage page, IWebElement element)
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
