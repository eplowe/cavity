namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.XPath;

    using Cavity.IO;

    using Xunit;
    using Xunit.Extensions;

    public sealed class XmlUriAttributeFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<XmlUriAttribute>()
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
            Assert.NotNull(new XmlUriAttribute("http://www.alan-dean.com/example.xml"));
        }

        [Fact]
        public void ctor_stringsEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new XmlUriAttribute(new List<string>().ToArray()));
        }

        [Fact]
        public void ctor_stringsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new XmlUriAttribute(null));
        }

        [Fact]
        public void op_Download_AbsoluteUriNull()
        {
            Assert.Throws<ArgumentNullException>(() => XmlUriAttribute.Download(null));
        }

        [Fact]
        public void op_Download_AbsoluteUriNotFound()
        {
            Assert.Throws<WebException>(() => XmlUriAttribute.Download("http://www.alan-dean.com/missing.xml"));
        }

        [Fact]
        public void op_Download_AbsoluteUri()
        {
            var expected = new FileInfo("example.xml").ReadToEnd();
            var actual = XmlUriAttribute.Download("http://www.alan-dean.com/example.xml").ReadToEnd();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_GetData_MethodInfoNull_Types()
        {
            var obj = new XmlUriAttribute("http://www.alan-dean.com/example.xml");

            Assert.Throws<ArgumentNullException>(() => obj.GetData(null, new[] { typeof(XmlDocument) }).ToList());
        }

        [Fact]
        public void op_GetData_MethodInfo_TypesNull()
        {
            var obj = new XmlUriAttribute("http://www.alan-dean.com/example.xml");

            Assert.Throws<ArgumentNullException>(() => obj.GetData(GetType().GetMethod("usage"), null).ToList());
        }

        [Fact]
        public void op_GetData_MethodInfo_Types_whenInvalidParameterType()
        {
            var obj = new XmlUriAttribute("http://www.alan-dean.com/example.xml");

            Assert.Throws<InvalidOperationException>(() => obj.GetData(GetType().GetMethod("usage"), new[] { typeof(string) }).ToList());
        }

        [Fact]
        public void op_GetData_MethodInfo_Types_whenParameterCountMismatch()
        {
            var obj = new XmlUriAttribute("http://www.alan-dean.com/one.xml", "http://www.alan-dean.com/two.xml");

            Assert.Throws<InvalidOperationException>(() => obj.GetData(GetType().GetMethod("usage"), new[] { typeof(XmlDocument) }).ToList());
        }

        [Fact]
        public void prop_FileName()
        {
            Assert.True(new PropertyExpectations<XmlUriAttribute>(x => x.Locations)
                            .TypeIs<IEnumerable<string>>()
                            .IsNotDecorated()
                            .Result);
        }

        [Theory]
        [XmlUri("http://www.alan-dean.com/example.xml")]
        public void usage(XmlDocument xml)
        {
            Assert.Equal("<example />", xml.OuterXml);
        }

        [Theory]
        [XmlUri("http://www.alan-dean.com/example.xml")]
        [XmlUri("http://www.alan-dean.com/example.xml")]
        public void usage_whenIXPathNavigable(IXPathNavigable xml)
        {
            Assert.Equal("<example />", xml.CreateNavigator().OuterXml);
        }

        [Theory]
        [XmlUri("http://www.alan-dean.com/one.xml", "http://www.alan-dean.com/two.xml")]
        public void usage_whenMultipleParameters(XmlDocument one, 
                                                 XmlDocument two)
        {
            Assert.Equal("<one />", one.OuterXml);
            Assert.Equal("<two />", two.OuterXml);
        }

        [Theory]
        [XmlUri("http://www.alan-dean.com/example.xml")]
        public void usage_whenXDocument(XDocument document)
        {
            Assert.Equal("<example />", document.ToXmlString());
        }

        [Theory]
        [XmlUri("http://www.alan-dean.com/example.xml")]
        public void usage_whenXmlDeserialize(Example example)
        {
            Assert.NotNull(example);
        }

        [Theory]
        [XmlUri("http://www.alan-dean.com/example.xml")]
        public void usage_whenXPathNavigator(XPathNavigator navigator)
        {
            Assert.Equal("<example />", navigator.OuterXml);
        }
    }
}