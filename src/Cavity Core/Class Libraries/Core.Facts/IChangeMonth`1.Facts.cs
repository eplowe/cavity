namespace Cavity
{
    using Moq;

    using Xunit;

    public sealed class IChangeMonthOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IChangeMonth<Month>>().IsInterface()
                                                                   .Result);
        }

        [Fact]
        public void op_Month_int()
        {
            var expected = Month.Today.LocalTime;

            var mock = new Mock<IChangeMonth<Month>>();
            mock
                .Setup(x => x.Month(2))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Month(2);

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_To_MonthOfYear()
        {
            var expected = Month.Today.LocalTime;

            var mock = new Mock<IChangeMonth<Month>>();
            mock
                .Setup(x => x.To(MonthOfYear.March))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.To(MonthOfYear.March);

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Year_int()
        {
            var expected = Month.Today.LocalTime;

            var mock = new Mock<IChangeMonth<Month>>();
            mock
                .Setup(x => x.Year(1999))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Year(1999);

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }
    }
}