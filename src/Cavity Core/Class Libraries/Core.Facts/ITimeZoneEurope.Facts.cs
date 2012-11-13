namespace Cavity
{
    using System;
    using Cavity;

    using Moq;

    using Xunit;

    public sealed class ITimeZoneEuropeFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<ITimeZoneEurope>()
                .IsInterface()
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void prop_CentralEuropeanTime_get()
        {
            var expected = TimeZoneInfo.CreateCustomTimeZone("example", TimeSpan.Zero, string.Empty, string.Empty);

            var mock = new Mock<ITimeZoneEurope>();
            mock
                .SetupGet(x => x.CentralEuropeanTime)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.CentralEuropeanTime;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_BritishTime_get()
        {
            var expected = TimeZoneInfo.CreateCustomTimeZone("example", TimeSpan.Zero, string.Empty, string.Empty);

            var mock = new Mock<ITimeZoneEurope>();
            mock
                .SetupGet(x => x.BritishTime)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.BritishTime;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_EasternEuropeanStandardTime_get()
        {
            var expected = TimeZoneInfo.CreateCustomTimeZone("example", TimeSpan.Zero, string.Empty, string.Empty);

            var mock = new Mock<ITimeZoneEurope>();
            mock
                .SetupGet(x => x.EasternEuropeanStandardTime)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.EasternEuropeanStandardTime;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_GreenwichMeanTime_get()
        {
            var expected = TimeZoneInfo.CreateCustomTimeZone("example", TimeSpan.Zero, string.Empty, string.Empty);

            var mock = new Mock<ITimeZoneEurope>();
            mock
                .SetupGet(x => x.GreenwichMeanTime)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.GreenwichMeanTime;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_RussianStandardTime_get()
        {
            var expected = TimeZoneInfo.CreateCustomTimeZone("example", TimeSpan.Zero, string.Empty, string.Empty);

            var mock = new Mock<ITimeZoneEurope>();
            mock
                .SetupGet(x => x.RussianStandardTime)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.RussianStandardTime;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_WesternEuropeanStandardTime_get()
        {
            var expected = TimeZoneInfo.CreateCustomTimeZone("example", TimeSpan.Zero, string.Empty, string.Empty);

            var mock = new Mock<ITimeZoneEurope>();
            mock
                .SetupGet(x => x.WesternEuropeanStandardTime)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.WesternEuropeanStandardTime;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }
    }
}