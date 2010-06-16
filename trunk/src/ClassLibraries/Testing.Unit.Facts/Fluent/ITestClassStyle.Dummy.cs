namespace Cavity.Fluent
{
    using System;

    public class ITestClassStyleDummy : ITestClassStyle
    {
        public ITestType IsAbstractBaseClass()
        {
            throw new NotSupportedException();
        }

        public ITestClassSealed IsConcreteClass()
        {
            throw new NotSupportedException();
        }
    }
}