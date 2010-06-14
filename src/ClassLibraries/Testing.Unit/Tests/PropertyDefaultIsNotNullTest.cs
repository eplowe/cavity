namespace Cavity.Tests
{
    using System;
    using System.Globalization;
    using System.Reflection;

    public class PropertyDefaultIsNotNullTest : PropertyTest
    {
        public PropertyDefaultIsNotNullTest(PropertyInfo property)
            : base(property)
        {
        }

        public override bool Check()
        {
            if (object.Equals(
                null,
                this.Property.GetGetMethod(true).Invoke(Activator.CreateInstance(this.Property.ReflectedType), null)))
            {
                throw new TestException(string.Format(CultureInfo.InvariantCulture, "{0}.{1} was unexpectedly null.", this.Property.ReflectedType.Name, this.Property.Name));
            }

            return true;
        }
    }
}