namespace Cavity.Net
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using Xunit;

    public sealed class TokenFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<Token>().DerivesFrom<object>()
                                                     .IsConcreteClass()
                                                     .IsUnsealed()
                                                     .NoDefaultConstructor()
                                                     .Serializable()
                                                     .IsDecoratedWith<ImmutableObjectAttribute>()
                                                     .Implements<IComparable>()
                                                     .Implements<IComparable<Token>>()
                                                     .Implements<IEquatable<Token>>()
                                                     .Result);
        }

        [Fact]
        public void ctor_SerializationInfo_StreamingContext()
        {
            var expected = new Token("Example");
            Token actual;

            using (Stream stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, new Token("Example"));
                stream.Position = 0;
                actual = (Token)formatter.Deserialize(stream);
            }

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_string()
        {
            Assert.NotNull(new Token("Example"));
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
        public void opEquality_Token_Token()
        {
            var obj = new Token("Example");
            var comparand = new Token("Example");

            Assert.True(obj == comparand);
        }

        [Fact]
        public void opEquality_Token_TokenDiffers()
        {
            var obj = new Token("ABC");
            var comparand = new Token("XYZ");

            Assert.False(obj == comparand);
        }

        [Fact]
        public void opEquality_Token_TokenSame()
        {
            var obj = new Token("Example");
            var comparand = obj;

            Assert.True(obj == comparand);
        }

        [Fact]
        public void opGreaterThan_TokenGreater_Token()
        {
            var obj = new Token("XYZ");
            var comparand = new Token("ABC");

            Assert.True(obj > comparand);
        }

        [Fact]
        public void opGreaterThan_TokenNull_Token()
        {
            var comparand = new Token("Example");

            Assert.False(null > comparand);
        }

        [Fact]
        public void opGreaterThan_Token_Token()
        {
            var obj = new Token("Example");
            var comparand = new Token("Example");

            Assert.False(obj > comparand);
        }

        [Fact]
        public void opGreaterThan_Token_TokenGreater()
        {
            var obj = new Token("ABC");
            var comparand = new Token("XYZ");

            Assert.False(obj > comparand);
        }

        [Fact]
        public void opGreaterThan_Token_TokenNull()
        {
            var obj = new Token("Example");

            Assert.True(obj > null);
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
            Assert.Null((Token)null);
        }

        [Fact]
        public void opImplicit_string_Token()
        {
            const string expected = "Example";
            string actual = new Token(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opInequality_Token_Token()
        {
            var obj = new Token("Example");
            var comparand = new Token("Example");

            Assert.False(obj != comparand);
        }

        [Fact]
        public void opInequality_Token_TokenDiffers()
        {
            var obj = new Token("ABC");
            var comparand = new Token("XYZ");

            Assert.True(obj != comparand);
        }

        [Fact]
        public void opInequality_Token_TokenSame()
        {
            var obj = new Token("Example");
            var comparand = obj;

            Assert.False(obj != comparand);
        }

        [Fact]
        public void opLesserThan_TokenLesser_Token()
        {
            var obj = new Token("ABC");
            var comparand = new Token("XYZ");

            Assert.True(obj < comparand);
        }

        [Fact]
        public void opLesserThan_TokenNull_Token()
        {
            var comparand = new Token("Example");

            Assert.True(null < comparand);
        }

        [Fact]
        public void opLesserThan_Token_Token()
        {
            var obj = new Token("Example");
            var comparand = new Token("Example");

            Assert.False(obj < comparand);
        }

        [Fact]
        public void opLesserThan_Token_TokenLesser()
        {
            var obj = new Token("XYZ");
            var comparand = new Token("ABC");

            Assert.False(obj < comparand);
        }

        [Fact]
        public void opLesserThan_Token_TokenNull()
        {
            var obj = new Token("Example");

            Assert.False(obj < null);
        }

        [Fact]
        [SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "Cavity.Net.Token.CompareTo(Cavity.Net.Token)", Justification = "This is needed for testing purposes")]
        public void op_CompareTo_Token()
        {
            const int expected = 1;
            var actual = new Token("Example").CompareTo(null);

            Assert.Equal(expected, actual);
        }

        [Fact]
        [SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "Cavity.Net.Token.CompareTo(Cavity.Net.Token)", Justification = "This is needed for testing purposes")]
        public void op_CompareTo_TokenEqual()
        {
            var obj = new Token("Example");
            var comparand = new Token("Example");

            const int expected = 0;
            var actual = obj.CompareTo(comparand);

            Assert.Equal(expected, actual);
        }

        [Fact]
        [SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "Cavity.Net.Token.CompareTo(Cavity.Net.Token)", Justification = "This is needed for testing purposes")]
        public void op_CompareTo_TokenGreater()
        {
            var obj = new Token("ABC");
            var comparand = new Token("XYZ");

            const int expected = -23;
            var actual = obj.CompareTo(comparand);

            Assert.Equal(expected, actual);
        }

        [Fact]
        [SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "Cavity.Net.Token.CompareTo(Cavity.Net.Token)", Justification = "This is needed for testing purposes")]
        public void op_CompareTo_TokenLesser()
        {
            var obj = new Token("XYZ");
            var comparand = new Token("ABC");

            const int expected = 23;
            var actual = obj.CompareTo(comparand);

            Assert.Equal(expected, actual);
        }

        [Fact]
        [SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "Cavity.Net.Token.CompareTo(Cavity.Net.Token)", Justification = "This is needed for testing purposes")]
        public void op_CompareTo_TokenSame()
        {
            var obj = new Token("Example");

            const int expected = 0;
            var actual = obj.CompareTo(obj);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_object()
        {
            const int expected = 1;
            var actual = new Token("Example").CompareTo(null as object);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_objectEqual()
        {
            var obj = new Token("Example");
            object comparand = new Token("Example");

            const int expected = 0;
            var actual = obj.CompareTo(comparand);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_objectEqual_StringComparison()
        {
            var obj = new Token("EXAMPLE");
            var comparand = new Token("example");

            const int expected = 0;
            var actual = obj.CompareTo(comparand, StringComparison.OrdinalIgnoreCase);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_objectGreater()
        {
            var obj = new Token("ABC");
            object comparand = new Token("XYZ");

            const int expected = -23;
            var actual = obj.CompareTo(comparand);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_objectLesser()
        {
            var obj = new Token("XYZ");
            object comparand = new Token("ABC");

            const int expected = 23;
            var actual = obj.CompareTo(comparand);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_objectSame()
        {
            var obj = new Token("Example");

            const int expected = 0;
            var actual = obj.CompareTo(obj as object);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CompareTo_objectString()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Token("Example").CompareTo("Example" as object));
        }

        [Fact]
        public void op_CompareTo_objectUnequal_StringComparison()
        {
            var obj = new Token("ABC");
            var comparand = new Token("XYZ");

            const int expected = -23;
            var actual = obj.CompareTo(comparand, StringComparison.OrdinalIgnoreCase);

            Assert.Equal(expected, actual);
        }

        [Fact]
        [SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "Cavity.Net.Token.Equals(Cavity.Net.Token)", Justification = "This is needed for testing purposes")]
        public void op_Equals_TokenEqual()
        {
            var obj = new Token("Example");
            var comparand = new Token("Example");

            Assert.True(obj.Equals(comparand));
        }

        [Fact]
        public void op_Equals_TokenEqual_StringComparison()
        {
            var obj = new Token("EXAMPLE");
            var comparand = new Token("example");

            Assert.True(obj.Equals(comparand, StringComparison.OrdinalIgnoreCase));
        }

        [Fact]
        [SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "Cavity.Net.Token.Equals(Cavity.Net.Token)", Justification = "This is needed for testing purposes")]
        public void op_Equals_TokenNull()
        {
            Assert.False(new Token("Example").Equals(null));
        }

        [Fact]
        [SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "Cavity.Net.Token.Equals(Cavity.Net.Token)", Justification = "This is needed for testing purposes")]
        public void op_Equals_TokenSame()
        {
            var obj = new Token("Example");
            var comparand = obj;

            Assert.True(obj.Equals(comparand));
        }

        [Fact]
        [SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "Cavity.Net.Token.Equals(Cavity.Net.Token)", Justification = "This is needed for testing purposes")]
        public void op_Equals_TokenUnequal()
        {
            var obj = new Token("ABC");
            var comparand = new Token("XYZ");

            Assert.False(obj.Equals(comparand));
        }

        [Fact]
        public void op_Equals_TokenUnequal_StringComparison()
        {
            var obj = new Token("ABC");
            var comparand = new Token("XYZ");

            Assert.False(obj.Equals(comparand, StringComparison.OrdinalIgnoreCase));
        }

        [Fact]
        public void op_Equals_object()
        {
            var obj = new Token("Example");
            var comparand = new Token("Example");

            Assert.True(obj.Equals(comparand as object));
        }

        [Fact]
        public void op_Equals_objectDiffer()
        {
            var obj = new Token("ABC");
            var comparand = new Token("XYZ");

            Assert.False(obj.Equals(comparand as object));
        }

        [Fact]
        public void op_Equals_objectNull()
        {
            Assert.False(new Token("Example").Equals(null as object));
        }

        [Fact]
        public void op_Equals_objectSame()
        {
            var obj = new Token("Example");

            Assert.True(obj.Equals(obj as object));
        }

        [Fact]
        public void op_Equals_objectString()
        {
            Assert.False(new Token("Example").Equals("Example" as object));
        }

        [Fact]
        public void op_GetHashCode()
        {
            var obj = new Token("Example");

            var expected = obj.ToString().GetHashCode();
            var actual = obj.GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_GetObjectData_SerializationInfoNull_StreamingContext()
        {
            var context = new StreamingContext(StreamingContextStates.All);

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => (new Token("Example") as ISerializable).GetObjectData(null, context));

            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Fact]
        public void op_GetObjectData_SerializationInfo_StreamingContext()
        {
            var info = new SerializationInfo(typeof(Token), new FormatterConverter());
            var context = new StreamingContext(StreamingContextStates.All);

            const string expected = "Example";

            (new Token(expected) as ISerializable).GetObjectData(info, context);

            var actual = info.GetString("_value");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString()
        {
            const string expected = "Example";
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
                            .FormatException(((char)31).ToString(CultureInfo.InvariantCulture)) // CTL
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
                            .FormatException(((char)127).ToString(CultureInfo.InvariantCulture)) // DEL
                            .IsNotDecorated()
                            .Result);
        }
    }
}