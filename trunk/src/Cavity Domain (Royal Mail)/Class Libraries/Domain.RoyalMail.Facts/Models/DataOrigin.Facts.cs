namespace Cavity.Models
{
    using System;
    using Cavity;
    using Moq;
    using Xunit;

    public sealed class DataOriginFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<DataOrigin>()
                .DerivesFrom<object>()
                .IsAbstractBaseClass()
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void prop_Code()
        {
            var category = new Mock<DataOrigin>();
            category.SetupProperty(x => x.Code);

            const char expected = 'x';

            category.Object.Code = expected;
            var actual = category.Object.Code;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Name()
        {
            var category = new Mock<DataOrigin>();
            category.SetupProperty(x => x.Name);

            const string expected = "Example";

            category.Object.Name = expected;
            var actual = category.Object.Name;

            Assert.Equal(expected, actual);
        }
    }
}