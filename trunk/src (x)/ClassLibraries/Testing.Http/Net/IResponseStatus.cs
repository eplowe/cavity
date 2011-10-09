namespace Cavity.Net
{
    using System.Diagnostics.CodeAnalysis;
    using System.Net;

    public interface IResponseStatus
    {
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Is", Justification = "The naming is intentional.")]
        IResponseCacheControl Is(HttpStatusCode status);

        ITestHttp IsSeeOther(AbsoluteUri location);
    }
}