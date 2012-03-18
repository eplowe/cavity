namespace Cavity
{
    using System;
    using System.Globalization;
    using System.Xml;

    using Xunit;

    public sealed class ObjectExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(ObjectExtensionMethods).IsStatic());
        }

        [Fact]
        public void op_NullOrToString_objectNull()
        {
            Assert.Null((null as object).NullOrToString());
        }

        [Fact]
        public void op_NullOrToString_object()
        {
            const string expected = "123";
            var actual = 123.NullOrToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToXmlString_objectNull()
        {
            Assert.Null((null as object).ToXmlString());
        }

        [Fact]
        public void op_ToXmlString_object_whenBoolean()
        {
            Assert.Equal("true", true.ToXmlString());
        }

        [Fact]
        public void op_ToXmlString_object_whenByte()
        {
            const short value = 1;

            Assert.Equal("1", value.ToXmlString());
        }

        [Fact]
        public void op_ToXmlString_object_whenChar()
        {
            Assert.Equal("a", 'a'.ToXmlString());
        }

        [Fact]
        public void op_ToXmlString_object_whenCultureInfo()
        {
            Assert.Equal("en", new CultureInfo("en").ToXmlString());
        }

        [Fact]
        public void op_ToXmlString_object_whenDateTime()
        {
            var value = new DateTime(1999, 12, 31);

            Assert.Equal(XmlConvert.ToString(value, XmlDateTimeSerializationMode.Utc), value.ToXmlString());
        }

        [Fact]
        public void op_ToXmlString_object_whenDateTimeOffset()
        {
            var value = new DateTimeOffset(new DateTime(1999, 12, 31));

            Assert.Equal(XmlConvert.ToString(value), value.ToXmlString());
        }

        [Fact]
        public void op_ToXmlString_object_whenDecimal()
        {
            Assert.Equal("123.45", ((decimal)123.45).ToXmlString());
        }

        [Fact]
        public void op_ToXmlString_object_whenDouble()
        {
            const double value = 123.45;

            Assert.Equal("123.45", value.ToXmlString());
        }

        [Fact]
        public void op_ToXmlString_object_whenGuid()
        {
            var value = Guid.NewGuid();

            Assert.Equal(XmlConvert.ToString(value), value.ToXmlString());
        }

        [Fact]
        public void op_ToXmlString_object_whenInt16()
        {
            const short value = 123;

            Assert.Equal("123", value.ToXmlString());
        }

        [Fact]
        public void op_ToXmlString_object_whenInt32()
        {
            Assert.Equal("123", 123.ToXmlString());
        }

        [Fact]
        public void op_ToXmlString_object_whenInt64()
        {
            const long value = 123;

            Assert.Equal("123", value.ToXmlString());
        }

        [Fact]
        public void op_ToXmlString_object_whenSingle()
        {
            const float value = 123.45f;

            Assert.Equal("123.45", value.ToXmlString());
        }

        [Fact]
        public void op_ToXmlString_object_whenString()
        {
            Assert.Equal("value", "value".ToXmlString());
        }

        [Fact]
        public void op_ToXmlString_object_whenTimeSpan()
        {
            var value = new TimeSpan(1, 2, 3);

            Assert.Equal(XmlConvert.ToString(value), value.ToXmlString());
        }

        [Fact]
        public void op_XmlSerialize_object()
        {
            const string expected = "2009-04-25T00:00:00";
            string actual = null;

            var xml = new DateTime(2009, 04, 25).XmlSerialize();
            if (null != xml)
            {
                var navigator = xml.CreateNavigator();
                if (null != navigator)
                {
                    var node = navigator.SelectSingleNode("//dateTime");
                    if (null != node)
                    {
                        actual = node.Value;
                    }
                }
            }

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_XmlSerialize_objectException()
        {
            var xml = new InvalidOperationException().XmlSerialize().CreateNavigator().OuterXml;

            Assert.True(xml.StartsWith("<SOAP-ENV:Envelope SOAP-ENV:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\"", StringComparison.Ordinal));
        }

        [Fact]
        public void op_XmlSerialize_objectNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as object).XmlSerialize());
        }
    }
}