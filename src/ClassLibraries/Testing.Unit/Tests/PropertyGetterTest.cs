namespace Cavity.Tests
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using Cavity.Properties;

    public sealed class PropertyGetterTest : PropertyTestBase
    {
        public PropertyGetterTest(PropertyInfo property, object expected)
            : base(property)
        {
            Expected = expected;
        }

        public object Expected
        {
            get;
            set;
        }

        public override bool Check()
        {
            if (!Equals(
                Expected,
                Property.GetGetMethod(true).Invoke(Activator.CreateInstance(Property.ReflectedType, true), null)))
            {
                throw new TestException(string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.PropertyGetterTestException_Message,
                    Property.ReflectedType.Name,
                    Property.Name));
            }

            return true;
        }
    }
}