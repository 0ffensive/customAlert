using System;
using System.IO;
using System.Threading;
using CustomAlertBoxDemo.Selenium;
using CustomAlertBoxDemo.Selenium.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Xunit;
using Xunit.Abstractions;

#pragma warning disable 0618

namespace XUnitTestProject1.Selenium
{
    public class Test : TestBase
    {
        public Test(ITestOutputHelper output) : base(output) { }

        string placowkaEdynburg = "https://secure.e-konsulat.gov.pl/Informacyjne/Placowka.aspx?IDPlacowki=159";

        [Fact]
        public void StartApplication()
        {
            driver.Navigate().GoToUrl(placowkaEdynburg);

            RegistrationPage reg = new RegistrationPage(driver);
            reg.Start("Edynburg");
            string txt = reg.KomunikatSpan.Text;
            bool exists = reg.DniDropDown.Exists();
            //bool dis = reg.Lokalizacja.Element.Enabled;

            if (exists)
            {
                reg.DniDropDown.AsSelect.SelectByIndex(0);
                reg.GodzinyDropDown.AsSelect.SelectByIndex(1);
                reg.RezerwujButton.Click();
            }

            string src = driver.PageSource;
            Assert.True(txt != null);
        }

        //public void WaitForHomePageToLoad()
        //{
        //    // Don't wait if the page returns ERR_SSL_PROTOCOL_ERROR which sometimes happens on the pipeline
        //    if (homePage.Heading.Text != "This site can’t provide a secure connection")
        //    {
        //        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        //        // Wait for the CQC logo to appear
        //        _ = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[src='/images/cqc-logo.png'")));
        //    }
        //}

    }
}
