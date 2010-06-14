namespace Cavity.Tests
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Xml.Serialization;
    using Cavity.Properties;

    public sealed class XmlNamespaceDeclarationsTest : MemberTest
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
                string message = string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlNamespaceDeclarationsDecorationTestException_Message,
                    this.Member.Name);
                throw new TestException(message);
            }

            return true;
        }
    }
}