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
            Value = value;
        }

        public Type ExpectedException { get; set; }

        public object Value { get; set; }

        public override bool Check()
        {
            try
            {
                var type = Property.DeclaringType;
                if (type.IsAbstract)
                {
                    type = Property.ReflectedType;
                }

                Property.GetSetMethod(true).Invoke(
                    Activator.CreateInstance(type, true),
                    new[]
                    {
                        Value
                    });

                if (null != ExpectedException)
                {
                    throw new TestException(string.Format(
                        CultureInfo.InvariantCulture,
                        Resources.PropertySetterTestException_Message,
                        Property.ReflectedType.Name,
                        Property.Name,
                        ExpectedException.Name));
                }
            }
            catch (TargetInvocationException exception)
            {
                if (null != ExpectedException
                    &&
                    !ExpectedException.GetType().Equals(exception.InnerException.GetType()))
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