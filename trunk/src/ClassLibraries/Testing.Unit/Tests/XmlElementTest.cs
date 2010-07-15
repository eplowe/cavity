namespace Cavity.Tests
{
    using System;
    using System.Globalization;
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
                throw new TestException(string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlElementDecorationTestException_Message1,
                    Member.Name));
            }
            else if (ElementName != attribute.ElementName)
            {
                throw new TestException(string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlElementDecorationTestException_Message2,
                    Member.Name,
                    ElementName));
            }
            else if (Namespace != attribute.Namespace)
            {
                throw new TestException(string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlElementDecorationTestException_Message3,
                    Member.Name,
                    ElementName,
                    Namespace));
            }

            return true;
        }
    }
}