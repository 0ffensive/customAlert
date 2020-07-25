using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;

namespace CustomAlertBoxDemo
{
    public class MyHttpClient : HttpClient
    {
        private readonly CookieContainer _cookieContainer;

        public MyHttpClient(HttpClientHandler messageHandler) : base(messageHandler)
        {
            _cookieContainer = messageHandler.CookieContainer;
        }

        public void SetCookie(string key, string value, string path, string domain)
        {
            var c = new Cookie(key, value, path, domain);
            _cookieContainer.Add(c);
        }

        public IEnumerable<Cookie> GetAllCookies()
        {
            var k = (Hashtable) _cookieContainer.GetType()
                    .GetField("m_domainTable", BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.GetValue(_cookieContainer);
            if (k == null) yield break;
            foreach (DictionaryEntry element in k)
            {
                var l = (SortedList) element.Value.GetType()
                        .GetField("m_list", BindingFlags.Instance | BindingFlags.NonPublic)
                        ?.GetValue(element.Value);
                
                if (l != null)
                    foreach (var e in l)
                    {
                        var cl = (CookieCollection) ((DictionaryEntry) e).Value;
                        foreach (Cookie fc in cl)
                            yield return fc;
                    }
            }
        }
    }
}