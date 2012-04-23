namespace Cavity.Net
{
    using System;
    using System.Net;

    public interface IHttpClient : IDisposable
    {
        bool AutoRedirect { get; set; }

        CookieContainer Cookies { get; }
    }
}