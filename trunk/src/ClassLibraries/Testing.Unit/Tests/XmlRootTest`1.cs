namespace Cavity.Tests
{
    using System;
    using System.Xml.Serialization;
    using Cavity.Fluent;
    using Cavity.Properties;

    public sealed class XmlRootTest<T> : ITestExpectation
    {
        public XmlRootTest(string elementName)
            : this(elementName, null)
        {
        }

        public XmlRootTest(string elementName,
                           string @namespace)
        {
            ElementName = elementName;
            Namespace = @namespace;
        }

        public string ElementName { get; set; }

        public string Namespace { get; set; }

        public bool Check()
        {
            var attribute = Attribute.GetCustomAttribute(typeof(T), typeof(XmlRootAttribute), false) as XmlRootAttribute;
            if (null == attribute)
            {
                throw new UnitTestException(Resources.XmlRootDecorationTestException_UndecoratedMessage.FormatWith(typeof(T).Name));
            }

            if (ElementName != attribute.ElementName)
            {
                throw new UnitTestException(Resources.XmlRootDecorationTestException_NameMessage.FormatWith(typeof(T).Name, ElementName));
            }

            if (Namespace != attribute.Namespace)
            {
                throw new UnitTestException(Resources.XmlRootDecorationTestException_NamespaceMessage.FormatWith(typeof(T).Name, ElementName, Namespace));
            }

            return true;
        }
    }
}