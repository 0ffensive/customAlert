//Base code courtesy of:
//https://dev.to/rachelsoderberg/create-a-simple-web-scraper-in-c-1l1m

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using AngleSharp.Text;

namespace CustomAlertBoxDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class Scraper
    {
        public StringBuilder Results { get; set; }

        private string Title { get; set; }
        private string Url { get; set; }
        private string siteUrl = "https://www.oceannetworks.ca/news/stories";
        public string[] QueryTerms { get; } = { "Ocean", "Nature", "Pollution" };
        
        public async Task<string> ScrapeWebsite()
        {
            Results =  new StringBuilder();

            CancellationTokenSource cancellationToken = new CancellationTokenSource();
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage request = await httpClient.GetAsync(siteUrl);
            cancellationToken.Token.ThrowIfCancellationRequested();

            Stream response = await request.Content.ReadAsStreamAsync();
            cancellationToken.Token.ThrowIfCancellationRequested();

            HtmlParser parser = new HtmlParser();
            IHtmlDocument document = parser.ParseDocument(response);

            //Add connection between initial scrape, and parsing of results
            return GetScrapeResults(document);
        }

        private string GetScrapeResults(IHtmlDocument document)
        {
            IEnumerable<IElement> articleLink = null;

            foreach (var term in QueryTerms)
            {
                articleLink = document.All.Where(x =>
                    x.ClassName == "views-field views-field-nothing" &&
                    (x.ParentElement.InnerHtml.Contains(term) || x.ParentElement.InnerHtml.Contains(term.ToLower()))).Skip(1);

                //Overwriting articleLink above means we have to print it's result for all QueryTerms
                //Appending to a pre-declared IEnumerable (like a List), could mean taking this out of the main loop.
                if (articleLink.Any())
                {
                    PrintResults(articleLink);
                }
            }

            return Results.ToString();
        }

        public void PrintResults(IEnumerable<IElement> articleLink)
        {
            //Every element needs to be cleaned and displayed
            foreach (var element in articleLink)
            {
                CleanUpResults(element);

                Results.Append($"{Title} - {Url}{Environment.NewLine}");
            }
        }

        private void CleanUpResults(IElement result)
        {
            string htmlResult = result.InnerHtml.ReplaceFirst("        <span class=\"field-content\"><div><a href=\"", @"https://www.oceannetworks.ca");
            htmlResult = htmlResult.ReplaceFirst("\">", "*");
            htmlResult = htmlResult.ReplaceFirst("</a></div>\n<div class=\"article-title-top\">", "-");
            htmlResult = htmlResult.ReplaceFirst("</div>\n<hr></span>  ", "");

            //Seperate the results into our class fields for use in PrintResults()
            SplitResults(htmlResult);
        }

        private void SplitResults(string htmlResult)
        {
            string[] splitResults = htmlResult.Split('*');
            Url = splitResults[0];
            Title = splitResults[1];
        }
    }
}