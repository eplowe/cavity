namespace Cavity.Tests
{
    using System.Reflection;
    using Cavity.Fluent;

    public abstract class PropertyTest : ITestExpectation
    {
        protected PropertyTest(PropertyInfo property)
        {
            this.Property = property;
        }

        public PropertyInfo Property
        {
            get;
            set;
        }

        public abstract bool Check();
    }
}