namespace Cavity.Tests
{
    using System;
    using System.Reflection;

    public sealed class DerivedPropertyTest : PropertyTestBase
    {
        public DerivedPropertyTest(PropertyInfo property)
            : base(property)
        {
        }

        public override bool Check()
        {
            throw new NotImplementedException();
        }
    }
}