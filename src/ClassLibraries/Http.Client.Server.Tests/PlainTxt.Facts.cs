namespace Cavity
{
    using System;
    using Cavity.Configuration;
    using Cavity.Net;
    using Microsoft.Practices.ServiceLocation;
    using Xunit;

    public sealed class PlainTxtFacts
    {
        [Fact]
        public void get_plain_txt()
        {
            try
            {
                ServiceLocation.Settings().Configure();

                HttpRequest request =
                    "GET http://cavity.example.net/plain.txt HTTP/1.1" + Environment.NewLine +
                    "Host: cavity.example.net" + Environment.NewLine +
                    "Connection: close" + Environment.NewLine +
                    string.Empty;

                HttpClient client = new HttpClient();
                client.Navigate(request);

                Assert.Equal<string>("HTTP/1.1 200 OK", client.Response.StatusLine);

                Assert.Equal<string>("text/plain", client.Response.Headers.ContentType.MediaType);
                Assert.True(client.Response.Headers.ContainsName("Accept-Ranges"));
                Assert.True(client.Response.Headers.ContainsName("Content-Length"));
                Assert.True(client.Response.Headers.ContainsName("Date"));
                Assert.True(client.Response.Headers.ContainsName("ETag"));
                Assert.True(client.Response.Headers.ContainsName("Last-Modified"));
                Assert.Equal<string>("close", client.Response.Headers["Connection"]);

                Assert.Equal<string>("text", (string)client.Response.Body.Content);
            }
            finally
            {
                ServiceLocator.SetLocatorProvider(null);
            }
        }
    }
}