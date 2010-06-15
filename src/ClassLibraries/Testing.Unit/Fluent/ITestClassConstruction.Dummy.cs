namespace Cavity.Fluent
{
    using System;

    public class ITestClassConstructionDummy : ITestClassConstruction
    {
        public ITestType HasDefaultConstructor()
        {
            throw new NotSupportedException();
        }

        public ITestType NoDefaultConstructor()
        {
            throw new NotSupportedException();
        }
    }
}