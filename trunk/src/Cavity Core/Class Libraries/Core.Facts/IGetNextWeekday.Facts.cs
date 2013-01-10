namespace Cavity
{
    using Moq;

    using Xunit;

    public sealed class IGetNextWeekdayFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IGetNextWeekday>().IsInterface()
                                                               .Result);
        }

        [Fact]
        public void prop_Friday_get()
        {
            var expected = Date.Today.LocalTime;

            var mock = new Mock<IGetNextWeekday>();
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
            var expected = Date.Today.LocalTime;

            var mock = new Mock<IGetNextWeekday>();
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
            var expected = Date.Today.LocalTime;

            var mock = new Mock<IGetNextWeekday>();
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
            var expected = Date.Today.LocalTime;

            var mock = new Mock<IGetNextWeekday>();
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
            var expected = Date.Today.LocalTime;

            var mock = new Mock<IGetNextWeekday>();
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
            var expected = Date.Today.LocalTime;

            var mock = new Mock<IGetNextWeekday>();
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
            var expected = Date.Today.LocalTime;

            var mock = new Mock<IGetNextWeekday>();
            mock
                .SetupGet(x => x.Wednesday)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Wednesday;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }
    }
}