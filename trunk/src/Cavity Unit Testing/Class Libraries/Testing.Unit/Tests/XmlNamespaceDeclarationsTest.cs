namespace Cavity.Tests
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Xml.Serialization;
    using Cavity.Properties;

    public sealed class XmlNamespaceDeclarationsTest : MemberTestBase
    {
        public XmlNamespaceDeclarationsTest(MemberInfo info)
            : base(info)
        {
            Member = info;
        }

        public override bool Check()
        {
            if (null == Attribute.GetCustomAttribute(Member, typeof(XmlNamespaceDeclarationsAttribute), false) as XmlNamespaceDeclarationsAttribute)
            {
                throw new UnitTestException(string.Format(CultureInfo.InvariantCulture, Resources.XmlNamespaceDeclarationsDecorationTestException_Message, Member.Name));
            }

            return true;
        }
    }
}