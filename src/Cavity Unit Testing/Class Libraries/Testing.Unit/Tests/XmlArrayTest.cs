namespace Cavity.Tests
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Xml.Serialization;
    using Cavity.Properties;

    public sealed class XmlArrayTest : MemberTestBase
    {
        public XmlArrayTest(MemberInfo info)
            : base(info)
        {
        }

        public string ArrayElementName { get; set; }

        public string ArrayItemElementName { get; set; }

        public override bool Check()
        {
            CheckArray();
            CheckArrayItems();

            return true;
        }

        private void CheckArray()
        {
            var attribute = Attribute.GetCustomAttribute(Member, typeof(XmlArrayAttribute), false) as XmlArrayAttribute;
            if (null == attribute)
            {
                throw new UnitTestException(string.Format(CultureInfo.InvariantCulture, Resources.XmlArrayDecorationTestException_Message1, Member.Name));
            }

            if (ArrayElementName != attribute.ElementName)
            {
                throw new UnitTestException(string.Format(CultureInfo.InvariantCulture, Resources.XmlArrayDecorationTestException_Message2, Member.Name, ArrayElementName));
            }
        }

        private void CheckArrayItems()
        {
            var attributes = Attribute.GetCustomAttributes(Member, typeof(XmlArrayItemAttribute), false).Cast<XmlArrayItemAttribute>().ToList();
            if (0 == attributes.Count)
            {
                throw new UnitTestException(string.Format(CultureInfo.InvariantCulture, Resources.XmlArrayDecorationTestException_Message3, Member.Name));
            }

            if (0 == attributes.Count(x => x.ElementName == ArrayItemElementName))
            {
                throw new UnitTestException(string.Format(CultureInfo.InvariantCulture, Resources.XmlArrayDecorationTestException_Message4, Member.Name, ArrayItemElementName));
            }
        }
    }
}