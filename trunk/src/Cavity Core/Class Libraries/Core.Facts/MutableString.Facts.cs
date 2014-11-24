namespace Cavity
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using Xunit;
    using Xunit.Extensions;

    public sealed class MutableStringFacts
    {
        [Fact]
        public void ICloneable_op_Clone()
        {
            ICloneable expected = new MutableString("One");
            var actual = expected.Clone();

            Assert.Equal(expected, actual);
            Assert.NotSame(expected, actual);
        }

        [Theory]
        [InlineData("a", "z")]
        [InlineData("z", "a")]
        [InlineData("", "z")]
        [InlineData("a", "")]
        public void IComparableOfMutableString_op_CompareTo_MutableString(string comparand1,
                                                                          string comparand2)
        {
            IComparable<MutableString> obj = new MutableString(comparand1);
            var expected = string.CompareOrdinal(comparand1, comparand2);
            var actual = obj.CompareTo(comparand2);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IComparableOfMutableString_op_CompareTo_objectNull()
        {
            IComparable<MutableString> obj = new MutableString("a");

            var expected = string.CompareOrdinal("a", null);

            Assert.Equal(expected, obj.CompareTo(null));
        }

        [Theory]
        [InlineData("a", "z")]
        [InlineData("z", "a")]
        [InlineData("", "z")]
        [InlineData("a", "")]
        public void IComparable_op_CompareTo_object(string comparand1,
                                                    string comparand2)
        {
            IComparable obj = new MutableString(comparand1);

            var expected = string.CompareOrdinal(comparand1, comparand2);
            var actual = obj.CompareTo(new MutableString(comparand2));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IComparable_op_CompareTo_objectNull()
        {
            IComparable obj = new MutableString("a");

            var expected = string.CompareOrdinal("a", null);

            Assert.Equal(expected, obj.CompareTo(null));
            Assert.Equal(expected, (obj as IComparable<MutableString>).CompareTo(null));
        }

        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<MutableString>().DerivesFrom<object>()
                                                             .IsConcreteClass()
                                                             .IsSealed()
                                                             .HasDefaultConstructor()
                                                             .Serializable()
                                                             .Implements<ICloneable>()
                                                             .Implements<IComparable<MutableString>>()
                                                             .Implements<IEnumerable<char>>()
                                                             .Implements<IEquatable<MutableString>>()
                                                             .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new MutableString());
        }

        [Fact]
        public void ctor_SerializationInfo_StreamingContext()
        {
            var expected = new MutableString("Example");
            MutableString actual;

            using (Stream stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, new MutableString("Example"));
                stream.Position = 0;
                actual = (MutableString)formatter.Deserialize(stream);
            }

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_StringBuilder()
        {
            var expected = string.Empty;
            var actual = new MutableString(new StringBuilder()).ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_StringBuilderNull()
        {
            Assert.Throws<ArgumentNullException>(() => new MutableString(null as StringBuilder));
        }

        [Fact]
        public void ctor_char_int()
        {
            const string expected = "00000";
            var actual = new MutableString('0', 5).ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_stringEmpty()
        {
            var expected = string.Empty;
            var actual = new MutableString(expected).ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_stringNull()
        {
            var expected = string.Empty;
            var actual = new MutableString(null as string).ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_strings()
        {
            const string expected = "OneTwoThree";
            var actual = new MutableString("One", "Two", "Three").ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opEquality_MutableStringNull_MutableString()
        {
            MutableString comparand1 = null;
            var comparand2 = new MutableString("One");

            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            Assert.False(comparand1 == comparand2);

            // ReSharper restore ConditionIsAlwaysTrueOrFalse
        }

        [Fact]
        public void opEquality_MutableStringNull_MutableStringNull()
        {
            MutableString comparand1 = null;
            MutableString comparand2 = null;

            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            Assert.True(comparand1 == comparand2);

            // ReSharper restore ConditionIsAlwaysTrueOrFalse
        }

        [Fact]
        public void opEquality_MutableString_MutableStringNull()
        {
            var comparand1 = new MutableString("One");
            MutableString comparand2 = null;

            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            Assert.False(comparand1 == comparand2);

            // ReSharper restore ConditionIsAlwaysTrueOrFalse
        }

        [Theory]
        [InlineData("One", "Ones")]
        [InlineData("One", "one")]
        [InlineData("One", "")]
        [InlineData("One", null)]
        [InlineData("", "one")]
        [InlineData(null, "one")]
        public void opEquality_MutableString_MutableString_whenFalse(string comparand1,
                                                                     string comparand2)
        {
            Assert.False(new MutableString(comparand1) == new MutableString(comparand2));
        }

        [Fact]
        public void opEquality_MutableString_MutableString_whenSame()
        {
            var comparand1 = new MutableString("One");
            var comparand2 = comparand1;

            Assert.True(comparand1 == comparand2);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("One", "One")]
        public void opEquality_MutableString_MutableString_whenTrue(string comparand1,
                                                                    string comparand2)
        {
            Assert.True(new MutableString(comparand1) == new MutableString(comparand2));
        }

        [Fact]
        public void opGreaterThan_MutableStringNull_MutableString()
        {
            MutableString comparand1 = null;
            var comparand2 = new MutableString("One");

            // ReSharper disable ExpressionIsAlwaysNull
            Assert.False(comparand1 > comparand2);

            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
        public void opGreaterThan_MutableStringNull_MutableStringNull()
        {
            MutableString comparand1 = null;
            MutableString comparand2 = null;

            // ReSharper disable ExpressionIsAlwaysNull
            Assert.False(comparand1 > comparand2);

            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
        public void opGreaterThan_MutableString_MutableStringNull()
        {
            var comparand1 = new MutableString("One");
            MutableString comparand2 = null;

            // ReSharper disable ExpressionIsAlwaysNull
            Assert.True(comparand1 > comparand2);

            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Theory]
        [InlineData("a", "a")]
        [InlineData("", "a")]
        public void opGreaterThan_MutableString_MutableString_whenFalse(string comparand1,
                                                                        string comparand2)
        {
            Assert.False(new MutableString(comparand1) > new MutableString(comparand2));
        }

        [Fact]
        public void opGreaterThan_MutableString_MutableString_whenSame()
        {
            var comparand1 = new MutableString("One");
            var comparand2 = comparand1;

            Assert.False(comparand1 > comparand2);
        }

        [Theory]
        [InlineData("z", "a")]
        [InlineData("z", "")]
        public void opGreaterThan_MutableString_MutableString_whenTrue(string comparand1,
                                                                       string comparand2)
        {
            Assert.True(new MutableString(comparand1) > new MutableString(comparand2));
        }

        [Theory]
        [InlineData("")]
        [InlineData("Example")]
        public void opImplicit_MutableString_StringBuilder(string text)
        {
            var expected = new MutableString(text);
            MutableString actual = new StringBuilder(text);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_MutableString_StringBuilderNull()
        {
            MutableString actual = null as StringBuilder;

            // ReSharper disable ExpressionIsAlwaysNull
            Assert.Null(actual);

            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Theory]
        [InlineData("")]
        [InlineData("Example")]
        public void opImplicit_MutableString_string(string text)
        {
            var expected = new MutableString(text);
            MutableString actual = text;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_MutableString_stringNull()
        {
            MutableString actual = null as string;

            // ReSharper disable ExpressionIsAlwaysNull
            Assert.Null(actual);

            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Theory]
        [InlineData("")]
        [InlineData("Example")]
        public void opImplicit_string_MutableString(string expected)
        {
            string actual = new MutableString(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_string_MutableStringNull()
        {
            string actual = null as MutableString;

            // ReSharper disable ExpressionIsAlwaysNull
            Assert.Null(actual);

            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Theory]
        [InlineData('E', "Example", 0)]
        [InlineData('e', "Example", 6)]
        public void opIndexer_get_int(char expected,
                                      string text,
                                      int index)
        {
            Assert.Equal(expected, new MutableString(text)[index]);
        }

        [Theory]
        [InlineData("", -1)]
        [InlineData("", 0)]
        [InlineData("Example", 7)]
        public void opIndexer_get_int_whenIndexOutOfRangeException(string text,
                                                                   int index)
        {
            Assert.Throws<IndexOutOfRangeException>(() => new MutableString(text)[index]);
        }

        [Fact]
        public void opIndexer_set_int()
        {
            var obj = new MutableString("Example");
            for (var i = 0; i < obj.Length; i++)
            {
                obj[i] = ' ';
            }

            const string expected = "       ";
            var actual = obj.ToString();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", -1)]
        [InlineData("", 0)]
        [InlineData("Example", 7)]
        public void opIndexer_set_int_whenIndexOutOfRangeException(string text,
                                                                   int index)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MutableString(text)[index] = ' ');
        }

        [Fact]
        public void opInequality_MutableStringNull_MutableString()
        {
            MutableString comparand1 = null;
            var comparand2 = new MutableString("One");

            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            Assert.True(comparand1 != comparand2);

            // ReSharper restore ConditionIsAlwaysTrueOrFalse
        }

        [Fact]
        public void opInequality_MutableStringNull_MutableStringNull()
        {
            MutableString comparand1 = null;
            MutableString comparand2 = null;

            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            Assert.False(comparand1 != comparand2);

            // ReSharper restore ConditionIsAlwaysTrueOrFalse
        }

        [Fact]
        public void opInequality_MutableString_MutableStringNull()
        {
            var comparand1 = new MutableString("One");
            MutableString comparand2 = null;

            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            Assert.True(comparand1 != comparand2);

            // ReSharper restore ConditionIsAlwaysTrueOrFalse
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("One", "One")]
        public void opInequality_MutableString_MutableString_whenFalse(string comparand1,
                                                                       string comparand2)
        {
            Assert.False(new MutableString(comparand1) != new MutableString(comparand2));
        }

        [Fact]
        public void opInequality_MutableString_MutableString_whenSame()
        {
            var comparand1 = new MutableString("One");
            var comparand2 = comparand1;

            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            Assert.False(comparand1 != comparand2);

            // ReSharper restore ConditionIsAlwaysTrueOrFalse
        }

        [Theory]
        [InlineData("One", "Ones")]
        [InlineData("One", "one")]
        [InlineData("One", "")]
        [InlineData("One", null)]
        [InlineData("", "one")]
        [InlineData(null, "one")]
        public void opInequality_MutableString_MutableString_whenTrue(string comparand1,
                                                                      string comparand2)
        {
            Assert.True(new MutableString(comparand1) != new MutableString(comparand2));
        }

        [Fact]
        public void opLessThan_MutableStringNull_MutableString()
        {
            MutableString comparand1 = null;
            var comparand2 = new MutableString("One");

            // ReSharper disable ExpressionIsAlwaysNull
            Assert.True(comparand1 < comparand2);

            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
        public void opLessThan_MutableStringNull_MutableStringNull()
        {
            MutableString comparand1 = null;
            MutableString comparand2 = null;

            // ReSharper disable ExpressionIsAlwaysNull
            Assert.False(comparand1 < comparand2);

            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Fact]
        public void opLessThan_MutableString_MutableStringNull()
        {
            var comparand1 = new MutableString("One");
            MutableString comparand2 = null;

            // ReSharper disable ExpressionIsAlwaysNull
            Assert.False(comparand1 < comparand2);

            // ReSharper restore ExpressionIsAlwaysNull
        }

        [Theory]
        [InlineData("a", "a")]
        [InlineData("a", "")]
        public void opLessThan_MutableString_MutableString_whenFalse(string comparand1,
                                                                     string comparand2)
        {
            Assert.False(new MutableString(comparand1) < new MutableString(comparand2));
        }

        [Fact]
        public void opLessThan_MutableString_MutableString_whenSame()
        {
            var comparand1 = new MutableString("One");
            var comparand2 = comparand1;

            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            Assert.False(comparand1 < comparand2);

            // ReSharper restore ConditionIsAlwaysTrueOrFalse
        }

        [Theory]
        [InlineData("a", "z")]
        [InlineData("", "z")]
        public void opLessThan_MutableString_MutableString_whenTrue(string comparand1,
                                                                    string comparand2)
        {
            Assert.True(new MutableString(comparand1) < new MutableString(comparand2));
        }

        [Fact]
        public void op_AppendLine_string()
        {
            var expected = new MutableString("Example" + Environment.NewLine);
            var actual = new MutableString();

            Assert.Same(actual, actual.AppendLine("Example"));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_AppendLine_stringEmpty()
        {
            var expected = new MutableString(Environment.NewLine);
            var actual = new MutableString();

            Assert.Same(actual, actual.AppendLine(string.Empty));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_AppendLine_stringNull()
        {
            var expected = new MutableString(Environment.NewLine);
            var actual = new MutableString();

            Assert.Same(actual, actual.AppendLine(null));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Append_IEnumerableOfString()
        {
            var values = new[]
                             {
                                 "One", "Two", "Three"
                             };

            var expected = new MutableString("OneTwoThree");
            var actual = new MutableString();

            Assert.Same(actual, actual.Append(values));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Append_IEnumerableOfStringEmpty()
        {
            var expected = new MutableString();
            var actual = new MutableString();

            Assert.Same(actual, actual.Append(new List<string>()));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Append_IEnumerableOfStringEmpty_char()
        {
            var expected = new MutableString();
            var actual = new MutableString();

            Assert.Same(actual, actual.Append(new List<string>(), '.'));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Append_IEnumerableOfStringNull()
        {
            var expected = new MutableString();
            var actual = new MutableString();

            Assert.Same(actual, actual.Append(null as IEnumerable<string>));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Append_IEnumerableOfStringNull_char()
        {
            var expected = new MutableString();
            var actual = new MutableString();

            Assert.Same(actual, actual.Append(null, '.'));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Append_IEnumerableOfString_char()
        {
            var values = new[]
                             {
                                 "One", "Two", "Three"
                             };

            var expected = new MutableString("One.Two.Three");
            var actual = new MutableString();

            Assert.Same(actual, actual.Append(values, '.'));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Append_char()
        {
            var expected = new MutableString(".");
            var actual = new MutableString();

            Assert.Same(actual, actual.Append('.'));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Append_string()
        {
            var expected = new MutableString("Example");
            var actual = new MutableString();

            Assert.Same(actual, actual.Append("Example"));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Append_stringEmpty()
        {
            var expected = new MutableString();
            var actual = new MutableString();

            Assert.Same(actual, actual.Append(string.Empty));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Append_stringNull()
        {
            var expected = new MutableString();
            var actual = new MutableString();

            Assert.Same(actual, actual.Append(null as string));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Clear()
        {
            var expected = new MutableString();
            var actual = new MutableString("Example");

            Assert.Same(actual, actual.Clear());

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Clone()
        {
            var expected = new MutableString("One");
            var actual = expected.Clone();

            Assert.Equal(expected, actual);
            Assert.NotSame(expected, actual);
        }

        [Theory]
        [InlineData("example", "example")]
        [InlineData("example", null)]
        [InlineData("example", "")]
        [InlineData("", "example")]
        [InlineData(null, "example")]
        [InlineData("", "")]
        [InlineData(null, "")]
        [InlineData("", null)]
        [InlineData(null, null)]
        public void op_Compare_MutableString_MutableString(string comparand1,
                                                           string comparand2)
        {
            var expected = string.CompareOrdinal(comparand1, comparand2);
            var actual = MutableString.Compare(comparand1, comparand2);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ContainsAny_charsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new MutableString().ContainsAny(null as char[]));
        }

        [Fact]
        public void op_ContainsAny_chars_whenFalse()
        {
            var values = new[]
                             {
                                 'X', 'y', 'z'
                             };

            Assert.False(new MutableString().ContainsAny(values));
            Assert.False(new MutableString("Example").ContainsAny(values));
        }

        [Fact]
        public void op_ContainsAny_chars_whenTrue()
        {
            var values = new[]
                             {
                                 'a', 'e', 'x'
                             };

            Assert.True(new MutableString("Example").ContainsAny(values));
        }

        [Fact]
        public void op_ContainsAny_stringsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new MutableString().ContainsAny(null as string[]));
        }

        [Fact]
        public void op_ContainsAny_strings_whenFalse()
        {
            var values = new[]
                             {
                                 string.Empty, null, "example", "solar system"
                             };

            Assert.False(new MutableString().ContainsAny(values));
            Assert.False(new MutableString("Example").ContainsAny(values));
        }

        [Fact]
        public void op_ContainsAny_strings_whenTrue()
        {
            var values = new[]
                             {
                                 "example", "ex", "ample"
                             };

            Assert.True(new MutableString("Example").ContainsAny(values));
        }

        [Theory]
        [InlineData(false, "")]
        [InlineData(true, "example")]
        public void op_ContainsText(bool expected,
                                    string value)
        {
            var actual = new MutableString(value).ContainsText();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Example", 'X')]
        [InlineData("Example", 'z')]
        public void op_Contains_char_whenFalse(string text,
                                               char value)
        {
            Assert.False(new MutableString(text).Contains(value));
        }

        [Theory]
        [InlineData("Example", 'x')]
        [InlineData("Example", 'e')]
        public void op_Contains_char_whenTrue(string text,
                                              char value)
        {
            Assert.True(new MutableString(text).Contains(value));
        }

        [Fact]
        public void op_Contains_stringEmpty()
        {
            var obj = new MutableString("Example");

            Assert.Throws<ArgumentOutOfRangeException>(() => obj.Contains(string.Empty));
        }

        [Fact]
        public void op_Contains_stringNull()
        {
            var obj = new MutableString("Example");

            Assert.Throws<ArgumentNullException>(() => obj.Contains(null));
        }

        [Theory]
        [InlineData("Example", "ex")]
        [InlineData("Example", "Missing")]
        public void op_Contains_string_whenFalse(string text,
                                                 string value)
        {
            Assert.False(new MutableString(text).Contains(value));
        }

        [Theory]
        [InlineData("Example", "ample")]
        [InlineData("Example", "Ex")]
        public void op_Contains_string_whenTrue(string text,
                                                string value)
        {
            Assert.True(new MutableString(text).Contains(value));
        }

        [Theory]
        [InlineData("Ex", "Example", 0, 2)]
        [InlineData("amp", "Example", 2, 3)]
        [InlineData("ample", "Example", 2, 5)]
        [InlineData("", "Example", 0, 7)]
        public void op_Crop_int_int(string expected,
                                    string text,
                                    int start,
                                    int length)
        {
            var actual = new MutableString(text);

            Assert.Same(actual, actual.Crop(start, length));

            Assert.Equal(new MutableString(expected), actual);
        }

        [Theory]
        [InlineData("", -1, 1)]
        [InlineData("", 2, 1)]
        [InlineData("example", 1, 0)]
        public void op_Crop_int_int_whenArgumentOutOfRangeException(string text,
                                                                    int start,
                                                                    int length)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MutableString(text).Crop(start, length));
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("", "Example")]
        [InlineData("123", "1 Example 2. 3")]
        public void op_Digits(string expected,
                              string text)
        {
            var actual = new MutableString(text).Digits().ToArray();

            Assert.Equal((expected ?? string.Empty).ToCharArray(), actual);
        }

        [Fact]
        public void op_EndsWithAny_stringsEmpty()
        {
            Assert.False(new MutableString().EndsWithAny(new List<string>().ToArray()));
        }

        [Fact]
        public void op_EndsWithAny_stringsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new MutableString().EndsWithAny(null));
        }

        [Fact]
        public void op_EndsWithAny_strings_whenFalse()
        {
            var values = new[]
                             {
                                 "test", null, string.Empty
                             };

            Assert.False(new MutableString().EndsWithAny(values));
            Assert.False(new MutableString("example").EndsWithAny(values));
        }

        [Fact]
        public void op_EndsWithAny_strings_whenTrue()
        {
            var values = new[]
                             {
                                 "test", "ample"
                             };

            Assert.True(new MutableString("example").EndsWithAny(values));
        }

        [Theory]
        [InlineData("example", 'x')]
        [InlineData("", 'e')]
        public void op_EndsWith_char_whenFalse(string text,
                                               char value)
        {
            Assert.False(new MutableString(text).EndsWith(value));
        }

        [Theory]
        [InlineData("example", 'e')]
        public void op_EndsWith_char_whenTrue(string text,
                                              char value)
        {
            Assert.True(new MutableString(text).EndsWith(value));
        }

        [Fact]
        public void op_EndsWith_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new MutableString().EndsWith(null));
        }

        [Theory]
        [InlineData("example", "x")]
        [InlineData("example", "test")]
        [InlineData("example", "")]
        [InlineData("", "")]
        [InlineData("", "test")]
        [InlineData("ample", "example")]
        [InlineData("road", "test")]
        public void op_EndsWith_string_whenFalse(string text,
                                                 string value)
        {
            Assert.False(new MutableString(text).EndsWith(value));
        }

        [Theory]
        [InlineData("example", "e")]
        [InlineData("example", "ample")]
        public void op_EndsWith_string_whenTrue(string text,
                                                string value)
        {
            Assert.True(new MutableString(text).EndsWith(value));
        }

        [Fact]
        public void op_EqualsAny_stringsEmpty()
        {
            Assert.False(new MutableString().EqualsAny(new List<string>().ToArray()));
        }

        [Fact]
        public void op_EqualsAny_stringsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new MutableString().EqualsAny(null));
        }

        [Fact]
        public void op_EqualsAny_strings_whenFalse()
        {
            var values = new[]
                             {
                                 "test", null, string.Empty
                             };

            Assert.False(new MutableString().EqualsAny(values));
            Assert.False(new MutableString("example").EqualsAny(values));
        }

        [Fact]
        public void op_EqualsAny_strings_whenTrue()
        {
            var values = new[]
                             {
                                 "test", "example"
                             };

            Assert.True(new MutableString("example").EqualsAny(values));
        }

        [Theory]
        [InlineData("", "one")]
        [InlineData("One", "one")]
        [InlineData("One", "Two")]
        [InlineData("1", "one")]
        [InlineData("1", "")]
        public void op_Equals_MutableString_whenFalse(string comparand1,
                                                      string comparand2)
        {
            Assert.False(new MutableString(comparand1).Equals(new MutableString(comparand2)));
        }

        [Fact]
        public void op_Equals_MutableString_whenSame()
        {
            var comparand1 = new MutableString("One");
            var comparand2 = comparand1;

            Assert.True(comparand1.Equals(comparand2));
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("One", "One")]
        public void op_Equals_MutableString_whenTrue(string comparand1,
                                                     string comparand2)
        {
            Assert.True(new MutableString(comparand1).Equals(new MutableString(comparand2)));
        }

        [Fact]
        public void op_Equals_objectNull()
        {
            Assert.False(new MutableString("One").Equals(null as object));
        }

        [Fact]
        public void op_Equals_objectSame()
        {
            var obj = new MutableString("One");

            Assert.True(obj.Equals(obj as object));
        }

        [Fact]
        public void op_Equals_objectString()
        {
            Assert.Throws<InvalidCastException>(() => new MutableString("One").Equals("One" as object));
        }

        [Theory]
        [InlineData("One", "Two")]
        public void op_Equals_object_whenFalse(string comparand1,
                                               string comparand2)
        {
            Assert.False(new MutableString(comparand1).Equals(new MutableString(comparand2) as object));
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("One", "One")]
        public void op_Equals_object_whenTrue(string comparand1,
                                              string comparand2)
        {
            Assert.True(new MutableString(comparand1).Equals(new MutableString(comparand2) as object));
        }

        [Fact]
        public void op_Equals_string()
        {
            Assert.True(new MutableString("One").Equals("One"));
        }

        [Fact]
        public void op_GetEnumerator()
        {
            const string expected = "Example";
            var actual = new MutableString(expected).Aggregate(string.Empty,
                                                               (current,
                                                                c) => current + c);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_GetEnumerator_whenEmpty()
        {
            Assert.Empty(new MutableString());
        }

        [Fact]
        public void op_GetHashCode()
        {
            var obj = new MutableString("Example");

            var expected = obj.ToString().GetHashCode();
            var actual = obj.GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_GetObjectData_SerializationInfoNull_StreamingContext()
        {
            var context = new StreamingContext(StreamingContextStates.All);

            // ReSharper disable AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => (new MutableString("Example") as ISerializable).GetObjectData(null, context));

            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Fact]
        public void op_GetObjectData_SerializationInfo_StreamingContext()
        {
            var info = new SerializationInfo(typeof(MutableString), new FormatterConverter());
            var context = new StreamingContext(StreamingContextStates.All);

            const string expected = "Example";

            (new MutableString(expected) as ISerializable).GetObjectData(info, context);

            var actual = info.GetString("_value");

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, "Example", 'x')]
        [InlineData(6, "Example", 'e')]
        public void op_IndexOf_char(int expected,
                                    string text,
                                    char value)
        {
            Assert.Equal(expected, new MutableString(text).IndexOf(value));
        }

        [Theory]
        [InlineData(1, "Example", 'x', 1)]
        [InlineData(6, "example", 'e', 1)]
        public void op_IndexOf_char_int(int expected,
                                        string text,
                                        char value,
                                        int start)
        {
            Assert.Equal(expected, new MutableString(text).IndexOf(value, start));
        }

        [Fact]
        public void op_IndexOf_char_intNegative()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MutableString().IndexOf(' ', -1));
        }

        [Fact]
        public void op_IndexOf_char_intTooLong()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MutableString().IndexOf(' ', 1));
        }

        [Theory]
        [InlineData(1, "Example", 'x', 1, 1)]
        [InlineData(6, "example", 'e', 1, 6)]
        public void op_IndexOf_char_int_int(int expected,
                                            string text,
                                            char value,
                                            int start,
                                            int length)
        {
            Assert.Equal(expected, new MutableString(text).IndexOf(value, start, length));
        }

        [Theory]
        [InlineData("", ' ', 0, -1)]
        [InlineData("", ' ', 0, 2)]
        [InlineData("", ' ', 2, 1)]
        public void op_IndexOf_char_int_int_throwsArgumentOutOfRangeException(string text,
                                                                              char value,
                                                                              int start,
                                                                              int length)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MutableString(text).IndexOf(value, start, length));
        }

        [Theory]
        [InlineData("Example", 'X', 1, 1)]
        [InlineData("Example", 'z', 2, 5)]
        [InlineData("example", 'e', 1, 5)]
        public void op_IndexOf_char_int_int_whenNotFound(string text,
                                                         char value,
                                                         int start,
                                                         int length)
        {
            Assert.Equal(-1, new MutableString(text).IndexOf(value, start, length));
        }

        [Theory]
        [InlineData("Example", 'X', 1)]
        [InlineData("Example", 'z', 2)]
        public void op_IndexOf_char_int_whenNotFound(string text,
                                                     char value,
                                                     int start)
        {
            Assert.Equal(-1, new MutableString(text).IndexOf(value, start));
        }

        [Theory]
        [InlineData("", '.')]
        [InlineData("", 'X')]
        [InlineData("Example", 'X')]
        [InlineData("Example", 'z')]
        public void op_IndexOf_char_whenNotFound(string text,
                                                 char value)
        {
            Assert.Equal(-1, new MutableString(text).IndexOf(value));
        }

        [Theory]
        [InlineData(1, "example", "x")]
        [InlineData(6, "Example", "e")]
        [InlineData(2, "Example", "ample")]
        public void op_IndexOf_string(int expected,
                                      string text,
                                      string value)
        {
            Assert.Equal(expected, new MutableString(text).IndexOf(value));
        }

        [Fact]
        public void op_IndexOf_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new MutableString().IndexOf(null));
        }

        [Theory]
        [InlineData(1, "example", "x", 1)]
        [InlineData(6, "Example", "e", 6)]
        [InlineData(2, "Example", "ample", 1)]
        [InlineData(2, "Example", "ample", 2)]
        public void op_IndexOf_string_int(int expected,
                                          string text,
                                          string value,
                                          int start)
        {
            Assert.Equal(expected, new MutableString(text).IndexOf(value, start));
        }

        [Theory]
        [InlineData(1, "example", "x", 1, 3)]
        [InlineData(6, "Example", "e", 6, 1)]
        [InlineData(2, "Example", "ample", 1, 6)]
        [InlineData(2, "Example", "ample", 2, 5)]
        public void op_IndexOf_string_int_int(int expected,
                                              string text,
                                              string value,
                                              int start,
                                              int length)
        {
            Assert.Equal(expected, new MutableString(text).IndexOf(value, start, length));
        }

        [Theory]
        [InlineData("", " ", 0, -1)]
        [InlineData("", " ", 0, 2)]
        [InlineData(" ", " ", 2, 1)]
        [InlineData("example", "ample", 6, 1)]
        [InlineData("", "example", 0, 6)]
        public void op_IndexOf_string_int_int_throwsArgumentOutOfRangeException(string text,
                                                                                string value,
                                                                                int start,
                                                                                int length)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MutableString(text).IndexOf(value, start, length));
        }

        [Theory]
        [InlineData("", "", 0, 0)]
        [InlineData("example", "", 1, 6)]
        [InlineData("example", "X", 1, 6)]
        [InlineData("example", "z", 6, 1)]
        [InlineData("Examine", "ample", 2, 5)]
        public void op_IndexOf_string_int_int_whenNotFound(string text,
                                                           string value,
                                                           int start,
                                                           int length)
        {
            Assert.Equal(-1, new MutableString(text).IndexOf(value, start, length));
        }

        [Theory]
        [InlineData("", " ", -1)]
        [InlineData("", " ", 1)]
        public void op_IndexOf_string_int_throwsArgumentOutOfRangeException(string text,
                                                                            string value,
                                                                            int start)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MutableString(text).IndexOf(value, start));
        }

        [Theory]
        [InlineData("example", "", 1)]
        [InlineData("example", "X", 1)]
        [InlineData("example", "z", 6)]
        public void op_IndexOf_string_int_whenNotFound(string text,
                                                       string value,
                                                       int start)
        {
            Assert.Equal(-1, new MutableString(text).IndexOf(value, start));
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("example", "")]
        [InlineData("example", "X")]
        [InlineData("example", "z")]
        [InlineData("Example", "amples")]
        public void op_IndexOf_string_whenNotFound(string text,
                                                   string value)
        {
            Assert.Equal(-1, new MutableString(text).IndexOf(value));
        }

        [Fact]
        public void op_IndexesOf_char()
        {
            var expected = new[]
                               {
                                   0, 3, 11, 16
                               };

            var actual = new MutableString(" an example test ").IndexesOf(' ').ToArray();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", ' ')]
        [InlineData("Example", ' ')]
        public void op_IndexesOf_char_whenEmpty(string text,
                                                char value)
        {
            Assert.Empty(new MutableString(text).IndexesOf(value));
        }

        [Theory]
        [InlineData("", -1, "example")]
        [InlineData("", 1, "example")]
        public void op_Insert_intNegative_IEnumerableOfChar_throwsArgumentOutOfRangeException(string text,
                                                                                              int index,
                                                                                              string value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MutableString(text).Insert(index, value.ToCharArray()));
        }

        [Theory]
        [InlineData("", -1, "example")]
        [InlineData("", 1, "example")]
        public void op_Insert_intNegative_string(string text,
                                                 int index,
                                                 string value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MutableString(text).Insert(index, value));
        }

        [Theory]
        [InlineData("Example", "ample", 0, "Ex")]
        public void op_Insert_int_IEnumerableOfChar(string expected,
                                                    string text,
                                                    int index,
                                                    string value)
        {
            var actual = new MutableString(text);

            Assert.Same(actual, actual.Insert(index, (value ?? string.Empty).ToCharArray()));

            Assert.Equal(new MutableString(expected), actual);
        }

        [Fact]
        public void op_Insert_int_IEnumerableOfCharNull()
        {
            Assert.Throws<ArgumentNullException>(() => new MutableString().Insert(0, null as IEnumerable<char>));
        }

        [Fact]
        public void op_Insert_int_char()
        {
            var expected = new MutableString("Example");
            var actual = new MutableString("ample");

            Assert.Same(actual, actual.Insert(0, 'x'));
            Assert.Same(actual, actual.Insert(0, 'E'));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Insert_int_char_whenDefault()
        {
            var expected = new MutableString("Ex");
            var actual = new MutableString();

            Assert.Same(actual, actual.Insert(0, 'x'));
            Assert.Same(actual, actual.Insert(0, 'E'));

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Example", "ample", 0, "Ex")]
        public void op_Insert_int_string(string expected,
                                         string text,
                                         int index,
                                         string value)
        {
            var actual = new MutableString(text);

            Assert.Same(actual, actual.Insert(index, value ?? string.Empty));

            Assert.Equal(new MutableString(expected), actual);
        }

        [Fact]
        public void op_Insert_int_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new MutableString().Insert(0, null));
        }

        [Fact]
        public void op_IsNullOrEmpty_MutableString()
        {
            Assert.False(MutableString.IsNullOrEmpty(new MutableString("example")));
        }

        [Fact]
        public void op_IsNullOrEmpty_MutableStringEmpty()
        {
            Assert.True(MutableString.IsNullOrEmpty(new MutableString()));
        }

        [Fact]
        public void op_IsNullOrEmpty_MutableStringNull()
        {
            Assert.True(MutableString.IsNullOrEmpty(null));
        }

        [Fact]
        public void op_IsNullOrWhiteSpace_MutableStringNull()
        {
            Assert.True(MutableString.IsNullOrWhiteSpace(null));
        }

        [Fact]
        public void op_IsNullOrWhiteSpace_MutableString_whenFalse()
        {
            Assert.False(MutableString.IsNullOrWhiteSpace(new MutableString("example")));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\u0009")]
        public void op_IsNullOrWhiteSpace_MutableString_whenTrue(string text)
        {
            Assert.True(MutableString.IsNullOrWhiteSpace(new MutableString(text)));
        }

        [Fact]
        public void op_LastIndexOf_stringNull_int_int()
        {
            Assert.Throws<ArgumentNullException>(() => new MutableString().LastIndexOf(null, 0, 0));
        }

        [Theory]
        [InlineData("example", "example", -1, 7)]
        [InlineData("example", "ex", 8, 7)]
        [InlineData("example", "ex", 0, 1)]
        [InlineData("", " ", 0, -1)]
        [InlineData("", " ", 0, 2)]
        [InlineData("", "example", 0, 6)]
        public void op_LastIndexOf_string_intNegative_int_throwsArgumentOutOfRangeException(string text,
                                                                                            string value,
                                                                                            int start,
                                                                                            int length)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MutableString(text).LastIndexOf(value, start, length));
        }

        [Theory]
        [InlineData(1, "example", "x", 1, 3)]
        [InlineData(6, "Example", "e", 0, 1)]
        [InlineData(2, "Example", "ample", 1, 5)]
        [InlineData(2, "Example", "ample", 2, 5)]
        public void op_LastIndexOf_string_int_int(int expected,
                                                  string text,
                                                  string value,
                                                  int start,
                                                  int length)
        {
            Assert.Equal(expected, new MutableString(text).LastIndexOf(value, start, length));
        }

        [Theory]
        [InlineData("", "", 0, 0)]
        [InlineData("example", "", 0, 0)]
        [InlineData("example", "", 1, 6)]
        [InlineData("example", "X", 1, 6)]
        [InlineData("example", "z", 6, 1)]
        [InlineData("Examine", "ample", 2, 5)]
        public void op_LastIndexOf_string_int_int_whenNotFound(string text,
                                                               string value,
                                                               int start,
                                                               int length)
        {
            Assert.Equal(-1, new MutableString(text).LastIndexOf(value, start, length));
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("Élan", "Élan")]
        [InlineData("Example", "Example")]
        [InlineData("Example", "1 Example. ")]
        public void op_Letters(string expected,
                               string text)
        {
            var actual = new MutableString(text).Letters().ToArray();

            Assert.Equal((expected ?? string.Empty).ToCharArray(), actual);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("Élan", "Élan")]
        [InlineData("Example", "Example")]
        [InlineData("1Example", "1 Example. ")]
        public void op_LettersAndDigits(string expected,
                                        string text)
        {
            var actual = new MutableString(text).LettersAndDigits().ToArray();

            Assert.Equal((expected ?? string.Empty).ToCharArray(), actual);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("Example 123", "Example 123")]
        [InlineData("Elan .", "Élan .")]
        public void op_NormalizeToEnglishAlphabet(string expected,
                                                  string text)
        {
            var actual = new MutableString(text);

            Assert.Same(actual, actual.NormalizeToEnglishAlphabet());

            Assert.Equal(new MutableString(expected), actual);
        }

        [Theory]
        [InlineData("")]
        [InlineData("Example")]
        [InlineData("Élan")]
        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "The lower case implementation is appropriate here.")]
        public void op_NormalizeToLowerInvariant(string text)
        {
            var expected = new MutableString((text ?? string.Empty).ToLowerInvariant());
            var actual = new MutableString(text);

            Assert.Same(actual, actual.NormalizeToLowerInvariant());

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", "en-GB")]
        [InlineData("Example", "en-US")]
        [InlineData("Élan", "fr-FR")]
        public void op_NormalizeToLower_CultureInfo(string text,
                                                    string language)
        {
            var culture = new CultureInfo(language);

            var expected = new MutableString((text ?? string.Empty).ToLower(culture));
            var actual = new MutableString(text);

            Assert.Same(actual, actual.NormalizeToLower(culture));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_NormalizeToLower_CultureInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => new MutableString().NormalizeToLower(null));
        }

        [Theory]
        [InlineData("")]
        [InlineData("Example")]
        [InlineData("Élan")]
        public void op_NormalizeToUpperInvariant(string text)
        {
            var expected = new MutableString((text ?? string.Empty).ToUpperInvariant());
            var actual = new MutableString(text);

            Assert.Same(actual, actual.NormalizeToUpperInvariant());

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", "en-GB")]
        [InlineData("Example", "en-US")]
        [InlineData("Élan", "fr-FR")]
        public void op_NormalizeToUpper_CultureInfo(string text,
                                                    string language)
        {
            var culture = new CultureInfo(language);

            var expected = new MutableString((text ?? string.Empty).ToUpper(culture));
            var actual = new MutableString(text);

            Assert.Same(actual, actual.NormalizeToUpper(culture));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_NormalizeToUpper_CultureInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => new MutableString().NormalizeToUpper(null));
        }

        [Fact]
        public void op_NormalizeWhiteSpace()
        {
            var expected = new MutableString(' ', Characters.WhiteSpace.Count);
            var value = Characters.WhiteSpace.Aggregate(string.Empty,
                                                        (current,
                                                         c) => current + c);
            var actual = new MutableString(value);

            Assert.Same(actual, actual.NormalizeWhiteSpace());

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(true, "")]
        [InlineData(false, "example")]
        public void op_NotContainsText(bool expected,
                                       string value)
        {
            var actual = new MutableString(value).NotContainsText();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", 0)]
        [InlineData(" ", 1)]
        [InlineData(".", 2)]
        [InlineData(".", 3)]
        [InlineData("Example", 7)]
        public void op_PadLeft_int(string text,
                                   int width)
        {
            var expected = new MutableString((text ?? string.Empty).PadLeft(width));
            var actual = new MutableString(text);

            Assert.Same(actual, actual.PadLeft(width));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_PadLeft_intNegative()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MutableString().PadLeft(-1));
        }

        [Fact]
        public void op_PadLeft_intNegative_char()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MutableString().PadLeft(-1, '_'));
        }

        [Theory]
        [InlineData("", 0, '_')]
        [InlineData(" ", 1, '_')]
        [InlineData(".", 2, '_')]
        [InlineData(".", 3, '_')]
        [InlineData("Example", 7, '_')]
        public void op_PadLeft_int_char(string text,
                                        int width,
                                        char padding)
        {
            var expected = new MutableString((text ?? string.Empty).PadLeft(width, '_'));
            var actual = new MutableString(text);

            Assert.Same(actual, actual.PadLeft(width, padding));

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", 0)]
        [InlineData(" ", 1)]
        [InlineData(".", 2)]
        [InlineData(".", 3)]
        [InlineData("Example", 7)]
        public void op_PadRight_int(string text,
                                    int width)
        {
            var expected = new MutableString((text ?? string.Empty).PadRight(width));
            var actual = new MutableString(text);

            Assert.Same(actual, actual.PadRight(width));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_PadRight_intNegative()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MutableString().PadRight(-1));
        }

        [Fact]
        public void op_PadRight_intNegative_char()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MutableString().PadRight(-1, '_'));
        }

        [Theory]
        [InlineData("", 0, '_')]
        [InlineData(" ", 1, '_')]
        [InlineData(".", 2, '_')]
        [InlineData(".", 3, '_')]
        [InlineData("Example", 7, '_')]
        public void op_PadRight_int_char(string text,
                                         int width,
                                         char padding)
        {
            var expected = new MutableString((text ?? string.Empty).PadRight(width, '_'));
            var actual = new MutableString(text);

            Assert.Same(actual, actual.PadRight(width, padding));

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", "Example", "")]
        [InlineData("", "Example", "test")]
        [InlineData("An ", "An Example", "Example")]
        public void op_Prefix_string(string expected,
                                     string text,
                                     string value)
        {
            Assert.Equal(expected, new MutableString(text).Prefix(value));
        }

        [Fact]
        public void op_Prefix_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new MutableString().Prefix(null));
        }

        [Fact]
        public void op_Prefix_stringNull_bool()
        {
            Assert.Throws<ArgumentNullException>(() => new MutableString().Prefix(null, true));
        }

        [Theory]
        [InlineData("", "Example", "", false)]
        [InlineData("", "Example", "test", false)]
        [InlineData("", "Example", "Example", false)]
        [InlineData(" An ", " An Example", "Example", false)]
        [InlineData("", "Example", "", true)]
        [InlineData("", "Example", "test", true)]
        [InlineData("An", " An Example ", "Example", true)]
        public void op_Prefix_string_bool(string expected,
                                          string text,
                                          string value,
                                          bool trim)
        {
            Assert.Equal(expected, new MutableString(text).Prefix(value, trim));
        }

        [Theory]
        [InlineData("Example", "An Example")]
        public void op_Prefix_string_bool_throwsArgumentOutOfRangeException(string text,
                                                                            string value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MutableString(text).Prefix(value, true));
        }

        [Theory]
        [InlineData("Example", "An Example")]
        public void op_Prefix_string_throwsArgumentOutOfRangeException(string text,
                                                                       string value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MutableString(text).Prefix(value));
        }

        [Theory]
        [InlineData(".", "", '.')]
        [InlineData(".example", "example", '.')]
        public void op_Prepend_char(string expected,
                                    string text,
                                    char value)
        {
            var actual = new MutableString(text);

            Assert.Same(actual, actual.Prepend(value));

            Assert.Equal(new MutableString(expected), actual);
        }

        [Theory]
        [InlineData("", "", "")]
        [InlineData("example", "", "example")]
        [InlineData("example", "ample", "ex")]
        public void op_Prepend_string(string expected,
                                      string text,
                                      string value)
        {
            var actual = new MutableString(text);

            Assert.Same(actual, actual.Prepend(value));

            Assert.Equal(new MutableString(expected), actual);
        }

        [Fact]
        public void op_Prepend_string_whenArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new MutableString().Prepend(null));
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("Example", "Example")]
        [InlineData(" An Example ", " An Example ")]
        [InlineData(" An Example ", "  An  Example  ")]
        [InlineData(" An Example ", "  Ann  Example  ")]
        public void op_RemoveAdjacentDuplicates(string expected,
                                                string text)
        {
            var actual = new MutableString(text);

            Assert.Same(actual, actual.RemoveAdjacentDuplicates());

            Assert.Equal(new MutableString(expected), actual);
        }

        [Theory]
        [InlineData("", "", ' ')]
        [InlineData("Example", "Example", ' ')]
        [InlineData(" An Example ", " An Example ", ' ')]
        [InlineData(" An Example ", "  An  Example  ", ' ')]
        public void op_RemoveAdjacentDuplicates_char(string expected,
                                                     string text,
                                                     char value)
        {
            var actual = new MutableString(text);

            Assert.Same(actual, actual.RemoveAdjacentDuplicates(value));

            Assert.Equal(new MutableString(expected), actual);
        }

        [Theory]
        [InlineData("", "", "abc")]
        [InlineData("", "example", "")]
        [InlineData("", "example", "123")]
        [InlineData("example", "example123", "example")]
        [InlineData("exe", "example", "ex")]
        public void op_RemoveAllExcept_IEnumerableOfChar(string expected,
                                                         string text,
                                                         string value)
        {
            var actual = new MutableString(text);

            Assert.Same(actual, actual.RemoveAllExcept(value ?? string.Empty));

            Assert.Equal(new MutableString(expected), actual);
        }

        [Fact]
        public void op_RemoveAllExcept_IEnumerableOfCharNull()
        {
            Assert.Throws<ArgumentNullException>(() => new MutableString().RemoveAllExcept(null));
        }

        [Fact]
        public void op_RemoveAnyFromEnd_IEnumerableOfString()
        {
            var actual = new MutableString("Example");

            Assert.Same(actual, actual.RemoveAnyFromEnd("test", null, string.Empty, "ample"));

            Assert.Equal(new MutableString("Ex"), actual);
        }

        [Fact]
        public void op_RemoveAnyFromEnd_IEnumerableOfStringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new MutableString().RemoveAnyFromEnd(null));
        }

        [Fact]
        public void op_RemoveAnyFromStart_IEnumerableOfString()
        {
            var actual = new MutableString("Example");

            Assert.Same(actual, actual.RemoveAnyFromStart("ex", null, string.Empty, "Ex"));

            Assert.Equal(new MutableString("ample"), actual);
        }

        [Fact]
        public void op_RemoveAnyFromStart_IEnumerableOfStringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new MutableString().RemoveAnyFromStart(null));
        }

        [Theory]
        [InlineData("", "", "example")]
        [InlineData("example", "example", "")]
        [InlineData("example", "example", "123")]
        [InlineData("", "example", "example123")]
        [InlineData("amp", "example", "exl")]
        public void op_RemoveAny_IEnumerableOfChar(string expected,
                                                   string text,
                                                   string value)
        {
            var actual = new MutableString(text);

            Assert.Same(actual, actual.RemoveAny((value ?? string.Empty).ToCharArray()));

            Assert.Equal(new MutableString(expected), actual);
        }

        [Fact]
        public void op_RemoveAny_IEnumerableOfCharNull()
        {
            Assert.Throws<ArgumentNullException>(() => new MutableString().RemoveAny(null as char[]));
        }

        [Fact]
        public void op_RemoveAny_stringsEmpty()
        {
            var expected = new MutableString();
            var actual = new MutableString();

            Assert.Same(actual, actual.RemoveAny(new List<string>().ToArray()));
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_RemoveAny_stringsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new MutableString().RemoveAny(null as string[]));
        }

        [Fact]
        public void op_RemoveAny_strings_whenFalse()
        {
            var values = new[]
                             {
                                 "test", null, string.Empty
                             };

            var expected = new MutableString("example");
            var actual = new MutableString("example");

            Assert.Same(actual, actual.RemoveAny(values));
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_RemoveAny_strings_whenTrue()
        {
            var values = new[]
                             {
                                 "ample", "ex"
                             };

            var expected = new MutableString();
            var actual = new MutableString("example");

            Assert.Same(actual, actual.RemoveAny(values));
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("Example", "Example")]
        [InlineData(" Example. ", "1 Example. ")]
        public void op_RemoveDigits(string expected,
                                    string text)
        {
            var actual = new MutableString(text);

            Assert.Same(actual, actual.RemoveDigits());

            Assert.Equal(new MutableString(expected), actual);
        }

        [Theory]
        [InlineData("", "", "example")]
        [InlineData("Example", "Example", "example")]
        [InlineData("", "Example", "Example")]
        [InlineData("Ex", "Example", "ample")]
        [InlineData("Test", "Test", "example")]
        public void op_RemoveFromEnd_string(string expected,
                                            string text,
                                            string value)
        {
            Assert.Equal(new MutableString(expected), new MutableString(text).RemoveFromEnd(value));
        }

        [Fact]
        public void op_RemoveFromEnd_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MutableString().RemoveFromEnd(string.Empty));
        }

        [Fact]
        public void op_RemoveFromEnd_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new MutableString().RemoveFromEnd(null));
        }

        [Theory]
        [InlineData("", "", "example")]
        [InlineData("Example", "Example", "example")]
        [InlineData("", "Example", "Example")]
        [InlineData("ample", "Example", "Ex")]
        [InlineData("Test", "Test", "example")]
        public void op_RemoveFromStart_string(string expected,
                                              string text,
                                              string value)
        {
            Assert.Equal(new MutableString(expected), new MutableString(text).RemoveFromStart(value));
        }

        [Fact]
        public void op_RemoveFromStart_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MutableString().RemoveFromStart(string.Empty));
        }

        [Fact]
        public void op_RemoveFromStart_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new MutableString().RemoveFromStart(null));
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("", "Élan")]
        [InlineData("", "Example")]
        [InlineData("1 . ", "1 Example. ")]
        public void op_RemoveLetters(string expected,
                                     string text)
        {
            var actual = new MutableString(text);

            Assert.Same(actual, actual.RemoveLetters());

            Assert.Equal(new MutableString(expected), actual);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("", "Élan")]
        [InlineData("", "Example")]
        [InlineData(" . ", "1 Example. ")]
        public void op_RemoveLettersAndDigits(string expected,
                                              string text)
        {
            var actual = new MutableString(text);

            Assert.Same(actual, actual.RemoveLettersAndDigits());

            Assert.Equal(new MutableString(expected), actual);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("", "    ")]
        [InlineData("Élan", "Élan")]
        [InlineData("Example", "Example")]
        [InlineData("1Example.", "1 Example. ")]
        public void op_RemoveWhiteSpace(string expected,
                                        string text)
        {
            var actual = new MutableString(text);

            Assert.Same(actual, actual.RemoveWhiteSpace());

            Assert.Equal(new MutableString(expected), actual);
        }

        [Theory]
        [InlineData("", "", '.')]
        [InlineData("example", "example", '.')]
        [InlineData("example", ".ex.ample.", '.')]
        public void op_Remove_char(string expected,
                                   string text,
                                   char value)
        {
            var actual = new MutableString(text);

            Assert.Same(actual, actual.Remove(value));

            Assert.Equal(new MutableString(expected), actual);
        }

        [Fact]
        public void op_Remove_int_int()
        {
            var expected = new MutableString("ample");
            var actual = new MutableString("Example");

            Assert.Same(actual, actual.Remove(0, 2));

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", "", "")]
        [InlineData("example", "example", "test")]
        [InlineData("", "example", "example")]
        [InlineData("ample", "ample", "example")]
        [InlineData("ex", "example", "ample")]
        [InlineData("example", "exampleexample", "example")]
        [InlineData("example", "exexampleample", "example")]
        public void op_Remove_string(string expected,
                                     string text,
                                     string value)
        {
            var actual = new MutableString(text);

            Assert.Same(actual, actual.Remove(value));

            Assert.Equal(new MutableString(expected), actual);
        }

        [Fact]
        public void op_Remove_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new MutableString().Remove(null));
        }

        [Theory]
        [InlineData("test", "example", "example", "test")]
        [InlineData("example", "example", "foo", "bar")]
        [InlineData("exclude", "example", "ample", "clude")]
        public void op_ReplaceEnd_string_string(string expected,
                                                string text,
                                                string value,
                                                string replacement)
        {
            var actual = new MutableString(text);

            Assert.Same(actual, actual.ReplaceEnd(value, replacement));

            Assert.Equal(new MutableString(expected), actual);
        }

        [Theory]
        [InlineData(null, "example")]
        [InlineData("example", null)]
        public void op_ReplaceEnd_string_string_ArgumentNullException(string value,
                                                                      string replacement)
        {
            Assert.Throws<ArgumentNullException>(() => new MutableString().ReplaceEnd(value, replacement));
        }

        [Theory]
        [InlineData("", "example")]
        [InlineData("example", "")]
        public void op_ReplaceEnd_string_string_ArgumentOutOfRangeException(string value,
                                                                            string replacement)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MutableString().ReplaceEnd(value, replacement));
        }

        [Theory]
        [InlineData("test", "example", "example", "test")]
        [InlineData("example", "example", "foo", "bar")]
        [InlineData("trample", "example", "ex", "tr")]
        public void op_ReplaceStart_string_string(string expected,
                                                  string text,
                                                  string value,
                                                  string replacement)
        {
            var actual = new MutableString(text);

            Assert.Same(actual, actual.ReplaceStart(value, replacement));

            Assert.Equal(new MutableString(expected), actual);
        }

        [Theory]
        [InlineData(null, "example")]
        [InlineData("example", null)]
        public void op_ReplaceStart_string_string_ArgumentNullException(string value,
                                                                        string replacement)
        {
            Assert.Throws<ArgumentNullException>(() => new MutableString().ReplaceStart(value, replacement));
        }

        [Theory]
        [InlineData("", "example")]
        [InlineData("example", "")]
        public void op_ReplaceStart_string_string_ArgumentOutOfRangeException(string value,
                                                                              string replacement)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MutableString().ReplaceStart(value, replacement));
        }

        [Fact]
        public void op_Replace_char_char()
        {
            var expected = new MutableString("Exampl_");
            var actual = new MutableString("Example");

            Assert.Same(actual, actual.Replace('e', '_'));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Replace_char_char_int()
        {
            var expected = new MutableString("exampl_");
            var actual = new MutableString("example");

            Assert.Same(actual, actual.Replace('e', '_', 3));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Replace_char_char_int_int()
        {
            var expected = new MutableString("Exampl_");
            var actual = new MutableString("Example");

            Assert.Same(actual, actual.Replace('e', '_', 0, 7));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Replace_char_char_int_int_whenDefault()
        {
            var obj = new MutableString();

            Assert.Throws<ArgumentOutOfRangeException>(() => obj.Replace('.', '-', 0, 1));
        }

        [Fact]
        public void op_Replace_char_char_whenDefault()
        {
            var expected = new MutableString();
            var actual = new MutableString();

            Assert.Same(actual, actual.Replace('.', '-'));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Replace_string_stringEmpty()
        {
            var expected = new MutableString("Ex");
            var actual = new MutableString("Example");

            Assert.Same(actual, actual.Replace("ample", string.Empty));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Replace_string_stringEmpty_int()
        {
            var expected = new MutableString("ex");
            var actual = new MutableString("example");

            Assert.Same(actual, actual.Replace("ample", string.Empty, 1));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Replace_string_stringEmpty_int_int()
        {
            var expected = new MutableString("Ex");
            var actual = new MutableString("Example");

            Assert.Same(actual, actual.Replace("ample", string.Empty, 0, 7));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Replace_string_stringEmpty_int_int_whenDefault()
        {
            var obj = new MutableString();

            Assert.Throws<ArgumentOutOfRangeException>(() => obj.Replace("example", string.Empty, 0, 1));
        }

        [Fact]
        public void op_Replace_string_stringEmpty_whenDefault()
        {
            var expected = new MutableString();
            var actual = new MutableString();

            Assert.Same(actual, actual.Replace("example", string.Empty));

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", '.', StringSplitOptions.None)]
        [InlineData("", '.', StringSplitOptions.RemoveEmptyEntries)]
        [InlineData("example", '.', StringSplitOptions.None)]
        [InlineData("example", '.', StringSplitOptions.RemoveEmptyEntries)]
        [InlineData("example", 'x', StringSplitOptions.None)]
        [InlineData("example", 'x', StringSplitOptions.RemoveEmptyEntries)]
        [InlineData(" an  example ", ' ', StringSplitOptions.None)]
        [InlineData(" an  example ", ' ', StringSplitOptions.RemoveEmptyEntries)]
        public void op_Split_char_StringSplitOptions(string text,
                                                     char value,
                                                     StringSplitOptions options)
        {
            var expected = text.Split(value, options);
            var actual = new MutableString(text).Split(value, options).ToArray();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_StartsWithAny_stringsEmpty()
        {
            Assert.False(new MutableString().StartsWithAny(new List<string>().ToArray()));
        }

        [Fact]
        public void op_StartsWithAny_stringsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new MutableString().StartsWithAny(null));
        }

        [Fact]
        public void op_StartsWithAny_strings_whenFalse()
        {
            var values = new[]
                             {
                                 "test", null, string.Empty
                             };

            Assert.False(new MutableString().StartsWithAny(values));
            Assert.False(new MutableString("example").StartsWithAny(values));
        }

        [Fact]
        public void op_StartsWithAny_strings_whenTrue()
        {
            var values = new[]
                             {
                                 "test", "ex"
                             };

            Assert.True(new MutableString("example").StartsWithAny(values));
        }

        [Theory]
        [InlineData("example", 'x')]
        [InlineData("", 'e')]
        public void op_StartsWith_char_whenFalse(string text,
                                                 char value)
        {
            Assert.False(new MutableString(text).StartsWith(value));
        }

        [Theory]
        [InlineData("example", 'e')]
        public void op_StartsWith_char_whenTrue(string text,
                                                char value)
        {
            Assert.True(new MutableString(text).StartsWith(value));
        }

        [Fact]
        public void op_StartsWith_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new MutableString().StartsWith(null));
        }

        [Theory]
        [InlineData("example", "x")]
        [InlineData("example", "test")]
        [InlineData("example", "")]
        [InlineData("", "")]
        [InlineData("", "test")]
        [InlineData("ample", "example")]
        [InlineData("road", "test")]
        public void op_StartsWith_string_whenFalse(string text,
                                                   string value)
        {
            Assert.False(new MutableString(text).StartsWith(value));
        }

        [Theory]
        [InlineData("example", "e")]
        [InlineData("example", "ex")]
        public void op_StartsWith_string_whenTrue(string text,
                                                  string value)
        {
            Assert.True(new MutableString(text).StartsWith(value));
        }

        [Theory]
        [InlineData("", 0)]
        [InlineData("Example", 0)]
        [InlineData("Example", 1)]
        [InlineData("Example", 6)]
        [InlineData("Example", 7)]
        public void op_Substring_int(string text,
                                     int start)
        {
            var expected = (text ?? string.Empty).Substring(start);
            var actual = new MutableString(text).Substring(start);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", 0, 0)]
        [InlineData("Example", 0, 0)]
        [InlineData("Example", 0, 1)]
        [InlineData("Example", 1, 1)]
        [InlineData("Example", 0, 7)]
        [InlineData("Example", 6, 1)]
        [InlineData("Example", 7, 0)]
        public void op_Substring_int_int(string text,
                                         int start,
                                         int length)
        {
            var expected = (text ?? string.Empty).Substring(start, length);
            var actual = new MutableString(text).Substring(start, length);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Example", 8, 1)]
        [InlineData("Example", -1, 7)]
        [InlineData("Example", 0, 8)]
        public void op_Substring_int_int_throwsArgumentOutOfRangeException(string text,
                                                                           int start,
                                                                           int length)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MutableString(text).Substring(start, length));
        }

        [Theory]
        [InlineData("Example", 8)]
        [InlineData("Example", -1)]
        public void op_Substring_int_throwsArgumentOutOfRangeException(string text,
                                                                       int start)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MutableString(text).Substring(start));
        }

        [Theory]
        [InlineData("", "Example", "")]
        [InlineData("", "Example", "test")]
        [InlineData("", "An Example", "Example")]
        [InlineData(" Example", "An Example", "An")]
        public void op_Suffix_string(string expected,
                                     string text,
                                     string value)
        {
            Assert.Equal(expected, new MutableString(text).Suffix(value));
        }

        [Fact]
        public void op_Suffix_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new MutableString().Suffix(null));
        }

        [Fact]
        public void op_Suffix_stringNull_bool()
        {
            Assert.Throws<ArgumentNullException>(() => new MutableString().Suffix(null, true));
        }

        [Theory]
        [InlineData("", "Example", "", false)]
        [InlineData("", "Example", "test", false)]
        [InlineData("", "Example", "Example", false)]
        [InlineData("", "An Example", "Example", false)]
        [InlineData(" Example", "An Example", "An", false)]
        [InlineData("", "Example", "", true)]
        [InlineData("", "Example", "test", true)]
        [InlineData("", "An Example", "Example", true)]
        [InlineData("Example", " An Example ", "An", true)]
        public void op_Suffix_string_bool(string expected,
                                          string text,
                                          string value,
                                          bool trim)
        {
            Assert.Equal(expected, new MutableString(text).Suffix(value, trim));
        }

        [Theory]
        [InlineData("Example", "An Example")]
        public void op_Suffix_string_bool_throwsArgumentOutOfRangeException(string text,
                                                                            string value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MutableString(text).Suffix(value, true));
        }

        [Theory]
        [InlineData("Example", "An Example")]
        public void op_Suffix_string_throwsArgumentOutOfRangeException(string text,
                                                                       string value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MutableString(text).Suffix(value));
        }

        [Fact]
        public void op_ToCharArray()
        {
            var expected = new[]
                               {
                                   'e', 'x', 'a', 'm', 'p', 'l', 'e'
                               };
            var actual = new MutableString("example").ToCharArray();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Example", 0, "Example")]
        [InlineData("Example", 2, "ample")]
        [InlineData("Example", 7, "")]
        public void op_ToCharArray_int(string text,
                                       int start,
                                       string result)
        {
            var expected = (result ?? string.Empty).ToCharArray();
            var actual = new MutableString(text).ToCharArray(start);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToCharArray_intNegative()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MutableString().ToCharArray(-1));
        }

        [Fact]
        public void op_ToCharArray_intNegative_int()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MutableString().ToCharArray(-1, 0));
        }

        [Fact]
        public void op_ToCharArray_intTooLong()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MutableString().ToCharArray(1));
        }

        [Fact]
        public void op_ToCharArray_intTooLong_int()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MutableString().ToCharArray(1, 1));
        }

        [Theory]
        [InlineData("Example", 0, 7, "Example")]
        [InlineData("Example", 2, 3, "amp")]
        [InlineData("Example", 7, 0, "")]
        public void op_ToCharArray_int_int(string text,
                                           int start,
                                           int length,
                                           string result)
        {
            var expected = (result ?? string.Empty).ToCharArray();
            var actual = new MutableString(text).ToCharArray(start, length);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToCharArray_int_intNegative()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MutableString().ToCharArray(0, -1));
        }

        [Fact]
        public void op_ToCharArray_int_intTooLong()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MutableString().ToCharArray(0, 1));
        }

        [Fact]
        public void op_ToCharArray_whenEmpty()
        {
            Assert.Empty(new MutableString().ToCharArray());
        }

        [Fact]
        public void op_ToString()
        {
            var expected = string.Empty;
            var actual = new MutableString().ToString();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("", "        ")]
        [InlineData(".", "    .   ")]
        [InlineData("Example", "Example")]
        [InlineData("Example", "Example ")]
        [InlineData("Example", "Example  ")]
        [InlineData("Example .", "Example . ")]
        [InlineData("Example", " Example")]
        [InlineData("Example", "  Example")]
        [InlineData(". Example", " . Example")]
        public void op_Trim(string expected,
                            string text)
        {
            var actual = new MutableString(text);

            Assert.Same(actual, actual.Trim());

            Assert.Equal(new MutableString(expected), actual);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("", "        ")]
        [InlineData(".", ".       ")]
        [InlineData(" Example", " Example")]
        [InlineData("Example", "Example")]
        [InlineData("Example", "Example ")]
        [InlineData("Example", "Example  ")]
        [InlineData("Example .", "Example . ")]
        public void op_TrimEnd(string expected,
                               string text)
        {
            var actual = new MutableString(text);

            Assert.Same(actual, actual.TrimEnd());

            Assert.Equal(new MutableString(expected), actual);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("", "        ")]
        [InlineData(".", "       .")]
        [InlineData("Example ", "Example ")]
        [InlineData("Example", "Example")]
        [InlineData("Example", " Example")]
        [InlineData("Example", "  Example")]
        [InlineData(". Example", " . Example")]
        public void op_TrimStart(string expected,
                                 string text)
        {
            var actual = new MutableString(text);

            Assert.Same(actual, actual.TrimStart());

            Assert.Equal(new MutableString(expected), actual);
        }

        [Fact]
        public void op_Truncate_int()
        {
            var expected = new MutableString("Ex");
            var actual = new MutableString("Example");

            Assert.Same(actual, actual.Truncate(2));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Truncate_intZero()
        {
            var expected = new MutableString();
            var actual = new MutableString("Example");

            Assert.Same(actual, actual.Truncate(0));

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", -1)]
        [InlineData("", 2)]
        public void op_Truncate_int_whenArgumentOutOfRangeException(string text,
                                                                    int start)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MutableString(text).Truncate(start));
        }

        [Theory]
        [InlineData("")]
        [InlineData("example")]
        [InlineData(" 123,  an  example. ")]
        [InlineData(" 123,  an  ex.ample ")]
        public void op_Words(string text)
        {
            var expected = text.RemoveAny(',', '.').Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var actual = new MutableString(text).Words().ToArray();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Capacity()
        {
            Assert.True(new PropertyExpectations<MutableString>(x => x.Capacity)
                            .IsAutoProperty(16)
                            .ArgumentOutOfRangeException(-1)
                            .Set(0)
                            .Set(123)
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Length()
        {
            Assert.True(new PropertyExpectations<MutableString>(x => x.Length)
                            .TypeIs<int>()
                            .DefaultValueIs(0)
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Length_get()
        {
            const int expected = 10;
            var actual = new MutableString("0123456789").Length;

            Assert.Equal(expected, actual);
        }
    }
}