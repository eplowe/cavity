namespace Cavity.Net.Mime
{
    using System;
    using System.Net.Mime;
    using Cavity;
    using Xunit;

    public sealed class TextFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(new TypeExpectations<Text>()
                .DerivesFrom<ComparableObject>()
                .IsAbstractBaseClass()
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void ctor_ContentType_string()
        {
            Assert.NotNull(new DerivedText(new ContentType("text/plain"), "value") as Text);
        }

        [Fact]
        public void ctor_ContentTypeNull_string()
        {
            Assert.Throws<ArgumentNullException>(() => new DerivedText(null as ContentType, "value"));
        }

        [Fact]
        public void ctor_ContentType_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DerivedText(new ContentType("text/plain"), null as string));
        }

        [Fact]
        public void ctor_ContentType_stringEmpty()
        {
            Assert.NotNull(new DerivedText(new ContentType("text/plain"), string.Empty) as Text);
        }

        [Fact]
        public void prop_ContentType()
        {
            Assert.NotNull(new PropertyExpectations<DerivedText>("ContentType")
                .TypeIs<ContentType>()
                .ArgumentNullException()
                .Set(new ContentType("text/plain"))
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void prop_Value()
        {
            Assert.NotNull(new PropertyExpectations<DerivedText>("Value")
                .TypeIs<string>()
                .ArgumentNullException()
                .Set("value")
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void op_ToString()
        {
            string expected = "value";
            string actual = (new DerivedText(new ContentType("text/plain"), expected) as Text).ToString();

            Assert.Equal<string>(expected, actual);
        }
    }
}