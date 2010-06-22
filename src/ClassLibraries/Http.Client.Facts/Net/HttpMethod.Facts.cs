namespace Cavity.Net
{
    using System;
    using Cavity;
    using Xunit;

    public sealed class HttpMethodFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(new TypeExpectations<HttpMethod>()
                .DerivesFrom<ComparableObject>()
                .IsConcreteClass()
                .IsSealed()
                .NoDefaultConstructor()
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void ctor_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HttpMethod(null as string));
        }

        [Fact]
        public void ctor_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new HttpMethod(string.Empty));
        }

        [Fact]
        public void ctor_string()
        {
            Assert.NotNull(new HttpMethod("GET"));
        }

        [Fact]
        public void prop_Value()
        {
            Assert.True(new PropertyExpectations<HttpMethod>("Value")
                .TypeIs<string>()
                .ArgumentOutOfRangeException(string.Empty)
                .FormatException("FOO BAR")
                .FormatException("FOO123BAR")
                .Set("OPTIONS")
                .Set("GET")
                .Set("HEAD")
                .Set("POST")
                .Set("PUT")
                .Set("DELETE")
                .Set("TRACE")
                .Set("CONNECT")
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void opImplicit_HttpMethod_stringNull()
        {
            HttpMethod obj = null as string;

            Assert.Null(obj);
        }

        [Fact]
        public void opImplicit_HttpMethod_stringEmpty()
        {
            HttpMethod expected;

            Assert.Throws<ArgumentOutOfRangeException>(() => expected = string.Empty);
        }

        [Fact]
        public void opImplicit_HttpMethod_string()
        {
            HttpMethod expected = "GET";
            HttpMethod actual = new HttpMethod("GET");

            Assert.Equal<HttpMethod>(expected, actual);
        }

        [Fact]
        public void op_ToString()
        {
            string expected = "GET";
            string actual = new HttpMethod(expected).ToString();

            Assert.Equal<string>(expected, actual);
        }
    }
}