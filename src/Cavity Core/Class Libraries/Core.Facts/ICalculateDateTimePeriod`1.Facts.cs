namespace Cavity
{
    using System;
    using Cavity;

    using Moq;

    using Xunit;

    public sealed class ICalculateDateTimePeriodFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<ICalculateDateTimePeriod<Date>>()
                            .IsInterface()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void op_Days_int()
        {
            var expected = new DateTimePeriod();

            var mock = new Mock<ICalculateDateTimePeriod<Date>>();
            mock
                .Setup(x => x.Days(1))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Days(1);

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Between_T()
        {
            var expected = new DateTimePeriod();
            var value = Date.Today.LocalTime;

            var mock = new Mock<ICalculateDateTimePeriod<Date>>();
            mock
                .Setup(x => x.Between(value))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Between(value);

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Months_int()
        {
            var expected = new DateTimePeriod();

            var mock = new Mock<ICalculateDateTimePeriod<Date>>();
            mock
                .Setup(x => x.Months(1))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Months(1);

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Since_T()
        {
            var expected = new DateTimePeriod();
            var value = Date.Today.LocalTime;

            var mock = new Mock<ICalculateDateTimePeriod<Date>>();
            mock
                .Setup(x => x.Since(value))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Since(value);

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Until_T()
        {
            var expected = new DateTimePeriod();
            var value = Date.Today.LocalTime;

            var mock = new Mock<ICalculateDateTimePeriod<Date>>();
            mock
                .Setup(x => x.Until(value))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Until(value);

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Weeks_int()
        {
            var expected = new DateTimePeriod();

            var mock = new Mock<ICalculateDateTimePeriod<Date>>();
            mock
                .Setup(x => x.Weeks(1))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Weeks(1);

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Years_int()
        {
            var expected = new DateTimePeriod();

            var mock = new Mock<ICalculateDateTimePeriod<Date>>();
            mock
                .Setup(x => x.Years(1))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Years(1);

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }
    }
}