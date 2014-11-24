namespace Cavity
{
    using System;

    public interface ITimeZoneEurope
    {
        TimeZoneInfo BritishTime { get; }

        TimeZoneInfo CentralEuropeanTime { get; }

        TimeZoneInfo EasternEuropeanStandardTime { get; }

        TimeZoneInfo GreenwichMeanTime { get; }

        TimeZoneInfo RussianStandardTime { get; }

        TimeZoneInfo WesternEuropeanStandardTime { get; }
    }
}