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
            this.Member = info;
        }

        public override bool Check()
        {
            XmlNamespaceDeclarationsAttribute attribute = Attribute.GetCustomAttribute(this.Member, typeof(XmlNamespaceDeclarationsAttribute), false) as XmlNamespaceDeclarationsAttribute;

            if (null == attribute)
            {
                throw new TestException(string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlNamespaceDeclarationsDecorationTestException_Message,
                    this.Member.Name));
            }

            return true;
        }
    }
}