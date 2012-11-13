namespace Cavity
{
    using System;

    public interface ITimeZoneEurope
    {
        TimeZoneInfo CentralEuropeanTime { get; }

        TimeZoneInfo BritishTime { get; }

        TimeZoneInfo EasternEuropeanStandardTime { get; }

        TimeZoneInfo GreenwichMeanTime { get; }

        TimeZoneInfo RussianStandardTime { get; }

        TimeZoneInfo WesternEuropeanStandardTime { get; }
    }
}