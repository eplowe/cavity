namespace Cavity.Tests
{
    using System;
    using System.Globalization;
    using System.Reflection;

    using Cavity.Properties;

    public sealed class AttributeUsageTest : MemberTestBase
    {
        public AttributeUsageTest(MemberInfo member, 
                                  AttributeTargets validOn, 
                                  bool allowMultiple, 
                                  bool inherited)
            : base(member)
        {
            ValidOn = validOn;
            AllowMultiple = allowMultiple;
            Inherited = inherited;
        }

        public bool AllowMultiple { get; set; }

        public bool Inherited { get; set; }

        public AttributeTargets ValidOn { get; set; }

        public override bool Check()
        {
            return Check(Attribute.GetCustomAttribute(Member, typeof(AttributeUsageAttribute), false) as AttributeUsageAttribute);
        }

        private bool Check(AttributeUsageAttribute attribute)
        {
            if (null == attribute)
            {
                throw new UnitTestException(string.Format(CultureInfo.InvariantCulture, Resources.DecorationTestException_MissingMessage, Member.Name, "AttributeUsage"));
            }

            if (ValidOn != attribute.ValidOn)
            {
                throw new UnitTestException(string.Format(CultureInfo.InvariantCulture, Resources.AttributeUsage_ValidOn, Member.Name));
            }

            if (AllowMultiple != attribute.AllowMultiple)
            {
                throw new UnitTestException(string.Format(CultureInfo.InvariantCulture, AllowMultiple ? Resources.AttributeUsage_AllowMultipleTrue : Resources.AttributeUsage_AllowMultipleFalse, Member.Name));
            }

            if (Inherited != attribute.Inherited)
            {
                throw new UnitTestException(string.Format(CultureInfo.InvariantCulture, Inherited ? Resources.AttributeUsage_InheritedTrue : Resources.AttributeUsage_InheritedFalse, Member.Name));
            }

            return true;
        }
    }
}