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

    public sealed class XmlFileAttributeFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<XmlFileAttribute>()
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
            Assert.NotNull(new XmlFileAttribute("example.xml"));
        }

        [Fact]
        public void ctor_stringsEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new XmlFileAttribute(new List<string>().ToArray()));
        }

        [Fact]
        public void ctor_stringsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new XmlFileAttribute(null));
        }

        [Fact]
        public void op_GetData_MethodInfoNull_Types()
        {
            var obj = new XmlFileAttribute("example.xml");

            Assert.Throws<ArgumentNullException>(() => obj.GetData(null, new[] { typeof(XmlDocument) }).ToList());
        }

        [Fact]
        public void op_GetData_MethodInfo_TypesNull()
        {
            var obj = new XmlFileAttribute("example.xml");

            Assert.Throws<ArgumentNullException>(() => obj.GetData(GetType().GetMethod("usage"), null).ToList());
        }

        [Fact]
        public void op_GetData_MethodInfo_Types_whenInvalidParameterType()
        {
            var obj = new XmlFileAttribute("example.xml");

            Assert.Throws<InvalidOperationException>(() => obj.GetData(GetType().GetMethod("usage"), new[] { typeof(string) }).ToList());
        }

        [Fact]
        public void op_GetData_MethodInfo_Types_whenParameterCountMismatch()
        {
            var obj = new XmlFileAttribute("one.xml", "two.xml");

            Assert.Throws<InvalidOperationException>(() => obj.GetData(GetType().GetMethod("usage"), new[] { typeof(XmlDocument) }).ToList());
        }

        [Fact]
        public void prop_FileName()
        {
            Assert.True(new PropertyExpectations<XmlFileAttribute>(x => x.Files)
                            .TypeIs<IEnumerable<string>>()
                            .IsNotDecorated()
                            .Result);
        }

        [Theory]
        [XmlFile("example.xml")]
        public void usage(XmlDocument xml)
        {
            Assert.Equal("<example />", xml.OuterXml);
        }

        [Theory]
        [XmlFile("example.xml")]
        [XmlFile("example.xml")]
        public void usage_whenIXPathNavigable(IXPathNavigable xml)
        {
            Assert.Equal("<example />", xml.CreateNavigator().OuterXml);
        }

        [Theory]
        [XmlFile("one.xml", "two.xml")]
        public void usage_whenMultipleParameters(XmlDocument one, 
                                                 XmlDocument two)
        {
            Assert.Equal("<one />", one.OuterXml);
            Assert.Equal("<two />", two.OuterXml);
        }

        [Theory]
        [XmlFile("example.xml")]
        public void usage_whenXDocument(XDocument document)
        {
            Assert.Equal("<example />", document.ToXmlString());
        }

        [Theory]
        [XmlFile("example.xml")]
        public void usage_whenXmlDeserialize(Example example)
        {
            Assert.NotNull(example);
        }

        [Theory]
        [XmlFile("example.xml")]
        public void usage_whenXPathNavigator(XPathNavigator navigator)
        {
            Assert.Equal("<example />", navigator.OuterXml);
        }
    }
}