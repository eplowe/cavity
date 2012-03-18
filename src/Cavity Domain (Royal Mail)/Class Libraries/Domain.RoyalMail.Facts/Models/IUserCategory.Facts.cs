namespace Cavity.Models
{
    using Moq;

    using Xunit;

    public sealed class IUserCategoryFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IUserCategory>()
                            .IsInterface()
                            .Result);
        }

        [Fact]
        public void prop_Code()
        {
            const char expected = 'x';

            var category = new Mock<IUserCategory>();
            category
                .SetupGet(x => x.Code)
                .Returns(expected)
                .Verifiable();

            var actual = category.Object.Code;

            Assert.Equal(expected, actual);

            category.VerifyAll();
        }

        [Fact]
        public void prop_Name()
        {
            const string expected = "Example";

            var category = new Mock<IUserCategory>();
            category
                .SetupGet(x => x.Name)
                .Returns(expected)
                .Verifiable();

            var actual = category.Object.Name;

            Assert.Equal(expected, actual);

            category.VerifyAll();
        }
    }
}