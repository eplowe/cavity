namespace Cavity.Web.Mvc
{
    using System;
    using System.Net;
    using System.Web;

    using Moq;

    using Xunit;

    public sealed class SeeOtherResultFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<SeeOtherResult>()
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
            Assert.NotNull(new SeeOtherResult("/path"));
        }

        [Fact]
        public void op_SetCache_HttpCachePolicyBase()
        {
            var cache = new Mock<HttpCachePolicyBase>(MockBehavior.Strict);
            cache
                .Setup(x => x.SetCacheability(HttpCacheability.NoCache))
                .Verifiable();

            new SeeOtherResult("/path").SetCache(cache.Object);

            cache.VerifyAll();
        }

        [Fact]
        public void op_SetCache_HttpCachePolicyBaseNull()
        {
            Assert.Throws<ArgumentNullException>(() => new SeeOtherResult("/path").SetCache(null));
        }

        [Fact]
        public void prop_StatusCode()
        {
            const HttpStatusCode expected = HttpStatusCode.SeeOther;
            var actual = new SeeOtherResult("/path").StatusCode;

            Assert.Equal(expected, actual);
        }
    }
}