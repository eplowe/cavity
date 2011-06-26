namespace Cavity.Models
{
    using System;
    using Cavity;
    using Moq;
    using Xunit;

    public sealed class IParseAddressLineFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IParseAddressLine>()
                .IsInterface()
                .Result);
        }

        [Fact]
        public void op_FromString_string()
        {
            var expected = new Mock<IAddressLine>().Object;
            var obj = new Mock<IParseAddressLine>();
            obj
                .Setup(x => x.FromString("example"))
                .Returns(expected)
                .Verifiable();

            var actual = obj.Object.FromString("example");

            Assert.Same(expected, actual);

            obj.VerifyAll();
        }
    }
}