namespace Cavity.Net
{
    using System;
    using Cavity;
    using Xunit;

    public sealed class HttpHeaderFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(new TypeExpectations<HttpHeader>()
                .DerivesFrom<ValueObject<HttpHeader>>()
                .IsConcreteClass()
                .IsSealed()
                .NoDefaultConstructor()
                .IsNotDecorated()
                .Implements<IHttpHeader>()
                .Result);
        }

        [Fact]
        public void ctor_Token_string()
        {
            Assert.NotNull(new HttpHeader(new Token("name"), "value"));
        }

        [Fact]
        public void ctor_Token_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpHeader(new Token("name"), null as string));
        }

        [Fact]
        public void ctor_Token_stringEmpty()
        {
            Assert.NotNull(new HttpHeader(new Token("name"), string.Empty));
        }

        [Fact]
        public void ctor_TokenNull_string()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpHeader(null as Token, "value"));
        }

        [Fact]
        public void prop_Name()
        {
            Assert.NotNull(new PropertyExpectations<HttpHeader>("Name")
                .TypeIs<Token>()
                .ArgumentNullException()
                .Set(new Token("name"))
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void prop_Value()
        {
            Assert.NotNull(new PropertyExpectations<HttpHeader>("Value")
                .TypeIs<string>()
                .ArgumentNullException()
                .Set(string.Empty)
                .Set("value")
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void opImplicit_HttpHeader_stringNull()
        {
            HttpHeader obj = null as string;

            Assert.Null(obj);
        }

        [Fact]
        public void opImplicit_HttpHeader_stringEmpty()
        {
            HttpHeader expected;

            Assert.Throws<ArgumentOutOfRangeException>(() => expected = string.Empty);
        }

        [Fact]
        public void opImplicit_HttpHeader_string()
        {
            HttpHeader expected = "name: value";
            HttpHeader actual = new HttpHeader("name", "value");

            Assert.Equal<HttpHeader>(expected, actual);
        }

        [Fact]
        public void op_Parse_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => HttpHeader.Parse(null as string));
        }

        [Fact]
        public void op_Parse_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => HttpHeader.Parse(string.Empty));
        }

        [Fact]
        public void op_Parse_string()
        {
            HttpHeader expected = new HttpHeader("name", "value");
            HttpHeader actual = HttpHeader.Parse("name: value");

            Assert.Equal<HttpHeader>(expected, actual);
        }

        [Fact]
        public void op_Parse_string_whenMissingColon()
        {
            Assert.Throws<FormatException>(() => HttpHeader.Parse("name value"));
        }

        [Fact]
        public void op_Parse_string_whenStartsWithColon()
        {
            Assert.Throws<FormatException>(() => HttpHeader.Parse(": value"));
        }

        [Fact]
        public void op_Parse_string_whenEndsWithColon()
        {
            HttpHeader expected = new HttpHeader("name", string.Empty);
            HttpHeader actual = HttpHeader.Parse("name:");

            Assert.Equal<HttpHeader>(expected, actual);
        }

        [Fact]
        public void op_ToString()
        {
            string expected = "name: value";
            string actual = new HttpHeader("name", "value").ToString();

            Assert.Equal<string>(expected, actual);
        }
    }
}