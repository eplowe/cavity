namespace Cavity
{
    using Moq;

    using Xunit;

    public sealed class IGetPreviousDateFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IGetPreviousDate>()
                            .IsInterface()
                            .Result);
        }

        [Fact]
        public void op_Month()
        {
            var expected = Date.Today;

            var mock = new Mock<IGetPreviousDate>();
            mock
                .Setup(x => x.Month())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Month();

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Month_int()
        {
            var expected = Date.Today;

            var mock = new Mock<IGetPreviousDate>();
            mock
                .Setup(x => x.Month(12))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Month(12);

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Year()
        {
            var expected = Date.Today;

            var mock = new Mock<IGetPreviousDate>();
            mock
                .Setup(x => x.Year())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Year();

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Year_MonthOfYear_int()
        {
            var expected = Date.Today;

            var mock = new Mock<IGetPreviousDate>();
            mock
                .Setup(x => x.Year(MonthOfYear.December, 31))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Year(MonthOfYear.December, 31);

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Year_int_int()
        {
            var expected = Date.Today;

            var mock = new Mock<IGetPreviousDate>();
            mock
                .Setup(x => x.Year(12, 31))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Year(12, 31);

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Day_get()
        {
            var expected = Date.Today;

            var mock = new Mock<IGetPreviousDate>();
            mock
                .SetupGet(x => x.Day)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Day;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Friday_get()
        {
            var expected = Date.Today;

            var mock = new Mock<IGetPreviousDate>();
            mock
                .SetupGet(x => x.Friday)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Friday;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Monday_get()
        {
            var expected = Date.Today;

            var mock = new Mock<IGetPreviousDate>();
            mock
                .SetupGet(x => x.Monday)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Monday;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Saturday_get()
        {
            var expected = Date.Today;

            var mock = new Mock<IGetPreviousDate>();
            mock
                .SetupGet(x => x.Saturday)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Saturday;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Sunday_get()
        {
            var expected = Date.Today;

            var mock = new Mock<IGetPreviousDate>();
            mock
                .SetupGet(x => x.Sunday)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Sunday;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Thursday_get()
        {
            var expected = Date.Today;

            var mock = new Mock<IGetPreviousDate>();
            mock
                .SetupGet(x => x.Thursday)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Thursday;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Tuesday_get()
        {
            var expected = Date.Today;

            var mock = new Mock<IGetPreviousDate>();
            mock
                .SetupGet(x => x.Tuesday)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Tuesday;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Wednesday_get()
        {
            var expected = Date.Today;

            var mock = new Mock<IGetPreviousDate>();
            mock
                .SetupGet(x => x.Wednesday)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Wednesday;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Week_get()
        {
            var expected = Date.Today;

            var mock = new Mock<IGetPreviousDate>();
            mock
                .SetupGet(x => x.Week)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Week;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }
    }
}