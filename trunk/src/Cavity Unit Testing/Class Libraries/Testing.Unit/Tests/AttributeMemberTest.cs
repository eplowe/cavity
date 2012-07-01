namespace Cavity.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
#if !NET20
    using System.Linq;
#endif
    using System.Reflection;

    using Cavity.Properties;

    public sealed class AttributeMemberTest : MemberTestBase
    {
        private static readonly IList<string> _ignore = new List<string>
        {
            "BrowsableAttribute",
            "CategoryAttribute",
            "CompilerGeneratedAttribute",
            "DebuggerDisplayAttribute",
            "DebuggerStepThroughAttribute",
            "DescriptionAttribute",
            "DynamicAttribute",
            "EditorAttribute",
            "ExcludeFromCodeCoverageAttribute",
            "SuppressMessageAttribute"
        };

        public AttributeMemberTest(MemberInfo member, 
                                   Type attribute)
            : base(member)
        {
            Attribute = attribute;
        }

        public Type Attribute { get; set; }

        public override bool Check()
        {
            if (null == Attribute)
            {
                if (0 != Member
                    .GetCustomAttributes(false)
                    .Count(x => !_ignore.Contains(x.GetType().Name)))
                {
                    throw new UnitTestException(string.Format(CultureInfo.InvariantCulture, Resources.DecorationTestException_UnexpectedMessage, Member.Name));
                }
            }
            else
            {
                if (0 == System.Attribute.GetCustomAttributes(Member, Attribute, false).Length)
                {
                    throw new UnitTestException(string.Format(CultureInfo.InvariantCulture, Resources.DecorationTestException_MissingMessage, Member.Name, Attribute.Name));
                }
            }

            return true;
        }
    }
}