namespace Cavity.Fluent
{
    using System;

    public class ITestObjectSealedDummy : ITestObjectSealed
    {
        public ITestObjectConstruction IsSealed()
        {
            throw new NotSupportedException();
        }

        public ITestObjectConstruction IsUnsealed()
        {
            throw new NotSupportedException();
        }
    }
}