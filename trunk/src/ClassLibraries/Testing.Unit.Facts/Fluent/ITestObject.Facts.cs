namespace Cavity.Fluent
{
    using System;
    using Cavity.Types;
    using Xunit;

    public class ITestObjectFacts
    {
        [Fact]
        public void typedef()
        {
            Assert.True(typeof(ITestObject).IsInterface);
        }

        [Fact]
        public void ITestObject_Result_get()
        {
            try
            {
                bool value = (new ITestObjectDummy() as ITestObject).Result;
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void ITestObject_Add_ITestExpectation()
        {
            try
            {
                ITestObject value = (new ITestObjectDummy() as ITestObject).Add(new ITestExpectationDummy());
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void ITestObject_ImplementsOfT()
        {
            try
            {
                ITestObject value = (new ITestObjectDummy() as ITestObject).Implements<Interface1>();
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void ITestObject_IsDecoratedWithOfT()
        {
            try
            {
                ITestObject value = (new ITestObjectDummy() as ITestObject).IsDecoratedWith<Attribute1>();
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void ITestObject_Serializable()
        {
            try
            {
                ITestObject value = (new ITestObjectDummy() as ITestObject).Serializable();
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void ITestObject_XmlRoot_string()
        {
            try
            {
                ITestObject value = (new ITestObjectDummy() as ITestObject).XmlRoot("name");
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void ITestObject_XmlRoot_string_string()
        {
            try
            {
                ITestObject value = (new ITestObjectDummy() as ITestObject).XmlRoot("name", "namespace");
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void ITestObject_IsNotDecorated()
        {
            try
            {
                ITestObject value = (new ITestObjectDummy() as ITestObject).IsNotDecorated();
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}