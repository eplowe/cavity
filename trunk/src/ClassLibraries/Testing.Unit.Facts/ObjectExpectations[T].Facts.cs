namespace Cavity
{
    using Cavity.Fluent;
    using Cavity.Types;
    using Xunit;

    public class ObjectExpectationsOfTFacts
    {
        [Fact]
        public void is_ITestObjectStyle()
        {
            Assert.IsAssignableFrom<ITestObjectStyle>(new ObjectExpectations<object>());
        }

        [Fact]
        public void is_ITestObject()
        {
            Assert.IsAssignableFrom<ITestObject>(new ObjectExpectations<object>());
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new ObjectExpectations<object>());
        }

        [Fact]
        public void prop_Result()
        {
            Assert.True(new ObjectExpectations<AbstractBaseClass1>()
                .DerivesFrom<object>()
                .IsAbstractBaseClass()
                .Result);
        }
    }
}