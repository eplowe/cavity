namespace Cavity.Net
{
    using System;
    using Cavity;
    using Xunit;

    public sealed class HttpGeneralHeadersFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(HttpGeneralHeaders).IsStatic());
        }

        [Fact]
        public void prop_CacheControl_get()
        {
            const string expected = "Cache-Control";
            var actual = HttpGeneralHeaders.CacheControl;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Connection_get()
        {
            const string expected = "Connection";
            var actual = HttpGeneralHeaders.Connection;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Date_get()
        {
            const string expected = "Date";
            var actual = HttpGeneralHeaders.Date;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Pragma_get()
        {
            const string expected = "Pragma";
            var actual = HttpGeneralHeaders.Pragma;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Trailer_get()
        {
            const string expected = "Trailer";
            var actual = HttpGeneralHeaders.Trailer;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_TransferEncoding_get()
        {
            const string expected = "Transfer-Encoding";
            var actual = HttpGeneralHeaders.TransferEncoding;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Upgrade_get()
        {
            const string expected = "Upgrade";
            var actual = HttpGeneralHeaders.Upgrade;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Via_get()
        {
            const string expected = "Via";
            var actual = HttpGeneralHeaders.Via;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Warning_get()
        {
            const string expected = "Warning";
            var actual = HttpGeneralHeaders.Warning;

            Assert.Equal(expected, actual);
        }
    }
}