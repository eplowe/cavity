namespace Cavity
{
    using System;
    using System.Reflection;
    using System.Xml.Serialization;
    using Cavity.Types;
    using Xunit;

    public class PropertyExpectationsFacts
    {
        [Fact]
        public void ctor_PropertyInfo()
        {
            Assert.NotNull(new PropertyExpectations(typeof(PropertiedClass1).GetProperty("AutoBoolean")));
        }

        [Fact]
        public void ctor_PropertyInfoNull()
        {
            Assert.NotNull(new PropertyExpectations(null as PropertyInfo));
        }

        [Fact]
        public void prop_Property()
        {
            var obj = new PropertyExpectations(null as PropertyInfo);

            PropertyInfo expected = typeof(PropertiedClass1).GetProperty("AutoBoolean");
            obj.Property = expected;
            PropertyInfo actual = obj.Property;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void prop_Result_whenAuto()
        {
            Assert.True(new PropertyExpectations(typeof(PropertiedClass1).GetProperty("AutoString"))
                .IsAutoProperty<string>()
                .Result);
        }

        [Fact]
        public void prop_Result_whenAutoBoolean()
        {
            Assert.True(new PropertyExpectations(typeof(PropertiedClass1).GetProperty("AutoBoolean"))
                .TypeIs<bool>()
                .DefaultValueIs(false)
                .DefaultValueIsNotNull()
                .Set<bool>()
                .Set(true)
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void prop_Result_whenAutoString()
        {
            Assert.True(new PropertyExpectations(typeof(PropertiedClass1).GetProperty("AutoString"))
                .TypeIs<string>()
                .DefaultValueIsNull()
                .Set<string>()
                .Set(string.Empty)
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void prop_Result_whenIsDecorated()
        {
            Assert.True(new PropertyExpectations(typeof(AttributedClass1).GetProperty("Value"))
                .TypeIs<string>()
                .DefaultValueIsNull()
                .IsDecoratedWith<Attribute2>()
                .Result);
        }

        [Fact]
        public void prop_Result_whenXmlAttribute()
        {
            Assert.True(new PropertyExpectations(typeof(XmlSerializableClass1).GetProperty("Attribute"))
                .TypeIs<string>()
                .DefaultValueIsNull()
                .XmlAttribute("attribute")
                .Result);
        }

        [Fact]
        public void prop_Result_whenIsDecoratedWithXmlAttribute()
        {
            Assert.Throws<TestException>(() => new PropertyExpectations(typeof(XmlSerializableClass1).GetProperty("Attribute"))
                .TypeIs<string>()
                .DefaultValueIsNull()
                .IsDecoratedWith<XmlAttributeAttribute>());
        }

        [Fact]
        public void prop_Result_whenNamespacedXmlAttribute()
        {
            Assert.True(new PropertyExpectations(typeof(XmlSerializableClass1).GetProperty("NamespacedAttribute"))
                .TypeIs<string>()
                .DefaultValueIsNull()
                .XmlAttribute("attribute", "urn:example.org")
                .Result);
        }

        [Fact]
        public void prop_Result_whenXmlElement()
        {
            Assert.True(new PropertyExpectations(typeof(XmlSerializableClass1).GetProperty("Element"))
                .TypeIs<string>()
                .DefaultValueIsNull()
                .XmlElement("element")
                .Result);
        }

        [Fact]
        public void prop_Result_whenIsDecoratedWithXmlElement()
        {
            Assert.Throws<TestException>(() => new PropertyExpectations(typeof(XmlSerializableClass1).GetProperty("Element"))
                .TypeIs<string>()
                .DefaultValueIsNull()
                .IsDecoratedWith<XmlElementAttribute>());
        }

        [Fact]
        public void prop_Result_whenNamespacedXmlElement()
        {
            Assert.True(new PropertyExpectations(typeof(XmlSerializableClass1).GetProperty("NamespacedElement"))
                .TypeIs<string>()
                .DefaultValueIsNull()
                .XmlElement("element", "urn:example.org")
                .Result);
        }

        [Fact]
        public void prop_Result_whenXmlIgnore()
        {
            Assert.True(new PropertyExpectations(typeof(XmlSerializableClass1).GetProperty("Ignore"))
                .TypeIs<string>()
                .DefaultValueIsNull()
                .XmlIgnore()
                .Result);
        }

        [Fact]
        public void prop_Result_whenIsDecoratedWithXmlIgnore()
        {
            Assert.Throws<TestException>(() => new PropertyExpectations(typeof(XmlSerializableClass1).GetProperty("Ignore"))
                .TypeIs<string>()
                .DefaultValueIsNull()
                .IsDecoratedWith<XmlIgnoreAttribute>());
        }

        [Fact]
        public void prop_Result_whenXmlText()
        {
            Assert.True(new PropertyExpectations(typeof(XmlSerializableClass1).GetProperty("Text"))
                .TypeIs<string>()
                .DefaultValueIsNull()
                .XmlText()
                .Result);
        }

        [Fact]
        public void prop_Result_whenIsDecoratedWithXmlText()
        {
            Assert.Throws<TestException>(() => new PropertyExpectations(typeof(XmlSerializableClass1).GetProperty("Text"))
                .TypeIs<string>()
                .DefaultValueIsNull()
                .IsDecoratedWith<XmlTextAttribute>());
        }

        [Fact]
        public void prop_Result_whenXmlArray()
        {
            Assert.True(new PropertyExpectations(typeof(XmlSerializableClass1).GetProperty("Array1"))
                .TypeIs<string[]>()
                .DefaultValueIsNull()
                .XmlArray("array1", "item1")
                .XmlNamespaceDeclarations()
                .Result);
        }

        [Fact]
        public void prop_Result_whenIsDecoratedWithXmlArray()
        {
            Assert.Throws<TestException>(() => new PropertyExpectations(typeof(XmlSerializableClass1).GetProperty("Array1"))
                .TypeIs<string[]>()
                .DefaultValueIsNull()
                .IsDecoratedWith<XmlArrayAttribute>());
        }

        [Fact]
        public void prop_Result_whenIsDecoratedWithXmlNamespaceDeclarations()
        {
            Assert.Throws<TestException>(() => new PropertyExpectations(typeof(XmlSerializableClass1).GetProperty("Array1"))
                .TypeIs<string[]>()
                .DefaultValueIsNull()
                .IsDecoratedWith<XmlNamespaceDeclarationsAttribute>());
        }

        [Fact]
        public void op_ArgumentNullException()
        {
            Assert.True(new PropertyExpectations(typeof(PropertiedClass1).GetProperty("ArgumentNullExceptionValue"))
                .ArgumentNullException()
                .Result);
        }

        [Fact]
        public void op_ArgumentOutOfRangeException_object()
        {
            Assert.True(new PropertyExpectations(typeof(PropertiedClass1).GetProperty("ArgumentOutOfRangeExceptionValue"))
                .ArgumentOutOfRangeException(string.Empty)
                .Result);
        }

        [Fact]
        public void op_FormatException_object()
        {
            Assert.True(new PropertyExpectations(typeof(PropertiedClass1).GetProperty("FormatExceptionValue"))
                .FormatException(string.Empty)
                .Result);
        }

        [Fact]
        public void op_Exception_object_Type()
        {
            Assert.True(new PropertyExpectations(typeof(PropertiedClass1).GetProperty("ArgumentExceptionValue"))
                .Exception(string.Empty, typeof(ArgumentException))
                .Result);
        }

        [Fact]
        public void op_Exception_object_TypeNull()
        {
            Assert.Throws<ArgumentNullException>(() => new PropertyExpectations(typeof(PropertiedClass1).GetProperty("AutoBoolean"))
                .Exception(string.Empty, null as Type));
        }

        [Fact]
        public void op_Exception_object_TypeInvalid()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new PropertyExpectations(typeof(PropertiedClass1).GetProperty("AutoBoolean"))
                .Exception(string.Empty, typeof(int)));
        }
    }
}