namespace Cavity.Net
{
    using System;
    using System.Globalization;
    using System.Net;

    public sealed class IResponseStatusDummy : IResponseStatus
    {
        IResponseCacheControl IResponseStatus.Is(HttpStatusCode status)
        {
            throw new NotSupportedException();
        }

        IResponseCacheControl IResponseStatus.Is(HttpStatusCode status, bool hasVersionHeaders)
        {
            throw new NotSupportedException();
        }

        ITestHttp IResponseStatus.IsContentNegotiation(string extension)
        {
            throw new NotSupportedException();
        }

        ITestHttp IResponseStatus.IsContentNegotiationToTextHtml()
        {
            throw new NotSupportedException();
        }

        ITestHttp IResponseStatus.IsLanguageNegotiation(CultureInfo language)
        {
            throw new NotSupportedException();
        }

        ITestHttp IResponseStatus.IsLanguageNegotiation(string language)
        {
            throw new NotSupportedException();
        }

        ITestHttp IResponseStatus.IsLanguageNegotiationToEnglish()
        {
            throw new NotSupportedException();
        }

        ITestHttp IResponseStatus.IsSeeOther(AbsoluteUri location)
        {
            throw new NotSupportedException();
        }
    }
}