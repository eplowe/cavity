namespace Cavity
{
    public interface IGetPreviousDate
    {
        Date Day { get; }

        Date Friday { get; }

        Date Monday { get; }

        Date Saturday { get; }

        Date Sunday { get; }

        Date Thursday { get; }

        Date Tuesday { get; }

        Date Wednesday { get; }

        Date Week { get; }

        Date Month();

        Date Month(int day);

        Date Year();

        Date Year(MonthOfYear month, 
                  int day);

        Date Year(int month, 
                  int day);
    }
}