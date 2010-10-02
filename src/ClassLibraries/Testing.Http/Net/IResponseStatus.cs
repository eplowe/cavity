namespace Cavity.Net
{
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Net;

    public interface IResponseStatus
    {
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Is", Justification = "The naming is intentional.")]
        IResponseCacheControl Is(HttpStatusCode status);

        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Is", Justification = "The naming is intentional.")]
        IResponseCacheControl Is(HttpStatusCode status, bool hasVersionHeaders);

        ITestHttp IsContentNegotiation(string extension);

        ITestHttp IsContentNegotiationToTextHtml();

        ITestHttp IsLanguageNegotiation(CultureInfo language);

        ITestHttp IsLanguageNegotiation(string language);

        ITestHttp IsLanguageNegotiationToEnglish();

        ITestHttp IsSeeOther(AbsoluteUri location);
    }
}