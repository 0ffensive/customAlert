using System.Linq;
using CustomAlertBoxDemo.Selenium.Core;
using CustomAlertBoxDemo.Selenium.Models;
using OpenQA.Selenium;

namespace CustomAlertBoxDemo.Selenium
{
    public class RegistrationPage : BasePage
    {
        string placowkaEdynburg = "https://secure.e-konsulat.gov.pl/Informacyjne/Placowka.aspx?IDPlacowki=159";
        string placowkaManchester = "https://secure.e-konsulat.gov.pl/Informacyjne/Placowka.aspx?IDPlacowki=115";
        string rezerwacja = "https://secure.e-konsulat.gov.pl/Wizyty/Paszportowe/RejestracjaTerminuWizytyPaszportowej.aspx?IDPlacowki=";

        public RegistrationPage(IWebDriver driver) : base(driver)
        {
        }

        string GetPlacowkaId(string input) => input.Substring(input.Length - 3);

        public void Start(string where, int dzieci = 1)
        {
            if (where == "Edynburg") // == "Edynburg"
                Url = placowkaEdynburg;
            else
                Url = placowkaManchester;

            string idPlacowki = GetPlacowkaId(Url);
            Driver.Navigate().GoToUrl(Url);
            Driver.Navigate().GoToUrl(rezerwacja + idPlacowki);
            
            if (where == "Edynburg")
            {
                LokalizacjaDropDown.AsSelect.SelectByText("Edynburg");
            }

            DzieciDropDown.AsSelect.SelectByText($"{dzieci}");
        }

        public void ReloadCurrentPage(int selectDzieci = 1)
        {
            string current = Driver.Url;
            Driver.Navigate().GoToUrl(current);
            
            this.ScrollToBottom();
            DzieciDropDown.AsSelect.SelectByText($"{selectDzieci}");
        }

        public void Rezerwuj()
        {
            DniDropDown.AsSelect.SelectByIndex(0);
            GodzinyDropDown.AsSelect.SelectByIndex(1);
            RezerwujButton.Click();
        }

        public MyElement LokalizacjaDropDown => new MyElement(this, By.Id("cp_cbLokalizacja"));

        public MyElement DzieciDropDown => new MyElement(this, By.Id("cp_ctrlDzieci"));
        public MyElement DniDropDown => new MyElement(this, By.Id("cp_ctrlDni"));
        public MyElement GodzinyDropDown => new MyElement(this, By.Id("cp_ctrlGodziny"));

        public IWebElement KomunikatSpan => Driver.FindElement(By.Id("cp_LabelKomunikat"));
        public IWebElement RezerwujButton => Driver.FindElement(By.Id("cp_btnZarejestruj"));
        //public IWebElement HomeLink => Driver.FindElement(By.LinkText("Home"));
    }
}