namespace Cavity
{
    using System;
    using Cavity;

    using Moq;

    using Xunit;

    public sealed class IGetTimeZoneOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IGetTimeZone<Month>>().IsInterface()
                                                                   .IsNotDecorated()
                                                                   .Result);
        }

        [Fact]
        public void prop_LocalTime_get()
        {
            var expected = Month.Today.LocalTime;

            var mock = new Mock<IGetTimeZone<Month>>();
            mock
                .SetupGet(x => x.LocalTime)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.LocalTime;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_UniversalTime_get()
        {
            var expected = Month.Today.LocalTime;

            var mock = new Mock<IGetTimeZone<Month>>();
            mock
                .SetupGet(x => x.UniversalTime)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.UniversalTime;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_For_TimeZoneInfo()
        {
            var expected = Month.Today.LocalTime;
            var zone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");

            var mock = new Mock<IGetTimeZone<Month>>();
            mock
                .Setup(x => x.For(zone))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.For(zone);

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }
    }
}