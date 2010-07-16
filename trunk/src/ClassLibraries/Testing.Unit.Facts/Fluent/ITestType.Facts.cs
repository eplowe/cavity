namespace Cavity.Fluent
{
    using System;
    using Cavity.Types;
    using Xunit;

    public sealed class ITestTypeFacts
    {
        [Fact]
        public void ITestType_Add_ITestExpectation()
        {
            try
            {
                var value = (new ITestTypeDummy() as ITestType).Add(new ITestExpectationDummy());
                Assert.NotNull(value);
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
                var value = (new ITestTypeDummy() as ITestType).Implements<IInterface1>();
                Assert.NotNull(value);
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
                var value = (new ITestTypeDummy() as ITestType).IsDecoratedWith<Attribute1>();
                Assert.NotNull(value);
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
                var value = (new ITestTypeDummy() as ITestType).IsNotDecorated();
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void ITestType_Result_get()
        {
            try
            {
                var value = (new ITestTypeDummy() as ITestType).Result;
                Assert.NotNull(value);
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
                var value = (new ITestTypeDummy() as ITestType).Serializable();
                Assert.NotNull(value);
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
                var value = (new ITestTypeDummy() as ITestType).XmlRoot("name");
                Assert.NotNull(value);
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
                var value = (new ITestTypeDummy() as ITestType).XmlRoot("name", "namespace");
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(ITestType).IsInterface);
        }
    }
}