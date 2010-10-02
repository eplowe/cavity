namespace Cavity.Tests
{
    using System;
    using System.Reflection;
    using Cavity.Properties;

    public sealed class PropertyGetterTest : PropertyTestBase
    {
        public PropertyGetterTest(PropertyInfo property, object expected)
            : base(property)
        {
            Expected = expected;
        }

        public object Expected { get; set; }

        public override bool Check()
        {
            if (Equals(
                Expected,
                Property.GetGetMethod(true).Invoke(Activator.CreateInstance(Property.ReflectedType, true), null)))
            {
                return true;
            }

            throw new UnitTestException(Resources.PropertyGetterTestException_Message.FormatWith(Property.ReflectedType.Name, Property.Name));
        }
    }
}