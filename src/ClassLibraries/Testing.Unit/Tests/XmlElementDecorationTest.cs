namespace Cavity.Tests
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Xml.Serialization;
    using Cavity.Properties;

    public sealed class XmlElementDecorationTest : AttributePropertyTest
    {
        public XmlElementDecorationTest(MemberInfo info)
            : base(info)
        {
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

        public override bool Check()
        {
            XmlElementAttribute attribute = Attribute.GetCustomAttribute(this.MemberInfo, typeof(XmlElementAttribute), false) as XmlElementAttribute;
            string message = null;
            if (null == attribute)
            {
                message = string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlElementDecorationTestException_Message1,
                    this.MemberInfo.Name);
                throw new TestException(message);
            }
            else if (this.Name != attribute.ElementName)
            {
                message = string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlElementDecorationTestException_Message2,
                    this.MemberInfo.Name,
                    this.Name);
                throw new TestException(message);
            }
            else if (this.Namespace != attribute.Namespace)
            {
                message = string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlElementDecorationTestException_Message3,
                    this.MemberInfo.Name,
                    this.Name,
                    this.Namespace);
                throw new TestException(message);
            }

            return true;
        }
    }
}