namespace Cavity.Net
{
    using System;
    using Xunit;

    public sealed class HttpHeaderFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<HttpHeader>()
                            .DerivesFrom<ComparableObject>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<IHttpHeader>()
                            .Result);
        }

        [Fact]
        public void ctor_TokenNull_string()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpHeader(null, "value"));
        }

        [Fact]
        public void ctor_Token_string()
        {
            Assert.NotNull(new HttpHeader(new Token("name"), "value"));
        }

        [Fact]
        public void ctor_Token_stringEmpty()
        {
            Assert.NotNull(new HttpHeader(new Token("name"), string.Empty));
        }

        [Fact]
        public void ctor_Token_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpHeader(new Token("name"), null));
        }

        [Fact]
        public void opImplicit_HttpHeader_string()
        {
            var expected = new HttpHeader("name", "value");
            HttpHeader actual = "name: value";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_HttpHeader_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => (HttpHeader)string.Empty);
        }

        [Fact]
        public void opImplicit_HttpHeader_stringNull()
        {
            HttpHeader obj = null as string;

            Assert.Null(obj);
        }

        [Fact]
        public void op_FromString_string()
        {
            var expected = new HttpHeader("name", "value");
            var actual = HttpHeader.FromString("name: value");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => HttpHeader.FromString(string.Empty));
        }

        [Fact]
        public void op_FromString_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => HttpHeader.FromString(null));
        }

        [Fact]
        public void op_FromString_string_whenEndsWithColon()
        {
            var expected = new HttpHeader("name", string.Empty);
            var actual = HttpHeader.FromString("name:");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_whenMissingColon()
        {
            Assert.Throws<FormatException>(() => HttpHeader.FromString("name value"));
        }

        [Fact]
        public void op_FromString_string_whenStartsWithColon()
        {
            Assert.Throws<FormatException>(() => HttpHeader.FromString(": value"));
        }

        [Fact]
        public void op_ToString()
        {
            const string expected = "name: value";
            var actual = new HttpHeader("name", "value").ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Name()
        {
            Assert.True(new PropertyExpectations<HttpHeader>(p => p.Name)
                            .TypeIs<Token>()
                            .ArgumentNullException()
                            .Set(new Token("name"))
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Value()
        {
            Assert.True(new PropertyExpectations<HttpHeader>(p => p.Value)
                            .TypeIs<string>()
                            .ArgumentNullException()
                            .Set(string.Empty)
                            .Set("value")
                            .IsNotDecorated()
                            .Result);
        }
    }
}