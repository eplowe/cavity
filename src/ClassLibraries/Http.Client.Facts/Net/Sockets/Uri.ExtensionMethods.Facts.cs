namespace Cavity.Net.Sockets
{
    using System;
    using Xunit;

    public sealed class UriExtensionMethodsFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(typeof(UriExtensionMethods).IsStatic());
        }

        [Fact]
        public void ToTcpClient_UriAbsolute()
        {
            Assert.NotNull(new Uri("http://www.example.com/").ToTcpClient());
        }

        [Fact]
        public void ToTcpClient_UriNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as Uri).ToTcpClient());
        }

        [Fact]
        public void ToTcpClient_UriRelative()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Uri("/", UriKind.Relative).ToTcpClient());
        }
    }
}