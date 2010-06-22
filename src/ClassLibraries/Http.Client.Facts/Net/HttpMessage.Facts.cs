namespace Cavity.Net
{
    using System;
    using Cavity;
    using Cavity.Net.Mime;
    using Xunit;

    public sealed class HttpMessageFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(new TypeExpectations<HttpMessage>()
                .DerivesFrom<ComparableObject>()
                .IsAbstractBaseClass()
                .IsNotDecorated()
                .Implements<IHttpMessage>()
                .Result);
        }

        [Fact]
        public void ctor_IHttpHeaderCollection_IContent()
        {
            Assert.NotNull(new DerivedHttpMessage(new IHttpHeaderCollectionDummy(), new IContentDummy()) as HttpMessage);
        }

        [Fact]
        public void ctor_IHttpHeaderCollectionNull_IContent()
        {
            Assert.Throws<ArgumentNullException>(() => new DerivedHttpMessage(null as IHttpHeaderCollection, new IContentDummy()));
        }

        [Fact]
        public void ctor_IHttpHeaderCollection_IContentNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DerivedHttpMessage(new IHttpHeaderCollectionDummy(), null as IContent));
        }

        [Fact]
        public void prop_Body()
        {
            Assert.NotNull(new PropertyExpectations<DerivedHttpMessage>("Body")
                .TypeIs<IContent>()
                .ArgumentNullException()
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void prop_Headers()
        {
            Assert.NotNull(new PropertyExpectations<DerivedHttpMessage>("Headers")
                .TypeIs<IHttpHeaderCollection>()
                .ArgumentNullException()
                .IsNotDecorated()
                .Result);
        }
    }
}