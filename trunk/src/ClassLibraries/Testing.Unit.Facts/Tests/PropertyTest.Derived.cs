namespace Cavity.Tests
{
    using System;
    using System.Reflection;

    public class DerivedPropertyTest : PropertyTest
    {
        public DerivedPropertyTest(PropertyInfo property)
            : base(property)
        {
        }

        public object Expected
        {
            get;
            set;
        }

        public override bool Check()
        {
            throw new NotImplementedException();
        }
    }
}