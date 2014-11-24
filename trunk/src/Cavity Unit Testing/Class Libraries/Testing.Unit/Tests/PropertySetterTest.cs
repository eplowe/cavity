namespace Cavity.Tests
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using Cavity.Properties;

    public sealed class PropertySetterTest : PropertyTestBase
    {
        public PropertySetterTest(PropertyInfo property,
                                  object value)
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

                // ReSharper disable PossibleNullReferenceException
                if (type.IsAbstract)
                {
                    // ReSharper restore PossibleNullReferenceException
                    type = Property.ReflectedType;
                }

                var parameters = new[]
                                     {
                                         Value
                                     };
                Property.GetSetMethod(true).Invoke(
                                                   Activator.CreateInstance(type, true),
                                                   parameters);

                if (null != ExpectedException)
                {
                    throw new UnitTestException(string.Format(CultureInfo.InvariantCulture, Resources.PropertySetterTestException_Message, Property.ReflectedType.Name, Property.Name, ExpectedException.Name));
                }
            }
            catch (TargetInvocationException exception)
            {
                if (null == ExpectedException)
                {
                    throw;
                }

                // ReSharper disable PossibleMistakenCallToGetType.2
                if (ExpectedException.GetType() == exception.InnerException.GetType())
                {
                    // ReSharper restore PossibleMistakenCallToGetType.2
                    throw;
                }
            }

            return true;
        }
    }
}