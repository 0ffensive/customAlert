using System.Net;
using System.Net.Http;

namespace CustomAlertBoxDemo
{
    public class MyClientHandler : HttpClientHandler
    {

        public MyClientHandler()
        {
            UseProxy = false;
            UseCookies = true;
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
        }
    }
}