namespace Cavity
{
    public interface IGetCalendarPeriod
    {
        DateTimePeriod Month { get; }

        DateTimePeriod Week { get; }

        DateTimePeriod Year { get; }
    }
}