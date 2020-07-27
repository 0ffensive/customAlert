using System;
using System.IO;
using System.Threading;
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
        //string url2 = "https://secure.e-konsulat.gov.pl/Wizyty/Paszportowe/RejestracjaTerminuWizytyPaszportowej.aspx?IDPlacowki=159";

        [Fact]
        public void StartApplication()
        {
            driver.Navigate().GoToUrl(placowkaEdynburg);
            //driver.Navigate().GoToUrl(url2);
            //var locationDdl = new SelectElement(driver.FindElement(By.Id("cp_cbLokalizacja")));
            //locationDdl.SelectByText("Edynburg");
            //var howManyPersonsDdl = new SelectElement(driver.FindElement(By.Id("cp_ctrlDzieci")));
            //locationDdl.SelectByText("1");

            RegistrationPage reg = new RegistrationPage(driver);
            reg.Start("Edynburg");
            string txt = reg.KomunikatSpan.Text;
            bool exists = reg.Dni.Exists();
            bool dis = reg.Lokalizacja.Element.Enabled;

            if (exists)
            {
                reg.Dni.AsSelect.SelectByIndex(0);
                reg.

            }

            //cp_LabelKomunikat
            //cp_ctrlDni
            //cp_ctrlGodziny
            //
            //driver.Wa

            string src = driver.PageSource;
            Assert.True(txt != null);
        }

        //public void OpenApplicationReturnedPage()
        //{
        //    // Wait up to 2 minutes for the application to be sent to CRM otherwise the application returned page will redirect user to the home page
        //    for (int i = 1; i <= 120; i++)
        //    {
        //        driver.Navigate().GoToUrl(driverFactory.ServiceUrl + applicationReturnedPage.PageUrl);
        //        if (driver.FindElement(By.CssSelector("h1")).Text == HomePageHeading)
        //        {
        //            Thread.Sleep(1000);
        //        }
        //        else
        //            break;
        //    }
        //}

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

        public void WaitForLoginPageToLoad()
        {
            // Wait for the login page to load
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // Better to wait for the logo to appear instead of waiting for the sign button
            //_ = wait.Until(e => e.FindElement(By.Id("next")));
            switch (driverFactory.ServiceUrl)
            {
                case Constants.LocalSettings.TestUrl:
                    _ = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[src='https://shareddevblob.blob.core.windows.net/cqcaccess/cqc-logo.png'")));
                    break;
                case Constants.LocalSettings.PreProdUrl:
                    _ = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[src='https://adb2cprodcstorage.blob.core.windows.net/custompages/cqc-logo.png'")));
                    break;
                default:
                    _ = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[src='https://shareddevblob.blob.core.windows.net/cqcaccess/cqc-logo.png'")));
                    break;
            }
        }

    }
}
