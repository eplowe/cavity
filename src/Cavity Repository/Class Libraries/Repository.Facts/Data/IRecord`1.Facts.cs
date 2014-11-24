namespace Cavity.Data
{
    using Moq;
    using Xunit;

    public sealed class IRecordOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IRecord<int>>()
                            .IsInterface()
                            .Implements<IRecord>()
                            .Result);
        }

        [Fact]
        public void prop_Value_get()
        {
            const int expected = 123;

            var mock = new Mock<IRecord<int>>();
            mock
                .SetupGet(x => x.Value)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Value;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Value_set()
        {
            const int value = 123;

            var mock = new Mock<IRecord<int>>();
            mock
                .SetupSet(x => x.Value = value)
                .Verifiable();

            mock.Object.Value = value;

            mock.VerifyAll();
        }
    }
}