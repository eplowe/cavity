namespace Cavity.Net
{
    using System.Diagnostics.CodeAnalysis;

    public interface IRequestMethod
    {
        IResponseStatus Delete();

        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get", Justification = "Using HTTP method names.")]
        IResponseStatus Get();

        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get", Justification = "Using HTTP method names.")]
        IResponseStatus Get(bool head);

        IResponseStatus Head();

        IResponseStatus Options();

        IResponseStatus Post(IHttpContent content);

        IResponseStatus Put(IHttpContent content);

        IResponseStatus Use(string method);

        IResponseStatus Use(string method, bool head);

        IResponseStatus Use(string method, IHttpContent content);
    }
}