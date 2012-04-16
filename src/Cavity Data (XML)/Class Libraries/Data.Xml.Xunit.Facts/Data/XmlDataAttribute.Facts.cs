namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.XPath;

    using Xunit;
    using Xunit.Extensions;

    public sealed class XmlDataAttributeFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<XmlDataAttribute>()
                            .DerivesFrom<DataAttribute>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .AttributeUsage(AttributeTargets.Method, true, true)
                            .Result);
        }

        [Fact]
        public void ctor_strings()
        {
            Assert.NotNull(new XmlDataAttribute("<example />"));
        }

        [Fact]
        public void ctor_stringsEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new XmlDataAttribute(new List<string>().ToArray()));
        }

        [Fact]
        public void ctor_stringsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new XmlDataAttribute(null));
        }

        [Fact]
        public void op_GetData_MethodInfoNull_Types()
        {
            var obj = new XmlDataAttribute("<example />");

            Assert.Throws<ArgumentNullException>(() => obj.GetData(null, new[] { typeof(XmlDocument) }).ToList());
        }

        [Fact]
        public void op_GetData_MethodInfo_TypesNull()
        {
            var obj = new XmlDataAttribute("<example />");

            Assert.Throws<ArgumentNullException>(() => obj.GetData(GetType().GetMethod("usage"), null).ToList());
        }

        [Fact]
        public void op_GetData_MethodInfo_Types_whenInvalidParameterType()
        {
            var obj = new XmlDataAttribute("<example />");

            Assert.Throws<InvalidOperationException>(() => obj.GetData(GetType().GetMethod("usage"), new[] { typeof(string) }).ToList());
        }

        [Fact]
        public void op_GetData_MethodInfo_Types_whenParameterCountMismatch()
        {
            var obj = new XmlDataAttribute("<one />", "<two />");

            Assert.Throws<InvalidOperationException>(() => obj.GetData(GetType().GetMethod("usage"), new[] { typeof(XmlDocument) }).ToList());
        }

        [Fact]
        public void prop_Values()
        {
            Assert.True(new PropertyExpectations<XmlDataAttribute>(x => x.Values)
                            .TypeIs<IEnumerable<string>>()
                            .IsNotDecorated()
                            .Result);
        }

        [Theory]
        [XmlData("<example />")]
        public void usage(XmlDocument xml)
        {
            Assert.Equal("<example />", xml.OuterXml);
        }

        [Theory]
        [XmlData("<example />")]
        [XmlData("<example />")]
        public void usage_whenIXPathNavigable(IXPathNavigable xml)
        {
            Assert.Equal("<example />", xml.CreateNavigator().OuterXml);
        }

        [Theory]
        [XmlData("<one />", "<two />")]
        public void usage_whenMultipleParameters(XmlDocument one, 
                                                 XmlDocument two)
        {
            Assert.Equal("<one />", one.OuterXml);
            Assert.Equal("<two />", two.OuterXml);
        }

        [Theory]
        [XmlData("<example />")]
        public void usage_whenXDocument(XDocument document)
        {
            Assert.Equal("<example />", document.ToXmlString());
        }

        [Theory]
        [XmlData("<example />")]
        public void usage_whenXmlDeserialize(Example example)
        {
            Assert.NotNull(example);
        }

        [Theory]
        [XmlData("<example />")]
        public void usage_whenXPathNavigator(XPathNavigator navigator)
        {
            Assert.Equal("<example />", navigator.OuterXml);
        }
    }
}