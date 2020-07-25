using AngleSharp;
using AngleSharp.Html.Dom;
using System;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1Async()
        {
            var context = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
            var queryDocument = await context.OpenAsync("https://google.com");
            IHtmlFormElement form = queryDocument.QuerySelector("form") as IHtmlFormElement;
            var resultDocument = await form.SubmitAsync(new { q = "anglesharp" });
            
            Assert.True(resultDocument != null);
            //
        }

        [Fact]
        public async Task t2()
        {
            //string url = "https://secure.e-konsulat.gov.pl/Wizyty/Paszportowe/RejestracjaTerminuWizytyPaszportowej.aspx?IDPlacowki=159";
            string url = "https://secure.e-konsulat.gov.pl/Informacyjne/Placowka.aspx?IDPlacowki=159";

            var context = BrowsingContext.New(Configuration.Default.WithDefaultLoader().WithCookies());
            var document = await context.OpenAsync(url);
            string result = document.DocumentElement.OuterHtml;

            Assert.True(result  != null);
            //https://secure.e-konsulat.gov.pl/Informacyjne/Placowka.aspx?IDPlacowki=159
        }
    }
}
