namespace Cavity.Net
{
    using System;
    using Cavity;
    using Xunit;

    public sealed class HttpRequestHeadersFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(HttpRequestHeaders).IsStatic());
        }

        [Fact]
        public void prop_Accept_get()
        {
            const string expected = "Accept";
            var actual = HttpRequestHeaders.Accept;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_UserAgent_get()
        {
            const string expected = "User-Agent";
            var actual = HttpRequestHeaders.UserAgent;

            Assert.Equal(expected, actual);
        }
    }
}