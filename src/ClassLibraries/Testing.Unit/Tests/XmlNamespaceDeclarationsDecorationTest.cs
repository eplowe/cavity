namespace Cavity.Tests
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Xml.Serialization;
    using Cavity.Properties;

    public sealed class XmlNamespaceDeclarationsDecorationTest : AttributePropertyTest
    {
        public XmlNamespaceDeclarationsDecorationTest(MemberInfo info)
            : base(info)
        {
            this.MemberInfo = info;
        }

        public override bool Check()
        {
            XmlNamespaceDeclarationsAttribute attribute = Attribute.GetCustomAttribute(this.MemberInfo, typeof(XmlNamespaceDeclarationsAttribute), false) as XmlNamespaceDeclarationsAttribute;

            if (null == attribute)
            {
                string message = string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.XmlNamespaceDeclarationsDecorationTestException_Message,
                    this.MemberInfo.Name);
                throw new TestException(message);
            }

            return true;
        }
    }
}