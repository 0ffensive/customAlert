using System.Threading;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Io;
using AngleSharp.Io.Network;

namespace CustomAlertBoxDemo
{
    public class Browser
    {
        public readonly IBrowsingContext Context;
        //public readonly MyHttpClient HttpClient;

        //public Browser(MyHttpClient httpClient)
        public Browser()
        {
            //HttpClient = httpClient;

            //We require a custom configuration
            var config = Configuration.Default.WithJs();

            //Create a new context for evaluating webpages with the given config
            var context = BrowsingContext.New(config);

            LoaderOptions lo = new LoaderOptions();
            lo.IsNavigationDisabled = false;
            lo.IsResourceLoadingEnabled = true;

            //var requester = new HttpClientRequester(httpClient);

            var configuration = Configuration.Default
                .WithDefaultLoader(lo)
                .WithJs()
                .WithPersistentCookies("d:\\logs")
                .WithRequesters(new MyClientHandler())
                //.WithCss()
                //.WithCookies()
                ;
            Context = BrowsingContext.New(configuration);
        }

        public async Task<IDocument> NavigateToUrl(string url)
        {
            var request = DocumentRequest.Get(Url.Create(url));

            //var response = await Context.NavigateTo.Loader.DownloadAsync(request).Task;
            //var document = await Context.OpenAsync(response, CancellationToken.None);
            
            var document = await Context.OpenAsync(request);
            return document;
        }
    }
}