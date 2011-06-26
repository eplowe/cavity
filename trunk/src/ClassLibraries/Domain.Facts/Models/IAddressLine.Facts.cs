namespace Cavity.Models
{
    using Moq;
    using Xunit;

    public sealed class IAddressLineFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IAddressLine>()
                            .IsInterface()
                            .Result);
        }

        [Fact]
        public void prop_Original()
        {
            const string expected = "example";
            var obj = new Mock<IAddressLine>();
            obj
                .SetupGet(x => x.Original)
                .Returns(expected)
                .Verifiable();

            var actual = obj.Object.Original;

            Assert.Equal(expected, actual);

            obj.VerifyAll();
        }

        [Fact]
        public void prop_Value()
        {
            var expected = new object();
            var obj = new Mock<IAddressLine>();
            obj
                .SetupGet(x => x.Value)
                .Returns(expected)
                .Verifiable();

            var actual = obj.Object.Value;

            Assert.Same(expected, actual);

            obj.VerifyAll();
        }
    }
}