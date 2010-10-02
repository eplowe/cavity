namespace Cavity.Tests
{
    using System;
    using System.Reflection;
    using System.Xml.Serialization;
    using Cavity.Properties;

    public sealed class XmlElementTest : MemberTestBase
    {
        public XmlElementTest(MemberInfo info)
            : base(info)
        {
        }

        public string ElementName { get; set; }

        public string Namespace { get; set; }

        public override bool Check()
        {
            var attribute = Attribute.GetCustomAttribute(Member, typeof(XmlElementAttribute), false) as XmlElementAttribute;
            if (null == attribute)
            {
                throw new UnitTestException(Resources.XmlElementDecorationTestException_Message1.FormatWith(Member.Name));
            }

            if (ElementName != attribute.ElementName)
            {
                throw new UnitTestException(Resources.XmlElementDecorationTestException_Message2.FormatWith(Member.Name, ElementName));
            }

            if (Namespace != attribute.Namespace)
            {
                throw new UnitTestException(Resources.XmlElementDecorationTestException_Message3.FormatWith(Member.Name, ElementName, Namespace));
            }

            return true;
        }
    }
}