namespace Cavity.Fluent
{
    using System;

    public class ITestObjectConstructionDummy : ITestObjectConstruction
    {
        public ITestObject HasDefaultConstructor()
        {
            throw new NotSupportedException();
        }

        public ITestObject NoDefaultConstructor()
        {
            throw new NotSupportedException();
        }
    }
}