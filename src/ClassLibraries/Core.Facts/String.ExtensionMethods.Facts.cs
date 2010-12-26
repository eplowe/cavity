namespace Cavity
{
    using System;
    using Xunit;

    public sealed class StringExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(StringExtensionMethods).IsStatic());
        }

        [Fact]
        public void op_Contains_stringEmpty_StringComparison()
        {
            Assert.False(string.Empty.Contains("example", StringComparison.Ordinal));
        }

        [Fact]
        public void op_Contains_stringNull_StringComparison()
        {
            Assert.Throws<ArgumentNullException>(() => (null as string).Contains("example", StringComparison.Ordinal));
        }

        [Fact]
        public void op_Contains_string_StringComparison()
        {
            Assert.True("abc".Contains("B", StringComparison.OrdinalIgnoreCase));
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
            var expected = new string(' ', WhiteSpace.Characters.Count);
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
                '\uFEFF').NormalizeWhiteSpace();

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
        public void op_RemoveAnyDigits_string()
        {
            const string expected = "abc";
            var actual = "a01234b56789c".RemoveAnyDigits();

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
        public void op_RemoveAnyWhiteSpace_string()
        {
            const string expected = "example";
            var actual = "e x a m p l e".RemoveAnyWhiteSpace();

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
        public void op_SameIndexesOfEach_stringAbba_charsAa()
        {
            Assert.True("Abba".SameIndexesOfEach('A', 'a'));
        }

        [Fact]
        public void op_SameIndexesOfEach_stringAbba_charsAbc()
        {
            Assert.False("Abba".SameIndexesOfEach('a', 'b', 'c'));
        }

        [Fact]
        public void op_SameIndexesOfEach_stringAbc_charsXbz()
        {
            Assert.True("abc".SameIndexesOfEach('X', 'b', 'z'));
        }

        [Fact]
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
        public void op_SameIndexesOfEach_string_charsNull()
        {
            Assert.True("Example".SameIndexesOfEach());
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
        public void op_StartsOrEndsWith_stringAbba_charsAz()
        {
            Assert.True("Abba".StartsOrEndsWith('A', 'z'));
        }

        [Fact]
        public void op_StartsOrEndsWith_stringAbba_charsZa()
        {
            Assert.True("Abba".StartsOrEndsWith('Z', 'a'));
        }

        [Fact]
        public void op_StartsOrEndsWith_stringEmpty_charsAz()
        {
            Assert.False(string.Empty.StartsOrEndsWith('A', 'z'));
        }

        [Fact]
        public void op_StartsOrEndsWith_stringNull_charsAz()
        {
            Assert.False((null as string).StartsOrEndsWith('A', 'z'));
        }

        [Fact]
        public void op_StartsOrEndsWith_stringZulu_charsAbc()
        {
            Assert.False("Zulu".StartsOrEndsWith('a', 'b', 'c'));
        }

        [Fact]
        public void op_StartsOrEndsWith_stringZulu_charsAz()
        {
            Assert.False("Zulu".StartsOrEndsWith('A', 'z'));
        }

        [Fact]
        public void op_StartsOrEndsWith_stringZulu_charsZa()
        {
            Assert.True("Zulu".StartsOrEndsWith('Z', 'a'));
        }

        [Fact]
        public void op_StartsOrEndsWith_string_charsNull()
        {
            Assert.False("Abba".StartsOrEndsWith());
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
        public void op_ToTitleCase_stringNull()
        {
            Assert.Null((null as string).ToTitleCase());
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