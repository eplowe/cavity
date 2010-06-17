namespace Cavity
{
    using System;
    using System.Globalization;
    using System.Xml;
    using Xunit;

    public sealed class ObjectExtensionMethodsFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(typeof(ObjectExtensionMethods).IsStatic());
        }

        [Fact]
        public void op_ToXmlString_objectNull()
        {
            Assert.Null((null as object).ToXmlString());
        }

        [Fact]
        public void op_ToXmlString_object_whenBoolean()
        {
            Assert.Equal<string>("true", true.ToXmlString());
        }

        [Fact]
        public void op_ToXmlString_object_whenByte()
        {
            Assert.Equal<string>("1", ((byte)1).ToXmlString());
        }

        [Fact]
        public void op_ToXmlString_object_whenChar()
        {
            Assert.Equal<string>("a", 'a'.ToXmlString());
        }

        [Fact]
        public void op_ToXmlString_object_whenCultureInfo()
        {
            Assert.Equal<string>("en", new CultureInfo("en").ToXmlString());
        }

        [Fact]
        public void op_ToXmlString_object_whenDateTime()
        {
            DateTime value = DateTime.UtcNow;

            Assert.Equal<string>(XmlConvert.ToString(value, XmlDateTimeSerializationMode.Utc), value.ToXmlString());
        }

        [Fact]
        public void op_ToXmlString_object_whenDateTimeOffset()
        {
            DateTimeOffset value = new DateTimeOffset(DateTime.Today);

            Assert.Equal<string>(XmlConvert.ToString(value), value.ToXmlString());
        }

        [Fact]
        public void op_ToXmlString_object_whenDecimal()
        {
            Assert.Equal<string>("123.45", ((decimal)123.45).ToXmlString());
        }

        [Fact]
        public void op_ToXmlString_object_whenDouble()
        {
            Assert.Equal<string>("123.45", ((double)123.45).ToXmlString());
        }

        [Fact]
        public void op_ToXmlString_object_whenGuid()
        {
            Guid value = Guid.NewGuid();

            Assert.Equal<string>(XmlConvert.ToString(value), value.ToXmlString());
        }

        [Fact]
        public void op_ToXmlString_object_whenInt16()
        {
            Assert.Equal<string>("123", ((short)123).ToXmlString());
        }

        [Fact]
        public void op_ToXmlString_object_whenInt32()
        {
            Assert.Equal<string>("123", 123.ToXmlString());
        }

        [Fact]
        public void op_ToXmlString_object_whenInt64()
        {
            Assert.Equal<string>("123", ((long)123).ToXmlString());
        }

        [Fact]
        public void op_ToXmlString_object_whenSingle()
        {
            Assert.Equal<string>("123.45", ((float)123.45).ToXmlString());
        }

        [Fact]
        public void op_ToXmlString_object_whenString()
        {
            Assert.Equal<string>("value", "value".ToXmlString());
        }

        [Fact]
        public void op_ToXmlString_object_whenTimeSpan()
        {
            TimeSpan value = new TimeSpan(1, 2, 3);

            Assert.Equal<string>(XmlConvert.ToString(value), value.ToXmlString());
        }

        [Fact]
        public void op_XmlSerialize_object()
        {
            string expected = "2009-04-25T00:00:00";
            string actual = new DateTime(2009, 04, 25).XmlSerialize().CreateNavigator().SelectSingleNode("//dateTime").Value;

            Assert.Equal<string>(expected, actual);
        }

        [Fact]
        public void op_XmlSerialize_objectNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as object).XmlSerialize());
        }

        [Fact]
        public void op_XmlSerialize_objectException()
        {
            string xml = new ArgumentOutOfRangeException("parameter").XmlSerialize().CreateNavigator().OuterXml;

            Assert.True(xml.StartsWith("<SOAP-ENV:Envelope SOAP-ENV:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\""));
        }
    }
}