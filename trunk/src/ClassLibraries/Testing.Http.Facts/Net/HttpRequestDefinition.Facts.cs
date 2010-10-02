namespace Cavity.Net
{
    using System;
    using Cavity.Data;
    using Xunit;

    public sealed class HttpRequestDefinitionFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<HttpRequestDefinition>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor_AbsoluteUri()
        {
            Assert.NotNull(new HttpRequestDefinition(new AbsoluteUri("http://example.com/")));
        }

        [Fact]
        public void ctor_AbsoluteUriNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpRequestDefinition(null));
        }

        [Fact]
        public void prop_Headers()
        {
            Assert.True(new PropertyExpectations<HttpRequestDefinition>(p => p.Headers)
                            .IsNotDecorated()
                            .TypeIs<DataCollection>()
                            .DefaultValueIsNotNull()
                            .Result);
        }

        [Fact]
        public void prop_Method()
        {
            Assert.True(new PropertyExpectations<HttpRequestDefinition>(p => p.Method)
                            .IsNotDecorated()
                            .IsAutoProperty<string>()
                            .Result);
        }

        [Fact]
        public void prop_RequestUri()
        {
            Assert.True(new PropertyExpectations<HttpRequestDefinition>(p => p.RequestUri)
                            .IsNotDecorated()
                            .TypeIs<AbsoluteUri>()
                            .ArgumentNullException()
                            .Result);
        }
    }
}