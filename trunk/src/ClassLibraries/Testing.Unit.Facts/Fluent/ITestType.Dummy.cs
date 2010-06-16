namespace Cavity.Fluent
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    public class ITestTypeDummy : ITestType
    {
        public bool Result
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public ITestType Add(ITestExpectation expectation)
        {
            throw new NotSupportedException();
        }

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Inference brings no benefit here.")]
        public ITestType Implements<TInterface>()
        {
            throw new NotSupportedException();
        }

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Inference brings no benefit here.")]
        public ITestType IsDecoratedWith<TAttribute>()
            where TAttribute : Attribute
        {
            throw new NotSupportedException();
        }

        public ITestType IsNotDecorated()
        {
            throw new NotSupportedException();
        }

        public ITestType Serializable()
        {
            throw new NotSupportedException();
        }

        public ITestType XmlRoot(string elementName)
        {
            throw new NotSupportedException();
        }

        public ITestType XmlRoot(string elementName, string @namespace)
        {
            throw new NotSupportedException();
        }
    }
}