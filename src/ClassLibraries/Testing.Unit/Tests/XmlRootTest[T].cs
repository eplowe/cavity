namespace Cavity.Tests
{
    using System;
    using System.Globalization;
    using System.Xml.Serialization;
    using Cavity.Fluent;
    using Cavity.Properties;

    public sealed class XmlRootTest<T> : ITestExpectation
    {
        public XmlRootTest(string elementName)
            : this(elementName, null as string)
        {
        }

        public XmlRootTest(string elementName, string @namespace)
        {
            this.ElementName = elementName;
            this.Namespace = @namespace;
        }

        public string ElementName
        {
            get;
            set;
        }

        public string Namespace
        {
            get;
            set;
        }

        public bool Check()
        {
            XmlRootAttribute attribute = Attribute.GetCustomAttribute(typeof(T), typeof(XmlRootAttribute), false) as XmlRootAttribute;
            if (null == attribute)
            {
                throw new TestException(string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlRootDecorationTestException_UndecoratedMessage,
                    typeof(T).Name));
            }
            else if (this.ElementName != attribute.ElementName)
            {
                throw new TestException(string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlRootDecorationTestException_NameMessage,
                    typeof(T).Name,
                    this.ElementName));
            }
            else if (this.Namespace != attribute.Namespace)
            {
                throw new TestException(string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlRootDecorationTestException_NamespaceMessage,
                    typeof(T).Name,
                    this.ElementName,
                    this.Namespace));
            }

            return true;
        }
    }
}