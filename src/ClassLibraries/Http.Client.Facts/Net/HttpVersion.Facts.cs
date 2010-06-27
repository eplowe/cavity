namespace Cavity.Net
{
    using System;
    using Cavity;
    using Xunit;

    public sealed class HttpVersionFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(new TypeExpectations<HttpVersion>()
                .DerivesFrom<ComparableObject>()
                .IsConcreteClass()
                .IsSealed()
                .NoDefaultConstructor()
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void ctor_int_int()
        {
            Assert.NotNull(new HttpVersion(1, 0));
        }

        [Fact]
        public void prop_Major()
        {
            Assert.True(new PropertyExpectations<HttpVersion>("Major")
                .TypeIs<int>()
                .ArgumentOutOfRangeException(-1)
                .Set(0)
                .Set(1)
                .Set(2)
                .Set(3)
                .Set(4)
                .Set(5)
                .Set(6)
                .Set(7)
                .Set(8)
                .Set(9)
                .ArgumentOutOfRangeException(10)
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void prop_Minor()
        {
            Assert.True(new PropertyExpectations<HttpVersion>("Minor")
                .TypeIs<int>()
                .ArgumentOutOfRangeException(-1)
                .Set(0)
                .Set(1)
                .Set(2)
                .Set(3)
                .Set(4)
                .Set(5)
                .Set(6)
                .Set(7)
                .Set(8)
                .Set(9)
                .ArgumentOutOfRangeException(10)
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void opImplicit_HttpVersion_stringNull()
        {
            HttpVersion obj = null as string;

            Assert.Null(obj);
        }

        [Fact]
        public void opImplicit_HttpVersion_stringEmpty()
        {
            HttpVersion expected;

            Assert.Throws<FormatException>(() => expected = string.Empty);
        }

        [Fact]
        public void opImplicit_HttpVersion_string()
        {
            HttpVersion expected = "HTTP/1.0";
            HttpVersion actual = new HttpVersion(1, 0);

            Assert.Equal<HttpVersion>(expected, actual);
        }

        [Fact]
        public void op_FromString_string()
        {
            HttpVersion expected = new HttpVersion(1, 0);
            HttpVersion actual = HttpVersion.FromString("HTTP/1.0");

            Assert.Equal<HttpVersion>(expected, actual);
        }

        [Fact]
        public void op_FromString_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => HttpVersion.FromString(null as string));
        }

        [Fact]
        public void op_FromString_stringEmpty()
        {
            Assert.Throws<FormatException>(() => HttpVersion.FromString(string.Empty));
        }

        [Fact]
        public void op_FromString_string_Invalid()
        {
            Assert.Throws<FormatException>(() => HttpVersion.FromString("1.0"));
        }

        [Fact]
        public void op_ToString()
        {
            string expected = "HTTP/1.0";
            string actual = new HttpVersion(1, 0).ToString();

            Assert.Equal<string>(expected, actual);
        }
    }
}