namespace Cavity
{
    public interface ICalculateDateTimePeriod<in T>
    {
        DateTimePeriod Days(int value);

        DateTimePeriod Between(T value);

        DateTimePeriod Months(int value);

        DateTimePeriod Since(T value);

        DateTimePeriod Until(T value);

        DateTimePeriod Weeks(int value);

        DateTimePeriod Years(int value);
    }
}