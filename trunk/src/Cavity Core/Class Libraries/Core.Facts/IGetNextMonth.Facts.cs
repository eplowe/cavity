namespace Cavity
{
    using Moq;

    using Xunit;

    public sealed class IGetNextMonthFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IGetNextMonth>()
                            .IsInterface()
                            .Result);
        }

        [Fact]
        public void op_Month()
        {
            var expected = Month.Current;

            var mock = new Mock<IGetNextMonth>();
            mock
                .Setup(x => x.Month())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Month();

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Year()
        {
            var expected = Month.Current;

            var mock = new Mock<IGetNextMonth>();
            mock
                .Setup(x => x.Year())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Year();

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Year_MonthOfYear()
        {
            var expected = Month.Current;

            var mock = new Mock<IGetNextMonth>();
            mock
                .Setup(x => x.Year(MonthOfYear.December))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Year(MonthOfYear.December);

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Year_int()
        {
            var expected = Month.Current;

            var mock = new Mock<IGetNextMonth>();
            mock
                .Setup(x => x.Year(12))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Year(12);

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }
    }
}