namespace Cavity.Tests
{
    using System;
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
                throw new UnitTestException(Resources.XmlArrayDecorationTestException_Message1.FormatWith(Member.Name));
            }

            if (ArrayElementName != attribute.ElementName)
            {
                throw new UnitTestException(Resources.XmlArrayDecorationTestException_Message2.FormatWith(Member.Name, ArrayElementName));
            }
        }

        private void CheckArrayItems()
        {
            var attribute = Attribute.GetCustomAttribute(Member, typeof(XmlArrayItemAttribute), false) as XmlArrayItemAttribute;
            if (null == attribute)
            {
                throw new UnitTestException(Resources.XmlArrayDecorationTestException_Message3.FormatWith(Member.Name));
            }

            if (ArrayItemElementName != attribute.ElementName)
            {
                throw new UnitTestException(Resources.XmlArrayDecorationTestException_Message4.FormatWith(Member.Name, ArrayItemElementName));
            }
        }
    }
}