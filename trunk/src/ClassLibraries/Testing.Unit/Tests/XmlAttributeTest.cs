namespace Cavity.Tests
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Xml.Serialization;
    using Cavity.Properties;

    public sealed class XmlAttributeTest : MemberTest
    {
        public XmlAttributeTest(MemberInfo info)
            : base(info)
        {
        }

        public string AttributeName
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
            XmlAttributeAttribute attribute = Attribute.GetCustomAttribute(this.Member, typeof(XmlAttributeAttribute), false) as XmlAttributeAttribute;
            string message = null;
            if (null == attribute)
            {
                message = string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlAttributeDecorationTestException_Message1,
                    this.Member.Name);
                throw new TestException(message);
            }
            else if (this.AttributeName != attribute.AttributeName)
            {
                message = string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlAttributeDecorationTestException_Message2,
                    this.Member.Name,
                    this.AttributeName);
                throw new TestException(message);
            }
            else if (this.Namespace != attribute.Namespace)
            {
                message = string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlAttributeDecorationTestException_Message3,
                    this.Member.Name,
                    this.AttributeName,
                    this.Namespace);
                throw new TestException(message);
            }

            return true;
        }
    }
}