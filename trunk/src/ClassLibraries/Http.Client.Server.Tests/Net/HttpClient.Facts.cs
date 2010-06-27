namespace Cavity
{
    using Cavity.Configuration;
    using Cavity.Net;
    using Xunit;

    public sealed class HttpClientFacts
    {
        [Fact]
        public void prop_UserAgent()
        {
            ServiceLocation.Settings().Configure();

            var client = new HttpClient();

            Assert.Equal<string>(new UserAgent().Value, client.UserAgent);
        }
    }
}