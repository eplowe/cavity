namespace Cavity.Tests
{
    using System.Reflection;
    using Cavity.Fluent;

    public abstract class PropertyTestBase : ITestExpectation
    {
        protected PropertyTestBase(PropertyInfo property)
        {
            Property = property;
        }

        public PropertyInfo Property { get; set; }

        public abstract bool Check();
    }
}