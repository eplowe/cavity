namespace Cavity.Tests
{
    using System.Globalization;
    using System.Reflection;
    using Cavity.Properties;

    public class PropertyGetterTest<T> : PropertyTest
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
                string message = string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.PropertyGetterTestOfTException_Message,
                    this.Property.PropertyType,
                    typeof(T));
                throw new TestException(message);
            }

            return true;
        }
    }
}