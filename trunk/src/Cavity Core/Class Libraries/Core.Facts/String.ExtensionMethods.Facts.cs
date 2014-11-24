namespace Cavity
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Text;
    using System.Xml;
    using Cavity.IO;
    using Xunit;
    using Xunit.Extensions;

    public sealed class StringExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(StringExtensionMethods).IsStatic());
        }

        [Fact]
        public void op_Append_string_chars()
        {
            const string expected = "cat dog";
            var actual = "cat".Append(" dog");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Append_stringEmpty_chars2()
        {
            const string expected = "12";
            var actual = string.Empty.Append('1', '2');

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Append_stringEmpty_chars3()
        {
            const string expected = "123";
            var actual = string.Empty.Append('1', '2', '3');

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Append_stringEmpty_chars4()
        {
            const string expected = "1234";
            var actual = string.Empty.Append('1', '2', '3', '4');

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Append_stringEmpty_chars5()
        {
            const string expected = "12345";
            var actual = string.Empty.Append('1', '2', '3', '4', '5');

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Append_stringEmpty_chars6()
        {
            const string expected = "123456";
            var actual = string.Empty.Append('1', '2', '3', '4', '5', '6');

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Append_stringEmpty_chars7()
        {
            const string expected = "1234567";
            var actual = string.Empty.Append('1', '2', '3', '4', '5', '6', '7');

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Append_stringEmpty_chars8()
        {
            const string expected = "12345678";
            var actual = string.Empty.Append('1', '2', '3', '4', '5', '6', '7', '8');

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Append_stringEmpty_chars9()
        {
            const string expected = "123456789";
            var actual = string.Empty.Append('1', '2', '3', '4', '5', '6', '7', '8', '9');

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Append_stringEmpty_chars10()
        {
            const string expected = "1234567890";
            var actual = string.Empty.Append('1', '2', '3', '4', '5', '6', '7', '8', '9', '0');

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Append_stringNull_chars()
        {
            Assert.Throws<ArgumentNullException>(() => (null as string).Append('+'));
        }

        [Fact]
        public void op_Append_string_charsEmpty()
        {
            const string expected = "example";
            var actual = expected.Append(new List<char>().ToArray());

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Append_string_charsNull()
        {
            const string expected = "example";
            var actual = expected.Append(null as char[]);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Append_string_strings()
        {
            const string expected = "cat dog";
            var actual = "cat".Append(" dog");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Append_stringEmpty_strings2()
        {
            const string expected = "12";
            var actual = string.Empty.Append("1", "2");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Append_stringEmpty_strings3()
        {
            const string expected = "123";
            var actual = string.Empty.Append("1", "2", "3");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Append_stringEmpty_strings4()
        {
            const string expected = "1234";
            var actual = string.Empty.Append("1", "2", "3", "4");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Append_stringEmpty_strings5()
        {
            const string expected = "12345";
            var actual = string.Empty.Append("1", "2", "3", "4", "5");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Append_stringEmpty_strings6()
        {
            const string expected = "123456";
            var actual = string.Empty.Append("1", "2", "3", "4", "5", "6");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Append_stringEmpty_strings7()
        {
            const string expected = "1234567";
            var actual = string.Empty.Append("1", "2", "3", "4", "5", "6", "7");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Append_stringEmpty_strings8()
        {
            const string expected = "12345678";
            var actual = string.Empty.Append("1", "2", "3", "4", "5", "6", "7", "8");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Append_stringEmpty_strings9()
        {
            const string expected = "123456789";
            var actual = string.Empty.Append("1", "2", "3", "4", "5", "6", "7", "8", "9");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Append_stringEmpty_strings10()
        {
            const string expected = "1234567890";
            var actual = string.Empty.Append("1", "2", "3", "4", "5", "6", "7", "8", "9", "0");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Append_stringNull_strings()
        {
            Assert.Throws<ArgumentNullException>(() => (null as string).Append("example"));
        }

        [Fact]
        public void op_Append_string_stringEmpty()
        {
            const string expected = "example";
            var actual = expected.Append(string.Empty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Append_string_stringNull()
        {
            const string expected = "example";
            var actual = expected.Append(null as string);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Append_string_stringsEmpty()
        {
            const string expected = "example";
            var actual = expected.Append(new List<string>().ToArray());

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Append_string_stringsNull()
        {
            const string expected = "example";
            var actual = expected.Append(null as string[]);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Contains_stringEmpty_char()
        {
            Assert.False(string.Empty.Contains('b'));
        }

        [Fact]
        public void op_Contains_stringNull_char()
        {
            Assert.Throws<ArgumentNullException>(() => (null as string).Contains('b'));
        }

        [Fact]
        public void op_Contains_string_char()
        {
            Assert.True("abc".Contains('b'));
        }

        [Fact]
        public void op_Contains_stringEmpty_string_StringComparison()
        {
            Assert.False(string.Empty.Contains("example", StringComparison.Ordinal));
        }

        [Fact]
        public void op_Contains_stringNull_string_StringComparison()
        {
            Assert.Throws<ArgumentNullException>(() => (null as string).Contains("example", StringComparison.Ordinal));
        }

        [Fact]
        public void op_Contains_string_string_StringComparison()
        {
            Assert.True("abc".Contains("B", StringComparison.OrdinalIgnoreCase));
        }

        [Fact]
        public void op_ContainsAny_stringEmpty_StringComparison_chars()
        {
            Assert.False(string.Empty.ContainsAny('a', 'b', 'c'));
        }

        [Fact]
        public void op_ContainsAny_stringNull_StringComparison_chars()
        {
            Assert.False((null as string).ContainsAny('a'));
        }

        [Fact]
        public void op_ContainsAny_string_StringComparison_chars()
        {
            Assert.True("example".ContainsAny('a'));
        }

        [Fact]
        public void op_ContainsAny_string_StringComparison_charsEmpty()
        {
            Assert.False("example".ContainsAny());
        }

        [Fact]
        public void op_ContainsAny_string_StringComparison_charsNull()
        {
            Assert.False("example".ContainsAny(null as char[]));
        }

        [Fact]
        public void op_ContainsAny_stringEmpty_StringComparison_strings()
        {
            Assert.False(string.Empty.ContainsAny(StringComparison.Ordinal, "abc", "xyz"));
        }

        [Fact]
        public void op_ContainsAny_stringNull_StringComparison_strings()
        {
            Assert.False((null as string).ContainsAny(StringComparison.Ordinal, "example"));
        }

        [Fact]
        public void op_ContainsAny_string_StringComparison_strings()
        {
            Assert.True("example".ContainsAny(StringComparison.OrdinalIgnoreCase, "A"));
        }

        [Fact]
        public void op_ContainsAny_string_StringComparison_stringsEmpty()
        {
            Assert.False("example".ContainsAny(StringComparison.Ordinal));
        }

        [Fact]
        public void op_ContainsAny_string_StringComparison_stringsNull()
        {
            Assert.False("example".ContainsAny(StringComparison.Ordinal, null as string[]));
        }

        [Theory]
        [InlineData(false, null)]
        [InlineData(false, "")]
        [InlineData(false, "example")]
        [InlineData(true, "example 123")]
        [InlineData(true, "123 example")]
        public void op_ContainsDigits_string(bool expected,
                                             string value)
        {
            var actual = value.ContainsDigits();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(false, null)]
        [InlineData(false, "")]
        [InlineData(false, "  ")]
        [InlineData(true, "example")]
        public void op_ContainsText_string(bool expected,
                                           string value)
        {
            var actual = value.ContainsText();

            Assert.Equal(expected, actual);
        }

#if !NET20
        [Fact]
        public void op_DefaultOrFromString_stringNull_Func()
        {
            const decimal expected = 0m;
            var actual = (null as string).DefaultOrFromString(XmlConvert.ToDecimal);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_DefaultOrFromString_string_Func()
        {
            const decimal expected = 123.45m;
            var actual = "123.45".DefaultOrFromString(XmlConvert.ToDecimal);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_DefaultOrFromString_string_FuncNull()
        {
            Assert.Throws<ArgumentNullException>(() => "123.45".DefaultOrFromString<decimal>(null));
        }

#endif

        [Fact]
        public void op_EndsWithAny_stringEmpty_StringComparison_strings()
        {
            Assert.False(string.Empty.EndsWithAny(StringComparison.Ordinal, "cat"));
        }

        [Fact]
        public void op_EndsWithAny_stringNull_StringComparison_strings()
        {
            Assert.False((null as string).EndsWithAny(StringComparison.Ordinal, "cat"));
        }

        [Fact]
        public void op_EndsWithAny_string_StringComparison_strings()
        {
            Assert.True("cat dog".EndsWithAny(StringComparison.Ordinal, " dog"));
        }

        [Fact]
        public void op_EndsWithAny_string_StringComparison_stringsEmpty()
        {
            Assert.False("cat".EndsWithAny(StringComparison.Ordinal));
        }

        [Fact]
        public void op_EndsWithAny_string_StringComparison_stringsNull()
        {
            Assert.False("cat".EndsWithAny(StringComparison.Ordinal, null as string[]));
        }

        [Fact]
        public void op_EqualsAny_stringEmpty_StringComparison_strings()
        {
            Assert.False(string.Empty.EqualsAny(StringComparison.Ordinal, "cat"));
        }

        [Fact]
        public void op_EqualsAny_stringNull_StringComparison_strings()
        {
            Assert.False((null as string).EqualsAny(StringComparison.Ordinal, "cat"));
        }

        [Fact]
        public void op_EqualsAny_string_StringComparison_strings()
        {
            Assert.True("dog".EqualsAny(StringComparison.Ordinal, "cat", "dog"));
        }

        [Fact]
        public void op_EqualsAny_string_StringComparison_stringsEmpty()
        {
            Assert.False("cat".EqualsAny(StringComparison.Ordinal));
        }

        [Fact]
        public void op_EqualsAny_string_StringComparison_stringsNull()
        {
            Assert.False("cat".EqualsAny(StringComparison.Ordinal, null as string[]));
        }

        [Theory]
        [InlineData(true, null, null)]
        [InlineData(false, null, "")]
        [InlineData(false, "", null)]
        [InlineData(true, "", "")]
        [InlineData(false, "", "  ")]
        [InlineData(true, "example", "EXAMPLE")]
        [InlineData(false, "example", " EXAMPLE ")]
        public void op_EqualsOrdinalIgnoreCase_string_string(bool expected,
                                                             string value,
                                                             string comparand)
        {
            var actual = value.EqualsOrdinalIgnoreCase(comparand);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FormatWith_stringEmpty_objects()
        {
            var expected = string.Empty;
            var actual = string.Empty.FormatWith(123);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FormatWith_stringEmpty_objectsNull()
        {
            var expected = string.Empty;
            var actual = string.Empty.FormatWith();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FormatWith_stringNull_objects()
        {
            Assert.Throws<ArgumentNullException>(() => (null as string).FormatWith(123));
        }

        [Fact]
        public void op_FormatWith_string_objects()
        {
            const string expected = "abc";
            var actual = "a{0}c".FormatWith('b');

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_FormatWith_string_objectsNull()
        {
            Assert.Throws<FormatException>(() => "a{0}c".FormatWith());
        }

        [Theory]
        [InlineData(false, null)]
        [InlineData(false, "")]
        [InlineData(false, "example")]
        [InlineData(false, "1999 12")]
        [InlineData(false, "2010-00")]
        [InlineData(true, "2010-01")]
        [InlineData(true, "2010-02")]
        [InlineData(true, "2010-03")]
        [InlineData(true, "2010-04")]
        [InlineData(true, "2010-05")]
        [InlineData(true, "2010-06")]
        [InlineData(true, "2010-07")]
        [InlineData(true, "2010-08")]
        [InlineData(true, "2010-09")]
        [InlineData(true, "2010-10")]
        [InlineData(true, "2010-11")]
        [InlineData(true, "2010-12")]
        [InlineData(false, "2010-13")]
        [InlineData(false, "a010-11")]
        [InlineData(false, "2a10-11")]
        [InlineData(false, "20a0-11")]
        [InlineData(false, "201a-11")]
        [InlineData(false, "2010-a1")]
        [InlineData(false, "2010-1a")]
        public void op_IsMonth_string(bool expected,
                                      string value)
        {
            var actual = value.IsMonth();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(false, null)]
        [InlineData(false, "")]
        [InlineData(true, "  ")]
        [InlineData(true, "example")]
        public void op_IsNotNullOrEmpty_string(bool expected,
                                               string value)
        {
            var actual = value.IsNotNullOrEmpty();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(false, null)]
        [InlineData(false, "")]
        [InlineData(false, "  ")]
        [InlineData(true, "example")]
        public void op_IsNotNullOrWhiteSpace_string(bool expected,
                                                    string value)
        {
            var actual = value.IsNotNullOrWhiteSpace();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(true, null)]
        [InlineData(true, "")]
        [InlineData(false, "  ")]
        [InlineData(false, "example")]
        public void op_IsNullOrEmpty_string(bool expected,
                                            string value)
        {
            var actual = value.IsNullOrEmpty();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(true, null)]
        [InlineData(true, "")]
        [InlineData(true, "  ")]
        [InlineData(false, "example")]
        public void op_IsNullOrWhiteSpace_string(bool expected,
                                                 string value)
        {
            var actual = value.IsNullOrWhiteSpace();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(' ', null)]
        [InlineData(' ', "")]
        [InlineData(' ', "  ")]
        [InlineData('e', "example")]
        public void op_LastCharacter_string(char expected,
                                            string value)
        {
            var actual = value.LastCharacter();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_LevenshteinDistance_stringABC_stringA2C()
        {
            const int expected = 1;
            var actual = "ABC".LevenshteinDistance("A2C");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_LevenshteinDistance_stringA_stringNull()
        {
            const int expected = 1;
            var actual = "A".LevenshteinDistance(null);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_LevenshteinDistance_stringA_stringZ()
        {
            const int expected = 1;
            var actual = "A".LevenshteinDistance("Z");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_LevenshteinDistance_stringAnt_stringAunt()
        {
            const int expected = 1;
            var actual = "Ant".LevenshteinDistance("Aunt");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_LevenshteinDistance_stringAunt_stringAnt()
        {
            const int expected = 1;
            var actual = "Aunt".LevenshteinDistance("Ant");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_LevenshteinDistance_stringEmpty_string()
        {
            const int expected = 1;
            var actual = string.Empty.LevenshteinDistance("Z");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_LevenshteinDistance_stringEmpty_stringEmpty()
        {
            const int expected = 0;
            var actual = string.Empty.LevenshteinDistance(string.Empty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_LevenshteinDistance_stringFoo_stringBar()
        {
            const int expected = 3;
            var actual = "Foo".LevenshteinDistance("Bar");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Caverphone_stringNull()
        {
            Assert.Null((null as string).Caverphone());
        }

        [Fact]
        public void op_Caverphone_stringEmpty()
        {
            var expected = string.Empty;
            var actual = expected.Caverphone();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Caverphone_string_whenLee()
        {
            const string expected = "L11111";
            var actual = "Lee".Caverphone();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Caverphone_string_whenAnd()
        {
            const string expected = "aNT111";
            var actual = "And".Caverphone();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Caverphone_string_whenMohawk()
        {
            const string expected = "aL1111";
            var actual = "hello".Caverphone();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Caverphone_string_whenFlack()
        {
            const string expected = "FLK111";
            var actual = "Flack".Caverphone();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Caverphone_string_whenJazz()
        {
            const string expected = "YS1111";
            var actual = "Jazz".Caverphone();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Caverphone_string_whenThompson()
        {
            const string expected = "TMPSN1";
            var actual = "Thompson".Caverphone();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CaverphoneStart_stringEmpty()
        {
            var expected = string.Empty;
            var actual = StringExtensionMethods.CaverphoneStart(expected);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("cou2f", "cough")]
        [InlineData("rou2f", "rough")]
        [InlineData("tou2f", "tough")]
        [InlineData("enou2f", "enough")]
        [InlineData("2nocchi", "gnocchi")]
        public void op_CaverphoneStart_stringMappings(string expected,
                                                      string value)
        {
            var actual = StringExtensionMethods.CaverphoneStart(value);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("a", "a")]
        [InlineData("example", "example")]
        [InlineData("plum2", "plumb")]
        [InlineData("plums", "plums")]
        public void op_CaverphoneEndings_StringBuilder(string expected,
                                                       string value)
        {
            var buffer = new StringBuilder(value);

            StringExtensionMethods.CaverphoneEndings(buffer);

            var actual = buffer.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CaverphoneEndings_StringBuilderNull()
        {
            Assert.Throws<ArgumentNullException>(() => StringExtensionMethods.CaverphoneEndings(null));
        }

        [Fact]
        public void op_MetaphoneFirstLetters_StringBuilderNull()
        {
            Assert.Throws<ArgumentNullException>(() => StringExtensionMethods.MetaphoneFirstLetters(null));
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("a", "a")]
        [InlineData("Smas", "Xmas")]
        [InlineData("Esop", "AEsop")]
        [InlineData("Nocchi", "GNocchi")]
        [InlineData("Now", "KNow")]
        [InlineData("Neumatic", "PNeumatic")]
        [InlineData("Rong", "WRong")]
        public void op_MetaphoneFirstLetters_StringBuilder(string expected,
                                                           string value)
        {
            var buffer = new StringBuilder(value);

            StringExtensionMethods.MetaphoneFirstLetters(buffer);

            var actual = buffer.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Bint", Justification = "The casing is correct.")]
        public void op_MetaphoneLetterB_int_StringBuilderNull()
        {
            Assert.Throws<ArgumentNullException>(() => StringExtensionMethods.MetaphoneLetterB(1, null));
        }

        [Theory]
        [InlineData("pluM ", 4, "pluMB")]
        [InlineData("pluMBer", 4, "pluMBer")]
        [InlineData("boM E", 3, "boMBE")]
        [InlineData("cruMBs", 4, "cruMBs")]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "Bint", Justification = "The casing is correct.")]
        public void op_MetaphoneLetterB_int_StringBuilder(string expected,
                                                          int index,
                                                          string value)
        {
            var buffer = new StringBuilder(value);

            StringExtensionMethods.MetaphoneLetterB(index, buffer);

            var actual = buffer.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_MetaphoneLetterC_int_StringBuilderNull()
        {
            Assert.Throws<ArgumentNullException>(() => StringExtensionMethods.MetaphoneLetterC(1, null));
        }

        [Fact]
        public void op_MetaphoneLetterC_intMax_StringBuilderNull()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => StringExtensionMethods.MetaphoneLetterC(int.MaxValue, new StringBuilder()));
        }

        [Fact]
        public void op_MetaphoneLetterC_intMin_StringBuilderNull()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => StringExtensionMethods.MetaphoneLetterC(int.MinValue, new StringBuilder()));
        }

        [Theory]
        [InlineData("Lanx  ", 3, "LanCIA")]
        public void op_MetaphoneLetterC_int_StringBuilder(string expected,
                                                          int index,
                                                          string value)
        {
            var buffer = new StringBuilder(value);

            StringExtensionMethods.MetaphoneLetterC(index, buffer);

            var actual = buffer.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_MetaphoneLetterG_int_StringBuilderNull()
        {
            Assert.Throws<ArgumentNullException>(() => StringExtensionMethods.MetaphoneLetterG(1, null));
        }

        [Fact]
        public void op_MetaphoneLetterG_intMax_StringBuilderNull()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => StringExtensionMethods.MetaphoneLetterG(int.MaxValue, new StringBuilder()));
        }

        [Theory]
        [InlineData("dou Hnut", 3, "douGHnut")]
        [InlineData("ali N", 3, "aliGN")]
        [InlineData("ali NED", 3, "aliGNED")]
        public void op_MetaphoneLetterG_int_StringBuilder(string expected,
                                                          int index,
                                                          string value)
        {
            var buffer = new StringBuilder(value);

            StringExtensionMethods.MetaphoneLetterG(index, buffer);

            var actual = buffer.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_MetaphoneEnd_StringBuilderNull()
        {
            Assert.Throws<ArgumentNullException>(() => StringExtensionMethods.MetaphoneEnd(null));
        }

        [Theory]
        [InlineData("", "")]
        public void op_MetaphoneEnd_StringBuilder(string expected,
                                                  string value)
        {
            var buffer = new StringBuilder(value);

            StringExtensionMethods.MetaphoneEnd(buffer);

            var actual = buffer.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_CaverphoneStart_stringNull()
        {
            Assert.Null(StringExtensionMethods.CaverphoneStart(null));
        }

        [Fact]
        public void op_Metaphone_stringNull()
        {
            Assert.Null((null as string).Metaphone());
        }

        [Fact]
        public void op_Metaphone_stringEmpty()
        {
            var expected = string.Empty;
            var actual = expected.Metaphone();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Metaphone_string()
        {
            const string expected = "EKSMPL";
            var actual = "Example".Metaphone();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Metaphone_string_whenSingleCharacter()
        {
            foreach (var c in "abcdefghijklmnopqrstuvwxyz")
            {
                Assert.NotNull(XmlConvert.ToString(c).Metaphone());
            }
        }

        [Fact]
        public void op_Metaphone_string_whenSame()
        {
            var expected = 0;
            var actual = 0;
            foreach (var line in new FileInfo("metaphone.txt").Lines())
            {
                expected++;
                var parts = line.Split('\t');

                var metaphone = parts[0].Metaphone();
                if (metaphone == parts[1])
                {
                    actual++;
                }
                else
                {
                    Trace.WriteLine("{0}\t{1}".FormatWith(line, metaphone));
                }
            }

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_LevenshteinDistance_stringNull_stringA()
        {
            const int expected = 1;
            var actual = (null as string).LevenshteinDistance("A");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_LevenshteinDistance_stringNull_stringNull()
        {
            const int expected = 0;
            var actual = (null as string).LevenshteinDistance(null);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_LevenshteinDistance_string_stringEmpty()
        {
            const int expected = 1;
            var actual = "A".LevenshteinDistance(string.Empty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_NormalizeWhiteSpace_string()
        {
            var expected = new string(' ', Characters.WhiteSpace.Count);
            var actual = string.Concat(
                                       '\u0009',
                                       // HT (Horizontal Tab)
                                       '\u000A',
                                       // LF (Line Feed)
                                       '\u000B',
                                       // VT (Vertical Tab)
                                       '\u000C',
                                       // FF (Form Feed)
                                       '\u000D',
                                       // CR (Carriage Return)
                                       '\u0020',
                                       // Space
                                       '\u0085',
                                       // NEL (control character next line)
                                       '\u00A0',
                                       // No-Break Space
                                       '\u1680',
                                       // Ogham Space Mark
                                       '\u180E',
                                       // Mongolian Vowel Separator
                                       '\u2000',
                                       // En quad
                                       '\u2001',
                                       // Em quad
                                       '\u2002',
                                       // En Space
                                       '\u2003',
                                       // Em Space
                                       '\u2004',
                                       // Three-Per-Em Space
                                       '\u2005',
                                       // Four-Per-Em Space
                                       '\u2006',
                                       // Six-Per-Em Space
                                       '\u2007',
                                       // Figure Space
                                       '\u2008',
                                       // Punctuation Space
                                       '\u2009',
                                       // Thin Space
                                       '\u200A',
                                       // Hair Space
                                       '\u200B',
                                       // Zero Width Space
                                       '\u200C',
                                       // Zero Width Non Joiner
                                       '\u200D',
                                       // Zero Width Joiner
                                       '\u2028',
                                       // Line Separator
                                       '\u2029',
                                       // Paragraph Separator
                                       '\u202F',
                                       // Narrow No-Break Space
                                       '\u205F',
                                       // Medium Mathematical Space
                                       '\u2060',
                                       // Word Joiner
                                       '\u3000',
                                       // Ideographic Space
                                       '\uFEFF',
                                       //// Zero Width No-Break Space
                                       '·').NormalizeWhiteSpace();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_NormalizeWhiteSpace_stringEmpty()
        {
            var expected = string.Empty;
            var actual = expected.NormalizeWhiteSpace();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_NormalizeWhiteSpace_stringNull()
        {
            Assert.Null((null as string).NormalizeWhiteSpace());
        }

        [Fact]
        public void op_RemoveAnyFractions_string()
        {
            const string expected = "00";
            var actual = "0½⅓¼⅕⅙⅛⅔⅖¾⅗⅜⅘⅚⅝⅞⁄0".RemoveAnyFractions();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_RemoveAnyFractions_stringEmpty()
        {
            var expected = string.Empty;
            var actual = expected.RemoveAnyFractions();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_RemoveAnyFractions_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as string).RemoveAnyFractions());
        }

        [Fact]
        public void op_RemoveAnyCurrencySymbols_string()
        {
            const string expected = "__";
            var actual = "_¤₳฿₵¢₡₢₠$₫৳₯€ƒ₣₲₴₭ℳ₥₦₧₱₰£₹₨₪₸₮₩¥៛_".RemoveAnyCurrencySymbols();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_RemoveAnyCurrencySymbols_stringEmpty()
        {
            var expected = string.Empty;
            var actual = expected.RemoveAnyCurrencySymbols();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_RemoveAnyCurrencySymbols_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as string).RemoveAnyCurrencySymbols());
        }

        [Fact]
        public void op_RemoveAnyDigits_string()
        {
            const string expected = "__";
            var actual = "_0123456789_".RemoveAnyDigits();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_RemoveAnyDigits_stringEmpty()
        {
            var expected = string.Empty;
            var actual = expected.RemoveAnyDigits();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_RemoveAnyDigits_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as string).RemoveAnyDigits());
        }

        [Fact]
        public void op_RemoveAnyMathematicalSymbols_string()
        {
            const string expected = "__";
            var actual = "_∞%‰‱+−-±∓×÷⁄∣∤=≠<>≪≫≺∝⊗′″‴√#ℵℶ𝔠:!~≈≀◅▻⋉⋈∴∵■□∎▮‣⇒→⊃⇔↔¬˜∧∨⊕⊻∀∃≜≝≐≅≡{}∅∈∉⊆⊂⊇⊃∪∩∆∖→↦∘ℕNℤZℙPℚQℝRℂCℍHO∑∏∐Δδ∂∇′•∫∮πσ†T⊤⊥⊧⊢o_".RemoveAnyMathematicalSymbols();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_RemoveAnyMathematicalSymbols_stringEmpty()
        {
            var expected = string.Empty;
            var actual = expected.RemoveAnyMathematicalSymbols();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_RemoveAnyMathematicalSymbols_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as string).RemoveAnyMathematicalSymbols());
        }

        [Fact]
        public void op_RemoveAnyPunctuation_string()
        {
            const string expected = "00";
            var actual = "0„“”‘’'\"‐‒–—―…!?.:;,[](){}⟨⟩«»/⁄0".RemoveAnyPunctuation();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_RemoveAnyPunctuation_stringEmpty()
        {
            var expected = string.Empty;
            var actual = expected.RemoveAnyPunctuation();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_RemoveAnyPunctuation_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as string).RemoveAnyPunctuation());
        }

        [Fact]
        public void op_RemoveAnyTypography_string()
        {
            const string expected = "0";
            var actual = "0&@*\\•^†‡°〃¡¿#№ºª¶§~_¦|©®℗℠™⁂⊤⊥☞∴∵‽؟◊※⁀".RemoveAnyTypography();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_RemoveAnyTypography_stringEmpty()
        {
            var expected = string.Empty;
            var actual = expected.RemoveAnyTypography();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_RemoveAnyTypography_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as string).RemoveAnyTypography());
        }

        [Fact]
        public void op_RemoveAnyWhiteSpace_string()
        {
            const string expected = "example";
            var actual = "e x a m p l e·".RemoveAnyWhiteSpace();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_RemoveAnyWhiteSpace_stringEmpty()
        {
            var expected = string.Empty;
            var actual = expected.RemoveAnyWhiteSpace();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_RemoveAnyWhiteSpace_stringNull()
        {
            Assert.Null((null as string).RemoveAnyWhiteSpace());
        }

        [Fact]
        public void op_RemoveAnyWhiteSpace_stringWhiteSpace()
        {
            var expected = string.Empty;
            var actual = string.Concat(
                                       '\u0009',
                                       // HT (Horizontal Tab)
                                       '\u000A',
                                       // LF (Line Feed)
                                       '\u000B',
                                       // VT (Vertical Tab)
                                       '\u000C',
                                       // FF (Form Feed)
                                       '\u000D',
                                       // CR (Carriage Return)
                                       '\u0020',
                                       // Space
                                       '\u0085',
                                       // NEL (control character next line)
                                       '\u00A0',
                                       // No-Break Space
                                       '\u1680',
                                       // Ogham Space Mark
                                       '\u180E',
                                       // Mongolian Vowel Separator
                                       '\u2000',
                                       // En quad
                                       '\u2001',
                                       // Em quad
                                       '\u2002',
                                       // En Space
                                       '\u2003',
                                       // Em Space
                                       '\u2004',
                                       // Three-Per-Em Space
                                       '\u2005',
                                       // Four-Per-Em Space
                                       '\u2006',
                                       // Six-Per-Em Space
                                       '\u2007',
                                       // Figure Space
                                       '\u2008',
                                       // Punctuation Space
                                       '\u2009',
                                       // Thin Space
                                       '\u200A',
                                       // Hair Space
                                       '\u200B',
                                       // Zero Width Space
                                       '\u200C',
                                       // Zero Width Non Joiner
                                       '\u200D',
                                       // Zero Width Joiner
                                       '\u2028',
                                       // Line Separator
                                       '\u2029',
                                       // Paragraph Separator
                                       '\u202F',
                                       // Narrow No-Break Space
                                       '\u205F',
                                       // Medium Mathematical Space
                                       '\u2060',
                                       // Word Joiner
                                       '\u3000',
                                       // Ideographic Space
                                       '\uFEFF').RemoveAnyWhiteSpace();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_RemoveAny_stringEmpty_chars()
        {
            var expected = string.Empty;
            var actual = expected.RemoveAny('.');

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_RemoveAny_stringNull_chars()
        {
            Assert.Throws<ArgumentNullException>(() => (null as string).RemoveAny('.'));
        }

        [Fact]
        public void op_RemoveAny_string_chars()
        {
            const string expected = "abc";
            var actual = "a.b,c".RemoveAny('.', ',');

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_RemoveAny_string_charsEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => "abc".RemoveAny());
        }

        [Fact]
        public void op_RemoveAny_string_charsNull()
        {
            Assert.Throws<ArgumentNullException>(() => "abc".RemoveAny(null as char[]));
        }

        [Fact]
        public void op_ReplaceBeginning_string_string_StringComparison_strings()
        {
            const string expected = "Success Test";
            var actual = "Example Test".ReplaceBeginning("Success ", StringComparison.Ordinal, "xxx", "Example ");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReplaceBeginning_string_string_StringComparison_strings_whenNoMatch()
        {
            const string expected = "Example";
            var actual = expected.ReplaceBeginning("xxx", StringComparison.Ordinal, "yyy");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReplaceBeginning_stringEmpty_string_StringComparison_strings_whenStringNullOrEmpty()
        {
            var expected = string.Empty;
            var actual = expected.ReplaceBeginning("xxx", StringComparison.Ordinal, string.Empty, null);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReplaceBeginning_stringNull_string_StringComparison_strings()
        {
            Assert.Null((null as string).ReplaceBeginning("xxx", StringComparison.Ordinal, "yyy"));
        }

        [Fact]
        public void op_ReplaceBeginning_stringEmpty_string_StringComparison_strings()
        {
            var expected = string.Empty;
            var actual = expected.ReplaceBeginning("xxx", StringComparison.Ordinal, "yyy");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReplaceBeginning_string_stringNull_StringComparison_strings()
        {
            Assert.Throws<ArgumentNullException>(() => "Example".ReplaceBeginning(null, StringComparison.Ordinal, "yyy"));
        }

        [Fact]
        public void op_ReplaceBeginning_string_string_StringComparison_strings_whenStringNullOrEmpty()
        {
            const string expected = "Example";
            var actual = expected.ReplaceBeginning("xxx", StringComparison.Ordinal, string.Empty, null);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReplaceBeginning_string_string_StringComparison_stringsNull()
        {
            Assert.Throws<ArgumentNullException>(() => "Example".ReplaceBeginning("xxx", StringComparison.Ordinal, null as string[]));
        }

        [Fact]
        public void op_ReplaceEnding_stringNull_string_StringComparison_strings()
        {
            Assert.Null((null as string).ReplaceEnding("xxx", StringComparison.Ordinal, "yyy"));
        }

        [Fact]
        public void op_ReplaceEnding_stringEmpty_string_StringComparison_strings()
        {
            var expected = string.Empty;
            var actual = expected.ReplaceEnding("xxx", StringComparison.Ordinal, "yyy");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReplaceEnding_string_stringNull_StringComparison_strings()
        {
            Assert.Throws<ArgumentNullException>(() => "Example".ReplaceEnding(null, StringComparison.Ordinal, "yyy"));
        }

        [Fact]
        public void op_ReplaceEnding_string_string_StringComparison_strings()
        {
            const string expected = "Test Success";
            var actual = "Test Example".ReplaceEnding(" Success", StringComparison.Ordinal, "xxx", " Example");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReplaceEnding_string_string_StringComparison_strings_whenNoMatch()
        {
            const string expected = "Example";
            var actual = expected.ReplaceEnding("xxx", StringComparison.Ordinal, "yyy");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReplaceEnding_string_string_StringComparison_strings_whenStringNullOrEmpty()
        {
            const string expected = "Example";
            var actual = expected.ReplaceEnding("xxx", StringComparison.Ordinal, string.Empty, null);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReplaceEnding_string_string_StringComparison_stringsNull()
        {
            Assert.Throws<ArgumentNullException>(() => "Example".ReplaceEnding("xxx", StringComparison.Ordinal, null as string[]));
        }

        [Fact]
        public void op_RemoveDefiniteArticle_string()
        {
            const string expected = " Example";
            var actual = "The Example".RemoveDefiniteArticle();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_RemoveDefiniteArticle_stringEmpty()
        {
            var expected = string.Empty;
            var actual = expected.RemoveDefiniteArticle();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_RemoveDefiniteArticle_stringNull()
        {
            Assert.Null((null as string).RemoveDefiniteArticle());
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        [InlineData("  ", " ")]
        [InlineData("   ", " ")]
        [InlineData(" a ", " a ")]
        [InlineData("  a  ", " a ")]
        [InlineData(" a b ", " a b ")]
        [InlineData(" a  b ", " a b ")]
        [InlineData(" a   b ", " a b ")]
        public void op_RemoveDoubleSpacing_string(string value,
                                                  string expected)
        {
            var actual = value.RemoveDoubleSpacing();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_RemoveFromEnd_stringEmpty_string_StringComparison()
        {
            var expected = string.Empty;
            var actual = expected.RemoveFromEnd("example", StringComparison.Ordinal);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_RemoveFromEnd_stringNull_string_StringComparison()
        {
            Assert.Null((null as string).RemoveFromEnd("example", StringComparison.Ordinal));
        }

        [Fact]
        public void op_RemoveFromEnd_string_stringEmpty_StringComparison()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => "example".RemoveFromEnd(string.Empty, StringComparison.Ordinal));
        }

        [Fact]
        public void op_RemoveFromEnd_string_stringNull_StringComparison()
        {
            Assert.Throws<ArgumentNullException>(() => "example".RemoveFromEnd(null, StringComparison.Ordinal));
        }

        [Fact]
        public void op_RemoveFromEnd_string_string_StringComparison()
        {
            const string expected = "Ex";
            var actual = "Example".RemoveFromEnd("ample", StringComparison.OrdinalIgnoreCase);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_RemoveFromStart_stringEmpty_string_StringComparison()
        {
            var expected = string.Empty;
            var actual = expected.RemoveFromStart("example", StringComparison.Ordinal);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_RemoveFromStart_stringNull_string_StringComparison()
        {
            Assert.Null((null as string).RemoveFromStart("example", StringComparison.Ordinal));
        }

        [Fact]
        public void op_RemoveFromStart_string_stringEmpty_StringComparison()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => "example".RemoveFromStart(string.Empty, StringComparison.Ordinal));
        }

        [Fact]
        public void op_RemoveFromStart_string_stringNull_StringComparison()
        {
            Assert.Throws<ArgumentNullException>(() => "example".RemoveFromStart(null, StringComparison.Ordinal));
        }

        [Fact]
        public void op_RemoveFromStart_string_string_StringComparison()
        {
            const string expected = "ample";
            var actual = "Example".RemoveFromStart("ex", StringComparison.OrdinalIgnoreCase);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_RemoveIllegalFileCharacters_stringNull()
        {
            Assert.Null((null as string).RemoveIllegalFileCharacters());
        }

        [Fact]
        public void op_RemoveIllegalFileCharacters_stringEmpty()
        {
            var expected = string.Empty;
            var actual = expected.RemoveIllegalFileCharacters();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_RemoveIllegalFileCharacters_string()
        {
            foreach (var c in new[]
                                  {
                                      '\\', '/', ':', '*', '?', '"', '<', '>', '|', (char)31
                                  })
            {
                const string expected = "example";
                var actual = expected.Append(c).RemoveIllegalFileCharacters();

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_ReplaceAllWith_stringEmpty_string_StringComparison_strings()
        {
            var expected = string.Empty;
            var actual = expected.ReplaceAllWith("-", StringComparison.OrdinalIgnoreCase, "a", "B", "c");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReplaceAllWith_stringNull_string_StringComparison_strings()
        {
            Assert.Null((null as string).ReplaceAllWith("-", StringComparison.OrdinalIgnoreCase, "a", "B", "c"));
        }

        [Fact]
        public void op_ReplaceAllWith_string_stringEmpty_StringComparison_strings()
        {
            const string expected = "Example";
            var actual = "-E-x-a-m-p-l-e-".ReplaceAllWith(string.Empty, StringComparison.OrdinalIgnoreCase, "-");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReplaceAllWith_string_stringNull_StringComparison_strings()
        {
            Assert.Throws<ArgumentNullException>(() => "Example".ReplaceAllWith(null, StringComparison.OrdinalIgnoreCase, "a", "B", "c"));
        }

        [Fact]
        public void op_ReplaceAllWith_string_string_StringComparison_strings()
        {
            const string expected = "X---Z";
            var actual = "XaBcZ".ReplaceAllWith("-", StringComparison.OrdinalIgnoreCase, "a", "B", "c");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReplaceAllWith_string_string_StringComparison_stringsEmpty()
        {
            const string expected = "Example";
            var actual = "Example".ReplaceAllWith("-", StringComparison.OrdinalIgnoreCase);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ReplaceAllWith_string_string_StringComparison_stringsNull()
        {
            Assert.Throws<ArgumentNullException>(() => "Example".ReplaceAllWith("-", StringComparison.OrdinalIgnoreCase, null as string[]));
        }

        [Fact]
        public void op_Replace_stringEmpty_string_string_StringComparison()
        {
            var expected = string.Empty;
            var actual = string.Empty.Replace("old", "new", StringComparison.Ordinal);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Replace_stringNull_string_string_StringComparison()
        {
            Assert.Throws<ArgumentNullException>(() => (null as string).Replace("old", "new", StringComparison.Ordinal));
        }

        [Fact]
        public void op_Replace_string_stringEmpty_string_StringComparison()
        {
            const string expected = "example";
            var actual = "example".Replace(string.Empty, "new", StringComparison.OrdinalIgnoreCase);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Replace_string_stringNull_string_StringComparison()
        {
            Assert.Throws<ArgumentNullException>(() => "example".Replace(null, "new", StringComparison.Ordinal));
        }

        [Fact]
        public void op_Replace_string_string_stringEmpty_StringComparison()
        {
            const string expected = "example";
            var actual = "example".Replace("old", string.Empty, StringComparison.OrdinalIgnoreCase);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Replace_string_string_stringEmpty_StringComparison_whenMultiple()
        {
            const string expected = "abc";
            var actual = "_a_b_c_".Replace("_", string.Empty, StringComparison.OrdinalIgnoreCase);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Replace_string_string_stringNull_StringComparison()
        {
            const string expected = "example";
            var actual = "example".Replace("old", null, StringComparison.OrdinalIgnoreCase);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Replace_string_string_string_StringComparison_whenEmbedded()
        {
            const string expected = "abc";
            var actual = "aXYZc".Replace("xyz", "b", StringComparison.OrdinalIgnoreCase);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Replace_string_string_string_StringComparison_whenEnd()
        {
            const string expected = "abc";
            var actual = "abXYZ".Replace("xyz", "c", StringComparison.OrdinalIgnoreCase);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Replace_string_string_string_StringComparison_whenMultiple()
        {
            const string expected = ".a.b.c.";
            var actual = "_a_b_c_".Replace("_", ".", StringComparison.OrdinalIgnoreCase);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Replace_string_string_string_StringComparison_whenNoMatch()
        {
            const string expected = "example";
            var actual = "example".Replace("old", "new", StringComparison.OrdinalIgnoreCase);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Replace_string_string_string_StringComparison_whenStart()
        {
            const string expected = "abc";
            var actual = "XYZbc".Replace("xyz", "a", StringComparison.OrdinalIgnoreCase);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_SameLengthAs_string_string()
        {
            Assert.True("Abba".SameLengthAs("Zulu"));
        }

        [Fact]
        public void op_SameLengthAs_string_stringEmpty()
        {
            Assert.False("example".SameLengthAs(string.Empty));
        }

        [Fact]
        public void op_SameLengthAs_stringEmpty_stringEmpty()
        {
            Assert.True(string.Empty.SameLengthAs(string.Empty));
        }

        [Fact]
        public void op_SameLengthAs_string_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => "example".SameLengthAs(null));
        }

        [Fact]
        public void op_SameLengthAs_stringNull_string()
        {
            Assert.Throws<ArgumentNullException>(() => (null as string).SameLengthAs("example"));
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Aa", Justification = "This is spelling is intended.")]
        public void op_SameIndexesOfEach_stringAbba_charsAa()
        {
            Assert.True("Abba".SameIndexesOfEach('A', 'a'));
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Abc", Justification = "This is spelling is intended.")]
        public void op_SameIndexesOfEach_stringAbba_charsAbc()
        {
            Assert.False("Abba".SameIndexesOfEach('a', 'b', 'c'));
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Abc", Justification = "This is for testing purposes.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Xbz", Justification = "This is for testing purposes.")]
        public void op_SameIndexesOfEach_stringAbc_charsXbz()
        {
            Assert.True("abc".SameIndexesOfEach('X', 'b', 'z'));
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Abc", Justification = "This is spelling is intended.")]
        public void op_SameIndexesOfEach_stringAbc_charsXyz()
        {
            Assert.True("abc".SameIndexesOfEach('X', 'y', 'z'));
        }

        [Fact]
        public void op_SameIndexesOfEach_stringEmpty_chars()
        {
            Assert.True(string.Empty.SameIndexesOfEach('a', 'b', 'c'));
        }

        [Fact]
        public void op_SameIndexesOfEach_stringExample_charsExample()
        {
            Assert.True("Example".SameIndexesOfEach('E', 'x', 'a', 'm', 'p', 'l', 'e'));
        }

        [Fact]
        public void op_SameIndexesOfEach_stringNull_chars()
        {
            Assert.True((null as string).SameIndexesOfEach('a', 'b', 'c'));
        }

        [Fact]
        public void op_SameIndexesOfEach_string_charsEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => "Example".SameIndexesOfEach());
        }

        [Fact]
        public void op_SameIndexesOfEach_string_charsNull()
        {
            Assert.Throws<ArgumentNullException>(() => "Example".SameIndexesOfEach(null));
        }

        [Fact]
        public void op_Split_stringNull_char_StringSplitOptions()
        {
            Assert.Throws<ArgumentNullException>(() => (null as string).Split(';', StringSplitOptions.RemoveEmptyEntries));
        }

        [Fact]
        public void op_Split_string_char_StringSplitOptions()
        {
            var actual = "a;;b".Split(';', StringSplitOptions.RemoveEmptyEntries);

            Assert.Equal(2, actual.Length);
        }

        [Fact]
        public void op_Split_stringNull_string_StringSplitOptions()
        {
            Assert.Throws<ArgumentNullException>(() => (null as string).Split(";", StringSplitOptions.RemoveEmptyEntries));
        }

        [Fact]
        public void op_Split_string_string_StringSplitOptions()
        {
            var actual = "a;;b".Split(";", StringSplitOptions.RemoveEmptyEntries);

            Assert.Equal(2, actual.Length);
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Az", Justification = "This is spelling is intended.")]
        public void op_StartsOrEndsWith_stringAbba_charsAz()
        {
            Assert.True("Abba".StartsOrEndsWith('A', 'z'));
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Za", Justification = "This is for testing purposes.")]
        public void op_StartsOrEndsWith_stringAbba_charsZa()
        {
            Assert.True("Abba".StartsOrEndsWith('Z', 'a'));
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Az", Justification = "This is spelling is intended.")]
        public void op_StartsOrEndsWith_stringEmpty_charsAz()
        {
            Assert.False(string.Empty.StartsOrEndsWith('A', 'z'));
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Az", Justification = "This is spelling is intended.")]
        public void op_StartsOrEndsWith_stringNull_charsAz()
        {
            Assert.False((null as string).StartsOrEndsWith('A', 'z'));
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Abc", Justification = "This is spelling is intended.")]
        public void op_StartsOrEndsWith_stringZulu_charsAbc()
        {
            Assert.False("Zulu".StartsOrEndsWith('a', 'b', 'c'));
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Az", Justification = "This is spelling is intended.")]
        public void op_StartsOrEndsWith_stringZulu_charsAz()
        {
            Assert.False("Zulu".StartsOrEndsWith('A', 'z'));
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Za", Justification = "This is for testing purposes.")]
        public void op_StartsOrEndsWith_stringZulu_charsZa()
        {
            Assert.True("Zulu".StartsOrEndsWith('Z', 'a'));
        }

        [Fact]
        public void op_StartsOrEndsWith_string_charsEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => "Abba".StartsOrEndsWith());
        }

        [Fact]
        public void op_StartsOrEndsWith_string_charsNull()
        {
            Assert.Throws<ArgumentNullException>(() => "Abba".StartsOrEndsWith(null));
        }

        [Fact]
        public void op_StartsWithAny_stringEmpty_StringComparison_strings()
        {
            Assert.False(string.Empty.StartsWithAny(StringComparison.Ordinal, "cat"));
        }

        [Fact]
        public void op_StartsWithAny_stringNull_StringComparison_strings()
        {
            Assert.False((null as string).StartsWithAny(StringComparison.Ordinal, "cat"));
        }

        [Fact]
        public void op_StartsWithAny_string_StringComparison_strings()
        {
            Assert.True("cat dog".StartsWithAny(StringComparison.Ordinal, "cat "));
        }

        [Fact]
        public void op_StartsWithAny_string_StringComparison_stringsEmpty()
        {
            Assert.False("cat".StartsWithAny(StringComparison.Ordinal));
        }

        [Fact]
        public void op_StartsWithAny_string_StringComparison_stringsNull()
        {
            Assert.False("cat".StartsWithAny(StringComparison.Ordinal, null as string[]));
        }

        [Fact]
        public void op_ToEnglishSpacedAlphanumeric_string()
        {
            const string expected = "An example 123";
            var actual = "An éxample 123.".ToEnglishSpacedAlphanumeric();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToEnglishSpacedAlphanumeric_stringEmpty()
        {
            var expected = string.Empty;
            var actual = expected.ToEnglishSpacedAlphanumeric();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToEnglishSpacedAlphanumeric_stringNull()
        {
            Assert.Null((null as string).ToEnglishSpacedAlphanumeric());
        }

        [Fact]
        public void op_ToOfBoolean_string()
        {
            const bool expected = true;
            var actual = expected.ToXmlString().To<bool>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToOfByte_string()
        {
            const byte expected = 1;
            var actual = expected.ToXmlString().To<byte>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToOfChar_string()
        {
            const char expected = 'a';
            var actual = expected.ToXmlString().To<char>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToOfDateTime_string()
        {
            var expected = DateTime.UtcNow;
            var actual = expected.ToXmlString().To<DateTime>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToOfDateTimeOffset_string()
        {
            var expected = DateTimeOffset.Now;
            var actual = expected.ToXmlString().To<DateTimeOffset>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToOfDecimal_string()
        {
            const decimal expected = 123.45m;
            var actual = XmlConvert.ToString(expected).To<decimal>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToOfDouble_string()
        {
            const double expected = 123.45f;
            var actual = XmlConvert.ToString(expected).To<double>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToOfGuid_string()
        {
            var expected = Guid.NewGuid();
            var actual = XmlConvert.ToString(expected).To<Guid>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToOfInt16_string()
        {
            const short expected = 123;
            var actual = XmlConvert.ToString(expected).To<short>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToOfInt32_string()
        {
            const int expected = 123;
            var actual = XmlConvert.ToString(expected).To<int>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToOfNullableInt32_string()
        {
            const int expected = 123;
            var actual = XmlConvert.ToString(expected).To<int?>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToOfNullableInt32_stringEmpty()
        {
            var actual = string.Empty.To<int?>();

            Assert.False(actual.HasValue);
        }

        [Fact]
        public void op_ToOfNullableInt32_stringNull()
        {
            var actual = (null as string).To<int?>();

            Assert.False(actual.HasValue);
        }

        [Fact]
        public void op_ToOfInt64_string()
        {
            const long expected = 123;
            var actual = XmlConvert.ToString(expected).To<long>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToOfSByte_string()
        {
            const sbyte expected = 123;
            var actual = XmlConvert.ToString(expected).To<sbyte>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToOfSingle_string()
        {
            const float expected = 123.45f;
            var actual = XmlConvert.ToString(expected).To<float>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToOfString_string()
        {
            const string expected = "value";
            var actual = expected.To<string>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToOfTimeSpan_string()
        {
            var expected = new TimeSpan(1, 2, 3, 4);
            var actual = XmlConvert.ToString(expected).To<TimeSpan>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToOfUInt16_string()
        {
            const ushort expected = 123;
            var actual = XmlConvert.ToString(expected).To<ushort>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToOfUInt32_string()
        {
            const uint expected = 123;
            var actual = XmlConvert.ToString(expected).To<uint>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToOfUInt64_string()
        {
            const ulong expected = 123;
            var actual = XmlConvert.ToString(expected).To<ulong>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Soundex_stringNull()
        {
            Assert.Null((null as string).Soundex());
        }

        [Fact]
        public void op_Soundex_stringEmpty()
        {
            var expected = string.Empty;
            var actual = expected.Soundex();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Soundex_stringNumber()
        {
            var expected = string.Empty;
            var actual = "123".Soundex();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Soundex_string_whenA261()
        {
            const string expected = "A261";

            Assert.Equal(expected, "Ashcraft".Soundex());
            Assert.Equal(expected, "Ashcroft".Soundex());
        }

        [Fact]
        public void op_Soundex_string_whenT522()
        {
            const string expected = "T522";

            Assert.Equal(expected, "Tymczak".Soundex());
        }

        [Fact]
        public void op_Soundex_string_whenP236()
        {
            const string expected = "P236";

            Assert.Equal(expected, "Pfister".Soundex());
        }

        [Fact]
        public void op_Soundex_string_whenS530()
        {
            const string expected = "S530";

            Assert.Equal(expected, "Smith".Soundex());
            Assert.Equal(expected, "Smythe".Soundex());
        }

        [Fact]
        public void op_Soundex_string_whenA626()
        {
            const string expected = "A626";

            Assert.Equal(expected, "Archer".Soundex());
            Assert.Equal(expected, "Arguer".Soundex());
            Assert.Equal(expected, "Aircrew".Soundex());
        }

        [Fact]
        public void op_Soundex_string_whenW410()
        {
            const string expected = "W410";

            Assert.Equal(expected, "Wolf".Soundex());
        }

        [Fact]
        public void op_Soundex_string_whenR163()
        {
            const string expected = "R163";

            Assert.Equal(expected, "Robert".Soundex());
            Assert.Equal(expected, "Rupert".Soundex());
        }

        [Fact]
        public void op_Soundex_string_whenR100()
        {
            const string expected = "R100";

            Assert.Equal(expected, "Rub".Soundex());
        }

        [Fact]
        public void op_Soundex_string_whenR000()
        {
            const string expected = "R000";

            Assert.Equal(expected, "R".Soundex());
        }

        [Fact]
        public void op_Soundex_string_whenR150()
        {
            const string expected = "R150";

            Assert.Equal(expected, "Rubin".Soundex());
        }

        [Fact]
        public void op_Soundex_string_whenS532()
        {
            const string expected = "S532";

            Assert.Equal(expected, "Soundex".Soundex());
            Assert.Equal(expected, "Sownteks".Soundex());
        }

        [Fact]
        public void op_Soundex_string_whenE251()
        {
            const string expected = "E251";

            Assert.Equal(expected, "Example".Soundex());
            Assert.Equal(expected, "Ekzampul".Soundex());
        }

        [Fact]
        public void op_Soundex_string_whenE460()
        {
            const string expected = "E460";

            Assert.Equal(expected, "Ellery".Soundex());
            Assert.Equal(expected, "Euler".Soundex());
        }

        [Fact]
        public void op_Soundex_string_whenG200()
        {
            const string expected = "G200";

            Assert.Equal(expected, "Gauss".Soundex());
            Assert.Equal(expected, "Ghosh".Soundex());
        }

        [Fact]
        public void op_Soundex_string_whenH416()
        {
            const string expected = "H416";

            Assert.Equal(expected, "Heilbronn".Soundex());
            Assert.Equal(expected, "Hilbert".Soundex());
        }

        [Fact]
        public void op_Soundex_string_whenK530()
        {
            const string expected = "K530";

            Assert.Equal(expected, "Kant".Soundex());
            Assert.Equal(expected, "Knuth".Soundex());
        }

        [Fact]
        public void op_Soundex_string_whenL300()
        {
            const string expected = "L300";

            Assert.Equal(expected, "Ladd".Soundex());
            Assert.Equal(expected, "Lloyd".Soundex());
        }

        [Fact]
        public void op_Soundex_string_whenL222()
        {
            const string expected = "L222";

            Assert.Equal(expected, "Lukasiewicz".Soundex());
            Assert.Equal(expected, "Lissajous".Soundex());
        }

        [Fact]
        public void op_Soundex_string_whenW350()
        {
            const string expected = "W350";

            Assert.Equal(expected, "Wheaton".Soundex());
        }

        [Fact]
        public void op_Soundex_string_whenB620()
        {
            const string expected = "B620";

            Assert.Equal(expected, "Burroughs".Soundex());
            Assert.Equal(expected, "Burrows".Soundex());
        }

        [Fact]
        public void op_Soundex_string_whenO600()
        {
            const string expected = "O600";

            Assert.Equal(expected, "O'Hara".Soundex());
        }

        [Fact]
        public void op_Soundex_string_whenW252()
        {
            const string expected = "W252";

            Assert.Equal(expected, "Washington".Soundex());
        }

        [Fact]
        public void op_Soundex_string_whenL000()
        {
            const string expected = "L000";

            Assert.Equal(expected, "Lee".Soundex());
        }

        [Fact]
        public void op_Soundex_string_whenG362()
        {
            const string expected = "G362";

            Assert.Equal(expected, "Gutierrez".Soundex());
        }

        [Fact]
        public void op_Soundex_string_whenJ250()
        {
            const string expected = "J250";

            Assert.Equal(expected, "Jackson".Soundex());
        }

        [Fact]
        public void op_Soundex_string_whenV532()
        {
            const string expected = "V532";

            Assert.Equal(expected, "Van Deusen".Soundex());
        }

        [Fact]
        public void op_ToEnglishAlphabet_stringNull()
        {
            Assert.Null((null as string).ToEnglishAlphabet());
        }

        [Fact]
        public void op_ToEnglishAlphabet_stringEmpty()
        {
            var expected = string.Empty;
            var actual = expected.ToEnglishAlphabet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToEnglishAlphabet_string()
        {
            const string expected = "example";
            var actual = expected.ToEnglishAlphabet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToEnglishAlphabet_string_whenLowercaseA()
        {
            const string expected = "aaaa";
            var actual = "aàâæ".ToEnglishAlphabet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToEnglishAlphabet_string_whenUppercaseA()
        {
            const string expected = "AAAA";
            var actual = "AÀÂÆ".ToEnglishAlphabet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToEnglishAlphabet_string_whenLowercaseC()
        {
            const string expected = "cc";
            var actual = "cç".ToEnglishAlphabet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToEnglishAlphabet_string_whenUppercaseC()
        {
            const string expected = "CC";
            var actual = "CÇ".ToEnglishAlphabet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToEnglishAlphabet_string_whenLowercaseE()
        {
            const string expected = "eeeee";
            var actual = "eéèêë".ToEnglishAlphabet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToEnglishAlphabet_string_whenUppercaseE()
        {
            const string expected = "EEEEE";
            var actual = "EÉÈÊË".ToEnglishAlphabet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToEnglishAlphabet_string_whenUppercaseI()
        {
            const string expected = "III";
            var actual = "IÎÏ".ToEnglishAlphabet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToEnglishAlphabet_string_whenLowercaseI()
        {
            const string expected = "iii";
            var actual = "iîï".ToEnglishAlphabet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToEnglishAlphabet_string_whenLowercaseO()
        {
            const string expected = "ooo";
            var actual = "oôœ".ToEnglishAlphabet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToEnglishAlphabet_string_whenUppercaseO()
        {
            const string expected = "OOO";
            var actual = "OÔŒ".ToEnglishAlphabet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToEnglishAlphabet_string_whenLowercaseU()
        {
            const string expected = "uuuu";
            var actual = "uùûü".ToEnglishAlphabet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToEnglishAlphabet_string_whenUppercaseU()
        {
            const string expected = "UUUU";
            var actual = "UÙÛÜ".ToEnglishAlphabet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToEnglishAlphabet_string_whenLowercaseY()
        {
            const string expected = "yy";
            var actual = "yÿ".ToEnglishAlphabet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToEnglishAlphabet_string_whenUppercaseY()
        {
            const string expected = "YY";
            var actual = "YŸ".ToEnglishAlphabet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToEnglishAlphabet_stringAlphaUppercase()
        {
            const string expected = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var actual = expected.ToEnglishAlphabet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToEnglishAlphabet_stringAlphaLowercase()
        {
            const string expected = "abcdefghijklmnopqrstuvwxyz";
            var actual = expected.ToEnglishAlphabet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToTitleCase_string()
        {
            const string expected = "Example";
            var actual = "EXAMPLE".ToTitleCase();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToTitleCase_stringEmpty()
        {
            var expected = string.Empty;
            var actual = expected.ToTitleCase();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryToOfBoolean_string()
        {
            const bool expected = true;
            var actual = expected.ToXmlString().TryTo<bool>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryToOfBoolean_stringInvalid()
        {
            const bool expected = false;
            var actual = "invalid".TryTo<bool>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryToOfByte_string()
        {
            const byte expected = 1;
            var actual = expected.ToXmlString().TryTo<byte>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryToOfByte_stringInvalid()
        {
            const byte expected = 0;
            var actual = "invalid".TryTo<byte>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryToOfChar_string()
        {
            const char expected = 'a';
            var actual = expected.ToXmlString().TryTo<char>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryToOfChar_stringInvalid()
        {
            const char expected = '\0';
            var actual = "invalid".TryTo<char>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryToOfDateTime_stringInvalid()
        {
            var expected = DateTime.MinValue;
            var actual = "invalid".TryTo<DateTime>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryToOfDateTimeOffset_string()
        {
            var expected = DateTimeOffset.Now;
            var actual = expected.ToXmlString().TryTo<DateTimeOffset>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryToOfDateTimeOffset_stringInvalid()
        {
            var expected = DateTimeOffset.MinValue;
            var actual = "invalid".TryTo<DateTimeOffset>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryToOfDecimal_string()
        {
            const decimal expected = 123.45m;
            var actual = "123.45".TryTo<decimal>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryToOfDecimal_stringInvalid()
        {
            const decimal expected = 0m;
            var actual = "invalid".TryTo<decimal>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryToOfDouble_string()
        {
            const double expected = 123f;
            var actual = XmlConvert.ToString(expected).TryTo<double>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryToOfDouble_stringInvalid()
        {
            const double expected = 0f;
            var actual = "invalid".TryTo<double>();

            Assert.Equal(expected, actual);
        }

#if !NET20 && !NET35
        [Fact]
        public void op_TryToOfGuid_string()
        {
            var expected = Guid.NewGuid();
            var actual = XmlConvert.ToString(expected).TryTo<Guid>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryToOfGuid_stringInvalid()
        {
            var expected = Guid.Empty;
            var actual = "invalid".TryTo<Guid>();

            Assert.Equal(expected, actual);
        }

#endif

        [Fact]
        public void op_TryToOfInt16_string()
        {
            const short expected = 123;
            var actual = XmlConvert.ToString(expected).TryTo<short>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryToOfInt16_stringInvalid()
        {
            const short expected = 0;
            var actual = "invalid".TryTo<short>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryToOfInt32_string()
        {
            const int expected = 123;
            var actual = XmlConvert.ToString(expected).TryTo<int>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryToOfInt32_stringInvalid()
        {
            const int expected = 0;
            var actual = "invalid".TryTo<int>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryToOfNullableInt32_string()
        {
            const int expected = 123;
            var actual = XmlConvert.ToString(expected).TryTo<int?>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryToOfNullableInt32_stringEmpty()
        {
            var actual = string.Empty.TryTo<int?>();

            Assert.False(actual.HasValue);
        }

        [Fact]
        public void op_TryToOfNullableInt32_stringInvalid()
        {
            var actual = "invalid".TryTo<int?>();

            Assert.False(actual.HasValue);
        }

        [Fact]
        public void op_TryToOfNullableInt32_stringNull()
        {
            var actual = (null as string).TryTo<int?>();

            Assert.False(actual.HasValue);
        }

        [Fact]
        public void op_TryToOfInt64_string()
        {
            const long expected = 123;
            var actual = XmlConvert.ToString(expected).TryTo<long>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryToOfInt64_stringInvalid()
        {
            const long expected = 0;
            var actual = "invalid".TryTo<long>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryToOfSByte_string()
        {
            const sbyte expected = 123;
            var actual = XmlConvert.ToString(expected).TryTo<sbyte>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryToOfSByte_stringInvalid()
        {
            const sbyte expected = 0;
            var actual = "invalid".TryTo<sbyte>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryToOfSingle_string()
        {
            const float expected = 123.45f;
            var actual = XmlConvert.ToString(expected).TryTo<float>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryToOfSingle_stringInvalid()
        {
            const float expected = 0f;
            var actual = "invalid".TryTo<float>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryToOfString_string()
        {
            const string expected = "value";
            var actual = expected.TryTo<string>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryToOfString_stringEmpty()
        {
            var expected = string.Empty;
            var actual = expected.TryTo<string>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryToOfString_stringNull()
        {
            Assert.Null((null as string).TryTo<string>());
        }

#if !NET20 && !NET35
        [Fact]
        public void op_TryToOfTimeSpan_string()
        {
            var expected = new TimeSpan(1, 2, 3, 4);
            var actual = expected.ToString().TryTo<TimeSpan>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryToOfTimeSpan_stringInvalid()
        {
            var expected = new TimeSpan(0);
            var actual = "invalid".TryTo<TimeSpan>();

            Assert.Equal(expected, actual);
        }

#endif

        [Fact]
        public void op_TryToOfUInt16_string()
        {
            const ushort expected = 123;
            var actual = XmlConvert.ToString(expected).TryTo<ushort>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryToOfUInt16_stringInvalid()
        {
            const ushort expected = 0;
            var actual = "invalid".TryTo<ushort>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryToOfUInt32_string()
        {
            const uint expected = 123;
            var actual = XmlConvert.ToString(expected).TryTo<uint>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryToOfUInt32_stringInvalid()
        {
            const uint expected = 0;
            var actual = "invalid".TryTo<uint>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryToOfUInt64_string()
        {
            const ulong expected = 123;
            var actual = XmlConvert.ToString(expected).TryTo<ulong>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_TryToOfUInt64_stringInvalid()
        {
            const ulong expected = 0;
            var actual = "invalid".TryTo<ulong>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToTitleCase_stringNull()
        {
            Assert.Null((null as string).ToTitleCase());
        }

        [Fact]
        public void op_XmlDeserialize_string()
        {
            const string expected = "<root />";
            // ReSharper disable once PossibleNullReferenceException
            var actual = expected.XmlDeserialize().CreateNavigator().OuterXml;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_XmlDeserialize_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as string).XmlDeserialize());
        }

        [Fact]
        public void op_XmlDeserialize_stringEmpty()
        {
            Assert.Throws<XmlException>(() => string.Empty.XmlDeserialize());
        }

        [Fact]
        public void op_XmlDeserializeOfT_string()
        {
            var expected = new DateTime(2009, 04, 25);
            var actual = string.Concat(
                                       "<?xml version=\"1.0\" encoding=\"utf-8\"?>",
                                       Environment.NewLine,
                                       "<dateTime>2009-04-25T00:00:00</dateTime>").XmlDeserialize<DateTime>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_XmlDeserializeOfT_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => string.Empty.XmlDeserialize<int>());
        }

        [Fact]
        public void op_XmlDeserializeOfT_stringEmpty_Type()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => string.Empty.XmlDeserialize(typeof(DateTime)));
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "This is for testing purposes.")]
        public void op_XmlDeserializeOfT_stringException()
        {
            const string xml = "<SOAP-ENV:Envelope SOAP-ENV:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:SOAP-ENC=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:clr=\"http://schemas.microsoft.com/soap/encoding/clr/1.0\">" +
                               "<SOAP-ENV:Body>" +
                               "<a1:ArgumentOutOfRangeException id=\"ref-1\" xmlns:a1=\"http://schemas.microsoft.com/clr/ns/System\">" +
                               "<ClassName id=\"ref-2\">System.ArgumentOutOfRangeException</ClassName>" +
                               "<Message id=\"ref-3\">Specified argument was out of the range of valid values.</Message>" +
                               "<Data xsi:null=\"1\" />" +
                               "<InnerException xsi:null=\"1\" />" +
                               "<HelpURL xsi:null=\"1\" />" +
                               "<StackTraceString xsi:null=\"1\" />" +
                               "<RemoteStackTraceString xsi:null=\"1\" />" +
                               "<RemoteStackIndex>0</RemoteStackIndex>" +
                               "<ExceptionMethod xsi:null=\"1\" />" +
                               "<HResult>-2146233086</HResult>" +
                               "<Source xsi:null=\"1\" />" +
                               "<ParamName id=\"ref-4\"></ParamName>" +
                               "<ActualValue xsi:type=\"xsd:anyType\" xsi:null=\"1\" />" +
                               "</a1:ArgumentOutOfRangeException>" +
                               "</SOAP-ENV:Body>" +
                               "</SOAP-ENV:Envelope>";

            var expected = new ArgumentOutOfRangeException();
            var actual = xml.XmlDeserialize<ArgumentOutOfRangeException>();

            Assert.Equal(expected.Message, actual.Message);
        }

        [Fact]
        public void op_XmlDeserializeOfT_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as string).XmlDeserialize<int>());
        }

        [Fact]
        public void op_XmlDeserializeOfT_stringNull_Type()
        {
            Assert.Throws<ArgumentNullException>(() => (null as string).XmlDeserialize(typeof(DateTime)));
        }

        [Fact]
        public void op_XmlDeserializeOfT_string_Type()
        {
            var expected = new DateTime(2009, 04, 25);
            var actual = (DateTime)string.Concat(
                                                 "<?xml version=\"1.0\" encoding=\"utf-8\"?>",
                                                 Environment.NewLine,
                                                 "<dateTime>2009-04-25T00:00:00</dateTime>").XmlDeserialize(typeof(DateTime));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_XmlDeserializeOfT_string_TypeNull()
        {
            Assert.Throws<ArgumentNullException>(() => "<dateTime>2009-04-25T00:00:00</dateTime>".XmlDeserialize(null));
        }
    }
}