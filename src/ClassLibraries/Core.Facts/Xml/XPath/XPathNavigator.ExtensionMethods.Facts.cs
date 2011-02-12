namespace Cavity.Xml.XPath
{
    using System;
    using System.Xml;
    using System.Xml.XPath;
    using Xunit;

    public sealed class XPathNavigatorExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(XPathNavigatorExtensionMethods).IsStatic());
        }

        [Fact]
        public void op_Evaluate_XPathNavigatorNull_string()
        {
            Assert.Throws<ArgumentNullException>(() => (null as XPathNavigator).Evaluate<string>("/text()"));
        }

        [Fact]
        public void op_Evaluate_XPathNavigatorNull_string_IXmlNamespaceResolver()
        {
            var xml = new XmlDocument();
            xml.LoadXml("<foo xmlns='urn:example'>bar</foo>");

            var namespaces = new XmlNamespaceManager(xml.NameTable);
            namespaces.AddNamespace("x", "urn:example");

            Assert.Throws<ArgumentNullException>(() => (null as XPathNavigator).Evaluate<string>("/text()", namespaces));
        }

        [Fact]
        public void op_Evaluate_XPathNavigator_string()
        {
            var xml = new XmlDocument();
            xml.LoadXml("<foo>bar</foo>");

            Assert.True(xml.CreateNavigator().Evaluate<bool>("1=count(/foo[text()='bar'])"));
        }

        [Fact]
        public void op_Evaluate_XPathNavigator_stringEmpty()
        {
            var xml = new XmlDocument();
            xml.LoadXml("<foo>bar</foo>");

            Assert.Throws<XPathException>(() => xml.CreateNavigator().Evaluate<string>(string.Empty));
        }

        [Fact]
        public void op_Evaluate_XPathNavigator_stringEmpty_IXmlNamespaceResolver()
        {
            var xml = new XmlDocument();
            xml.LoadXml("<foo xmlns='urn:example'>bar</foo>");

            var namespaces = new XmlNamespaceManager(xml.NameTable);
            namespaces.AddNamespace("x", "urn:example");

            Assert.Throws<XPathException>(() => xml.CreateNavigator().Evaluate<string>(string.Empty, namespaces));
        }

        [Fact]
        public void op_Evaluate_XPathNavigator_stringNull()
        {
            var xml = new XmlDocument();
            xml.LoadXml("<foo>bar</foo>");

            Assert.Throws<ArgumentNullException>(() => xml.CreateNavigator().Evaluate<string>(null));
        }

        [Fact]
        public void op_Evaluate_XPathNavigator_stringNull_IXmlNamespaceResolver()
        {
            var xml = new XmlDocument();
            xml.LoadXml("<foo xmlns='urn:example'>bar</foo>");

            var namespaces = new XmlNamespaceManager(xml.NameTable);
            namespaces.AddNamespace("x", "urn:example");

            Assert.Throws<ArgumentNullException>(() => xml.CreateNavigator().Evaluate<string>(null, namespaces));
        }

        [Fact]
        public void op_Evaluate_XPathNavigator_string_IXmlNamespaceResolver()
        {
            var xml = new XmlDocument();
            xml.LoadXml("<foo xmlns='urn:example'>bar</foo>");

            var namespaces = new XmlNamespaceManager(xml.NameTable);
            namespaces.AddNamespace("x", "urn:example");

            Assert.True(xml.CreateNavigator().Evaluate<bool>("1=count(/x:foo[text()='bar'])", namespaces));
        }
    }
}