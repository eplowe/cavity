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
                throw new TestException(string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.PropertyGetterTestException_Message,
                    this.Property.ReflectedType.Name,
                    this.Property.Name));
            }

            return true;
        }
    }
}