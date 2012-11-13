namespace Cavity
{
    public interface IGetNextDate : IGetNextMonth, 
                                    IGetNextWeekday
    {
        Date Day { get; }

        Date Month { get; }

        Date Week { get; }

        Date Year { get; }
    }
}