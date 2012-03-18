namespace Cavity.Tests
{
    using System.Globalization;
    using System.Reflection;

    using Cavity.Properties;

    public sealed class PropertyGetterTest<T> : PropertyTestBase
    {
        public PropertyGetterTest(PropertyInfo property)
            : base(property)
        {
        }

        public object Expected { get; set; }

        public override bool Check()
        {
            if (typeof(T) == Property.PropertyType)
            {
                return true;
            }

            throw new UnitTestException(string.Format(CultureInfo.InvariantCulture, Resources.PropertyGetterTestOfTException_Message, Property.PropertyType, typeof(T)));
        }
    }
}