namespace Cavity
{
    using System;
    using Moq;
    using Xunit;

    public sealed class ITimeZoneNorthAmericaFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<ITimeZoneNorthAmerica>().IsInterface()
                                                                     .IsNotDecorated()
                                                                     .Result);
        }

        [Fact]
        public void prop_AlaskanStandardTime_get()
        {
            var expected = TimeZoneInfo.CreateCustomTimeZone("example", TimeSpan.Zero, string.Empty, string.Empty);

            var mock = new Mock<ITimeZoneNorthAmerica>();
            mock
                .SetupGet(x => x.AlaskanStandardTime)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.AlaskanStandardTime;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_AtlanticStandardTime_get()
        {
            var expected = TimeZoneInfo.CreateCustomTimeZone("example", TimeSpan.Zero, string.Empty, string.Empty);

            var mock = new Mock<ITimeZoneNorthAmerica>();
            mock
                .SetupGet(x => x.AtlanticStandardTime)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.AtlanticStandardTime;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_CentralStandardTime_get()
        {
            var expected = TimeZoneInfo.CreateCustomTimeZone("example", TimeSpan.Zero, string.Empty, string.Empty);

            var mock = new Mock<ITimeZoneNorthAmerica>();
            mock
                .SetupGet(x => x.CentralStandardTime)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.CentralStandardTime;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_EasternStandardTime_get()
        {
            var expected = TimeZoneInfo.CreateCustomTimeZone("example", TimeSpan.Zero, string.Empty, string.Empty);

            var mock = new Mock<ITimeZoneNorthAmerica>();
            mock
                .SetupGet(x => x.EasternStandardTime)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.EasternStandardTime;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_GreenlandStandardTime_get()
        {
            var expected = TimeZoneInfo.CreateCustomTimeZone("example", TimeSpan.Zero, string.Empty, string.Empty);

            var mock = new Mock<ITimeZoneNorthAmerica>();
            mock
                .SetupGet(x => x.GreenlandStandardTime)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.GreenlandStandardTime;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_HawaiianStandardTime_get()
        {
            var expected = TimeZoneInfo.CreateCustomTimeZone("example", TimeSpan.Zero, string.Empty, string.Empty);

            var mock = new Mock<ITimeZoneNorthAmerica>();
            mock
                .SetupGet(x => x.HawaiianStandardTime)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.HawaiianStandardTime;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_MountainStandardTime_get()
        {
            var expected = TimeZoneInfo.CreateCustomTimeZone("example", TimeSpan.Zero, string.Empty, string.Empty);

            var mock = new Mock<ITimeZoneNorthAmerica>();
            mock
                .SetupGet(x => x.MountainStandardTime)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.MountainStandardTime;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_NewfoundlandStandardTime_get()
        {
            var expected = TimeZoneInfo.CreateCustomTimeZone("example", TimeSpan.Zero, string.Empty, string.Empty);

            var mock = new Mock<ITimeZoneNorthAmerica>();
            mock
                .SetupGet(x => x.NewfoundlandStandardTime)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.NewfoundlandStandardTime;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_PacificStandardTime_get()
        {
            var expected = TimeZoneInfo.CreateCustomTimeZone("example", TimeSpan.Zero, string.Empty, string.Empty);

            var mock = new Mock<ITimeZoneNorthAmerica>();
            mock
                .SetupGet(x => x.PacificStandardTime)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.PacificStandardTime;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }
    }
}