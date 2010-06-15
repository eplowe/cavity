namespace Cavity.Fluent
{
    using System;

    public class ITestClassSealedDummy : ITestClassSealed
    {
        public ITestClassConstruction IsSealed()
        {
            throw new NotSupportedException();
        }

        public ITestClassConstruction IsUnsealed()
        {
            throw new NotSupportedException();
        }
    }
}