namespace Cavity.Net
{
    using Cavity.Configuration;
    using Xunit;

    public sealed class HttpClientFacts
    {
        [Fact]
        public void prop_UserAgent()
        {
            ServiceLocation.Settings().Configure();

            var client = new HttpClient();

            Assert.Equal(new UserAgent().Value, client.UserAgent);
        }
    }
}