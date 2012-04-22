namespace Cavity.Net
{
    using System;

    using Xunit;
    using Xunit.Extensions;

    public sealed class ParameterFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<HttpParameter>()
                            .DerivesFrom<ComparableObject>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor_Token_string()
        {
            Assert.NotNull(new HttpParameter("name", "value"));
        }

        [Fact]
        public void opImplicit_Parameter_string()
        {
            const string expected = "name=value";

            HttpParameter obj = expected;

            var actual = obj.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_string_Parameter()
        {
            const string expected = "name=value";

            var obj = HttpParameter.FromString(expected);

            string actual = obj;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string()
        {
            const string expected = "name=value";

            var obj = HttpParameter.FromString(expected);

            var actual = obj.ToString();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void op_FromString_stringEmpty(string value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => HttpParameter.FromString(value));
        }

        [Fact]
        public void op_FromString_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => HttpParameter.FromString(null));
        }

        [Fact]
        public void op_FromString_string_whenEmptyName()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => HttpParameter.FromString("=value"));
        }

        [Fact]
        public void op_FromString_string_whenEmptyValue()
        {
            const string expected = "name=";

            var obj = HttpParameter.FromString(expected);

            var actual = obj.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FromString_string_whenTwoEquals()
        {
            Assert.Throws<FormatException>(() => HttpParameter.FromString("name=value=example"));
        }

        [Fact]
        public void op_FromString_string_withSemicolon()
        {
            Assert.Throws<FormatException>(() => HttpParameter.FromString(";example"));
        }

        [Fact]
        public void op_FromString_string_withoutEquals()
        {
            Assert.Throws<FormatException>(() => HttpParameter.FromString("example"));
        }

        [Fact]
        public void op_ToString()
        {
            var obj = new HttpParameter("name", "value");
            const string expected = "name=value";
            var actual = obj.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Name()
        {
            Assert.True(new PropertyExpectations<HttpParameter>(x => x.Name)
                            .IsAutoProperty<Token>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Value()
        {
            Assert.True(new PropertyExpectations<HttpParameter>(x => x.Value)
                            .IsAutoProperty<string>()
                            .IsNotDecorated()
                            .Result);
        }
    }
}