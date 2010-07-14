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

        public object Expected
        {
            get;
            set;
        }

        public override bool Check()
        {
            if (!Equals(typeof(T), Property.PropertyType))
            {
                throw new TestException(string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.PropertyGetterTestOfTException_Message,
                    Property.PropertyType,
                    typeof(T)));
            }

            return true;
        }
    }
}