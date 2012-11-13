namespace Cavity
{
    using System;

    public interface ITimeZoneNorthAmerica
    {
        TimeZoneInfo AlaskanStandardTime { get; }

        TimeZoneInfo AtlanticStandardTime { get; }

        TimeZoneInfo CentralStandardTime { get; }

        TimeZoneInfo EasternStandardTime { get; }

        TimeZoneInfo GreenlandStandardTime { get; }

        TimeZoneInfo HawaiianStandardTime { get; }

        TimeZoneInfo MountainStandardTime { get; }

        TimeZoneInfo NewfoundlandStandardTime { get; }

        TimeZoneInfo PacificStandardTime { get; }
    }
}