namespace Cavity.Net
{
    using System.Net;
    using Xunit;

    public sealed class HttpWebClientFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<HttpWebClient>()
                            .DerivesFrom<DisposableObject>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<IHttpClient>()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            using (var client = new HttpWebClient())
            {
                Assert.NotNull(client);
            }
        }

        [Fact]
        public void prop_AutoRedirect()
        {
            Assert.True(new PropertyExpectations<HttpWebClient>(x => x.AutoRedirect)
                            .IsAutoProperty<bool>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Cookies()
        {
            Assert.True(new PropertyExpectations<HttpWebClient>(x => x.Cookies)
                            .TypeIs<CookieContainer>()
                            .DefaultValueIsNotNull()
                            .IsNotDecorated()
                            .Result);
        }
    }
}