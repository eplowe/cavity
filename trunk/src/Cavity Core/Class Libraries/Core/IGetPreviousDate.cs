namespace Cavity
{
    public interface IGetPreviousDate : IGetPreviousMonth,
                                        IGetPreviousWeekday
    {
        Date Day { get; }

        Date Month { get; }

        Date Week { get; }

        Date Year { get; }
    }
}