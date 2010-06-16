namespace Cavity.Tests
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Xml.Serialization;
    using Cavity.Properties;

    public sealed class XmlAttributeTest : MemberTestBase
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
            if (null == attribute)
            {
                throw new TestException(string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlAttributeDecorationTestException_Message1,
                    this.Member.Name));
            }
            else if (this.AttributeName != attribute.AttributeName)
            {
                throw new TestException(string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlAttributeDecorationTestException_Message2,
                    this.Member.Name,
                    this.AttributeName));
            }
            else if (this.Namespace != attribute.Namespace)
            {
                throw new TestException(string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlAttributeDecorationTestException_Message3,
                    this.Member.Name,
                    this.AttributeName,
                    this.Namespace));
            }

            return true;
        }
    }
}