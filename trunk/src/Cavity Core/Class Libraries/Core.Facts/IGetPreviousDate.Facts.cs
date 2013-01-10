namespace Cavity
{
    using Moq;

    using Xunit;

    public sealed class IGetPreviousDateFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IGetPreviousDate>().IsInterface()
                                                                .Implements<IGetPreviousMonth>()
                                                                .Implements<IGetPreviousWeekday>()
                                                                .Result);
        }

        [Fact]
        public void prop_Day_get()
        {
            var expected = Date.Today.LocalTime;

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
        public void prop_Month_get()
        {
            var expected = Date.Today.LocalTime;

            var mock = new Mock<IGetPreviousDate>();
            mock
                .SetupGet(x => x.Month)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Month;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Week_get()
        {
            var expected = Date.Today.LocalTime;

            var mock = new Mock<IGetPreviousDate>();
            mock
                .SetupGet(x => x.Week)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Week;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Year_get()
        {
            var expected = Date.Today.LocalTime;

            var mock = new Mock<IGetPreviousDate>();
            mock
                .SetupGet(x => x.Year)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Year;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }
    }
}