namespace Cavity.Tests
{
    using System;
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
                throw new UnitTestException(Resources.XmlAttributeDecorationTestException_Message1.FormatWith(Member.Name));
            }

            if (AttributeName != attribute.AttributeName)
            {
                throw new UnitTestException(Resources.XmlAttributeDecorationTestException_Message2.FormatWith(Member.Name, AttributeName));
            }

            if (Namespace != attribute.Namespace)
            {
                throw new UnitTestException(Resources.XmlAttributeDecorationTestException_Message3.FormatWith(Member.Name, AttributeName, Namespace));
            }

            return true;
        }
    }
}