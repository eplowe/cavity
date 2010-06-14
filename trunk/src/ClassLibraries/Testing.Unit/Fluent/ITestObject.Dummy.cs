namespace Cavity.Fluent
{
    using System;

    public class ITestObjectDummy : ITestObject
    {
        public bool Result
        {
            get
            {
                throw new NotSupportedException();
            }
        }
    }
}