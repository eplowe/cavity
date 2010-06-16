namespace Cavity.Tests
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using Cavity.Properties;

    public class PropertyGetterTest : PropertyTestBase
    {
        public PropertyGetterTest(PropertyInfo property, object expected)
            : base(property)
        {
            this.Expected = expected;
        }

        public object Expected
        {
            get;
            set;
        }

        public override bool Check()
        {
            if (!object.Equals(
                this.Expected,
                this.Property.GetGetMethod(true).Invoke(Activator.CreateInstance(this.Property.ReflectedType), null)))
            {
                string message = string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.PropertyGetterTestException_Message,
                    this.Property.ReflectedType.Name,
                    this.Property.Name);
                throw new TestException(message);
            }

            return true;
        }
    }
}