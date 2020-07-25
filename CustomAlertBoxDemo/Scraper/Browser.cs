using System.Threading;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Io;

namespace CustomAlertBoxDemo
{
    public class Browser
    {
        public readonly IBrowsingContext Context;
        public readonly MyHttpClient HttpClient;

        public Browser(MyHttpClient httpClient)
        {
            //We require a custom configuration
            var config = Configuration.Default.WithJs();

            //Create a new context for evaluating webpages with the given config
            var context = BrowsingContext.New(config);


                HttpClient = httpClient;
            var requester = new HttpClientRequester(httpClient);
            var configuration = Configuration
                .Default
                .WithDefaultLoader(requesters: new[] {requester},
                    setup: setup => { setup.IsNavigationEnabled = true; })
                .WithJavaScript()
                .WithCss()
                .WithCookies();
            Context = BrowsingContext.New(configuration);
        }

        public async Task<IDocument> NavigateToUrl(string url)
        {
            var request = DocumentRequest.Get(Url.Create(url));


            var response = await Context.Loader.DownloadAsync(request).Task;
            var document = await Context.OpenAsync(response, CancellationToken.None);
            return document;
        }
    }
}