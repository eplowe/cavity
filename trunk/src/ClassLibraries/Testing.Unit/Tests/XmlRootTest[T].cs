namespace Cavity.Tests
{
    using System;
    using System.Globalization;
    using System.Xml.Serialization;
    using Cavity.Fluent;
    using Cavity.Properties;

    public class XmlRootTest<T> : ITestExpectation
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

        public virtual bool Check()
        {
            XmlRootAttribute attribute = Attribute.GetCustomAttribute(typeof(T), typeof(XmlRootAttribute), false) as XmlRootAttribute;
            string message = null;
            if (null == attribute)
            {
                message = string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlRootDecorationTestException_UndecoratedMessage,
                    typeof(T).Name);
                throw new TestException(message);
            }
            else if (this.ElementName != attribute.ElementName)
            {
                message = string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlRootDecorationTestException_NameMessage,
                    typeof(T).Name,
                    this.ElementName);
                throw new TestException(message);
            }
            else if (this.Namespace != attribute.Namespace)
            {
                message = string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlRootDecorationTestException_NamespaceMessage,
                    typeof(T).Name,
                    this.ElementName,
                    this.Namespace);
                throw new TestException(message);
            }

            return true;
        }
    }
}