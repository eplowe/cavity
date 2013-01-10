namespace Cavity
{
    using System;

    using Xunit;

    public sealed class TimeZonesFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<TimeZones>().DerivesFrom<object>()
                                                         .IsConcreteClass()
                                                         .IsSealed()
                                                         .HasDefaultConstructor()
                                                         .IsNotDecorated()
                                                         .Result);
        }

        [Fact]
        public void prop_Europe_CentralEuropeanTime_get()
        {
            var expected = TimeZoneInfo.FindSystemTimeZoneById("Central Europe Standard Time");
            var actual = TimeZones.Europe.CentralEuropeanTime;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Europe_BritishTime_get()
        {
            var expected = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var actual = TimeZones.Europe.BritishTime;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Europe_EasternEuropeanStandardTime_get()
        {
            var expected = TimeZoneInfo.FindSystemTimeZoneById("E. Europe Standard Time");
            var actual = TimeZones.Europe.EasternEuropeanStandardTime;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Europe_GreenwichMeanTime_get()
        {
            var expected = TimeZoneInfo.FindSystemTimeZoneById("Greenwich Standard Time");
            var actual = TimeZones.Europe.GreenwichMeanTime;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Europe_RussianEuropeanStandardTime_get()
        {
            var expected = TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time");
            var actual = TimeZones.Europe.RussianStandardTime;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Europe_WesternEuropeanStandardTime_get()
        {
            var expected = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
            var actual = TimeZones.Europe.WesternEuropeanStandardTime;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_NorthAmerica_AlaskanStandardTime_get()
        {
            var expected = TimeZoneInfo.FindSystemTimeZoneById("Alaskan Standard Time");
            var actual = TimeZones.NorthAmerica.AlaskanStandardTime;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_NorthAmerica_AtlanticStandardTime_get()
        {
            var expected = TimeZoneInfo.FindSystemTimeZoneById("Atlantic Standard Time");
            var actual = TimeZones.NorthAmerica.AtlanticStandardTime;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_NorthAmerica_CentralStandardTime_get()
        {
            var expected = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            var actual = TimeZones.NorthAmerica.CentralStandardTime;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_NorthAmerica_EasternStandardTime_get()
        {
            var expected = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            var actual = TimeZones.NorthAmerica.EasternStandardTime;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_NorthAmerica_GreenlandStandardTime_get()
        {
            var expected = TimeZoneInfo.FindSystemTimeZoneById("Greenland Standard Time");
            var actual = TimeZones.NorthAmerica.GreenlandStandardTime;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_NorthAmerica_HawaiianStandardTime_get()
        {
            var expected = TimeZoneInfo.FindSystemTimeZoneById("Hawaiian Standard Time");
            var actual = TimeZones.NorthAmerica.HawaiianStandardTime;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_NorthAmerica_MountainStandardTime_get()
        {
            var expected = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time");
            var actual = TimeZones.NorthAmerica.MountainStandardTime;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Newfoundland_MountainStandardTime_get()
        {
            var expected = TimeZoneInfo.FindSystemTimeZoneById("Newfoundland Standard Time");
            var actual = TimeZones.NorthAmerica.NewfoundlandStandardTime;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_NorthAmerica_PacificStandardTime_get()
        {
            var expected = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
            var actual = TimeZones.NorthAmerica.PacificStandardTime;

            Assert.Equal(expected, actual);
        }
    }
}