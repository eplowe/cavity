namespace Cavity
{
    public interface IGetPreviousWeekday
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