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
            if (!object.Equals(typeof(T), this.Property.PropertyType))
            {
                throw new TestException(string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.PropertyGetterTestOfTException_Message,
                    this.Property.PropertyType,
                    typeof(T)));
            }

            return true;
        }
    }
}