using AngleSharp;
using AngleSharp.Html.Dom;
using System;
using System.Threading.Tasks;
using AngleSharp.Browser;
using AngleSharp.Dom;
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

            Configuration cc = new Configuration();
            var context = BrowsingContext.New(Configuration.Default.WithDefaultCookies().WithDefaultLoader()
                    //.WithJs()
                //.WithTemporaryCookies()
                //.WithPersistentCookies("D:\\logs")
            );
            var document1 = await context.OpenAsync(url1)
                .WaitUntilAvailable();
            string result1 = document1.DocumentElement.OuterHtml;
            //string result1 = document1.TextContent;
            //var cc1 = context.GetCookie(new Url(url1));
            //context.

            var document2 = await context.OpenAsync(url2)
                .WaitUntilAvailable();
            string result2 = document2.DocumentElement.OuterHtml;
            string result2a = document2.DocumentElement.InnerHtml;
            //string result2 = document2.TextContent;

            //ctl00$cp$cbLokalizacja
            var document3 = await SubmitForm(document2, "ctl00$cp$cbLokalizacja", "562");
            //var document3 = await SubmitForm(document2, "cp_cbLokalizacja", "562");
            //
            string result3 = document3.DocumentElement.OuterHtml;
            Assert.True(result3  != null);
            //https://secure.e-konsulat.gov.pl/Informacyjne/Placowka.aspx?IDPlacowki=159
        }

        Task<IDocument> SubmitForm(IDocument document, string eventTarget, string eventArgument)
        {
            var theForm = document.Forms["form1"];

            //if (!theForm) {
            //    throw new InvalidOperationException("The form cannot be found!");
            //}

            void SetElement(string name, string value)
            {
                var element = theForm.Elements[name] as IHtmlInputElement;

                if (element != null)
                {
                    element.Value = value;
                }
            }

            SetElement("__EVENTTARGET", eventTarget);
            SetElement("__EVENTARGUMENT", eventArgument);
            return theForm.SubmitAsync();
        }
    }
}
