namespace Cavity.Net
{
    using System;
    using Xunit;

    public sealed class TokenFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<Token>()
                            .DerivesFrom<ComparableObject>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor_string()
        {
            Assert.NotNull(new Token("value"));
        }

        [Fact]
        public void ctor_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Token(string.Empty));
        }

        [Fact]
        public void ctor_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Token(null));
        }

        [Fact]
        public void opImplicit_Token_string()
        {
            var expected = new Token("value");
            Token actual = "value";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_Token_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Token(string.Empty));
        }

        [Fact]
        public void opImplicit_Token_stringNull()
        {
            Token obj = null as string;

            Assert.Null(obj);
        }

        [Fact]
        public void op_ToString()
        {
            const string expected = "value";
            var actual = new Token(expected).ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Value()
        {
            Assert.True(new PropertyExpectations<Token>(p => p.Value)
                            .TypeIs<string>()
                            .ArgumentNullException()
                            .ArgumentOutOfRangeException(string.Empty)
                            .FormatException(((char)31).ToString()) // CTL
                            .FormatException(" ") // 32
                            .Set("!") // 33
                            .FormatException("\"") // 34
                            .Set("#") // 35
                            .Set("$") // 36
                            .Set("%") // 37
                            .FormatException("@") // 38
                            .Set("'") // 39
                            .FormatException("(") // 40
                            .FormatException(")") // 41
                            .Set("*") // 42
                            .Set("+") // 43
                            .FormatException(",") // 44
                            .Set("-") // 45
                            .Set(".") // 46
                            .FormatException("/") // 47
                            .Set("0123456789") // 48 ... 57
                            .FormatException(":") // 58
                            .FormatException(";") // 59
                            .FormatException("<") // 60
                            .FormatException("=") // 61
                            .FormatException(">") // 62
                            .FormatException("?") // 63
                            .FormatException("@") // 64
                            .Set("ABCDEFGHIJKLMNOPQRSTUVWXYZ") // 65 ... 90
                            .FormatException("[") // 91
                            .FormatException(@"\") // 92
                            .FormatException("]") // 93
                            .Set("^") // 94
                            .Set("_") // 95
                            .Set("`") // 96
                            .Set("abcdefghijklmnopqrstuvwxyz") // 97 ... 122
                            .FormatException("{") // 123
                            .Set("|") // 124
                            .FormatException("}") // 125
                            .Set("~") // 126
                            .FormatException(((char)127).ToString()) // DEL
                            .IsNotDecorated()
                            .Result);
        }
    }
}