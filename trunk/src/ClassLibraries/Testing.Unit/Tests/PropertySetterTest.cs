namespace Cavity.Tests
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using Cavity.Properties;

    public sealed class PropertySetterTest : PropertyTestBase
    {
        public PropertySetterTest(PropertyInfo property, object value)
            : base(property)
        {
            this.Value = value;
        }

        public object Value
        {
            get;
            set;
        }

        public Type ExpectedException
        {
            get;
            set;
        }

        public override bool Check()
        {
            try
            {
                Type type = this.Property.DeclaringType;
                if (type.IsAbstract)
                {
                    type = this.Property.ReflectedType;
                }

                this.Property.GetSetMethod(true).Invoke(
                    Activator.CreateInstance(type, true),
                    new object[] { this.Value });

                if (null != this.ExpectedException)
                {
                    throw new TestException(string.Format(
                        CultureInfo.InvariantCulture,
                        Resources.PropertySetterTestException_Message,
                        this.Property.ReflectedType.Name,
                        this.Property.Name,
                        this.ExpectedException.Name));
                }
            }
            catch (TargetInvocationException exception)
            {
                if (null != this.ExpectedException
                    && !this.ExpectedException.GetType().Equals(exception.InnerException.GetType()))
                {
                }
                else
                {
                    throw;
                }
            }

            return true;
        }
    }
}