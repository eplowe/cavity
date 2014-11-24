namespace Cavity.Web.Mvc
{
    using System;
    using System.Net;
    using System.Web;
    using Moq;
    using Xunit;

    public sealed class MovedPermanentlyResultFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<MovedPermanentlyResult>()
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
            Assert.NotNull(new MovedPermanentlyResult("/path"));
        }

        [Fact]
        public void op_SetCache_HttpCachePolicyBase()
        {
            var obj = new MovedPermanentlyResult("/path")
                          {
                              Expires = DateTime.UtcNow.AddMinutes(5)
                          };

            var cache = new Mock<HttpCachePolicyBase>(MockBehavior.Strict);
            cache
                .Setup(x => x.SetCacheability(HttpCacheability.Public))
                .Verifiable();
            cache
                .Setup(x => x.SetExpires(obj.Expires))
                .Verifiable();

            obj.SetCache(cache.Object);

            cache.VerifyAll();
        }

        [Fact]
        public void op_SetCache_HttpCachePolicyBaseNull()
        {
            Assert.Throws<ArgumentNullException>(() => new MovedPermanentlyResult("/path").SetCache(null));
        }

        [Fact]
        public void prop_Expires()
        {
            Assert.NotNull(new PropertyExpectations<MovedPermanentlyResult>(x => x.Expires)
                               .TypeIs<DateTime>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_Expires_get()
        {
            Assert.True(new MovedPermanentlyResult("/path").Expires > DateTime.UtcNow);
        }

        [Fact]
        public void prop_StatusCode()
        {
            const HttpStatusCode expected = HttpStatusCode.MovedPermanently;
            var actual = new MovedPermanentlyResult("/path").StatusCode;

            Assert.Equal(expected, actual);
        }
    }
}