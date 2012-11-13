namespace Cavity
{
    public interface IGetNextWeekday
    {
        Date Friday { get; }

        Date Monday { get; }

        Date Saturday { get; }

        Date Sunday { get; }

        Date Thursday { get; }

        Date Tuesday { get; }

        Date Wednesday { get; }
    }
}