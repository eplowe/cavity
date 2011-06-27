namespace Cavity.Models
{
    using System.Collections.Generic;
    using Moq;
    using Xunit;

    public sealed class IFormatAddressFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IFormatAddress>()
                            .IsInterface()
                            .Result);
        }

        [Fact]
        public void op_ToString_IEnumerableAddressLine()
        {
            const string expected = "example";
            var address = new Mock<IEnumerable<IAddressLine>>().Object;
            var obj = new Mock<IFormatAddress>();
            obj
                .Setup(x => x.ToString(address))
                .Returns(expected)
                .Verifiable();

            var actual = obj.Object.ToString(address);

            Assert.Equal(expected, actual);

            obj.VerifyAll();
        }
    }
}