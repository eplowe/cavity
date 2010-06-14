namespace Cavity.Tests
{
    using System;
    using System.Reflection;

    public sealed class PropertySetterTest : PropertyTest
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
                    Activator.CreateInstance(type),
                    new object[] { this.Value });
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