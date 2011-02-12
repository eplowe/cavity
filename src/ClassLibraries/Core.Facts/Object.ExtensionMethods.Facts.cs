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
        public void op_EqualsOneOf_TNull_Ts()
        {
            Assert.False((null as string).EqualsOneOf("xyz"));
        }

        [Fact]
        public void op_EqualsOneOf_T_TNull()
        {
            Assert.Throws<ArgumentNullException>(() => "abc".EqualsOneOf(null));
        }

        [Fact]
        public void op_EqualsOneOf_T_Ts()
        {
            Assert.True(123.EqualsOneOf(123, 456));
        }

        [Fact]
        public void op_EqualsOneOf_T_Ts_whenFalse()
        {
            Assert.False(1.EqualsOneOf(2, 3));
        }

        [Fact]
        public void op_EqualsOneOf_T_Ts_whenStringFalse()
        {
            Assert.False("abc".EqualsOneOf("xyz"));
        }

        [Fact]
        public void op_EqualsOneOf_T_Ts_whenStringTrue()
        {
            Assert.True("abc".EqualsOneOf("abc"));
        }

        [Fact]
        public void op_IsBoundedBy_TNull_T_T()
        {
            Assert.False((null as string).IsBoundedBy("a", "c"));
        }

        [Fact]
        public void op_IsBoundedBy_T_TNull_T()
        {
            Assert.True("b".IsBoundedBy(null, "c"));
        }

        [Fact]
        public void op_IsBoundedBy_T_T_T()
        {
            Assert.True(2.IsBoundedBy(1, 3));
        }

        [Fact]
        public void op_IsBoundedBy_T_T_TNull()
        {
            Assert.Throws<ArgumentNullException>(() => "b".IsBoundedBy("a", null));
        }

        [Fact]
        public void op_IsBoundedBy_T_T_T_whenFalse()
        {
            Assert.False(3.IsBoundedBy(1, 2));
        }

        [Fact]
        public void op_IsBoundedBy_T_T_T_whenLower()
        {
            Assert.True(1.IsBoundedBy(1, 2));
        }

        [Fact]
        public void op_IsBoundedBy_T_T_T_whenLowerGreaterThanUpper()
        {
            Assert.Throws<ArgumentException>(() => 2.IsBoundedBy(3, 1));
        }

        [Fact]
        public void op_IsBoundedBy_T_T_T_whenSameBounds()
        {
            Assert.Throws<ArgumentException>(() => 1.IsBoundedBy(1, 1));
        }

        [Fact]
        public void op_IsBoundedBy_T_T_T_whenString()
        {
            Assert.True("b".IsBoundedBy("a", "c"));
        }

        [Fact]
        public void op_IsBoundedBy_T_T_T_whenUpper()
        {
            Assert.True(2.IsBoundedBy(1, 2));
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