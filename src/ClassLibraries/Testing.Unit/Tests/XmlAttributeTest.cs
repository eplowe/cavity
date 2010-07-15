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

        public string AttributeName { get; set; }

        public string Namespace { get; set; }

        public override bool Check()
        {
            var attribute = Attribute.GetCustomAttribute(Member, typeof(XmlAttributeAttribute), false) as XmlAttributeAttribute;
            if (null == attribute)
            {
                throw new TestException(string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlAttributeDecorationTestException_Message1,
                    Member.Name));
            }
            else if (AttributeName != attribute.AttributeName)
            {
                throw new TestException(string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlAttributeDecorationTestException_Message2,
                    Member.Name,
                    AttributeName));
            }
            else if (Namespace != attribute.Namespace)
            {
                throw new TestException(string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlAttributeDecorationTestException_Message3,
                    Member.Name,
                    AttributeName,
                    Namespace));
            }

            return true;
        }
    }
}