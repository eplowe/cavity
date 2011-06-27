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
        public void prop_Data()
        {
            var expected = new object();
            var obj = new Mock<IAddressLine>();
            obj
                .SetupGet(x => x.Data)
                .Returns(expected)
                .Verifiable();

            var actual = obj.Object.Data;

            Assert.Same(expected, actual);

            obj.VerifyAll();
        }

        [Fact]
        public void op_ToString_IFormatAddress()
        {
            const string expected = "example";

            var renderer = new Mock<IFormatAddress>().Object;
            var obj = new Mock<IAddressLine>();
            obj
                .Setup(x => x.ToString(renderer))
                .Returns(expected)
                .Verifiable();

            var actual = obj.Object.ToString(renderer);

            Assert.Equal(expected, actual);

            obj.VerifyAll();
        }
    }
}