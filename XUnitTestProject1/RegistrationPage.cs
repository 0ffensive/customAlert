using OpenQA.Selenium;

namespace XUnitTestProject1.Selenium
{
    public class RegistrationPage : BasePage
    {
        public RegistrationPage(IWebDriver driver) : base(driver)
        {
            Url = "https://secure.e-konsulat.gov.pl/Wizyty/Paszportowe/RejestracjaTerminuWizytyPaszportowej.aspx?IDPlacowki=159";
        }

        public void Start(string where, int dzieci = 1)
        {
            Driver.Navigate().GoToUrl(Url);
            //
            LokalizacjaDropDown.AsSelect.SelectByText(where);
            DzieciDropDown.AsSelect.SelectByText($"{dzieci}");
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