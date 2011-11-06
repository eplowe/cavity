namespace Cavity.Web.Mvc
{
    using System;
    using System.Net;
    using System.Web;
    using Moq;
    using Xunit;

    public sealed class FoundResultFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<FoundResult>()
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
            Assert.NotNull(new FoundResult("/path"));
        }

        [Fact]
        public void op_SetCache_HttpCachePolicyBase()
        {
            var expires = DateTime.UtcNow.AddMinutes(5);
            var obj = new FoundResult("/path")
            {
                Expires = expires
            };

            var cache = new Mock<HttpCachePolicyBase>();
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
            Assert.Throws<ArgumentNullException>(() => new FoundResult("/path").SetCache(null));
        }

        [Fact]
        public void op_SetCache_HttpCachePolicyBase_whenNoExpires()
        {
            var obj = new FoundResult("/path");

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
            Assert.NotNull(new PropertyExpectations<FoundResult>(x => x.Expires)
                               .TypeIs<DateTime?>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_Expires_get()
        {
            Assert.False(new FoundResult("/path").Expires.HasValue);
        }

        [Fact]
        public void prop_StatusCode()
        {
            const HttpStatusCode expected = HttpStatusCode.Found;
            var actual = new FoundResult("/path").StatusCode;

            Assert.Equal(expected, actual);
        }
    }
}