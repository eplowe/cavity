namespace Cavity.Net
{
    using System;
    using Xunit;

    public sealed class HttpExpectationsFacts
    {
        [Fact]
        public void ITestHttp_prop_Result()
        {
            Assert.False((new HttpExpectations("http://example.com/") as ITestHttp).Result);
        }

        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<HttpExpectations>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<ITestHttp>()
                            .Result);
        }

        [Fact]
        public void ctor_AbsoluteUri()
        {
            Assert.NotNull(new HttpExpectations(new AbsoluteUri("http://example.com/")));
        }

        [Fact]
        public void ctor_AbsoluteUriNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpExpectations(null));
        }

        [Fact]
        public void prop_RequestUri()
        {
            Assert.True(new PropertyExpectations<HttpExpectations>(p => p.RequestUri)
                            .IsNotDecorated()
                            .TypeIs<AbsoluteUri>()
                            .ArgumentNullException()
                            .Result);
        }
    }
}