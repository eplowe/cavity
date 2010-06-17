namespace Cavity.Net
{
    using System.Xml.XPath;
    using Cavity;
    using Xunit;

    public sealed class HttpClientSettingsFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(new TypeExpectations<HttpClientSettings>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsSealed()
                .HasDefaultConstructor()
                .XmlRoot("httpClient")
                .Result);
        }

        [Fact]
        public void deserialize()
        {
            var actual = "<httpClient keepAlive='true' />".XmlDeserialize<HttpClientSettings>();

            Assert.True(actual.KeepAlive);
        }

        [Fact]
        public void deserializeEmpty()
        {
            var actual = "<httpClient />".XmlDeserialize<HttpClientSettings>();

            Assert.False(actual.KeepAlive);
        }

        [Fact]
        public void serialize()
        {
            var obj = new HttpClientSettings
            {
                KeepAlive = true
            };

            XPathNavigator navigator = obj.XmlSerialize().CreateNavigator();

            Assert.True((bool)navigator.Evaluate("1=count(/httpClient[@keepAlive='true'])"));
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new HttpClientSettings());
        }

        [Fact]
        public void prop_KeepAlive()
        {
            Assert.True(new PropertyExpectations<HttpClientSettings>("KeepAlive")
                .IsAutoProperty<bool>()
                .XmlAttribute("keepAlive")
                .Result);
        }
    }
}