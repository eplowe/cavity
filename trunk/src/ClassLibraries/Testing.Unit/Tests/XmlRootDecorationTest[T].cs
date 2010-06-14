namespace Cavity.Tests
{
    using System;
    using System.Globalization;
    using System.Xml.Serialization;
    using Cavity.Fluent;
    using Cavity.Properties;

    public class XmlRootDecorationTest<T> : ITestExpectation
    {
        public XmlRootDecorationTest(string name)
            : this(name, null as string)
        {
        }

        public XmlRootDecorationTest(string name, string @namespace)
        {
            this.Name = name;
            this.Namespace = @namespace;
        }

        public string Name
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
            string message = null;
            if (null == attribute)
            {
                message = string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlRootDecorationTestException_UndecoratedMessage,
                    typeof(T).Name);
                throw new TestException(message);
            }
            else if (this.Name != attribute.ElementName)
            {
                message = string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlRootDecorationTestException_NameMessage,
                    typeof(T).Name,
                    this.Name);
                throw new TestException(message);
            }
            else if (this.Namespace != attribute.Namespace)
            {
                message = string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlRootDecorationTestException_NamespaceMessage,
                    typeof(T).Name,
                    this.Name,
                    this.Namespace);
                throw new TestException(message);
            }

            return true;
        }
    }
}