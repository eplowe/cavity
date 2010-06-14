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

        public ITestObject Add(ITestExpectation expectation)
        {
            throw new NotSupportedException();
        }

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Inference brings no benefit here.")]
        public ITestObject Implements<TInterface>()
        {
            throw new NotSupportedException();
        }

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Inference brings no benefit here.")]
        public ITestObject IsDecoratedWith<TAttribute>()
            where TAttribute : Attribute
        {
            throw new NotSupportedException();
        }

        public ITestObject Serializable()
        {
            throw new NotSupportedException();
        }

        public ITestObject XmlRoot(string elementName)
        {
            throw new NotSupportedException();
        }

        public ITestObject XmlRoot(string elementName, string @namespace)
        {
            throw new NotSupportedException();
        }

        public ITestObject IsNotDecorated()
        {
            throw new NotSupportedException();
        }
    }
}