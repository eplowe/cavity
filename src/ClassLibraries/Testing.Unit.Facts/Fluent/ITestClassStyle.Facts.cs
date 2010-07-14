namespace Cavity.Fluent
{
    using System;
    using Xunit;

    public sealed class ITestClassStyleFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(typeof(ITestClassStyle).IsInterface);
        }

        [Fact]
        public void ITestClassStyle_IsAbstractBaseClass()
        {
            try
            {
                var value = (new ITestClassStyleDummy() as ITestClassStyle).IsAbstractBaseClass();
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void ITestClassStyle_IsConcreteClass()
        {
            try
            {
                var value = (new ITestClassStyleDummy() as ITestClassStyle).IsConcreteClass();
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}