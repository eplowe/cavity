namespace Cavity
{
    using Moq;
    using Xunit;

    public sealed class IGetPreviousMonthFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IGetPreviousMonth>().IsInterface()
                                                                 .Result);
        }

        [Fact]
        public void prop_April_get()
        {
            var expected = Date.Today.LocalTime;

            var mock = new Mock<IGetPreviousMonth>();
            mock
                .SetupGet(x => x.April)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.April;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_August_get()
        {
            var expected = Date.Today.LocalTime;

            var mock = new Mock<IGetPreviousMonth>();
            mock
                .SetupGet(x => x.August)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.August;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_December_get()
        {
            var expected = Date.Today.LocalTime;

            var mock = new Mock<IGetPreviousMonth>();
            mock
                .SetupGet(x => x.December)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.December;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_February_get()
        {
            var expected = Date.Today.LocalTime;

            var mock = new Mock<IGetPreviousMonth>();
            mock
                .SetupGet(x => x.February)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.February;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_January_get()
        {
            var expected = Date.Today.LocalTime;

            var mock = new Mock<IGetPreviousMonth>();
            mock
                .SetupGet(x => x.January)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.January;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_July_get()
        {
            var expected = Date.Today.LocalTime;

            var mock = new Mock<IGetPreviousMonth>();
            mock
                .SetupGet(x => x.July)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.July;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_June_get()
        {
            var expected = Date.Today.LocalTime;

            var mock = new Mock<IGetPreviousMonth>();
            mock
                .SetupGet(x => x.June)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.June;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_March_get()
        {
            var expected = Date.Today.LocalTime;

            var mock = new Mock<IGetPreviousMonth>();
            mock
                .SetupGet(x => x.March)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.March;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_May_get()
        {
            var expected = Date.Today.LocalTime;

            var mock = new Mock<IGetPreviousMonth>();
            mock
                .SetupGet(x => x.May)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.May;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_November_get()
        {
            var expected = Date.Today.LocalTime;

            var mock = new Mock<IGetPreviousMonth>();
            mock
                .SetupGet(x => x.November)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.November;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_October_get()
        {
            var expected = Date.Today.LocalTime;

            var mock = new Mock<IGetPreviousMonth>();
            mock
                .SetupGet(x => x.October)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.October;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_September_get()
        {
            var expected = Date.Today.LocalTime;

            var mock = new Mock<IGetPreviousMonth>();
            mock
                .SetupGet(x => x.September)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.September;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }
    }
}