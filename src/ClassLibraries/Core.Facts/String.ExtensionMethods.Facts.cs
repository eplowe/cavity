namespace Cavity
{
    using System;
    using Cavity;
    using Xunit;

    public sealed class StringExtensionMethodsFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(typeof(StringExtensionMethods).IsStatic());
        }

        [Fact]
        public void op_XmlDeserializeOfT_string()
        {
            DateTime expected = new DateTime(2009, 04, 25);
            DateTime actual = string.Concat(
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>",
                Environment.NewLine,
                "<dateTime>2009-04-25T00:00:00</dateTime>").XmlDeserialize<DateTime>();

            Assert.Equal<DateTime>(expected, actual);
        }

        [Fact]
        public void op_XmlDeserializeOfT_stringException()
        {
            string xml =
                "<SOAP-ENV:Envelope SOAP-ENV:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:SOAP-ENC=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:clr=\"http://schemas.microsoft.com/soap/encoding/clr/1.0\">" +
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
                            "<ParamName id=\"ref-4\">parameter</ParamName>" +
                            "<ActualValue xsi:type=\"xsd:anyType\" xsi:null=\"1\" />" +
                        "</a1:ArgumentOutOfRangeException>" +
                    "</SOAP-ENV:Body>" +
                "</SOAP-ENV:Envelope>";

            ArgumentOutOfRangeException expected = new ArgumentOutOfRangeException("parameter");
            ArgumentOutOfRangeException actual = xml.XmlDeserialize<ArgumentOutOfRangeException>();

            Assert.Equal<string>(expected.Message, actual.Message);
        }

        [Fact]
        public void op_XmlDeserializeOfT_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => string.Empty.XmlDeserialize<int>());
        }

        [Fact]
        public void op_XmlDeserializeOfT_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as string).XmlDeserialize<int>());
        }

        [Fact]
        public void op_XmlDeserializeOfT_string_Type()
        {
            DateTime expected = new DateTime(2009, 04, 25);
            DateTime actual = (DateTime)string.Concat(
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>",
                Environment.NewLine,
                "<dateTime>2009-04-25T00:00:00</dateTime>").XmlDeserialize(typeof(DateTime));

            Assert.Equal<DateTime>(expected, actual);
        }

        [Fact]
        public void op_XmlDeserializeOfT_stringEmpty_Type()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => string.Empty.XmlDeserialize(typeof(DateTime)));
        }

        [Fact]
        public void op_XmlDeserializeOfT_stringNull_Type()
        {
            Assert.Throws<ArgumentNullException>(() => (null as string).XmlDeserialize(typeof(DateTime)));
        }

        [Fact]
        public void op_XmlDeserializeOfT_string_TypeNull()
        {
            Assert.Throws<ArgumentNullException>(() => "<dateTime>2009-04-25T00:00:00</dateTime>".XmlDeserialize(null as Type));
        }
    }
}