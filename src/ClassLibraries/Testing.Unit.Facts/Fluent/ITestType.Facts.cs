namespace Cavity.Fluent
{
    using System;
    using Cavity.Types;
    using Xunit;

    public sealed class ITestTypeFacts
    {
        [Fact]
        public void typedef()
        {
            Assert.True(typeof(ITestType).IsInterface);
        }

        [Fact]
        public void ITestType_Result_get()
        {
            try
            {
                bool value = (new ITestTypeDummy() as ITestType).Result;
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void ITestType_Add_ITestExpectation()
        {
            try
            {
                ITestType value = (new ITestTypeDummy() as ITestType).Add(new ITestExpectationDummy());
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void ITestType_ImplementsOfT()
        {
            try
            {
                ITestType value = (new ITestTypeDummy() as ITestType).Implements<Interface1>();
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void ITestType_IsDecoratedWithOfT()
        {
            try
            {
                ITestType value = (new ITestTypeDummy() as ITestType).IsDecoratedWith<Attribute1>();
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void ITestType_IsNotDecorated()
        {
            try
            {
                ITestType value = (new ITestTypeDummy() as ITestType).IsNotDecorated();
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void ITestType_Serializable()
        {
            try
            {
                ITestType value = (new ITestTypeDummy() as ITestType).Serializable();
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void ITestType_XmlRoot_string()
        {
            try
            {
                ITestType value = (new ITestTypeDummy() as ITestType).XmlRoot("name");
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void ITestType_XmlRoot_string_string()
        {
            try
            {
                ITestType value = (new ITestTypeDummy() as ITestType).XmlRoot("name", "namespace");
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}