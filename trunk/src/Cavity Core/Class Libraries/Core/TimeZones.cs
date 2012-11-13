namespace Cavity
{
    using System;

    public sealed class TimeZones : ITimeZoneEurope, 
                                    ITimeZoneNorthAmerica
    {
        public static ITimeZoneEurope Europe
        {
            get
            {
                return new TimeZones();
            }
        }

        public static ITimeZoneNorthAmerica NorthAmerica
        {
            get
            {
                return new TimeZones();
            }
        }

        TimeZoneInfo ITimeZoneEurope.CentralEuropeanTime
        {
            get
            {
                return TimeZoneInfo.FindSystemTimeZoneById("Central Europe Standard Time");
            }
        }

        TimeZoneInfo ITimeZoneEurope.BritishTime
        {
            get
            {
                return TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            }
        }

        TimeZoneInfo ITimeZoneEurope.EasternEuropeanStandardTime
        {
            get
            {
                return TimeZoneInfo.FindSystemTimeZoneById("E. Europe Standard Time");
            }
        }

        TimeZoneInfo ITimeZoneEurope.GreenwichMeanTime
        {
            get
            {
                return TimeZoneInfo.FindSystemTimeZoneById("Greenwich Standard Time");
            }
        }

        TimeZoneInfo ITimeZoneEurope.RussianStandardTime
        {
            get
            {
                return TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time");
            }
        }

        TimeZoneInfo ITimeZoneEurope.WesternEuropeanStandardTime
        {
            get
            {
                return TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
            }
        }

        TimeZoneInfo ITimeZoneNorthAmerica.AlaskanStandardTime
        {
            get
            {
                return TimeZoneInfo.FindSystemTimeZoneById("Alaskan Standard Time");
            }
        }

        TimeZoneInfo ITimeZoneNorthAmerica.AtlanticStandardTime
        {
            get
            {
                return TimeZoneInfo.FindSystemTimeZoneById("Atlantic Standard Time");
            }
        }

        TimeZoneInfo ITimeZoneNorthAmerica.CentralStandardTime
        {
            get
            {
                return TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            }
        }

        TimeZoneInfo ITimeZoneNorthAmerica.EasternStandardTime
        {
            get
            {
                return TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            }
        }

        TimeZoneInfo ITimeZoneNorthAmerica.GreenlandStandardTime
        {
            get
            {
                return TimeZoneInfo.FindSystemTimeZoneById("Greenland Standard Time");
            }
        }

        TimeZoneInfo ITimeZoneNorthAmerica.HawaiianStandardTime
        {
            get
            {
                return TimeZoneInfo.FindSystemTimeZoneById("Hawaiian Standard Time");
            }
        }

        TimeZoneInfo ITimeZoneNorthAmerica.MountainStandardTime
        {
            get
            {
                return TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time");
            }
        }

        TimeZoneInfo ITimeZoneNorthAmerica.NewfoundlandStandardTime
        {
            get
            {
                return TimeZoneInfo.FindSystemTimeZoneById("Newfoundland Standard Time");
            }
        }

        TimeZoneInfo ITimeZoneNorthAmerica.PacificStandardTime
        {
            get
            {
                return TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
            }
        }
    }
}