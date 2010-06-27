namespace Cavity
{
    using Cavity.Configuration;
    using Cavity.Net;
    using Xunit;

    public sealed class PlainTxtFacts
    {
        [Fact]
        public void ctor()
        {
            ServiceLocation.Settings().Configure();

            var client = new HttpClient();

            Assert.NotNull(client.UserAgent);
        }
    }
}