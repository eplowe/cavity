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
        public void get()
        {
            try
            {
                ServiceLocation.Settings().Configure();

                HttpRequest request =
                    "GET http://cavity.example.net/plain.txt HTTP/1.1" + Environment.NewLine +
                    "Host: cavity.example.net" + Environment.NewLine +
                    "Connection: close" + Environment.NewLine +
                    string.Empty;

                var client = new HttpClient();
                client.Navigate(request);

                Assert.Equal<string>("HTTP/1.1 200 OK", client.Response.StatusLine);

                Assert.Equal("text/plain", client.Response.Headers.ContentType.MediaType);
                Assert.True(client.Response.Headers.ContainsName("Accept-Ranges"));
                Assert.Equal("4", client.Response.Headers["Content-Length"]);
                Assert.True(client.Response.Headers.ContainsName("Date"));
                Assert.True(client.Response.Headers.ContainsName("ETag"));
                Assert.True(client.Response.Headers.ContainsName("Last-Modified"));
                Assert.Equal("close", client.Response.Headers["Connection"]);

                Assert.Equal("text", (string)client.Response.Body.Content);
            }
            finally
            {
                ServiceLocator.SetLocatorProvider(null);
            }
        }

        [Fact]
        public void head()
        {
            try
            {
                ServiceLocation.Settings().Configure();

                HttpRequest request =
                    "HEAD http://cavity.example.net/plain.txt HTTP/1.1" + Environment.NewLine +
                    "Host: cavity.example.net" + Environment.NewLine +
                    "Connection: close" + Environment.NewLine +
                    string.Empty;

                var client = new HttpClient();
                client.Navigate(request);

                Assert.Equal<string>("HTTP/1.1 200 OK", client.Response.StatusLine);

                Assert.Equal("text/plain", client.Response.Headers.ContentType.MediaType);
                Assert.True(client.Response.Headers.ContainsName("Accept-Ranges"));
                Assert.Equal("4", client.Response.Headers["Content-Length"]);
                Assert.True(client.Response.Headers.ContainsName("Date"));
                Assert.True(client.Response.Headers.ContainsName("ETag"));
                Assert.True(client.Response.Headers.ContainsName("Last-Modified"));
                Assert.Equal("close", client.Response.Headers["Connection"]);

                Assert.Equal(string.Empty, (string)client.Response.Body.Content);
            }
            finally
            {
                ServiceLocator.SetLocatorProvider(null);
            }
        }

        [Fact]
        public void options()
        {
            try
            {
                ServiceLocation.Settings().Configure();

                HttpRequest request =
                    "OPTIONS http://cavity.example.net/plain.txt HTTP/1.1" + Environment.NewLine +
                    "Host: cavity.example.net" + Environment.NewLine +
                    "Connection: close" + Environment.NewLine +
                    string.Empty;

                var client = new HttpClient();
                client.Navigate(request);

                Assert.Equal<string>("HTTP/1.1 200 OK", client.Response.StatusLine);

                Assert.Equal("OPTIONS, TRACE, GET, HEAD, POST", client.Response.Headers["Allow"]);
                Assert.Equal("OPTIONS, TRACE, GET, HEAD, POST", client.Response.Headers["Public"]);
                Assert.Null(client.Response.Headers.ContentType);
                Assert.True(client.Response.Headers.ContainsName("Date"));
                Assert.Equal("close", client.Response.Headers["Connection"]);

                Assert.Null(client.Response.Body);
            }
            finally
            {
                ServiceLocator.SetLocatorProvider(null);
            }
        }
    }
}