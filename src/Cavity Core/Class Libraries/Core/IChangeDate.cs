namespace Cavity
{
    public interface IChangeDate : IChangeMonth<Date>
    {
        Date Day(int value);
    }
}