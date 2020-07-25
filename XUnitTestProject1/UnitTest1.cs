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
            string url1 = "https://secure.e-konsulat.gov.pl/Informacyjne/Placowka.aspx?IDPlacowki=159";
            string url2 = "https://secure.e-konsulat.gov.pl/Wizyty/Paszportowe/RejestracjaTerminuWizytyPaszportowej.aspx?IDPlacowki=159";

            var context = BrowsingContext.New(Configuration.Default.WithDefaultLoader().WithJs()
                .WithTemporaryCookies()
                //.WithPersistentCookies("D:\\logs")
            );
            var document1 = await context.OpenAsync(url1);
            string result1 = document1.DocumentElement.OuterHtml;
            var cc1 = context.GetCookie(new Url(url1));

            var document2 = await context.OpenAsync(url2);
            string result2 = document2.DocumentElement.OuterHtml;

            Assert.True(result2  != null);
            //https://secure.e-konsulat.gov.pl/Informacyjne/Placowka.aspx?IDPlacowki=159
        }
    }
}
