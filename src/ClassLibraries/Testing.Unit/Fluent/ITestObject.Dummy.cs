namespace Cavity.Fluent
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    public class ITestObjectDummy : ITestObject
    {
        public bool Result
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Inference brings no benefit here.")]
        public ITestObject Implements<TInterface>()
        {
            throw new NotSupportedException();
        }
    }
}