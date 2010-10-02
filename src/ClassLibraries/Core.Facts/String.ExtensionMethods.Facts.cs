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
            Assert.True("abc".Contains("B", StringComparison.InvariantCultureIgnoreCase));
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
            var actual = "example".Replace(string.Empty, "new", StringComparison.InvariantCultureIgnoreCase);

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
            var actual = "example".Replace("old", string.Empty, StringComparison.InvariantCultureIgnoreCase);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Replace_string_string_stringEmpty_StringComparison_whenMultiple()
        {
            const string expected = "abc";
            var actual = "_a_b_c_".Replace("_", string.Empty, StringComparison.InvariantCultureIgnoreCase);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Replace_string_string_stringNull_StringComparison()
        {
            const string expected = "example";
            var actual = "example".Replace("old", null, StringComparison.InvariantCultureIgnoreCase);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Replace_string_string_string_StringComparison_whenEmbedded()
        {
            const string expected = "abc";
            var actual = "aXYZc".Replace("xyz", "b", StringComparison.InvariantCultureIgnoreCase);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Replace_string_string_string_StringComparison_whenEnd()
        {
            const string expected = "abc";
            var actual = "abXYZ".Replace("xyz", "c", StringComparison.InvariantCultureIgnoreCase);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Replace_string_string_string_StringComparison_whenMultiple()
        {
            const string expected = ".a.b.c.";
            var actual = "_a_b_c_".Replace("_", ".", StringComparison.InvariantCultureIgnoreCase);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Replace_string_string_string_StringComparison_whenNoMatch()
        {
            const string expected = "example";
            var actual = "example".Replace("old", "new", StringComparison.InvariantCultureIgnoreCase);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Replace_string_string_string_StringComparison_whenStart()
        {
            const string expected = "abc";
            var actual = "XYZbc".Replace("xyz", "a", StringComparison.InvariantCultureIgnoreCase);

            Assert.Equal(expected, actual);
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