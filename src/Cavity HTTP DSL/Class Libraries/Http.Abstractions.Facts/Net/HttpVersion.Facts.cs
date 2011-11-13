namespace Cavity.Net
{
    using System;
    using Xunit;

    public sealed class HttpVersionFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<HttpVersion>()
                            .DerivesFrom<ComparableObject>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new HttpVersion());
        }

        [Fact]
        public void ctor_intNegative_int()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new HttpVersion(-1, 0));
        }

        [Fact]
        public void ctor_int_int()
        {
            Assert.NotNull(new HttpVersion(1, 0));
        }

        [Fact]
        public void ctor_int_intNegative()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new HttpVersion(0, -1));
        }

        [Fact]
        public void opImplicit_HttpVersion_string()
        {
            var expected = new HttpVersion(1, 0);
            HttpVersion actual = "HTTP/1.0";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_string_HttpVersion()
        {
            const string expected = "HTTP/1.0";
            string actual = new HttpVersion(1, 0);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string()
        {
            var expected = new HttpVersion(1, 0);
            var actual = HttpVersion.FromString("HTTP/1.0");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => HttpVersion.FromString(string.Empty));
        }

        [Fact]
        public void op_FromString_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => HttpVersion.FromString(null));
        }

        [Fact]
        public void op_FromString_string_whenHttpLatest()
        {
            var expected = HttpVersion.Latest;
            var actual = HttpVersion.FromString(HttpVersion.Latest);

            Assert.Same(expected, actual);
        }

        [Fact]
        public void op_FromString_string_whenHttpMajor()
        {
            Assert.Throws<FormatException>(() => HttpVersion.FromString("HTTP/1"));
        }

        [Fact]
        public void op_FromString_string_whenHttpMajorPoint()
        {
            Assert.Throws<FormatException>(() => HttpVersion.FromString("HTTP/1."));
        }

        [Fact]
        public void op_FromString_string_whenMissingHttp()
        {
            Assert.Throws<FormatException>(() => HttpVersion.FromString("1.1"));
        }

        [Fact]
        public void op_ToString()
        {
            const string expected = "HTTP/1.0";
            var actual = new HttpVersion(1, 0).ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Latest()
        {
            const string expected = "HTTP/1.1";
            var actual = HttpVersion.Latest.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Major()
        {
            Assert.True(new PropertyExpectations<HttpVersion>(x => x.Major)
                            .TypeIs<int>()
                            .DefaultValueIs(1)
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Minor()
        {
            Assert.True(new PropertyExpectations<HttpVersion>(x => x.Minor)
                            .TypeIs<int>()
                            .DefaultValueIs(1)
                            .IsNotDecorated()
                            .Result);
        }
    }
}