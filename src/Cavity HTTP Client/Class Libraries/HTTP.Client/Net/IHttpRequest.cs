namespace Cavity.Net
{
    using System;

    public interface IHttpRequest : IHttpMessage
    {
        Uri AbsoluteUri { get; }

        RequestLine RequestLine { get; }
    }
}