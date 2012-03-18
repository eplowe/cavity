namespace Cavity.Web.Mvc
{
    using System;
    using System.Net;
    using System.Web;

    using Moq;

    using Xunit;

    public sealed class TemporaryRedirectResultFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<TemporaryRedirectResult>()
                            .DerivesFrom<RedirectionResult>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor_string()
        {
            Assert.NotNull(new TemporaryRedirectResult("/path"));
        }

        [Fact]
        public void op_SetCache_HttpCachePolicyBase()
        {
            var expires = DateTime.UtcNow.AddMinutes(5);
            var obj = new TemporaryRedirectResult("/path")
                          {
                              Expires = expires
                          };

            var cache = new Mock<HttpCachePolicyBase>(MockBehavior.Strict);
            cache
                .Setup(x => x.SetCacheability(HttpCacheability.Public))
                .Verifiable();
            cache
                .Setup(x => x.SetExpires(expires))
                .Verifiable();

            obj.SetCache(cache.Object);

            cache.VerifyAll();
        }

        [Fact]
        public void op_SetCache_HttpCachePolicyBaseNull()
        {
            Assert.Throws<ArgumentNullException>(() => new TemporaryRedirectResult("/path").SetCache(null));
        }

        [Fact]
        public void op_SetCache_HttpCachePolicyBase_whenNoExpires()
        {
            var obj = new TemporaryRedirectResult("/path");

            var cache = new Mock<HttpCachePolicyBase>(MockBehavior.Strict);
            cache
                .Setup(x => x.SetCacheability(HttpCacheability.NoCache))
                .Verifiable();

            obj.SetCache(cache.Object);

            cache.VerifyAll();
        }

        [Fact]
        public void prop_Expires()
        {
            Assert.NotNull(new PropertyExpectations<TemporaryRedirectResult>(x => x.Expires)
                               .TypeIs<DateTime?>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_Expires_get()
        {
            Assert.False(new TemporaryRedirectResult("/path").Expires.HasValue);
        }

        [Fact]
        public void prop_StatusCode()
        {
            const HttpStatusCode expected = HttpStatusCode.TemporaryRedirect;
            var actual = new TemporaryRedirectResult("/path").StatusCode;

            Assert.Equal(expected, actual);
        }
    }
}