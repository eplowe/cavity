namespace Cavity.Net
{
    using System.Diagnostics.CodeAnalysis;

    using Xunit;
    using Xunit.Extensions;

    [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "HttpRequest", Justification = "This naming is correct.")]
    public sealed class HttpRequestAttributeFacts
    {
        [Theory]
        [HttpRequest("example.request")]
        public void usage(HttpRequest request)
        {
            Assert.NotNull(request);
        }
    }
}